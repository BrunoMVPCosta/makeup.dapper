using MakeUpORM.Mapping;
namespace Integration.Test
{
    public class UserEntityMapping : EntityMapper<User>
    {
        public UserEntityMapping()
        {
            this.ToTable("Users");

            this.Property(p => p.Id).ToColumn("Id");
            this.Property(p => p.Name).ToColumn("Name");
            this.Property(p => p.Age).ToColumn("Age");
            //TODO Fix problem with enum
            //this.Property(p => p.ScheduledDayOff).ToColumn("ScheduledDayOff");
        }
    }
}