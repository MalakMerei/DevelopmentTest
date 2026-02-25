CREATE TABLE dbo.Customer (
    Id   INT IDENTITY(1,1) NOT NULL,
    Name NVARCHAR(200) NOT NULL,
    CONSTRAINT PK_Customer PRIMARY KEY CLUSTERED (Id)
);
GO

CREATE TABLE dbo.Item (
    Id              INT IDENTITY(1,1) NOT NULL,
    Name            NVARCHAR(200) NOT NULL,
    UnitPrice       DECIMAL(18,2) NOT NULL,
    OnHandQuantity  INT NOT NULL CONSTRAINT DF_Item_OnHandQuantity DEFAULT 0,
    CONSTRAINT PK_Item PRIMARY KEY CLUSTERED (Id)
);
GO

CREATE TABLE dbo.SalesOrder (
    Id         INT IDENTITY(1,1) NOT NULL,
    CustomerId INT NOT NULL,
    Date       DATE NOT NULL,
    Status     NVARCHAR(50) NOT NULL CONSTRAINT DF_SalesOrder_Status DEFAULT N'Open',
    CONSTRAINT PK_SalesOrder PRIMARY KEY CLUSTERED (Id),
    CONSTRAINT FK_SalesOrder_Customer FOREIGN KEY (CustomerId) REFERENCES dbo.Customer(Id)
);
GO

CREATE TABLE dbo.SalesOrderLine (
    SalesOrderId INT NOT NULL,
    ItemId       INT NOT NULL,
    Qty          INT NOT NULL,
    UnitPrice    DECIMAL(18,2) NOT NULL,
    CONSTRAINT PK_SalesOrderLine PRIMARY KEY CLUSTERED (SalesOrderId, ItemId),
    CONSTRAINT FK_SalesOrderLine_SalesOrder FOREIGN KEY (SalesOrderId) REFERENCES dbo.SalesOrder(Id),
    CONSTRAINT FK_SalesOrderLine_Item FOREIGN KEY (ItemId) REFERENCES dbo.Item(Id)
);
GO

CREATE TABLE dbo.Invoice (
    Id           INT IDENTITY(1,1) NOT NULL,
    SalesOrderId INT NOT NULL,
    CustomerId   INT NOT NULL,
    Date         DATE NOT NULL,
    NetTotal     DECIMAL(18,2) NOT NULL,
    TaxTotal     DECIMAL(18,2) NOT NULL,
    GrossTotal   DECIMAL(18,2) NOT NULL,
    Status       NVARCHAR(50) NOT NULL CONSTRAINT DF_Invoice_Status DEFAULT N'Open',
    CONSTRAINT PK_Invoice PRIMARY KEY CLUSTERED (Id),
    CONSTRAINT FK_Invoice_SalesOrder FOREIGN KEY (SalesOrderId) REFERENCES dbo.SalesOrder(Id),
    CONSTRAINT FK_Invoice_Customer FOREIGN KEY (CustomerId) REFERENCES dbo.Customer(Id)
);
GO

CREATE TABLE dbo.InvoiceLine (
    Id        INT IDENTITY(1,1) NOT NULL,
    InvoiceId INT NOT NULL,
    ItemId    INT NOT NULL,
    Qty       INT NOT NULL,
    UnitPrice DECIMAL(18,2) NOT NULL,
    LineTotal DECIMAL(18,2) NOT NULL,
    TaxAmount DECIMAL(18,2) NOT NULL,
    CONSTRAINT PK_InvoiceLine PRIMARY KEY CLUSTERED (Id),
    CONSTRAINT FK_InvoiceLine_Invoice FOREIGN KEY (InvoiceId) REFERENCES dbo.Invoice(Id),
    CONSTRAINT FK_InvoiceLine_Item FOREIGN KEY (ItemId) REFERENCES dbo.Item(Id)
);
GO

CREATE TABLE dbo.Payment (
    Id         INT IDENTITY(1,1) NOT NULL,
    CustomerId INT NOT NULL,
    Date       DATE NOT NULL,
    Amount     DECIMAL(18,2) NOT NULL,
    CONSTRAINT PK_Payment PRIMARY KEY CLUSTERED (Id),
    CONSTRAINT FK_Payment_Customer FOREIGN KEY (CustomerId) REFERENCES dbo.Customer(Id)
);
GO

