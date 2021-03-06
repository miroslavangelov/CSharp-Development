namespace Git.ViewModels.Repositories
{
    public class AllRepositoriesViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string OwnerName { get; set; }

        public string CreatedOn { get; set; }

        public int CommitsCount { get; set; }
    }
}