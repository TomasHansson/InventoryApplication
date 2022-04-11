using InventoryApplication.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryApplication
{
    public class CategoriesRepository : ICategoriesRepository
    {
        // Lets pretend this actually does something fancy, like using Entity Framework to get data from a database.
        private readonly List<Category> _categoryDAOs = new()
        {
            new Category { Id = 1, Name = "Tools" },
            new Category { Id = 2, Name = "Construction" },
            new Category { Id = 3, Name = "Clothing" }
        };

        public List<Category> Get()
        {
            return _categoryDAOs;
        }

        public Category? GetById(int id)
        {
            return _categoryDAOs.FirstOrDefault(x => x.Id == id);
        }

        public Category? GetByName(string name)
        {
            return _categoryDAOs.FirstOrDefault(x => x.Name == name);
        }
    }
}
