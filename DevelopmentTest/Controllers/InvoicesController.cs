using System.Web.Http;
using DevelopmentTest.App_Start;

namespace DevelopmentTest.Controllers
{
    [RoutePrefix("api/invoices")]
    public class InvoicesController : ApiController
    {
        [HttpPost]
        [Route("{id:int}/post")]
        public IHttpActionResult Post(int id)
        {
            var invoice = ServiceLocator.InvoiceService.GetById(id);
            if (invoice == null)
                return NotFound();

            if (invoice.Status != "Open")
                return BadRequest("Invoice can only be posted when Status is Open.");

            try
            {
                ServiceLocator.InvoiceService.PostInvoice(id);
                return Ok(new { message = "Invoice posted successfully.", invoiceId = id });
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
