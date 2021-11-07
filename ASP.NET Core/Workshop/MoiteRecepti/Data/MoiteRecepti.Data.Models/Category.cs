using System.Collections.Generic;
using MoiteRecepti.Data.Common.Models;

namespace MoiteRecepti.Data.Models
{
    public class Category : BaseDeletableModel<int>
    {
        public Category()
        {
            this.Recipes = new HashSet<Recipe>();
        }

        public string Name { get; set; }

        public virtual ICollection<Recipe> Recipes { get; set; }
    }
}
