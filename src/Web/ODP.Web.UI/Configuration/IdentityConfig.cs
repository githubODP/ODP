using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace ODP.Web.UI.Configuration
{
    public static class IdentityConfig
    {
        public static void AddIdentityConfiguration(this IServiceCollection services)
        {
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.Cookie.HttpOnly = true;
                    options.Cookie.SameSite = SameSiteMode.Lax;
                    options.Cookie.SecurePolicy = CookieSecurePolicy.Always; // Ajuste conforme necessário
                    /*options.Cookie.Expiration = TimeSpan.FromMinutes(30); */// Opcional, pois não é necessário se a expiração for configurada por sessão
                    options.LoginPath = "/login";
                    options.LogoutPath = "/Account/Logout";
                    options.AccessDeniedPath = "/erro/403";
                });

            ;


        }

        public static void UseIdentityConfiguration(this IApplicationBuilder app)
        {
            app.UseAuthentication();
            app.UseAuthorization();
        }
    }
}
