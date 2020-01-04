using System.Collections.Generic;
using DynamicExpresso;
using Newtonsoft.Json;
using src.Controllers;

namespace Engine.RuleTypes
{
    public class ExpressionEvaluatorEngine : IRuleEngine
    {
        public Response Execute(Rule rule, ValuesController.ExpressionRequest request){
            var kv = new KeyValuePair<string, string>(rule.Context, request.Payload);
            bool result = (bool) Eval(kv, rule.Value.ToString());
            if (result){
                return new Response()
                {
                    Reason = $"{rule.Name}",
                    IsThreeD = true
                };
            }

            return new Response();
        }

        private static object Eval(KeyValuePair<string, string> model, string expression){
            var interpreter = new Interpreter();
            switch (model.Key){
                case "customer":
                    var customer = JsonConvert.DeserializeObject<Customer>(model.Value);
                    interpreter.SetVariable(model.Key, customer);
                    break;
            }

            return interpreter.Eval(expression);
        }
    }
}