using DataAccess.Abstract;
using Entity.Concrete;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstateController : ControllerBase
    {
        private readonly IEstateService _estateService;

        public EstateController(IEstateService estateService)
        {
            _estateService = estateService;
        }

        [HttpGet("getall")]
        public ActionResult<List<Estate>> Get()
        {
            return _estateService.Get();
        }

        [HttpGet("get")]
        public ActionResult<Estate> Get(string id)
        {
            var essate = _estateService.Get(id);
            if (essate == null)
            {
                return NotFound("Ürün Bulunamadı.");
            }

            return essate;
        }

        [HttpPost("add")]
        public ActionResult<Estate> Post([FromBody] Estate estate)
        {
            estate.Id = ObjectId.GenerateNewId().ToString();
            _estateService.Add(estate);

            return CreatedAtAction(nameof(Get), new { id = estate.Id }, estate);
        }


        [HttpPut("update")]
        public ActionResult Put(string id, [FromBody] Estate estate)
        {
            var existingEssate = _estateService.Get(id);
            if (existingEssate == null)
            {
                return NotFound("Ürün Bulunamadı.");
            }

            _estateService.Update(id, estate);
            return NoContent();
        }

        [HttpDelete("delete")]
        public ActionResult Delete(string id)
        {
            var essate = _estateService.Get(id);
            if (essate == null)
            {
                return NotFound("Ürün Bulunamadı.");
            }

            _estateService.Delete(essate.Id);
            return Ok("Silindi");
        }
    }
}
