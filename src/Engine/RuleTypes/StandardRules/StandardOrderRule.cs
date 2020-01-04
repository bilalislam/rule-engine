using System;
using src.Controllers;

namespace Engine.RuleTypes.StandardRules
{
    public class StandardOrderRule : IRuleEngine
    {
        private const int TOTAL_ORDER_COUNT = 1;

        public Response Execute(Rule rule, ValuesController.ExpressionRequest request){
            int count = TOTAL_ORDER_COUNT + 1;
            var result = count == Convert.ToInt32(rule.Value);
            if (result){
                return new Response()
                {
                    Reason = $"{rule.Name}",
                    IsThreeD = true
                };
            }
            
            return new Response();
        }
    }
}