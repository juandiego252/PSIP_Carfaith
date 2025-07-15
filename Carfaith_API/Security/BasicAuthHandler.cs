using System.Security.Claims;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Encodings.Web;
using Carfaith.Aplicacion.Servicios;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;

namespace Carfaith_API.Security
{
    public class BasicAuthHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly IUsuariosServicio _usuariosServicio;

        public BasicAuthHandler(IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            IUsuariosServicio usuariosServicio) : base(options, logger, encoder)
        {
            _usuariosServicio = usuariosServicio;
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey("Authorization"))
            {
                return Task.FromResult(AuthenticateResult.Fail("No viene el header"));
            }

            bool result = false;

            try
            {
                var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
                var credentialBytes = Convert.FromBase64String(authHeader.Parameter);
                var credentials = Encoding.UTF8.GetString(credentialBytes).Split([':'], 2);
                var email = credentials[0];
                var password = credentials[1];
                result = _usuariosServicio.VerifyUsuariosAsync(email, password).GetAwaiter().GetResult();
            }
            catch
            {
                return Task.FromResult(AuthenticateResult.Fail("No vienen los elementos necesarios"));
            }

            if (!result)
            {
                return Task.FromResult(AuthenticateResult.Fail("Credenciales incorrectas"));
            }

            var claims = new Claim[] {
                new Claim(ClaimTypes.Name, "Usuario autenticado"),
            };

            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);
            return Task.FromResult(AuthenticateResult.Success(ticket));

        }
    }
}
