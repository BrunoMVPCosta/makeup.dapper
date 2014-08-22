using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Reflection;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data;
using Dapper;
using MakeUpORM.Mapping;
using MakeUp;
using System.Linq;
using System.Diagnostics;

namespace Integration.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestInitialize()]
        public void Initialize()
        {
            using (var connection = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=tempdb;Integrated Security=True"))
            {
                connection.Open();
                try
                {
                    connection.Execute(@"ALTER DATABASE DapperSimpleCrudTestDb SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
                                                     DROP DATABASE DapperSimpleCrudTestDb ; ");
                }
                catch { }

                connection.Execute(@" CREATE DATABASE DapperSimpleCrudTestDb; ");
            }

            using (IDbConnection connection = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=DapperSimpleCrudTestDb;Integrated Security=True"))
            {
                connection.Open();
                connection.Execute(@" create table Users (Id int IDENTITY(1,1) not null, Name nvarchar(100) not null, Age int not null, ScheduledDayOff int null) ");
                connection.Execute(@" create table Car (CarId int IDENTITY(1,1) not null, Make nvarchar(100) not null, Model nvarchar(100) not null) ");
                connection.Execute(@" create table City (Name nvarchar(100) not null, Population int not null, Version rowversion) ");
                connection.Execute(@" CREATE SCHEMA Log; ");
                connection.Execute(@" create table Log.CarLog (Id int IDENTITY(1,1) not null, LogNotes nvarchar(100) NOT NULL) ");

                connection.Execute("INSERT INTO USERS VALUES ('teste', 21, 1)");
            }
            Console.WriteLine("Created database");
        }

        [TestMethod]
        public void TestMethod1()
        {
            using (TestDbContext context = new TestDbContext("ConnectionString"))
            {
                var query = context
                    .Users
                    .Where(p => p.Id == 1);

                foreach (var user in query)
                {
                    Debug.WriteLine(user.Name);
                }
            }
        }
    }
}
