using System.Collections.Generic;
using Git.Services;
using Git.ViewModels.Repositories;
using SUS.HTTP;
using SUS.MvcFramework;

namespace Git.Controllers
{
    public class RepositoriesController : Controller
    {
        private readonly IRepositoriesService repositoriesService;

        public RepositoriesController(IRepositoriesService repositoriesService)
        {
            this.repositoriesService = repositoriesService;
        }

        public HttpResponse All()
        {
            if (!IsUserSignedIn())
            {
                return Redirect("/");
            }

            List<AllRepositoriesViewModel> repositories = repositoriesService.GetAllRepositories();

            return View(repositories);
        }

        public HttpResponse Create()
        {
            if (!IsUserSignedIn())
            {
                return Redirect("/");
            }

            return View();
        }

        [HttpPost]
        public HttpResponse Create(RepositoryInputModel repository)
        {
            if (!IsUserSignedIn())
            {
                return Redirect("/");
            }

            if (string.IsNullOrEmpty(repository.Name) ||
                repository.Name.Length < 3 ||
                repository.Name.Length > 10)
            {
                return Error("Name should be between 3 and 10 characters.");
            }

            string userId = GetUserId();
            repositoriesService.CreateRepository(userId, repository.Name, repository.RepositoryType);

            return Redirect("/Repositories/All");
        }
    }
}