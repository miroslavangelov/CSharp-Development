using MoiteRecepti.Data.Models;
using MoiteRecepti.Services.Mapping;

namespace MoiteRecepti.Web.ViewModels.SearchRecipes
{
    public class IngredientNameIdViewModel : IMapFrom<Ingredient>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
