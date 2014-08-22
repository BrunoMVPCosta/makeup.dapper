using System;
using System.Linq.Expressions;
using System.Text;
using DapperORM.SQLBuilder;
using System.Collections.Generic;

namespace DapperORM
{
	public class WhereClauseBuilder : ExpressionVisitor, IQueryBuilder
	{
		private readonly StringBuilder sb = new StringBuilder ();
		private		Expression expression;
		private IDictionary<string, object> parameters = new Dictionary<string, object>();
		private string aux;

		public WhereClauseBuilder()
		{
			sb.Append ("WHERE ");
		}

		public void Add(Expression node)
		{
			this.expression = node;
		}

		public string Compile()
		{
			this.Visit (this.expression);
			return sb.ToString ();
		}

		protected new Expression Visit (Expression node)
		{
			return base.Visit (node);
		}

		protected override Expression VisitBinary (BinaryExpression node)
		{
			sb.Append ("(");

			this.Visit (node.Left);

			switch (node.NodeType) {
				case ExpressionType.And:
				case ExpressionType.AndAlso:
					sb.Append( " AND ");
					break;
				case ExpressionType.Or:
				case ExpressionType.OrElse:
					sb.Append( " OR ");
					break;
				case ExpressionType.LessThan:
					sb.Append( " < ");
					break;
				case ExpressionType.LessThanOrEqual:
					sb.Append(" <= ");
					break;
				case ExpressionType.GreaterThan:
					sb.Append(" > ");
					break;
				case ExpressionType.GreaterThanOrEqual:
					sb.Append(" >= ");
					break;
				case ExpressionType.Equal:
					sb.Append(" = ");
					break;

				default:
					break;
			}

			this.Visit (node.Right);
			this.Visit (node.Conversion);

			sb.Append (")");

			return node;
		}

		protected override Expression VisitConstant(ConstantExpression c)
		{
			if (c.Type == typeof(string)) {
				sb.AppendFormat ("\'{0}\'", c.Value.ToString ());
			} else {
				sb.Append (c.Value.ToString ());
			}

			return base.VisitConstant (c);
		}

		protected override Expression VisitParameter(ParameterExpression p)
		{
			return base.VisitParameter (p);
		}

		protected override Expression VisitMember (MemberExpression node)
		{
			sb.AppendFormat ("[{0}]", node.Member.Name);
			return base.VisitMember (node);
		}

		protected override Expression VisitMethodCall (MethodCallExpression node)
		{
			Expression result = base.VisitMethodCall (node);
			if (node.Method.Name.Equals("Contains")) {
				sb.AppendFormat (" LIKE '%{0}%'", node.Arguments.ToString());
			}
			return result;
		}

		public string Build(Expression expression)
		{
			this.Visit (expression);

			return sb.ToString ();
		}

	}
}

