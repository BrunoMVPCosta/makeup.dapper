using System;
using NUnit.Framework;
using System.Linq;
using MakeUpORM.Mapping;
using MakeUp;
using System.Text;

namespace MakeUp.Tests
{
	[TestFixture]
    public class MapperFixture
    {
		[Test]
        public void AddMapping()
        {
            DapperContext context = new DapperContext();
            context.DatabaseModel.AddMapping(new UserEntityMapping());

            Assert.IsTrue(context.DatabaseModel.Mappers().Count == 1);
        }

		[Test]
        public void MappingTableName()
        {
            var mapping = new UserEntityMapping();
            Assert.AreEqual("Users", mapping.TableName);
        }

		[Test]
        public void MappingProperty()
        {
            var mapping = new UserEntityMapping();
            Assert.AreEqual("Id", mapping.Properties.ToList().First().ColumnName);
        } 

		[Test]
        public void DatabaseModel_GetMapperByType_ReturnsExpectedType()
        {
            DatabaseModel model = new DatabaseModel();
            model.AddMapping(new UserEntityMapping());

            IEntityMapper mapper = model.MapperWithType(typeof(User));
			Assert.IsInstanceOf(typeof(EntityMapper<User>), mapper);
        }

		[Test]
        public void Mapper_GetColumnNames_ReturnsExpectedColumns()
        {
            var expectedColumns = "[Id] AS [Id]";

            IEntityMapper mapper = new UserEntityMapping();
            StringBuilder sb = new StringBuilder();
            var columns = mapper.GetColumnsName(sb);

			Assert.AreEqual (expectedColumns, columns.ToString());
        }
    }
}
