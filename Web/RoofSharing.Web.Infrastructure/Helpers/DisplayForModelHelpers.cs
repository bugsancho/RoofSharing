namespace RoofSharing.Web.Infrastructure.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    public static class DisplayForModelHelpers
    {
        public static bool ShouldShow(ModelMetadata metadata, ViewDataDictionary viewData)
        {
            return metadata.ShowForDisplay &&
                   metadata.ModelType != typeof(System.Data.Entity.EntityState) &&
                   !metadata.IsComplexType &&
                   !viewData.TemplateInfo.Visited(metadata);
        }

        public static IEnumerable<ModelMetadata> GetPropertiesToDisplay(ViewDataDictionary viewData)
        {
            return viewData.ModelMetadata.Properties.Where(prop => ShouldShow(prop, viewData));
        }
    }
}