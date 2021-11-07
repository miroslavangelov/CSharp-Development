﻿using System.Collections.Generic;
using MoiteRecepti.Data.Common.Models;

namespace MoiteRecepti.Data.Models
{
    public class Vote : BaseModel<int>
    {
        public int RecipeId { get; set; }

        public virtual Recipe Recipe { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public byte Value { get; set; }
    }
}
