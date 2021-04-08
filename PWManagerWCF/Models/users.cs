using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace PWManagerWCF.Models
{
    [DataContract]
    public class users
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [DataMember]
        public long id { get; set; }

        [StringLength(128)]
        [DataMember]
        public string login { get; set; }

        [StringLength(128)]
        [DataMember]
        public string password { get; set; }
    }
}