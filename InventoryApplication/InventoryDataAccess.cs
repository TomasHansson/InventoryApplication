using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryApplication
{
    public static class InventoryDataAccess
    {
        // Lets pretend this actually does something fancy, like calling an API.
        private static readonly List<InventoryItem> _inventoryItems = new();

        public static void AddInventory(InventoryItem inventoryItem)
        {
            _inventoryItems.Add(inventoryItem);
        }
    }
}
