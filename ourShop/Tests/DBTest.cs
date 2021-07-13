using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NUnit.Framework;
using ourShop.DataBase;
using Npgsql;
using NpgsqlTypes;

namespace ourShop.Tests
{
    [TestFixture]
    public class DBTest
    {
        [Test]
        public void LoginCheck()
        {
            var ret = DbStoredProcedure.Instance().LoginUser(null, "AZajebacCi?!", "Crouty", "", "127.0.0.1");
            Assert.Positive(ret.Id.Value);
        }
        [Test]
        public void PermissionCheck()
        {
           Assert.IsTrue(DbFunction.Instance().IsUserHasPermission(27, "CHANGE_PAGE_SETTINGS"));
        }

        [Test]
        public void GetToolbars()
        {
            Assert.IsNotNull(DbFunction.Instance().GetMenuToolbar(1)); 
        }
    }
}