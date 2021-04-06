using System;
using System.Collections.Generic;

#nullable disable

namespace PWManager.Models
{
    public partial class User
    {
        public User()
        {
            ServiceCredentials = new HashSet<ServiceCredential>();
        }

        public long Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }

        public virtual ICollection<ServiceCredential> ServiceCredentials { get; set; }
    }
}
