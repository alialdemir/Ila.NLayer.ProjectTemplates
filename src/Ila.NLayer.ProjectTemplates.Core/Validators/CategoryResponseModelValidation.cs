using FluentValidation;
using Ila.NLayer.ProjectTemplates.Core.Models.Response;

namespace Ila.NLayer.ProjectTemplates.Core.Validator
{
    public class CategoryResponseModelValidator : AbstractValidator<CategoryResponseModel>
    {
        public CategoryResponseModelValidator()
        {
            RuleFor(x => x.Name).MinimumLength(3).NotEmpty();

            RuleFor(x => x.Id).NotEmpty();
        }
    }
}
