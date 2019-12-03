using System.Linq;
using System.Threading;
using Microsoft.OpenApi.Models;
using Microsoft.Win32.SafeHandles;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Example.Core.Extensions
{
    public class CancellationTokenOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var apiDescription = context.ApiDescription;
            var excludedParameters = apiDescription.ParameterDescriptions
                .Where(p => p.ModelMetadata.ContainerType == typeof(CancellationToken) || p.ModelMetadata.ContainerType == typeof(WaitHandle) || p.ModelMetadata.ContainerType == typeof(SafeWaitHandle))
                .Select(p => operation.Parameters.FirstOrDefault(operationParam => operationParam.Name == p.Name))
                .ToArray();

            foreach (var parameter in excludedParameters)
                operation.Parameters.Remove(parameter);
        }
    }
}
