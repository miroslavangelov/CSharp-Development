using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace MoiteRecepti.Web.ViewModels.Recipes
{
    public class CreateRecipeInputModel : BaseRecipeInputModel
    {
        public IEnumerable<IFormFile> Images { get; set; }

        public IEnumerable<RecipeIngredientInputModel> Ingredients { get; set; }
    }
}
