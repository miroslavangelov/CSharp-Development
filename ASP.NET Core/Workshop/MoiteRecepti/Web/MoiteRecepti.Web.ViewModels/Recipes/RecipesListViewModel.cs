using System.Collections.Generic;
using MoiteRecepti.Web.ViewModels.Home;

namespace MoiteRecepti.Web.ViewModels.Recipes
{
    public class RecipesListViewModel : PagingViewModel
    {
        public IEnumerable<RecipeInListViewModel> Recipes { get; set; }
    }
}
