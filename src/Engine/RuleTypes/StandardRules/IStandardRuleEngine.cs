using src.Controllers;

namespace Engine.RuleTypes.StandardRules
{
    public interface IStandardRuleEngine
    {
        bool Execute(ValuesController.ExpressionRequest request);
    }
}