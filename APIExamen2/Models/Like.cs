using System;
using System.Collections.Generic;

namespace APIExamen2.Models
{
    public partial class Like
    {
        public long LikeId { get; set; }
        public int UserId { get; set; }
        public long AnswerId { get; set; }

        public virtual Answer Answer { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
