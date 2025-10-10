using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Identity;
using Aliance.Domain.Entities;

namespace Aliance.API.Filters
{
    [AttributeUsage(AttributeTargets.Method)]
    public class ValidateActiveUsersAttribute : ActionFilterAttribute
    {
        private readonly string[] _propertyNames;

        public ValidateActiveUsersAttribute(params string[] propertyNames)
        {
            _propertyNames = propertyNames ?? throw new ArgumentNullException(nameof(propertyNames));
        }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (_propertyNames.Length == 0)
            {
                context.Result = new BadRequestObjectResult(new { message = "No user ID property specified." });
                return;
            }

            var userManager = context.HttpContext.RequestServices.GetRequiredService<UserManager<ApplicationUser>>();

            foreach (var arg in context.ActionArguments.Values)
            {
                if (arg == null) continue;

                var dtoType = arg.GetType();

                foreach (var propName in _propertyNames)
                {
                    var propInfo = dtoType.GetProperty(propName);
                    if (propInfo == null) continue;

                    var value = propInfo.GetValue(arg)?.ToString();
                    if (string.IsNullOrWhiteSpace(value))
                    {
                        context.Result = new BadRequestObjectResult(new { message = $"'{propName}' is required." });
                        return;
                    }

                    var user = await userManager.FindByIdAsync(value);
                    if (user == null)
                    {
                        context.Result = new NotFoundObjectResult(new { message = $"User '{value}' not found." });
                        return;
                    }

                    if (!user.Status)
                    {
                        context.Result = new BadRequestObjectResult(new { message = $"User '{value}' is not active." });
                        return;
                    }
                }
            }

            await next();
        }
    }
}
