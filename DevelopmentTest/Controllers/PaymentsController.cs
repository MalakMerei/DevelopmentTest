using System.Web.Http;
using BusinessLayer.DTOs;
using DevelopmentTest.App_Start;

namespace DevelopmentTest.Controllers
{
    [RoutePrefix("api/payments")]
    public class PaymentsController : ApiController
    {
        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult GetById(int id)
        {
            PaymentDto payment = ServiceLocator.PaymentService.GetById(id);
            if (payment == null)
                return NotFound();
            return Ok(payment);
        }


        [HttpPost]
        [Route("")]
        public IHttpActionResult Create([FromBody] PaymentCreateDto dto)
        {
            if (dto == null)
                return BadRequest("Body required.");
            try
            {
                int id = ServiceLocator.PaymentService.CreatePayment(dto);
                return Ok(new { id });
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost]
        [Route("{id:int}/apply")]
        public IHttpActionResult Apply(int id, [FromBody] ApplyPaymentDto dto)
        {
            if (dto == null)
                return BadRequest("Body required.");
            try
            {
                ServiceLocator.PaymentService.ApplyToInvoice(id, dto.InvoiceId, dto.AmountApplied);
                return Ok(new { message = "Payment applied." });
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
