using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ourShop.DataBase
{
    class AppDatabase : DbContext
    {
        private readonly string schema;

        public AppDatabase(string schema)
          : base("AppDatabaseConnectionString")
        {
            this.schema = schema;
        }

        protected override void OnModelCreating(DbModelBuilder builder)
        {
            builder.HasDefaultSchema(this.schema);
            base.OnModelCreating(builder);
        }
    }
}