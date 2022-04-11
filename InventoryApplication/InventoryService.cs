using InventoryApplication.Interfaces;

namespace InventoryApplication
{
    public class InventoryService
    {
        private readonly ICategoriesRepository _categoriesRepository;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IInventoryDataAccess _inventoryDataAccess;

        public InventoryService(ICategoriesRepository categoriesRepository, IDateTimeProvider dateTimeProvider, IInventoryDataAccess inventoryDataAccess)
        {
            _categoriesRepository = categoriesRepository;
            _dateTimeProvider = dateTimeProvider;
            _inventoryDataAccess = inventoryDataAccess;
        }

        public InventoryService()
        {
            _categoriesRepository = new CategoriesRepository();
            _dateTimeProvider = new DateTimeProvider();
            _inventoryDataAccess = new InventoryDataAccessProxy();
        }

        public bool TryAddItem(string name, string description, string category, double price, DateTime saleStart)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return false;
            }

            if (string.IsNullOrWhiteSpace(description))
            {
                return false;
            }

            if (string.IsNullOrWhiteSpace(category))
            {
                return false;
            }

            var categoryFromDatabase = _categoriesRepository.GetByName(category);
            if (categoryFromDatabase is null)
            {
                return false;
            }

            if (price <= 0)
            {
                return false;
            }

            var now = _dateTimeProvider.GetCurrentDate();
            if (saleStart < now.Date)
            {
                return false;
            }
            else
            {
                var timespan = saleStart - now;
                if (category == "Clothing")
                {
                    if (timespan.Days > 7)
                    {
                        return false;
                    }
                }
                else
                {
                    if (timespan.Days > 14)
                    {
                        return false;
                    }
                }
            }

            _inventoryDataAccess.AddInventory(new InventoryItem
            {
                Id = 0,
                Name = name,
                Description = description,
                CategoryId = categoryFromDatabase.Id,
                Price = price,
                SaleStart = saleStart
            });

            return true;
        }
    }
}