using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Git.Data.Models
{
    public class Repository
    {
        public Repository()
        {
            Id = Guid.NewGuid().ToString();
            Commits = new HashSet<Commit>();
        }

        public string Id { get; set; }
        
        [Required]
        [MinLength(3)]
        [MaxLength(10)]
        public string Name { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

        [Required]
        public bool IsPublic { get; set; }

        public virtual User Owner { get; set; }

        public string OwnerId { get; set; }
        
        public virtual ICollection<Commit> Commits { get; set; }
    }
}