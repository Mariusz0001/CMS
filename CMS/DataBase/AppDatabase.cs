using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CMS.DataBase
{
    class AppDatabase : DbContext
    {
        public readonly string schema;

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