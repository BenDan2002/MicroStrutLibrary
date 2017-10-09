using System.Collections.Generic;
using System.Linq.Expressions;

namespace MicroStrutLibrary.Infrastructure.Core.Specifications
{

    /*****
     * 
     * ExpressionVisitor类得关键，就在于提供了便利表达式树的标准方式，直接继承ExpressionVisitor类
     * 并调用Visit方法，那么最终返回的结果便是传入的Expression本身。但是，如果覆盖的任意一个方法，
     * 返回了与传入时不同的对象，那么最终返回的是一个新的Expresstion本身。ExpressionVisitor类中的
     * 每个方法都负责一类表达式，也都遵循了类似的原则，它们会递归调用Visit方法，如果Visit方法返回
     * 新对象，那么它们也会构造新对象并返回。
     * 
     * 参考 http://blogs.msdn.com/b/meek/archive/2008/05/02/linq-to-entities-combining-predicates.aspx.
     *              
     *                      小蔡 备注 
     * 
     * *****/

    /// <summary>
    /// Represents the parameter rebinder used for rebinding the parameters
    /// for the given expressions. This is part of the solution which solves
    /// the expression parameter problem when going to Entity Framework by using
    /// Apworks specifications. For more information about this solution please
    /// refer to http://blogs.msdn.com/b/meek/archive/2008/05/02/linq-to-entities-combining-predicates.aspx.
    /// </summary>
    internal class ParameterRebinder : ExpressionVisitor
    {
        #region Private Fields
        private readonly Dictionary<ParameterExpression, ParameterExpression> map;
        #endregion

        #region Ctor
        internal ParameterRebinder(Dictionary<ParameterExpression, ParameterExpression> map)
        {
            this.map = map ?? new Dictionary<ParameterExpression, ParameterExpression>();
        }
        #endregion

        #region Internal Static Methods
        internal static Expression ReplaceParameters(Dictionary<ParameterExpression, ParameterExpression> map, Expression exp)
        {
            return new ParameterRebinder(map).Visit(exp);
        }
        #endregion

        #region Protected Methods
        protected override Expression VisitParameter(ParameterExpression p)
        {
            if (map.TryGetValue(p, out ParameterExpression replacement))
            {
                p = replacement;
            }
            return base.VisitParameter(p);
        }
        #endregion
    }
}
