using System;
using System.ComponentModel.DataAnnotations;

namespace Git.Data.Models
{
    public class Commit
    {
        public Commit()
        {
            Id = Guid.NewGuid().ToString();
        }
        
        public string Id { get; set; }
        
        [Required]
        [MinLength(5)]
        public string Description { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }
        
        public virtual User Creator { get; set; }

        public string CreatorId { get; set; }

        public virtual Repository Repository { get; set; }

        public string RepositoryId { get; set; }
    }
}