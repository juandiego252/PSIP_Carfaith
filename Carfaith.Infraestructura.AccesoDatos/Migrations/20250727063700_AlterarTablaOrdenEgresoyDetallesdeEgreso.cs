using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Carfaith.Infraestructura.AccesoDatos.Migrations
{
    /// <inheritdoc />
    public partial class AlterarTablaOrdenEgresoyDetallesdeEgreso : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Tipo_egreso",
                table: "Orden_egreso");

            migrationBuilder.AddColumn<string>(
                name: "TipoEgreso",
                table: "Detalle_orden_egreso",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TipoEgreso",
                table: "Detalle_orden_egreso");

            migrationBuilder.AddColumn<string>(
                name: "Tipo_egreso",
                table: "Orden_egreso",
                type: "varchar(50)",
                unicode: false,
                maxLength: 50,
                nullable: true);
        }
    }
}
