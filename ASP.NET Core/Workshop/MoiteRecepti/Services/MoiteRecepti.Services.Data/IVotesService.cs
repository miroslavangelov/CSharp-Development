using System.Threading.Tasks;

namespace MoiteRecepti.Services.Data
{
    public interface IVotesService
    {
        Task SetVoteAsync(int recipeId, string userId, byte value);

        double GetAverageVotes(int recipeId);
    }
}
