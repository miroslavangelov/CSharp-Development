using System;
using System.Collections.Generic;
using Git.Data.Models;
using Git.Services;
using Git.Services.Commits;
using Git.ViewModels.Commits;
using Git.ViewModels.Repositories;
using SUS.HTTP;
using SUS.MvcFramework;

namespace Git.Controllers
{
    public class CommitsController : Controller
    {
        private readonly ICommitsService commitsService;
        private readonly IRepositoriesService repositoriesService;

        public CommitsController(ICommitsService commitsService, IRepositoriesService repositoriesService)
        {
            this.commitsService = commitsService;
            this.repositoriesService = repositoriesService;
        }
        
        public HttpResponse All()
        {
            if (!IsUserSignedIn())
            {
                return Redirect("/");
            }

            string userId = GetUserId();
            List<AllCommitsViewModel> commits = commitsService.GetAllCommits(userId);

            return View(commits);
        }
        
        public HttpResponse Create(string id)
        {
            if (!IsUserSignedIn())
            {
                return Redirect("/");
            }

            Repository repository = repositoriesService.GetRepositoryById(id);

            return View(repository);
        }

        [HttpPost]
        public HttpResponse Create(string id, CommitInputModel commit)
        {
            if (!IsUserSignedIn())
            {
                return Redirect("/");
            }

            if (string.IsNullOrEmpty(commit.Description) ||
                commit.Description.Length < 5)
            {
                return Error("Description should be at least 5 characters.");
            }

            string userId = GetUserId();
            Repository repository = repositoriesService.GetRepositoryById(id);
            commitsService.CreateCommit(userId, repository.Id, commit.Description);

            return Redirect("/Commits/All");
        }

        public HttpResponse Delete(string id)
        {
            if (!IsUserSignedIn())
            {
                return Redirect("/");
            }

            commitsService.Delete(id);

            return Redirect("/Commits/All");
        }
    }
}