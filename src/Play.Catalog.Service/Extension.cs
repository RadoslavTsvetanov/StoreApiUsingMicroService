using Play.Catalog.Service.Dios;
using Play.Catalog.Service.Entities;

namespace Play.Catalog.Service
{
    public static class Extensions
    {
        public static ItemDto AsDto(this Item item)
        {
            return new ItemDto(item.id, item.Name, item.Description, item.Price, item.CreatedDate);
        }
    }
}