using System;
using System.Linq.Expressions;
using System.Text;
using MakeUpORM.Mapping;
using MakeUpORM.SQLBuilder.MsSql;

namespace MakeUpORM.SqlBuilder
{
	public class ProjectorVisitor : ExpressionVisitor
	{
		private Dialect currenctDialect = new MsSqlDialect();
		private readonly StringBuilder sb = new StringBuilder ();
	
		private bool isFirst = true;

        public IEntityMapper Mapper
        {
            get;
            set;
        }

        public ProjectorVisitor()
            : this(new StringBuilder(), null)
        {
        }

		public ProjectorVisitor(StringBuilder sb, IEntityMapper mapper)
		{
            this.Mapper = mapper;
			this.sb = sb;
		}

		public override Expression Visit(Expression node)
		{
			if (isFirst) 
			{
				sb.Append ("SELECT ");
				isFirst = false;
			}

			return base.Visit (node);
		}

		protected override Expression VisitNew (NewExpression node)
		{
			int countMembers = node.Members.Count;
			for(int i = 1; i<= countMembers; i++)
			{
				var item = node.Members [i - 1];

                var column = Mapper.GetColumn(item.Name);

                sb.AppendFormat("{0}{2}{1} AS {0}{3}{1}", 
                    currenctDialect.LeftIdentifier, 
                    currenctDialect.RightIdentifier,                    
                    column.ColumnName, 
                    column.PropertyName);

				if(i != countMembers)
				{
					sb.Append(", ");
				}
			}

			return base.VisitNew (node);
		}

		public string GetStatement ()
		{
            if (isFirst)
            {
                sb.Append("SELECT ");
                Mapper.GetColumnsName(sb);
            }
           
			return sb.ToString ();
		}
	}
}

