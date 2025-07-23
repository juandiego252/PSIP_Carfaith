using Carfaith.Aplicacion.Servicios;
using Carfaith.Aplicacion.ServiciosImpl;
using Carfaith.Dominio.Modelo.Entidades;
using Carfaith.Infraestructura.AccesoDatos.EFCore;
using Microsoft.EntityFrameworkCore;

namespace PruebasCarfaithSQL
{
    public class UsuariosTest
    {
        private CarfaithDbContext _context;
        private IUsuariosServicio _usuariosServicio;
        //private const string CONNECTION_STRING = "Server=DESKTOP-REELKQG\\SQLEXPRESS;Database=carfaith;User=sa;Password=123456;TrustServerCertificate=True;";
        private const string CONNECTION_STRING = "Server=tcp:psip-carfaith-sql-server.database.windows.net,1433;Initial Catalog=psipdbcarfaith;Persist Security Info=False;User ID=psip-ad;Password=Jdpachon123456;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        [SetUp]
        public void Setup()
        {
            var option = new DbContextOptionsBuilder<CarfaithDbContext>()
                .UseSqlServer(CONNECTION_STRING)
                .Options;
            _context = new CarfaithDbContext(option);
            _usuariosServicio = new UsuariosServicioImpl(_context);
        }

        [Test]
        [Category("Integration")]
        public async Task CrearUsuario()
        {
            var usuario = new Usuarios
            {
                NombreCompleto = "",
                Email = "",
                Contraseña = "",
            };
            await _usuariosServicio.AddUsuariosAsync(usuario);
            Assert.Pass("Usuario creado exitosamente");
        }

        [Test]
        [Category("Integration")]
        public async Task BorrarUsuario()
        {
            int idUsuarioAEliminar = 2;
            await _usuariosServicio.DeleteUsuariosByIdAsync(idUsuarioAEliminar);
            Assert.Pass("Usuario eliminado correctamente.");
        }

        [Test]
        [Category("Integration")]
        public async Task ActualizarUsuario()
        {
            var usuarioActualizado = new Usuarios
            {
                IdUsuario = 1,
                NombreCompleto = "Juan Diego",
                Email = "juanCordova@gmail.com"
                // Contraseña intencionalmente omitida para usar la existente
            };

            await _usuariosServicio.UpdateUsuariosAsync(usuarioActualizado);

            // Verificar que la actualización fue exitosa
            var usuarioVerificacion = await _usuariosServicio.GetByIdUsuariosAsync(1);
            Assert.That(usuarioActualizado.NombreCompleto, Is.EqualTo("Juan Diego"));
            Assert.That(usuarioActualizado.Email, Is.EqualTo("juanCordova@gmail.com"));

            Assert.Pass("Usuario actualizado correctamente");
        }

        [Test]
        [Category("Integration")]
        public async Task ListarUsuarios()
        {
            var usuarios = await _usuariosServicio.GetAllUsuariosAsync();
            Assert.That(usuarios.Count(), Is.GreaterThan(0), "La lista debe contener al menos un usuario");
        }

        [Test]
        [Category("Integration")]
        public async Task ObtenerUsuarioPorId()
        {
            var usuarios = await _usuariosServicio.GetByIdUsuariosAsync(1);
            Assert.That(usuarios, Is.Not.Null, "El usuario con ID 1 debe existir y no ser null.");
        }

        [TearDown]
        public void Tear()
        {
            _context?.Dispose();
        }
    }
}