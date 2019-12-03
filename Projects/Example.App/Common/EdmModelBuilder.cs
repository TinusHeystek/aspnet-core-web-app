using Example.App.Data.Models;
using Example.App.Shared.Models.View;
using Microsoft.AspNet.OData.Builder;
using Microsoft.OData.Edm;

namespace Example.App.Common
{
    public static class EdmModelBuilder
    {
        private static IEdmModel _edmModel;

        public static IEdmModel GetEdmModel()
        {
            if (_edmModel == null)
            {
                var builder = new ODataConventionModelBuilder();
                builder.EntitySet<Contact>("Contacts");
                builder.EntitySet<ContactSummary>("ContactSummary");
                // builder.RemoveEnumType(typeof(Gender));
                // builder.AddEnumType(typeof(Gender));
                builder.EnableLowerCamelCase();
                _edmModel = builder.GetEdmModel();
            }

            return _edmModel;
        }
    }
}
