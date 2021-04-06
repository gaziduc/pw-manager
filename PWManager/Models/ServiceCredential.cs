using System;
using System.Collections.Generic;

#nullable disable

namespace PWManager.Models
{
    public partial class ServiceCredential
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public long UserId { get; set; }
        public short CategoryId { get; set; }
        public bool IsFavorite { get; set; }

        public virtual Category Category { get; set; }
        public virtual User User { get; set; }
    }
}
