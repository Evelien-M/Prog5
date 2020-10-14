using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ninja_manager.Repository
{
    public class CategoryRepository
    {
        public List<string> GetCategories()
        {
            var list = new List<string>();
            using (Ninja_managerEntities db = new Ninja_managerEntities())
            {
                list = db.Category.Select(s => s.Name).ToList();
            }
            return list;
        }
    }
}
