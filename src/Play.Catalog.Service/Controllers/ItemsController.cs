using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Play.Catalog.Service.Dios;
namespace Play.Catalog.Service.Controllers
{



    [ApiController]
    [Route("Items")]

    public class ItemController : ControllerBase
    {
        private static readonly List<ItemDto> items = new(){
            new ItemDto(Guid.NewGuid(), "Potion", "Restors hp", 5, DateTimeOffset.UtcNow),
            new ItemDto(Guid.NewGuid(), "Potion2", "Restors 5 hp", 7, DateTimeOffset.UtcNow),
    };


        [HttpGet]

        public IEnumerable<ItemDto> Get()
        {
            return items;
        }

        [HttpGet("{id}")]
        public ItemDto GetById(Guid id)
        {
            return items.Where(item => item.Id == id).SingleOrDefault();
        }
    }
}