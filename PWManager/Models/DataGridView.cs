using System;
using System.Collections.Generic;
using System.Text;

namespace PWManager.Models
{
    public class DataGridView
    {
        public int rowIndex { get; set; }
        public long id { get; set; }
        public string name { get; set; }
        public string url { get; set; }
        public string login { get; set; }
        public string password { get; set; }
        public string hidden_password { get; set; }
        public long user_id { get; set; }
        public string category_name { get; set; }
        public bool is_favorite { get; set; }
    }
}
