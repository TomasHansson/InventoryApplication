namespace InventoryApplication
{
    public class InventoryService
    {
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

            var categoriesRepository = new CategoriesRepository();
            var categoryFromDatabase = categoriesRepository.GetByName(category);
            if (categoryFromDatabase is null)
            {
                return false;
            }

            if (price <= 0)
            {
                return false;
            }

            var now = DateTime.Now;
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

            InventoryDataAccess.AddInventory(new InventoryItem
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