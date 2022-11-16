using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.Extensions.Primitives;

namespace SportCity.Web.Attributes;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
public class QueryStringConstraintAttribute : ActionMethodSelectorAttribute
{
    public string Name { get; set; }
    public bool CanPass { get; set; }

    public QueryStringConstraintAttribute(string name, bool canPass)
    {
        Name = name;
        CanPass = canPass;
    }

    public override bool IsValidForRequest(RouteContext routeContext, ActionDescriptor action)
    {
        if (Name == "" && CanPass) return true;
        routeContext.HttpContext.Request.Query.TryGetValue(Name, out var value);
        return CanPass ? !StringValues.IsNullOrEmpty(value) : StringValues.IsNullOrEmpty(value);
    }
}
