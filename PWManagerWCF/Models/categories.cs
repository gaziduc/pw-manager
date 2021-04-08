using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace PWManagerWCF.Models
{
    [DataContract]
    public class categories
    {
        [DataMember]
        public short id { get; set; }

        [StringLength(30)]
        [DataMember]
        public string name { get; set; }
    }
}