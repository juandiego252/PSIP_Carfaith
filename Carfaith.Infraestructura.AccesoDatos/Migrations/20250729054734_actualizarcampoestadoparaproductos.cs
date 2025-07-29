using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Carfaith.Infraestructura.AccesoDatos.Migrations
{
    /// <inheritdoc />
    public partial class actualizarcampoestadoparaproductos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "GestionaLote",
                table: "Producto",
                newName: "Estado");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Estado",
                table: "Producto",
                newName: "GestionaLote");
        }
    }
}
