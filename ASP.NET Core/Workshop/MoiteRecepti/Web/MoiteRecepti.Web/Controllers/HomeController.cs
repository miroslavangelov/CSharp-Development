using MoiteRecepti.Services.Data;
using MoiteRecepti.Web.ViewModels.Home;

namespace MoiteRecepti.Web.Controllers
{
    using System.Diagnostics;

    using MoiteRecepti.Web.ViewModels;

    using Microsoft.AspNetCore.Mvc;

    public class HomeController : BaseController
    {
        private readonly IGetCountsService countsService;
        private readonly IRecipesService recipesService;

        public HomeController(IGetCountsService countsService,
            IRecipesService recipesService)
        {
            this.countsService = countsService;
            this.recipesService = recipesService;
        }

        public IActionResult Index()
        {
            var randomRecipes = this.recipesService.GetRandom<IndexPageRecipeViewModel>(10);
            var countsDto = this.countsService.GetCounts();
            var viewModel = new IndexViewModel
            {
                RandomRecipes = randomRecipes,
                CategoriesCount = countsDto.CategoriesCount,
                ImagesCount = countsDto.ImagesCount,
                RecipesCount = countsDto.RecipesCount,
                IngredientsCount = countsDto.IngredientsCount,
            };

            return this.View(viewModel);
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
