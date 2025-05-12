using BackEnd.Util;
using System.Reflection;

namespace BackEnd.Extensions;

public static class ServiceExtensions
{
    public static IApplicationBuilder UseEndpoints(this IApplicationBuilder app)
    {
        Assembly.GetCallingAssembly().DefinedTypes.Where(e => e.IsAssignableTo(typeof(IEndpoint))).ToList().ForEach(e =>
        {
            var endpoint = (IEndpoint)Activator.CreateInstance(e)! ?? throw new Exception("Unable to create endpoint");
            endpoint.Map(app);
        });

        return app;
    }
}
