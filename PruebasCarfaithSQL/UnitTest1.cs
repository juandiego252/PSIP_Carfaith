using Carfaith.Aplicacion.Servicios;
using Carfaith.Aplicacion.ServiciosImpl;
using Carfaith.Dominio.Modelo.Entidades;
using Carfaith.Infraestructura.AccesoDatos.EFCore;
using Microsoft.EntityFrameworkCore;

namespace PruebasCarfaithSQL
{
    public class Tests
    {
        private CarfaithDbContext _context;
        private IUsuariosServicio _usuariosServicio;

        [SetUp]
        public void Setup()
        {
            var option = new DbContextOptionsBuilder<CarfaithDbContext>()
                .UseSqlServer("Server=DESKTOP-REELKQG\\SQLEXPRESS;Database=carfaith;User=sa;Password=123456;TrustServerCertificate=True;")
                .Options;
            _context = new CarfaithDbContext(option);
            _usuariosServicio = new UsuariosServicioImpl(_context);
        }

        [Test]
        public async Task CrearUsuario()
        {
            var usuario = new Usuarios
            {
                NombreCompleto = "Juan Cordova",
                Email = "juanCordova@gmail.com",
                Contraseña = "juan12345",
            };
            await _usuariosServicio.AddUsuariosAsync(usuario);
            Assert.Pass();
        }

        [TearDown]
        public void Tear()
        {
            _context.Dispose();
        }
    }
}