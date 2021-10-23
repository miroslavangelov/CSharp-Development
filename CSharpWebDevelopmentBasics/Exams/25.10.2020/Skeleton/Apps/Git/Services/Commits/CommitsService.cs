using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Git.Data;
using Git.Data.Models;
using Git.ViewModels.Commits;

namespace Git.Services.Commits
{
    public class CommitsService : ICommitsService
    {
        private readonly ApplicationDbContext db;

        public CommitsService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public string CreateCommit(string userId, string repositoryId, string description)
        {
            Commit commit = new Commit
            {
                Description = description,
                CreatedOn = DateTime.UtcNow,
                CreatorId = userId,
                RepositoryId = repositoryId
            };

            db.Commits.Add(commit);
            db.SaveChanges();

            return commit.Id;
        }

        public List<AllCommitsViewModel> GetAllCommits(string userId)
        {
            return db.Commits
                .Where(commit => commit.CreatorId == userId)
                .Select(commit => new AllCommitsViewModel
                {
                    Id = commit.Id,
                    Description = commit.Description,
                    Repository = commit.Repository.Name,
                    CreatedOn = commit.CreatedOn.ToString(CultureInfo.InvariantCulture)
                })
                .ToList();
        }

        public void Delete(string commitId)
        {
            Commit commit = db.Commits.FirstOrDefault(commit => commit.Id == commitId);

            db.Commits.Remove(commit);
            db.SaveChanges();
        }
    }
}