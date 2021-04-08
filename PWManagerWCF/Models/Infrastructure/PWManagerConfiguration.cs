using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PWManagerWCF.Models.Infrastructure
{
    public class PWManagerConfiguration : DbContext
    {
        private static readonly string configConnectionName = "EntityModel";

        public static string ConnectionString
        {
            get
            {
                var envConnectionString = Environment.GetEnvironmentVariable("ConnectionString");
                return envConnectionString ?? $"name={configConnectionName}";
            }
        }
    }
}