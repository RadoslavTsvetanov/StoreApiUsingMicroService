using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Play.Catalog.Service.Dios;
using Play.Catalog.Service.Repositories;
using Play.Catalog.Service.Entities;
namespace Play.Catalog.Service.Controllers
{



    [ApiController]
    [Route("Items")]

    public class ItemController : ControllerBase
    {
        private readonly Repository ItemsRepository = new();


        [HttpGet]

        public async Task<IEnumerable<ItemDto>> GetAsync()
        {
            var items = (await ItemsRepository.GetAllAsync()).Select(item => item.AsDto());

            return items;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ItemDto>> GetById(Guid id)
        {
            var item = await ItemsRepository.GetAsync(id);
            return item.AsDto();
        }


        [HttpPost]
        public async Task<ActionResult<ItemDto>> CreateItem(CreateItemDto createItemDto)
        {
            var item = new Item
            {
                Name = createItemDto.Name,
                Description = createItemDto.Description,
                Price = createItemDto.Price,
                CreatedDate = DateTimeOffset.UtcNow,
            };

            await ItemsRepository.CreateAsync(item);

            return CreatedAtAction(nameof(GetAsync), new { id = item.id }, item);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateItem(Guid id, UpdateItemDto updateItemDto)
        {



            var ExistingItem = await ItemsRepository.GetAsync(id);

            ExistingItem.Name = updateItemDto.Name;
            ExistingItem.Description = updateItemDto.Description;
            ExistingItem.Price = updateItemDto.Price;

            await ItemsRepository.UpdateAsync(ExistingItem);
            return NoContent();
        }

        [HttpDelete("{id}")]

        public async Task<IActionResult> Delete(Guid id)
        {
            var item = await ItemsRepository.GetAsync(id);
            await ItemsRepository.RemoveAsync(item.id);
            return NoContent();
        }
    }
}