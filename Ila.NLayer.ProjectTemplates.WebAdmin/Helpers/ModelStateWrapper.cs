using FluentValidation;
using FluentValidation.Results;
using Ila.NLayer.ProjectTemplates.Core.Abctract;
using Ila.NLayer.ProjectTemplates.Core.Models.Base;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Ila.NLayer.ProjectTemplates.WebAdmin.Helpers
{
    [Serializable]
    public class ModelStateWrapper : ModelStateDictionary, IValidationDictionary
    {
        private readonly IServiceProvider _serviceProvider;

        public ModelStateWrapper(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void AddError(string key, string errorMessage)
        {
            AddModelError(key, errorMessage);
        }


        private bool Validation(ValidationResult valid)
        {
            foreach (var error in valid.Errors)
            {
                AddError(error.PropertyName, error.ErrorMessage);
            }

            return IsValid;
        }

        public bool Validation<TModel>(TModel model)
             where TModel : class, IModelBase, new()
        {

            IValidator<TModel> validator = (IValidator<TModel>)_serviceProvider.GetService(typeof(IValidator<TModel>));
            if (validator == null)
                return true;

            return Validation(validator.Validate(model));
        }
    }
}