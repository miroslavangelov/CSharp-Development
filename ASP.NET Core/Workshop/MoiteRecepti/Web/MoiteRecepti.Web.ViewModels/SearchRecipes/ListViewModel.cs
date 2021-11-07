using System.Collections.Generic;
using MoiteRecepti.Web.ViewModels.Recipes;

namespace MoiteRecepti.Web.ViewModels.SearchRecipes
{
    public class ListViewModel
    {
        public IEnumerable<RecipeInListViewModel> Recipes { get; set; }
    }
}
