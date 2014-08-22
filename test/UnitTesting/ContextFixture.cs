using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace MakeUp.Tests
{
	[TestFixture]
	public class ContextFixture
	{
		[Test]
		public void Context_Create_AddsTheMappers()
		{
			TestDbContext a = new TestDbContext();
			Assert.IsTrue(a.DatabaseModel.Mappers().Count > 0);
		}
	}
}
