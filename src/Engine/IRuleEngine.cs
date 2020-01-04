using System;
using src.Controllers;

namespace Engine
{
    public interface IRuleEngine
    {
        Response Execute(Rule rule, ValuesController.ExpressionRequest request);
    }
}