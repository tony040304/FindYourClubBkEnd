using Service.IServices;
using Service.Services;

namespace FindYourClub
{
    public static class CompisteRoot
    {
        public static void DependencyInjection(WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IAuthService, AuthServices>();
        }
    }
}
