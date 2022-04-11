using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryApplication.Interfaces
{
    public interface ICategoriesRepository
    {
        Category? GetByName(string name);
    }
}
