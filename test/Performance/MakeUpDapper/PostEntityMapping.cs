using MakeUpORM.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerformanceTest.MakeUpDapper
{
    public class PostEntityMapping : EntityMapper<Post>
    {
        public PostEntityMapping()
        {
            this.ToTable("Posts");

            this.Property(p => p.Id).ToColumn("Id");
            this.Property(p => p.Text).ToColumn("Text");
            //this.Property(p => p.CreationDate).ToColumn("CreationDate");
            //this.Property(p => p.Counter1).ToColumn("Counter1");
            //this.Property(p => p.Counter2).ToColumn("Counter2");
            //this.Property(p => p.Counter3).ToColumn("Counter3");
            //this.Property(p => p.Counter4).ToColumn("Counter4");
            //this.Property(p => p.Counter5).ToColumn("Counter5");
            //this.Property(p => p.Counter6).ToColumn("Counter6");
            //this.Property(p => p.Counter7).ToColumn("Counter7");
            //this.Property(p => p.Counter7).ToColumn("Counter8");
            //this.Property(p => p.Counter7).ToColumn("Counter9");
        }
    }
}
