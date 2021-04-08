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
    public class service_credentials
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [DataMember]
        public long id { get; set; }

        [StringLength(50)]
        [DataMember]
        public string name { get; set; }

        [StringLength(2048)]
        [DataMember]
        public string url { get; set; }

        [DataMember]
        public string login { get; set; }

        [DataMember]
        public string password { get; set; }

        [DataMember]
        public long user_id { get; set; }

        [DataMember]
        public short category_id { get; set; }

        [DataMember]
        public bool is_favorite { get; set; }

    }
}