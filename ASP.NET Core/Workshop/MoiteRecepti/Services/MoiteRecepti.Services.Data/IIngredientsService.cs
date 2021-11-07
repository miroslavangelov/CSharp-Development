using System.Collections.Generic;

namespace MoiteRecepti.Services.Data
{
    public interface IIngredientsService
    {
        IEnumerable<T> GetAllPopular<T>();
    }
}
