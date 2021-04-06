using System;
using System.Collections.Generic;

#nullable disable

namespace PWManager.Models
{
    public partial class Category
    {
        public Category()
        {
            ServiceCredentials = new HashSet<ServiceCredential>();
        }

        public short Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<ServiceCredential> ServiceCredentials { get; set; }
    }
}