CREATE TABLE dbo.PaymentApplication (
    Id            INT IDENTITY(1,1) NOT NULL,
    PaymentId     INT NOT NULL,
    InvoiceId     INT NOT NULL,
    AmountApplied DECIMAL(18,2) NOT NULL,
    CONSTRAINT PK_PaymentApplication PRIMARY KEY CLUSTERED (Id),
    CONSTRAINT FK_PaymentApplication_Payment FOREIGN KEY (PaymentId) REFERENCES dbo.Payment(Id),
    CONSTRAINT FK_PaymentApplication_Invoice FOREIGN KEY (InvoiceId) REFERENCES dbo.Invoice(Id)
);
GO

CREATE TABLE dbo.GLTransaction (
    Id   INT IDENTITY(1,1) NOT NULL,
    Date DATE NOT NULL,
    CONSTRAINT PK_GLTransaction PRIMARY KEY CLUSTERED (Id)
);
GO

CREATE TABLE dbo.GLTransactionLine (
    Id             INT IDENTITY(1,1) NOT NULL,
    GLTransactionId INT NOT NULL,
    Account        NVARCHAR(100) NOT NULL,
    Debit          DECIMAL(18,2) NOT NULL CONSTRAINT DF_GLTransactionLine_Debit DEFAULT 0,
    Credit         DECIMAL(18,2) NOT NULL CONSTRAINT DF_GLTransactionLine_Credit DEFAULT 0,
    CONSTRAINT PK_GLTransactionLine PRIMARY KEY CLUSTERED (Id),
    CONSTRAINT FK_GLTransactionLine_GLTransaction FOREIGN KEY (GLTransactionId) REFERENCES dbo.GLTransaction(Id)
);
GO






--- Index
-- Index 1: Open invoices by customer (used by vw_OpenInvoicesByCustomer and API)
CREATE NONCLUSTERED INDEX IX_Invoice_CustomerId_Status
    ON dbo.Invoice (CustomerId, Status)
    INCLUDE (Date, NetTotal, TaxTotal, GrossTotal);

-- Index 2: SalesOrderLine by SalesOrder (join order lines, used when posting invoice from order)
CREATE NONCLUSTERED INDEX IX_SalesOrderLine_SalesOrderId
    ON dbo.SalesOrderLine (SalesOrderId)
    INCLUDE (ItemId, Qty, UnitPrice);





alter PROCEDURE dbo.sp_PostInvoice
    @InvoiceId INT
AS
SET NOCOUNT ON;

    IF NOT EXISTS (SELECT 1 FROM dbo.Invoice WHERE Id = @InvoiceId)
    BEGIN
        RAISERROR('Invoice not found.', 16, 1);
        RETURN;
    END

    DECLARE @Status NVARCHAR(50);
    SELECT @Status = Status FROM dbo.Invoice WHERE Id = @InvoiceId;

    IF @Status <> 'Open'
    BEGIN
        RAISERROR('Invoice can only be posted when Status is Open.', 16, 1);
        RETURN;
    END

    DECLARE @GrossTotal DECIMAL(18,2), @TaxTotal DECIMAL(18,2), @NetTotal DECIMAL(18,2), @InvDate DATE;
    
    SELECT @GrossTotal = GrossTotal, @TaxTotal = TaxTotal, @NetTotal = NetTotal,
       @InvDate    = Date
    FROM dbo.Invoice 
    WHERE Id = @InvoiceId;
    
    IF EXISTS (SELECT 1 FROM dbo.InvoiceLine il
                INNER JOIN dbo.Item i ON i.Id = il.ItemId
                WHERE il.InvoiceId = @InvoiceId AND il.Qty > i.OnHandQuantity)
    BEGIN
        RAISERROR(N'Insufficient OnHandQuantity for one or more items.', 16, 1);
        RETURN;
    END

    BEGIN TRANSACTION;

        UPDATE i
        SET i.OnHandQuantity = i.OnHandQuantity - il.Qty
        FROM dbo.Item i
            INNER JOIN dbo.InvoiceLine il ON il.ItemId = i.Id
        WHERE il.InvoiceId = @InvoiceId;


        DECLARE @GLTransaction TABLE (Id INT);

        INSERT INTO dbo.GLTransaction (Date)
        OUTPUT INSERTED.Id INTO @GLTransaction
        select @InvDate;

        DECLARE @GLTransactionId INT = (SELECT Id FROM @GLTransaction);


        INSERT INTO dbo.GLTransactionLine (GLTransactionId, Account, Debit, Credit)
        VALUES (@GLTransactionId, 'Accounts Receivable', @GrossTotal, 0);

        INSERT INTO dbo.GLTransactionLine (GLTransactionId, Account, Debit, Credit)
        VALUES (@GLTransactionId, 'Sales Revenue', 0, @NetTotal);

        INSERT INTO dbo.GLTransactionLine (GLTransactionId, Account, Debit, Credit)
        VALUES (@GLTransactionId, 'Sales Tax Payable', 0, @TaxTotal);

        UPDATE dbo.Invoice SET Status = 'Posted' WHERE Id = @InvoiceId;

    COMMIT TRANSACTION;
