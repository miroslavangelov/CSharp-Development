using System.Collections.Generic;

namespace MoiteRecepti.Web.ViewModels.SearchRecipes
{
    public class SearchIndexViewModel
    {
        public IEnumerable<IngredientNameIdViewModel> Ingredients { get; set; }
    }
}
