using Ila.NLayer.ProjectTemplates.Core.Models.Base;

namespace Ila.NLayer.ProjectTemplates.Core.Abctract
{
    public interface IValidationDictionary
    {

        void AddError(string key, string errorMessage);
        bool Validation<TModel>(TModel model) where TModel : class, IModelBase, new();

        bool IsValid { get; }
    }
}