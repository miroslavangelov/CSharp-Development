using System.Collections.Generic;

namespace MoiteRecepti.Services.Data
{
    public interface ICategoriesService
    {
        IEnumerable<KeyValuePair<string, string>> GetAllASKeyValuePair();
    }
}
