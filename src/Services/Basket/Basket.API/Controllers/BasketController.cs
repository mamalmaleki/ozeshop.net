using System.Net;
using Basket.API.Entities;
using Basket.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Basket.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepository _repository;
        private readonly ILogger<BasketController> _logger;


        public BasketController(
            IBasketRepository repository, 
            ILogger<BasketController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet("{userName}", Name = "GetBasket")]
        [ProducesResponseType(typeof(ShoppingCart),
            (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCart>>
            GetBasket(string userName)
            => Ok(await
                _repository.GetBasket(userName) ?? 
                  new ShoppingCart(userName));

        [HttpPost()]
        [ProducesResponseType(typeof(ShoppingCart),
            (int) HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCart>>
            UpdateBasket([FromBody] ShoppingCart basket)
            => Ok(await _repository.UpdateBasket(basket));


        [HttpDelete("{userName}", Name = "DeleteBasket")]
        [ProducesResponseType(typeof(ShoppingCart),
            (int) HttpStatusCode.OK)]
        public async Task<ActionResult>
            DeleteBasket([FromBody] string userName)
        {
            await _repository.DeleteBasket(userName);
            return Ok();
        }    
        


    }
}
