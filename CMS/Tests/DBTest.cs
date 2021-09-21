using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NUnit.Framework;
using CMS.DataBase;
using Npgsql;
using NpgsqlTypes;

namespace CMS.Tests
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

    }
}