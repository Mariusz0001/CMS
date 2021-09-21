using Microsoft.VisualStudio.TestTools.UnitTesting;
using ourShop.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nunit.Framework;

namespace ourShop.Tests.Tests
{
    [TestClass()]
    public class DBTestTests
    {
        [TestMethod()]
        public void SetPageTitleTest()
        {
            var r = 1;
            Assert.IsNotNull(r);
            int ret;
            using (var dbo = new ourShop.DataBase.ourShopEntities())
            {
                ret = dbo.Database.ExecuteSqlCommand("call public.set_settings({0}, {1});", "PageTitle", "Test1");
            }
            Assert.IsNotNull(ret);
        }
    }
}