﻿namespace Ila.NLayer.ProjectTemplates.Core.Abctract
{
    public interface IValidationDictionary
    {
        void AddError(string key, string errorMessage);

        bool IsValid { get; }
    }
}