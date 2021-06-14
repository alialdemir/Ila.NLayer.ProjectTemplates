﻿using AutoMapper;
using Ila.NLayer.ProjectTemplates.BusinessLayer.Services.Base;
using Ila.NLayer.ProjectTemplates.Core.Abctract;
using Ila.NLayer.ProjectTemplates.Core.Abctract.Database.DataProvider;
using Ila.NLayer.ProjectTemplates.Core.Models.PagedList;
using Ila.NLayer.ProjectTemplates.Core.Models.Paging;
using Ila.NLayer.ProjectTemplates.Core.Models.Response;
using Ila.NLayer.ProjectTemplates.DataAccessLayer.Repositories.Category;

namespace Ila.NLayer.ProjectTemplates.BusinessLayer.Services.Category
{
    public class CategoryService : ServiceBase<DataAccessLayer.Entities.Category, ICategoryRepository>, ICategoryService
    {
        private readonly IMapper _mapper;

        private readonly IValidationDictionary _validationDictionary;


        public CategoryService(IDataProvider dataProvider,
                               IMapper mapper,
                               IValidationDictionary validationDictionary) : base(dataProvider)
        {
            _mapper = mapper;
            _validationDictionary = validationDictionary;
        }

        public IPagedList<CategoryResponseModel> GetCategoryPagedList(Paging paging)
        {

            return CurrentRepository
                      .GetCategoryPagedList(paging);
        }

        public void Insert(CategoryResponseModel category)
        {
            if (_validationDictionary.Validation(category))
            {
                Insert(_mapper.Map<DataAccessLayer.Entities.Category>(category));
            }
        }

    }
}