using Ila.NLayer.ProjectTemplates.Core.Abctract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Ila.NLayer.ProjectTemplates.WebAdmin.Filters
{
    public class ModelValidateFilter : ActionFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            var modelState = (ModelStateDictionary)context.HttpContext.RequestServices.GetService<IValidationDictionary>();
            if (!modelState.IsValid)
            {
                var errors = modelState.ToDictionary(x => x.Key, x => x.Value.Errors.Select(e => e.ErrorMessage).ToArray());

                context.Result = new BadRequestObjectResult(new
                {
                    Errors = errors
                });

            }

            base.OnResultExecuting(context);
        }
    }
}