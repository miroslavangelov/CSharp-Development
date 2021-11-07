using System.Linq;
using System.Threading.Tasks;
using MoiteRecepti.Data.Common.Repositories;
using MoiteRecepti.Data.Models;

namespace MoiteRecepti.Services.Data
{
    public class VotesService : IVotesService
    {
        private readonly IRepository<Vote> votesRepository;

        public VotesService(IRepository<Vote> votesRepository)
        {
            this.votesRepository = votesRepository;
        }

        public async Task SetVoteAsync(int recipeId, string userId, byte value)
        {
            var vote = this.votesRepository.All()
                .FirstOrDefault(vote => vote.RecipeId == recipeId && vote.UserId == userId);

            if (vote == null)
            {
                vote = new Vote
                {
                    RecipeId = recipeId,
                    UserId = userId,
                };
                await this.votesRepository.AddAsync(vote);
            }

            vote.Value = value;
            await this.votesRepository.SaveChangesAsync();
        }

        public double GetAverageVotes(int recipeId)
        {
            return this.votesRepository.All()
                .Where(x => x.RecipeId == recipeId)
                .Average(x => x.Value);
        }
    }
}
