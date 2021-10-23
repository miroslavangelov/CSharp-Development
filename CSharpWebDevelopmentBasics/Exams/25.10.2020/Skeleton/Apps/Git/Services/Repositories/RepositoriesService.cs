using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Git.Data;
using Git.Data.Models;
using Git.ViewModels.Repositories;

namespace Git.Services
{
    public class RepositoriesService : IRepositoriesService
    {
        private readonly ApplicationDbContext db;

        public RepositoriesService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public string CreateRepository(string userId, string name, string repositoryType)
        {
            Repository repository = new Repository()
            {
                Name = name,
                CreatedOn = DateTime.UtcNow,
                IsPublic = repositoryType == "Public",
                OwnerId = userId
            };

            db.Repositories.Add(repository);
            db.SaveChanges();

            return repository.Id;
        }

        public List<AllRepositoriesViewModel> GetAllRepositories()
        {
            return db.Repositories
                .Where(repository => repository.IsPublic == true)
                .Select(repository => new AllRepositoriesViewModel
                {
                    Id = repository.Id,
                    Name = repository.Name,
                    OwnerName = repository.Owner.Username,
                    CreatedOn = repository.CreatedOn.ToString(CultureInfo.InvariantCulture),
                    CommitsCount = repository.Commits.Count
                })
                .ToList();
        }
        
        public Repository GetRepositoryById(string repositoryId)
        {
            Repository repository = db.Repositories.FirstOrDefault(repository => repository.Id == repositoryId);

            return repository;
        }
    }
}