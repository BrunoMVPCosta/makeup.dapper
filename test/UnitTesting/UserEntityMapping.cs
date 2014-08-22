using MakeUpORM.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MakeUp.Tests
{
	public class UserEntityMapping : EntityMapper<User>
	{
		public UserEntityMapping ()
		{
			this.ToTable ("Users");

			this.Property (p => p.Id).ToColumn ("Id");
		}
	}
}
