﻿using Gnome.Application.G2.Query.ListProducts;
using Gnome.Application.Shared;

namespace Gnome.Api.Bindings
{
    /// <summary>
    /// Configuration for all command bindings
    /// </summary>
    public class BinderConfiguration
    {
        public LinqModelBuilderConfiguration CreateConfiguration()
        {
            LinqModelBuilderConfiguration configuration = new();

            // List Query Bindings
            configuration.ApplyConfiguration(new ListProductsCommandBinding());
            configuration.ApplyConfiguration(new ListCategoriesCommandBinding());

            // Add Command Bindings
            configuration.ApplyConfiguration(new AddCategoryCommandBinding());
            configuration.ApplyConfiguration(new AddProductCommandBinding());
            configuration.ApplyConfiguration(new AddImageCommandBinding());

            // Update Command Bindings
            configuration.ApplyConfiguration(new UpdateCategoryCommandBinding());
            configuration.ApplyConfiguration(new UpdateProductCommandBinding());

            return configuration;
        }
    }
}
