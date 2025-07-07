using Carfaith.Infraestructura.AccesoDatos.EFCore;
using Microsoft.EntityFrameworkCore;

namespace PruebasCarfaithSQL;

public class TestBase
{
    protected CarfaithDbContext Context { get; private set; }
    private const string CONNECTION_STRING = "Server=DESKTOP-REELKQG\\SQLEXPRESS;Database=carfaith;User=sa;Password=123456;TrustServerCertificate=True;";
    [SetUp]
    public virtual void Setup()
    {
        var options = new DbContextOptionsBuilder<CarfaithDbContext>()
            .UseSqlServer(CONNECTION_STRING)
            .Options;
        Context = new CarfaithDbContext(options);
    }

    [TearDown]
    public virtual void TearDown()
    {
        Context.Dispose();
    }
}
