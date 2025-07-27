using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Carfaith.Infraestructura.AccesoDatos.Migrations
{
    /// <inheritdoc />
    public partial class CrearBaseDatosInicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "GestionaLote",
                table: "Producto",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IdLoteProducto",
                table: "Detalle_transferencia",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LoteProductoId",
                table: "Detalle_transferencia",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IdLoteProducto",
                table: "Detalle_orden_ingreso",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LoteProductoId",
                table: "Detalle_orden_ingreso",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IdLoteProducto",
                table: "Detalle_orden_egreso",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LoteProductoId",
                table: "Detalle_orden_egreso",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IdLoteProducto",
                table: "Detalle_orden_compra",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LoteProductoId",
                table: "Detalle_orden_compra",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "LoteProducto",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdProducto = table.Column<int>(type: "int", nullable: false),
                    CodigoLote = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaIngreso = table.Column<DateOnly>(type: "date", nullable: false),
                    FechaVencimiento = table.Column<DateOnly>(type: "date", nullable: true),
                    CantidadActual = table.Column<int>(type: "int", nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductoNavigationIdProducto = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoteProducto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LoteProducto_Producto_ProductoNavigationIdProducto",
                        column: x => x.ProductoNavigationIdProducto,
                        principalTable: "Producto",
                        principalColumn: "Id_producto");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Detalle_transferencia_LoteProductoId",
                table: "Detalle_transferencia",
                column: "LoteProductoId");

            migrationBuilder.CreateIndex(
                name: "IX_Detalle_orden_ingreso_LoteProductoId",
                table: "Detalle_orden_ingreso",
                column: "LoteProductoId");

            migrationBuilder.CreateIndex(
                name: "IX_Detalle_orden_egreso_LoteProductoId",
                table: "Detalle_orden_egreso",
                column: "LoteProductoId");

            migrationBuilder.CreateIndex(
                name: "IX_Detalle_orden_compra_LoteProductoId",
                table: "Detalle_orden_compra",
                column: "LoteProductoId");

            migrationBuilder.CreateIndex(
                name: "IX_LoteProducto_ProductoNavigationIdProducto",
                table: "LoteProducto",
                column: "ProductoNavigationIdProducto");

            migrationBuilder.AddForeignKey(
                name: "FK_Detalle_orden_compra_LoteProducto_LoteProductoId",
                table: "Detalle_orden_compra",
                column: "LoteProductoId",
                principalTable: "LoteProducto",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Detalle_orden_egreso_LoteProducto_LoteProductoId",
                table: "Detalle_orden_egreso",
                column: "LoteProductoId",
                principalTable: "LoteProducto",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Detalle_orden_ingreso_LoteProducto_LoteProductoId",
                table: "Detalle_orden_ingreso",
                column: "LoteProductoId",
                principalTable: "LoteProducto",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Detalle_transferencia_LoteProducto_LoteProductoId",
                table: "Detalle_transferencia",
                column: "LoteProductoId",
                principalTable: "LoteProducto",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Detalle_orden_compra_LoteProducto_LoteProductoId",
                table: "Detalle_orden_compra");

            migrationBuilder.DropForeignKey(
                name: "FK_Detalle_orden_egreso_LoteProducto_LoteProductoId",
                table: "Detalle_orden_egreso");

            migrationBuilder.DropForeignKey(
                name: "FK_Detalle_orden_ingreso_LoteProducto_LoteProductoId",
                table: "Detalle_orden_ingreso");

            migrationBuilder.DropForeignKey(
                name: "FK_Detalle_transferencia_LoteProducto_LoteProductoId",
                table: "Detalle_transferencia");

            migrationBuilder.DropTable(
                name: "LoteProducto");

            migrationBuilder.DropIndex(
                name: "IX_Detalle_transferencia_LoteProductoId",
                table: "Detalle_transferencia");

            migrationBuilder.DropIndex(
                name: "IX_Detalle_orden_ingreso_LoteProductoId",
                table: "Detalle_orden_ingreso");

            migrationBuilder.DropIndex(
                name: "IX_Detalle_orden_egreso_LoteProductoId",
                table: "Detalle_orden_egreso");

            migrationBuilder.DropIndex(
                name: "IX_Detalle_orden_compra_LoteProductoId",
                table: "Detalle_orden_compra");

            migrationBuilder.DropColumn(
                name: "GestionaLote",
                table: "Producto");

            migrationBuilder.DropColumn(
                name: "IdLoteProducto",
                table: "Detalle_transferencia");

            migrationBuilder.DropColumn(
                name: "LoteProductoId",
                table: "Detalle_transferencia");

            migrationBuilder.DropColumn(
                name: "IdLoteProducto",
                table: "Detalle_orden_ingreso");

            migrationBuilder.DropColumn(
                name: "LoteProductoId",
                table: "Detalle_orden_ingreso");

            migrationBuilder.DropColumn(
                name: "IdLoteProducto",
                table: "Detalle_orden_egreso");

            migrationBuilder.DropColumn(
                name: "LoteProductoId",
                table: "Detalle_orden_egreso");

            migrationBuilder.DropColumn(
                name: "IdLoteProducto",
                table: "Detalle_orden_compra");

            migrationBuilder.DropColumn(
                name: "LoteProductoId",
                table: "Detalle_orden_compra");
        }
    }
}
