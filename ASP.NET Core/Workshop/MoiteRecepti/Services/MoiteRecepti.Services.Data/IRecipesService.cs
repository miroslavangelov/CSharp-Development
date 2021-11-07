﻿using System.Collections.Generic;
using System.Threading.Tasks;
using MoiteRecepti.Web.ViewModels.Recipes;

namespace MoiteRecepti.Services.Data
{
    public interface IRecipesService
    {
        Task CreateAsync(CreateRecipeInputModel input, string userId, string imagePath);

        IEnumerable<T> GetAll<T>(int page, int itemsPerPage = 12);

        int GetCount();

        T GetById<T>(int id);

        IEnumerable<T> GetRandom<T>(int count);

        Task UpdateAsync(int id, EditRecipeInputModel input);

        IEnumerable<T> GetByIngredients<T>(IEnumerable<int> ingredientIds);

        Task DeleteAsync(int id);
    }
}
