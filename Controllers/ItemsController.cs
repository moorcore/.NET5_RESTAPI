using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catalog.DTO;
using Catalog.Entities;
using Catalog.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Controllers 
{
    [ApiController]
    [Route("items")]
    public class ItemsController : ControllerBase 
    {
        private readonly IItemsRepository repository;

        public ItemsController(IItemsRepository repository) 
        {
            this.repository = repository;
        }

        [HttpGet]
        public async Task <IEnumerable<ItemDTO>> GetItemsAsync() 
        {
            var items = (await repository.GetItemsAsync())
                        .Select( item => item.AsDTO());
            
            return items;
        } 

        [HttpGet("{id}")]    
        public async Task <ActionResult<ItemDTO>> GetItemAsync(Guid id) 
        {
            var item =  await repository.GetItemAsync(id);

            if (item is null)
            {
                return NotFound();
            }

            return item.AsDTO();
        }

        [HttpPost]
        public async Task<ActionResult<ItemDTO>> CreateItemAsync(CreateItemDTO itemDTO)
        {
            Item item = new() 
            {
                Id = Guid.NewGuid(),
                Name = itemDTO.Name,
                Price = itemDTO.Price,
                CreatedDate = DateTimeOffset.UtcNow
            };

            await repository.CreateItemAsync(item);

            return CreatedAtAction(nameof(GetItemAsync), new { id = item.Id}, item.AsDTO());
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateItemAsync(Guid id, UpdateItemDTO itemDTO)
        {
            var exisitingItem = await repository.GetItemAsync(id);

            if (exisitingItem is null)
            {
                return NotFound();
            }

            Item updatedItem = exisitingItem with 
            {
                Name = itemDTO.Name,
                Price = itemDTO.Price,
            };

            await repository.UpdateItemAsync(updatedItem);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteItemAsync(Guid id)
        {
            var exisitingItem = await repository.GetItemAsync(id);

            if (exisitingItem is null)
            {
                return NotFound();
            }
            
            await repository.DeleteItemAsync(id);

            return NoContent();
        }
    }
}