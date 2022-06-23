using Microsoft.AspNetCore.Mvc;
using Services;

namespace WebAPI.Controllers
{
    public class TokenTuple
    {
        public string username { get; set; }
        public string token { get; set; }
       
    }

    [Route("api/token")]
    [ApiController]
    public class TokensController : Controller
    {

        private readonly TokenList _tokensList;

        public TokensController(TokenList listContext)

        {
           _tokensList = listContext;
        }
        [HttpPost]
        public IActionResult Post([FromBody] TokenTuple tuple)
        {
            string token = _tokensList.getTokenByUser(tuple.username);
            if (token == tuple.token) // token in list
            {
                return Ok();
            }
            else if (token == null)// not in token list
            {
                _tokensList.addToken(tuple.username, tuple.token);
                return Ok();
            }
            return NotFound();
        }

    }
}
