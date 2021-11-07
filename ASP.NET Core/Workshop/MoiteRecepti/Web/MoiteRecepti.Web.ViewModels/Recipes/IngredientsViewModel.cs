using MoiteRecepti.Data.Models;
using MoiteRecepti.Services.Mapping;

namespace MoiteRecepti.Web.ViewModels.Recipes
{
    public class IngredientsViewModel : IMapFrom<RecipeIngredient>
    {
        public string IngredientName { get; set; }

        public string Quantity { get; set; }
    }
}
