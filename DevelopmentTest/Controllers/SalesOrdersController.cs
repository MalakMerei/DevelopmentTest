using System.Web.Http;
using BusinessLayer.DTOs;
using DevelopmentTest.App_Start;

namespace DevelopmentTest.Controllers
{
    [RoutePrefix("api/salesorders")]
    public class SalesOrdersController : ApiController
    {
        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult GetById(int id)
        {
            var so = ServiceLocator.SalesOrderService.GetById(id);
            if (so == null)
                return NotFound();
            return Ok(so);
        }

        [HttpPost]
        [Route("")]
        public IHttpActionResult Create([FromBody] SalesOrderCreateDto dto)
        {
            if (dto == null)
                return BadRequest("Body required.");
            try
            {
                int id = ServiceLocator.SalesOrderService.CreateSalesOrder(dto);
                return Ok(new { id });
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("{id:int}/lines")]
        public IHttpActionResult AddLine(int id, [FromBody] SalesOrderLineDto dto)
        {
            if (dto == null)
                return BadRequest("Body required.");
            try
            {
                ServiceLocator.SalesOrderService.AddLine(id, dto);
                return Ok();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("{id:int}/release")]
        public IHttpActionResult Release(int id)
        {
            try
            {
                var result = ServiceLocator.SalesOrderService.ReleaseToInvoice(id);
                return Ok(result);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
