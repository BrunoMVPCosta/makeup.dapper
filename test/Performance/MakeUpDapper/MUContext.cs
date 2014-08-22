using MakeUp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerformanceTest.MakeUpDapper
{
    public class MUContext : DapperContext
    {
        public MUContext(string cs)
            : base(cs)
        {
            this.DatabaseModel.AddMapping(new PostEntityMapping());
        }
        public IQueryable<Post> Posts
        {
            get
            {
                return Query<Post>();
            }
        }
    }
}
