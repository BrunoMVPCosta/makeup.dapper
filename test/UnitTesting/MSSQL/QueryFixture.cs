using System;
using System.Text;
using System.Linq;
using NUnit.Framework;
using MakeUpORM.SQLBuilder;
using MakeUpORM.SqlBuilder;
using MakeUpORM.Mapping;

namespace MakeUp.Tests.MSSQL
{
	[TestFixture]
    public class QueryFixture
    {
		[Test]
        public void MSSQLQuery_Select_ReturnsTheRightQuery()
        {
            var expectedQuery = @"SELECT [Id] AS [Id] FROM Users";

            TestDbContext context = new TestDbContext();
            var query = context.Query<User>().Select(p => new { p.Id });

            IEntityMapper mapper = context.DatabaseModel.MapperWithType(typeof(User));
            QueryVisitor visitor = new QueryVisitor(mapper, new WhereVisitor(), new ProjectorVisitor());
            //Tuple<string, IdD text = visitor.Translate(query.Expression);

            //Assert.AreEqual(expectedQuery, text);
        }

		[Test]
		public void MSSQLQuery_SelectWithWhereClause_ReturnsTheRightQuery()
		{
			var expectedQuery = @"SELECT [Id] AS [Id] FROM Users WHERE ([Id] = @Id)";

            TestDbContext context = new TestDbContext();
            var query = context.Query<User>().Where(p => p.Id == 2);
            
            IEntityMapper mapper = context.DatabaseModel.MapperWithType(typeof(User));
            QueryVisitor visitor = new QueryVisitor(mapper, new WhereVisitor(), new ProjectorVisitor());
            //string text = visitor.Translate(query.Expression);

            //Assert.AreEqual(expectedQuery, text);
		}
    }
}
