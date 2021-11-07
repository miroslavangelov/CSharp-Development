using System.Linq;
using MoiteRecepti.Data.Common.Repositories;
using MoiteRecepti.Data.Models;
using MoiteRecepti.Services.Data.Models;
using MoiteRecepti.Web.ViewModels.Home;

namespace MoiteRecepti.Services.Data
{
    public class GetCountsService : IGetCountsService
    {
        private readonly IDeletableEntityRepository<Category> categoriesRepository;
        private readonly IRepository<Image> imagesRepository;
        private readonly IDeletableEntityRepository<Ingredient> ingredientsRepository;
        private readonly IDeletableEntityRepository<Recipe> recipesRepository;

        public GetCountsService(IDeletableEntityRepository<Category> categoriesRepository,
            IRepository<Image> imagesRepository,
            IDeletableEntityRepository<Ingredient> ingredientsRepository,
            IDeletableEntityRepository<Recipe> recipesRepository)
        {
            this.categoriesRepository = categoriesRepository;
            this.imagesRepository = imagesRepository;
            this.ingredientsRepository = ingredientsRepository;
            this.recipesRepository = recipesRepository;
        }

        public CountsDto GetCounts()
        {
            var countsDto = new CountsDto
            {
                RecipesCount = this.recipesRepository.All().Count(),
                CategoriesCount = this.categoriesRepository.All().Count(),
                IngredientsCount = this.ingredientsRepository.All().Count(),
                ImagesCount = this.imagesRepository.All().Count(),
            };

            return countsDto;
        }
    }
}