using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MakeUp;

namespace MakeUp.Tests
{
    public class TestDbContext : DapperContext
    {
        public TestDbContext()
        {
            this.DatabaseModel.AddMapping(new UserEntityMapping());
        }
    }
}
