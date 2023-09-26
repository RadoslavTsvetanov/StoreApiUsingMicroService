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


        [HttpPost]
        public ActionResult<ItemDto> CreateItem(CreateItemDto createItemDto)
        {
            var item = new ItemDto(Guid.NewGuid(), createItemDto.Name, createItemDto.Description, createItemDto.Price, DateTimeOffset.UtcNow);

            items.Add(item);

            return CreatedAtAction(nameof(GetById), new { id = item.Id }, item);
        }


        [HttpPut("{id}")]
        public IActionResult UpdateItem(Guid id, UpdateItemDto updateItemDto)
        {
            var item = items.Where(i => i.Id == id).FirstOrDefault();
            var updatedItem = item with
            {
                Name = updateItemDto.Name,
                Description = updateItemDto.Description,
                Price = updateItemDto.Price
            };




            var index = items.FindIndex(item => item.Id == id);
            items[index] = updatedItem;

            return NoContent();
        }

        [HttpDelete("{id}")]

        public IActionResult Delete(Guid id)
        {
            items.RemoveAt(items.FindIndex(item => item.Id == id));
            return NoContent();
        }
    }
}