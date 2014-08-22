using MakeUp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Integration.Test
{
    public class TestDbContext : DapperContext
    {
        public IQueryable<User> Users
        {
            get
            {
                return Query<User>();
            }
        }

        public TestDbContext(string connection)
            : base(connection)
        {
            this.DatabaseModel.AddMapping(new UserEntityMapping());
        }

        public TestDbContext()
        {
            this.DatabaseModel.AddMapping(new UserEntityMapping());
        }
    }
}
