using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Git.Data.Models
{
    public class User
    {
        public User()
        {
            Id = Guid.NewGuid().ToString();
            Commits = new HashSet<Commit>();
            Repositories = new HashSet<Repository>();
        }

        public string Id { get; set; }

        [Required]
        [MinLength(5)]
        [MaxLength(20)]
        public string Username { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
        
        public virtual ICollection<Commit> Commits { get; set; }

        public virtual ICollection<Repository> Repositories { get; set; }
    }
}