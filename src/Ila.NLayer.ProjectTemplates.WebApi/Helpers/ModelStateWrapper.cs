using Ila.NLayer.ProjectTemplates.Core.Abctract;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;

namespace Ila.NLayer.ProjectTemplates.WebApi.Helpers
{
    [Serializable]
    public class ModelStateWrapper : ModelStateDictionary, IValidationDictionary
    {
        public void AddError(string key, string errorMessage)
        {
            AddModelError(key, errorMessage);
        }
    }
}