using System;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Text;
using MakeUpORM.Mapping;

namespace MakeUpORM.SqlBuilder
{
	public class WhereVisitor : ExpressionVisitor
	{
		private readonly StringBuilder sb;
		private readonly IDictionary<string, object> parameters = new Dictionary<string, object>();

		private string currentParameter = string.Empty;
		private bool isFirst = true;

        public IEntityMapper Mapper
        {
            get;
            set;
        }

		public WhereVisitor()
			: this(new StringBuilder(), null)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="DapperORM.WhereVisitor"/> class.
		/// </summary>
		/// <param name="sb">Sb.</param>
		public WhereVisitor(StringBuilder sb, IEntityMapper mapper)
		{
			this.sb = sb;
            this.Mapper = mapper;
		}

		public override Expression Visit (Expression node)
		{
			if (isFirst) 
			{
				sb.Append(" WHERE ");
				isFirst = false;
			}

			return base.Visit (node);
		}

		protected override Expression VisitBinary (BinaryExpression node)
		{
			sb.Append ("(");

			this.Visit (node.Left);

			switch (node.NodeType) {
			case ExpressionType.And:
			case ExpressionType.AndAlso:
				sb.Append (" AND ");
				break;
			case ExpressionType.Or:
			case ExpressionType.OrElse:
				sb.Append (" OR ");
				break;
			case ExpressionType.LessThan:
				sb.Append (" < ");
				break;
			case ExpressionType.LessThanOrEqual:
				sb.Append (" <= ");
				break;
			case ExpressionType.GreaterThan:
				sb.Append (" > ");
				break;
			case ExpressionType.GreaterThanOrEqual:
				sb.Append (" >= ");
				break;
			case ExpressionType.Equal:
				sb.Append (" = ");
				break;

			default:
				break;
			}

			this.Visit (node.Right);
			this.Visit (node.Conversion);

			sb.Append (")");

			return node;
		}

		protected override Expression VisitConstant (ConstantExpression c)
		{
			this.parameters.Add (currentParameter, c.Value);
			this.sb.Append (currentParameter);

			return base.VisitConstant (c);
		}

		protected override Expression VisitParameter (ParameterExpression p)
		{
			return base.VisitParameter (p);
		}

		protected override Expression VisitMember (MemberExpression node)
		{
			currentParameter = string.Format("@{0}", node.Member.Name);

			sb.AppendFormat ("[{0}]", node.Member.Name);
			return base.VisitMember (node);
		}

		public string GetStatement ()
		{
			return sb.ToString ();
		}

		public IDictionary<string, object> GetParameters()
		{
			return parameters;
		}
	}
}

