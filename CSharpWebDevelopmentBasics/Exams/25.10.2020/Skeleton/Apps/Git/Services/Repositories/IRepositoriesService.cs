using System.Collections.Generic;
using Git.Data.Models;
using Git.ViewModels.Repositories;

namespace Git.Services
{
    public interface IRepositoriesService
    {
        string CreateRepository(string userId, string name, string repositoryType);

        List<AllRepositoriesViewModel> GetAllRepositories();

        Repository GetRepositoryById(string repositoryId);
    }
}