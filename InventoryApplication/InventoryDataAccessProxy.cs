using InventoryApplication.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryApplication
{
    public class InventoryDataAccessProxy : IInventoryDataAccess
    {
        public void AddInventory(InventoryItem inventoryItem)
        {
            InventoryDataAccess.AddInventory(inventoryItem);
        }
    }
}
