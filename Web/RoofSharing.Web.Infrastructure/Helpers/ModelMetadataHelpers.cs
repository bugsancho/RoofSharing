﻿namespace RoofSharing.Web.Infrastructure.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    public static class ModelMetadataHelpers
    {
        public static IEnumerable<ModelMetadata> GetPropertiesForDisplay(ViewDataDictionary viewData)
        {
            return viewData.ModelMetadata.Properties.Where(prop => ShouldShowForDisplay(prop, viewData));
        }

        public static IEnumerable<ModelMetadata> GetPropertiesForEdit(ViewDataDictionary viewData)
        {
            return viewData.ModelMetadata.Properties.Where(prop => ShouldShowForEdit(prop, viewData));
        }

        private static bool ShouldShowForEdit(ModelMetadata metadata, ViewDataDictionary viewData)
        {
            return metadata.ShowForEdit && ShouldShow(metadata, viewData);
        }

        private static bool ShouldShowForDisplay(ModelMetadata metadata, ViewDataDictionary viewData)
        {
            return metadata.ShowForDisplay && ShouldShow(metadata, viewData);
        }

        private static bool ShouldShow(ModelMetadata metadata, ViewDataDictionary viewData)
        {
            return metadata.ModelType != typeof(System.Data.Entity.EntityState) &&
                   !metadata.IsComplexType &&
                   !viewData.TemplateInfo.Visited(metadata);
        }
    }
}