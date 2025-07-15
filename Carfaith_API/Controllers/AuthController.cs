using Carfaith.Aplicacion.Servicios;
using Carfaith_API.Models;
using Microsoft.AspNetCore.Mvc;

namespace Carfaith_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUsuariosServicio _usuarioServicio;
        public AuthController(IUsuariosServicio usuarioServicio)
        {
            _usuarioServicio = usuarioServicio;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
        {
            if (string.IsNullOrEmpty(loginModel.Email) || string.IsNullOrEmpty(loginModel.Password))
            {
                return BadRequest("Credenciales requeridas");
            }

            var isAuthenticated = await _usuarioServicio.VerifyUsuariosAsync(loginModel.Email, loginModel.Password);
            if (!isAuthenticated)
            {
                return Unauthorized("Credenciales Incorrectas");
            }

            var usuarios = await _usuarioServicio.GetAllUsuariosAsync();
            var usuario = usuarios.FirstOrDefault(u => u.Email == loginModel.Email);

            return Ok(new
            {
                message = "Login exitoso",
                usuario = new
                {
                    id = usuario.IdUsuario,
                    nombre = usuario.NombreCompleto,
                    email = usuario.Email
                }
            });
        }
    }
}
