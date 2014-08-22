using System;

namespace MakeUpORM.SqlBuilder
{
	public abstract class Dialect
	{
		public abstract string LeftIdentifier { get; }

		public abstract string RightIdentifier { get; }
	}
}

