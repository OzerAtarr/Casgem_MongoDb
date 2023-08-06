using DataAccess.Abstract;
using Entity.Concrete;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }


        [HttpGet("getall")]
        public ActionResult<List<User>> Get()
        {
            return _userService.Get();
        }

        [HttpGet("get")]
        public ActionResult<User> Get(string id)
        {
            var user = _userService.Get(id);
            if (user == null)
            {
                return NotFound("Kullanıcı Bulunamadı.");
            }

            return user;
        }

        [HttpPost("add")]
        public ActionResult<User> Post([FromBody] User user)
        {
            user.Id = ObjectId.GenerateNewId().ToString();
            _userService.Add(user);

            return CreatedAtAction(nameof(Get), new { id = user.Id }, user);
        }

        [HttpPut("update")]
        public ActionResult Put(string id, [FromBody] User user)
        {
            var existingEssate = _userService.Get(id);
            if (existingEssate == null)
            {
                return NotFound("Kullanıcı Bulunamadı.");
            }

            _userService.Update(id, user);
            return NoContent();
        }

        [HttpDelete("delete")]
        public ActionResult Delete(string id)
        {
            var essate = _userService.Get(id);
            if (essate == null)
            {
                return NotFound("Kullanıcı Bulunamadı.");
            }

            _userService.Delete(essate.Id);
            return Ok("Kullanıcı Silindi.");
        }

        [HttpGet("filter")]
        public ActionResult<List<User>> GetByFilter([FromQuery] string? userName = null)
        {
            var user = _userService.GetByFilter(userName);

            if (user.Count == 0)
            {
                return NotFound("Bu filtrede kullanıcı bulunamadı");
            }
            return Ok(user);
        }
    }
}
