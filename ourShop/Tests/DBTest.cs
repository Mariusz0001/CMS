using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NUnit.Framework;
using ourShop.DataBase;

namespace ourShop.Tests
{
    [TestFixture]
    public class DBTest
    {
        [Test]
        public void SetPageTitle()
        {
            Assert.NotNull(null);
            int ret;
            using (var dbo = new ourShopEntities())
            {
                ret = dbo.Database.ExecuteSqlCommand("call public.set_settings({0}, {1});", "PageTitle", "Test1");
            }
            Assert.NotNull(ret);
        }
    }
}