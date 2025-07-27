using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Carfaith.Infraestructura.AccesoDatos.Migrations
{
    /// <inheritdoc />
    public partial class AgregarCascadeBorradoOrdenIngreso : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Lineas_de_Producto",
                columns: table => new
                {
                    Id_linea = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    Descripcion = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Lineas_d__CF744F1B011B9CEA", x => x.Id_linea);
                });

            migrationBuilder.CreateTable(
                name: "Orden_egreso",
                columns: table => new
                {
                    Id_orden_egreso = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tipo_egreso = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Fecha = table.Column<DateOnly>(type: "date", nullable: true),
                    Destino = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    Estado = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Orden_eg__21C30FEBA07A17C6", x => x.Id_orden_egreso);
                });

            migrationBuilder.CreateTable(
                name: "Proveedores",
                columns: table => new
                {
                    Id_proveedor = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreProveedor = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    PaisOrigen = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    TipoProveedor = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    Telefono = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    Email = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    PersonaContacto = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    FechaRegistro = table.Column<DateOnly>(type: "date", nullable: true),
                    Ruc = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    Direccion = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true),
                    Estado = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Proveedo__6704E5A8BC809F68", x => x.Id_proveedor);
                });

            migrationBuilder.CreateTable(
                name: "Ubicaciones",
                columns: table => new
                {
                    Id_ubicacion = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Lugar_ubicacion = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Ubicacio__CFBBC8617A7EF099", x => x.Id_ubicacion);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id_usuario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreCompleto = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    Email = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    Contraseña = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Usuarios__EF59F76266B4A2BA", x => x.Id_usuario);
                });

            migrationBuilder.CreateTable(
                name: "Producto",
                columns: table => new
                {
                    Id_producto = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Codigo_producto = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Nombre = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    Linea_de_producto = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Producto__1D8EFF01EF921852", x => x.Id_producto);
                    table.ForeignKey(
                        name: "FK__Producto__Linea___68487DD7",
                        column: x => x.Linea_de_producto,
                        principalTable: "Lineas_de_Producto",
                        principalColumn: "Id_linea");
                });

            migrationBuilder.CreateTable(
                name: "Orden_de_compra",
                columns: table => new
                {
                    Id_orden = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Numero_orden = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Id_proveedor = table.Column<int>(type: "int", nullable: true),
                    Archivo_pdf = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    Estado = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Fecha_creacion = table.Column<DateOnly>(type: "date", nullable: true),
                    Fecha_estimada_entrega = table.Column<DateOnly>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Orden_de__33F95B581743FAF6", x => x.Id_orden);
                    table.ForeignKey(
                        name: "FK__Orden_de___Id_pr__6C190EBB",
                        column: x => x.Id_proveedor,
                        principalTable: "Proveedores",
                        principalColumn: "Id_proveedor");
                });

            migrationBuilder.CreateTable(
                name: "Transferencias",
                columns: table => new
                {
                    Id_transferencia = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fecha = table.Column<DateOnly>(type: "date", nullable: true),
                    Ubicacion_origen_id = table.Column<int>(type: "int", nullable: true),
                    Ubicacion_destino_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Transfer__749D659DE0C5161E", x => x.Id_transferencia);
                    table.ForeignKey(
                        name: "FK__Transfere__Ubica__778AC167",
                        column: x => x.Ubicacion_origen_id,
                        principalTable: "Ubicaciones",
                        principalColumn: "Id_ubicacion");
                    table.ForeignKey(
                        name: "FK__Transfere__Ubica__787EE5A0",
                        column: x => x.Ubicacion_destino_id,
                        principalTable: "Ubicaciones",
                        principalColumn: "Id_ubicacion");
                });

            migrationBuilder.CreateTable(
                name: "Producto_Proveedor",
                columns: table => new
                {
                    Id_producto_proveedor = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id_producto = table.Column<int>(type: "int", nullable: true),
                    Id_proveedor = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Producto__A0750A0C29A3249E", x => x.Id_producto_proveedor);
                    table.ForeignKey(
                        name: "FK__Producto___Id_pr__693CA210",
                        column: x => x.Id_producto,
                        principalTable: "Producto",
                        principalColumn: "Id_producto");
                    table.ForeignKey(
                        name: "FK__Producto___Id_pr__6A30C649",
                        column: x => x.Id_proveedor,
                        principalTable: "Proveedores",
                        principalColumn: "Id_proveedor");
                });

            migrationBuilder.CreateTable(
                name: "Orden_de_ingreso",
                columns: table => new
                {
                    Id_orden_ingreso = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id_orden_compra = table.Column<int>(type: "int", nullable: true),
                    Origen_de_compra = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Fecha = table.Column<DateOnly>(type: "date", nullable: true),
                    Estado = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Orden_de__78798AE1D9CE5B46", x => x.Id_orden_ingreso);
                    table.ForeignKey(
                        name: "FK__Orden_de___Id_or__6EF57B66",
                        column: x => x.Id_orden_compra,
                        principalTable: "Orden_de_compra",
                        principalColumn: "Id_orden");
                });

            migrationBuilder.CreateTable(
                name: "Detalle_orden_compra",
                columns: table => new
                {
                    Id_detalle_orden = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id_orden = table.Column<int>(type: "int", nullable: true),
                    Id_producto_proveedor = table.Column<int>(type: "int", nullable: true),
                    Cantidad = table.Column<int>(type: "int", nullable: true),
                    Precio_unitario = table.Column<decimal>(type: "decimal(18,0)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Detalle___05923E9FC7275972", x => x.Id_detalle_orden);
                    table.ForeignKey(
                        name: "FK__Detalle_o__Id_or__6D0D32F4",
                        column: x => x.Id_orden,
                        principalTable: "Orden_de_compra",
                        principalColumn: "Id_orden");
                    table.ForeignKey(
                        name: "FK__Detalle_o__Id_pr__6E01572D",
                        column: x => x.Id_producto_proveedor,
                        principalTable: "Producto_Proveedor",
                        principalColumn: "Id_producto_proveedor");
                });

            migrationBuilder.CreateTable(
                name: "Detalle_orden_egreso",
                columns: table => new
                {
                    Id_detalle_orden_egreso = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Orden_egreso_id = table.Column<int>(type: "int", nullable: true),
                    Id_producto_proveedor = table.Column<int>(type: "int", nullable: true),
                    Cantidad = table.Column<int>(type: "int", nullable: true),
                    Ubicacion_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Detalle___6B21310D7218CD36", x => x.Id_detalle_orden_egreso);
                    table.ForeignKey(
                        name: "FK__Detalle_o__Id_pr__73BA3083",
                        column: x => x.Id_producto_proveedor,
                        principalTable: "Producto_Proveedor",
                        principalColumn: "Id_producto_proveedor");
                    table.ForeignKey(
                        name: "FK__Detalle_o__Orden__72C60C4A",
                        column: x => x.Orden_egreso_id,
                        principalTable: "Orden_egreso",
                        principalColumn: "Id_orden_egreso");
                    table.ForeignKey(
                        name: "FK__Detalle_o__Ubica__74AE54BC",
                        column: x => x.Ubicacion_id,
                        principalTable: "Ubicaciones",
                        principalColumn: "Id_ubicacion");
                });

            migrationBuilder.CreateTable(
                name: "Detalle_transferencia",
                columns: table => new
                {
                    Id_detalle_transferencia = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id_transferencia = table.Column<int>(type: "int", nullable: true),
                    Id_producto_proveedor = table.Column<int>(type: "int", nullable: true),
                    Cantidad = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Detalle___595FCCF0C909F886", x => x.Id_detalle_transferencia);
                    table.ForeignKey(
                        name: "FK__Detalle_t__Id_pr__7A672E12",
                        column: x => x.Id_producto_proveedor,
                        principalTable: "Producto_Proveedor",
                        principalColumn: "Id_producto_proveedor");
                    table.ForeignKey(
                        name: "FK__Detalle_t__Id_tr__797309D9",
                        column: x => x.Id_transferencia,
                        principalTable: "Transferencias",
                        principalColumn: "Id_transferencia");
                });

            migrationBuilder.CreateTable(
                name: "PreciosHistoricos",
                columns: table => new
                {
                    Id_precios_historicos = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id_producto_proveedor = table.Column<int>(type: "int", nullable: true),
                    Precio = table.Column<decimal>(type: "decimal(18,0)", nullable: true),
                    Fecha_inicio = table.Column<DateOnly>(type: "date", nullable: true),
                    Fecha_finalizacion = table.Column<DateOnly>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__PreciosH__0578A7F607777C6B", x => x.Id_precios_historicos);
                    table.ForeignKey(
                        name: "FK__PreciosHi__Id_pr__6B24EA82",
                        column: x => x.Id_producto_proveedor,
                        principalTable: "Producto_Proveedor",
                        principalColumn: "Id_producto_proveedor");
                });

            migrationBuilder.CreateTable(
                name: "Stock",
                columns: table => new
                {
                    id_stock = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id_producto_proveedor = table.Column<int>(type: "int", nullable: true),
                    Id_ubicacion = table.Column<int>(type: "int", nullable: true),
                    Cantidad = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Stock__3A39590A77516DD3", x => x.id_stock);
                    table.ForeignKey(
                        name: "FK__Stock__Id_produc__75A278F5",
                        column: x => x.Id_producto_proveedor,
                        principalTable: "Producto_Proveedor",
                        principalColumn: "Id_producto_proveedor");
                    table.ForeignKey(
                        name: "FK__Stock__Id_ubicac__76969D2E",
                        column: x => x.Id_ubicacion,
                        principalTable: "Ubicaciones",
                        principalColumn: "Id_ubicacion");
                });

            migrationBuilder.CreateTable(
                name: "Detalle_orden_ingreso",
                columns: table => new
                {
                    Id_detalle_orden_ingreso = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Orden_ingreso_id = table.Column<int>(type: "int", nullable: true),
                    Id_producto_proveedor = table.Column<int>(type: "int", nullable: true),
                    Cantidad = table.Column<int>(type: "int", nullable: true),
                    Precio_unitario = table.Column<decimal>(type: "decimal(18,0)", nullable: true),
                    Ubicacion_id = table.Column<int>(type: "int", nullable: true),
                    tipo_ingreso = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    numero_lote = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Detalle___4A2B71A4EC7C2C48", x => x.Id_detalle_orden_ingreso);
                    table.ForeignKey(
                        name: "FK__Detalle_o__Id_pr__70DDC3D8",
                        column: x => x.Id_producto_proveedor,
                        principalTable: "Producto_Proveedor",
                        principalColumn: "Id_producto_proveedor");
                    table.ForeignKey(
                        name: "FK__Detalle_o__Orden__6FE99F9F",
                        column: x => x.Orden_ingreso_id,
                        principalTable: "Orden_de_ingreso",
                        principalColumn: "Id_orden_ingreso",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__Detalle_o__Ubica__71D1E811",
                        column: x => x.Ubicacion_id,
                        principalTable: "Ubicaciones",
                        principalColumn: "Id_ubicacion");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Detalle_orden_compra_Id_orden",
                table: "Detalle_orden_compra",
                column: "Id_orden");

            migrationBuilder.CreateIndex(
                name: "IX_Detalle_orden_compra_Id_producto_proveedor",
                table: "Detalle_orden_compra",
                column: "Id_producto_proveedor");

            migrationBuilder.CreateIndex(
                name: "IX_Detalle_orden_egreso_Id_producto_proveedor",
                table: "Detalle_orden_egreso",
                column: "Id_producto_proveedor");

            migrationBuilder.CreateIndex(
                name: "IX_Detalle_orden_egreso_Orden_egreso_id",
                table: "Detalle_orden_egreso",
                column: "Orden_egreso_id");

            migrationBuilder.CreateIndex(
                name: "IX_Detalle_orden_egreso_Ubicacion_id",
                table: "Detalle_orden_egreso",
                column: "Ubicacion_id");

            migrationBuilder.CreateIndex(
                name: "IX_Detalle_orden_ingreso_Id_producto_proveedor",
                table: "Detalle_orden_ingreso",
                column: "Id_producto_proveedor");

            migrationBuilder.CreateIndex(
                name: "IX_Detalle_orden_ingreso_Orden_ingreso_id",
                table: "Detalle_orden_ingreso",
                column: "Orden_ingreso_id");

            migrationBuilder.CreateIndex(
                name: "IX_Detalle_orden_ingreso_Ubicacion_id",
                table: "Detalle_orden_ingreso",
                column: "Ubicacion_id");

            migrationBuilder.CreateIndex(
                name: "IX_Detalle_transferencia_Id_producto_proveedor",
                table: "Detalle_transferencia",
                column: "Id_producto_proveedor");

            migrationBuilder.CreateIndex(
                name: "IX_Detalle_transferencia_Id_transferencia",
                table: "Detalle_transferencia",
                column: "Id_transferencia");

            migrationBuilder.CreateIndex(
                name: "IX_Orden_de_compra_Id_proveedor",
                table: "Orden_de_compra",
                column: "Id_proveedor");

            migrationBuilder.CreateIndex(
                name: "IX_Orden_de_ingreso_Id_orden_compra",
                table: "Orden_de_ingreso",
                column: "Id_orden_compra");

            migrationBuilder.CreateIndex(
                name: "IX_PreciosHistoricos_Id_producto_proveedor",
                table: "PreciosHistoricos",
                column: "Id_producto_proveedor");

            migrationBuilder.CreateIndex(
                name: "IX_Producto_Linea_de_producto",
                table: "Producto",
                column: "Linea_de_producto");

            migrationBuilder.CreateIndex(
                name: "IX_Producto_Proveedor_Id_producto",
                table: "Producto_Proveedor",
                column: "Id_producto");

            migrationBuilder.CreateIndex(
                name: "IX_Producto_Proveedor_Id_proveedor",
                table: "Producto_Proveedor",
                column: "Id_proveedor");

            migrationBuilder.CreateIndex(
                name: "IX_Stock_Id_producto_proveedor",
                table: "Stock",
                column: "Id_producto_proveedor");

            migrationBuilder.CreateIndex(
                name: "IX_Stock_Id_ubicacion",
                table: "Stock",
                column: "Id_ubicacion");

            migrationBuilder.CreateIndex(
                name: "IX_Transferencias_Ubicacion_destino_id",
                table: "Transferencias",
                column: "Ubicacion_destino_id");

            migrationBuilder.CreateIndex(
                name: "IX_Transferencias_Ubicacion_origen_id",
                table: "Transferencias",
                column: "Ubicacion_origen_id");

            migrationBuilder.CreateIndex(
                name: "UQ__Usuarios__A9D10534609B768C",
                table: "Usuarios",
                column: "Email",
                unique: true,
                filter: "[Email] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Detalle_orden_compra");

            migrationBuilder.DropTable(
                name: "Detalle_orden_egreso");

            migrationBuilder.DropTable(
                name: "Detalle_orden_ingreso");

            migrationBuilder.DropTable(
                name: "Detalle_transferencia");

            migrationBuilder.DropTable(
                name: "PreciosHistoricos");

            migrationBuilder.DropTable(
                name: "Stock");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Orden_egreso");

            migrationBuilder.DropTable(
                name: "Orden_de_ingreso");

            migrationBuilder.DropTable(
                name: "Transferencias");

            migrationBuilder.DropTable(
                name: "Producto_Proveedor");

            migrationBuilder.DropTable(
                name: "Orden_de_compra");

            migrationBuilder.DropTable(
                name: "Ubicaciones");

            migrationBuilder.DropTable(
                name: "Producto");

            migrationBuilder.DropTable(
                name: "Proveedores");

            migrationBuilder.DropTable(
                name: "Lineas_de_Producto");
        }
    }
}
