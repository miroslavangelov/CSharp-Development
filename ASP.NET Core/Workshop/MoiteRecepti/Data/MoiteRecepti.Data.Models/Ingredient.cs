using System.Collections.Generic;
using MoiteRecepti.Data.Common.Models;

namespace MoiteRecepti.Data.Models
{
    public class Ingredient : BaseDeletableModel<int>
    {
        public Ingredient()
        {
            this.Recipes = new HashSet<RecipeIngredient>();
        }

        public string Name { get; set; }

        public virtual ICollection<RecipeIngredient> Recipes { get; set; }
    }
}
