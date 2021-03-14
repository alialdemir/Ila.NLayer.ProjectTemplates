using Ila.NLayer.ProjectTemplates.Core.Abctract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.DependencyInjection;

namespace Ila.NLayer.ProjectTemplates.WebApi.Filters
{
    public class ModelValidateFilter : ActionFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            var modelState = (ModelStateDictionary)context.HttpContext.RequestServices.GetService<IValidationDictionary>();
            if (!modelState.IsValid)
            {
                var currentController = (ControllerBase)context.Controller;

                foreach (var item in modelState)
                {
                    foreach (var error in item.Value.Errors)
                    {
                        currentController.ModelState.AddModelError(item.Key, error.ErrorMessage);
                    }
                }

                context.Result = new BadRequestObjectResult(currentController.ModelState);
            }

            base.OnResultExecuting(context);
        }
    }
}