using Microsoft.AspNetCore.Diagnostics;
using Serilog;
using TakeASeat.Configurations;

namespace TakeASeat.ProgramConfigurations
{
    public static class AppExtensions
    {

        public static void ConfigureGlobalExceptionHandler(this WebApplication app, WebApplication app1)
        {
            app.UseExceptionHandler(error =>
             {
                 error.Run(async context =>
                 {
                     context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                     context.Response.ContentType = "application/json";
                     var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                     if (contextFeature != null)
                     {
                         Log.Error($"Something went wrong in the {contextFeature.Error}");
                         await context.Response.WriteAsync(new ErrorProps
                         {
                             StatusCode = StatusCodes.Status500InternalServerError,
                             Message = "Internal server error. Please try again later."
                         }.ToString());
                     }
                 });
             });
        }

    }
}
