using System.Collections.Generic;
using Git.ViewModels.Commits;

namespace Git.Services.Commits
{
    public interface ICommitsService
    {
        string CreateCommit(string userId, string repositoryId, string description);

        List<AllCommitsViewModel> GetAllCommits(string userId);

        void Delete(string commitId);
    }
}