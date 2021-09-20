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
        public void SaveLogTest()
        {
            testLog(DbStoredProcedure.LogType.Error);
            testLog(DbStoredProcedure.LogType.Information);
            testLog(DbStoredProcedure.LogType.Warning);
        }

        private void testLog(DbStoredProcedure.LogType logType)
        {
            var ret = DbStoredProcedure.Instance().AddLog(-1, logType, "TEST", "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.");
            Assert.IsFalse(ret.IsError);
        }

        [Test]
        public void PermissionCheck()
        {
           Assert.IsTrue(DbFunction.Instance().IsUserHasPermission(27, "CHANGE_PAGE_SETTINGS"));
        }

        [Test]
        public void GetToolbars()
        {
            Assert.IsNotNull(DbFunction.Instance().GetMenuToolbar(27)); 
        }

        [Test]
        public void GetProductsList()
        {
            Assert.IsNotNull((DbFunction.Instance().GetProductsList("Accessories")));
        }

        [Test]
        public void SetProduct()
        {
            var ret = DbStoredProcedure.Instance().SetProduct(null, "Test", false, "test barcoe !!", 100.99, 1, 100, 1, "test description", 1);

            Assert.Greater(ret.Id, 0);
        }

        [Test]
        public void SetProductPicture()
        {
            var ret = DbStoredProcedure.Instance().SetProductPicture(-1, 1, "test", @"~/Test/Test/\", false, 1, 1);

            Assert.Greater(ret.Id, 0);
        }

        [Test]
        public void GetProduct()
        {
            var ret = DbFunction.Instance().GetProduct(1, 1);

            Assert.Greater(ret.Id, 0);
        }

        [Test]

        public void GetProductsConfigurationList()
        {
            var ret = DbFunction.Instance().GetProductsConfigurationList(1).FirstOrDefault();

            Assert.Greater(ret.Qty, 0);
        }
    }
}