GO





CREATE VIEW dbo.Vw_OpenInvoicesByCustomer
AS
    SELECT c.Id   AS CustomerId, c.Name AS CustomerName, i.Id   AS InvoiceId, i.Date AS InvoiceDate,
        i.NetTotal, i.TaxTotal, i.GrossTotal
        FROM dbo.Invoice i
        INNER JOIN dbo.Customer c ON c.Id = i.CustomerId

    WHERE i.Status = 'Open';
GO





INSERT INTO dbo.Customer (Name) VALUES
    ('Nour'),
    ('Samer'),
    ('Mohammad');
GO

INSERT INTO dbo.Item (Name, UnitPrice, OnHandQuantity) VALUES
    ('Item A', 10, 100),
    ('Item B', 25.5, 50),
    ('Item 3', 99, 0);
GO

INSERT INTO dbo.SalesOrder (CustomerId, Date, Status) VALUES
    (1, CAST('2025-01-15' AS DATE), 'Open'),
    (1, CAST('2025-02-01' AS DATE), 'Invoiced'),
    (2, CAST('2025-02-10' AS DATE), 'Open');
GO

INSERT INTO dbo.SalesOrderLine (SalesOrderId, ItemId, Qty, UnitPrice) VALUES
    (1, 1, 5,  10),
    (1, 2, 2,  25.5),
    (2, 1, 10, 10),
    (3, 3, 1,  99);
GO

INSERT INTO dbo.Invoice (SalesOrderId, CustomerId, Date, NetTotal, TaxTotal, GrossTotal, Status) VALUES
    (2, 1, CAST('2025-02-02' AS DATE), 100, 10, 110, 'Open'),
    (1, 1, CAST('2025-01-16' AS DATE), 76,  7.6,  83.6,  'Posted');
GO

INSERT INTO dbo.InvoiceLine (InvoiceId, ItemId, Qty, UnitPrice, LineTotal, TaxAmount) VALUES
    (1, 1, 10, 10, 100, 10),
    (2, 1, 5,  10,  50,  5),
    (2, 2, 2,  25.5,  51,  5.1);
GO

INSERT INTO dbo.Payment (CustomerId, Date, Amount) VALUES
    (1, CAST('2025-02-05' AS DATE), 50),
    (1, CAST('2025-02-12' AS DATE), 60),
    (2, CAST('2025-02-14' AS DATE), 109);
GO


INSERT INTO dbo.GLTransaction (Date) VALUES (CAST('2025-01-16' AS DATE));
INSERT INTO dbo.GLTransactionLine (GLTransactionId, Account, Debit, Credit) VALUES
    (1, 'Accounts Receivable', 83.6, 0),
    (1, 'Sales Revenue', 0, 76),
    (1, 'Sales Tax Payable', 0, 7.6);
GO






