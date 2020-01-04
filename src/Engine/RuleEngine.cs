using System.Collections.Generic;
using System.Linq;
using Engine.RuleTypes;
using src.Controllers;

namespace Engine
{
    public class RuleEngine
    {
        private readonly Dictionary<RuleType, dynamic> rulelist = new Dictionary<RuleType, dynamic>();
        private List<Rule> _rules;

        public RuleEngine(){
            Load();
            rulelist.Add(RuleType.Expression, new ExpressionEvaluatorEngine());
            rulelist.Add(RuleType.Standard, new StandardRuleEngine());
        }

        //Load all rules from db with sorted
        public void Load(){
            _rules = Rule.Get();
        }

        public Response Execute(ValuesController.ExpressionRequest request){
            var response = new Response();
            foreach (var rule in _rules){
                response = rulelist[rule.Type].Execute(rule, request);
                if (response.IsThreeD)
                    break;
            }

            return response;
        }
    }

    public class Response
    {
        public string Reason { get; set; }
        public bool IsThreeD { get; set; }
    }

    public class Rule
    {
        public string Context { get; set; }
        public string Name { get; set; }
        public object Value { get; set; }
        public RuleType Type { get; set; }
        public int Priority { get; set; }
        public StandardRule StandardRule { get; set; }

        public static List<Rule> Get(){
            var rule = new List<Rule>();

            var rule_1 = new Rule()
            {
                Context = "customer",
                Name = "yaşı 18'den küçük ve cinsiyeti erkek olanlar",
                Value = "customer.Age < 18  && customer.Gender == 'E'",
                Type = RuleType.Expression,
                Priority = 1
            };
            var rule_2 = new Rule()
            {
                Context = "order",
                Name = "ilk defa siparişi olanlar",
                Value = 1,
                Type = RuleType.Standard,
                Priority = 2,
                StandardRule = new StandardRule()
                {
                    StandardRuleType = StandardRuleType.Order
                }
            };


            rule.Add(rule_1);
            rule.Add(rule_2);

            return rule.OrderBy(x => x.Priority).ToList();
        }
    }

    public class StandardRule
    {
        public StandardRuleType StandardRuleType { get; set; }
    }

    public enum RuleType
    {
        Expression = 1,
        Standard = 2
    }

    public enum StandardRuleType
    {
        Order = 1
    }
}