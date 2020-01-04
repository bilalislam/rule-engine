using System;
using System.Collections.Generic;
using System.IO.Compression;
using Engine.RuleTypes.StandardRules;
using src.Controllers;

namespace Engine.RuleTypes
{
    public class StandardRuleEngine : IRuleEngine
    {
        private readonly Dictionary<StandardRuleType, dynamic> rulelist = new Dictionary<StandardRuleType, dynamic>();

        public StandardRuleEngine(){
            rulelist.Add(StandardRuleType.Order, new StandardOrderRule());
        }

        public Response Execute(Rule rule, ValuesController.ExpressionRequest request){
            return rulelist[rule.StandardRule.StandardRuleType].Execute(rule, request);
        }
    }
}