using System;
using System.Collections.Generic;
using Carfaith.Dominio.Modelo.Entidades;
using Microsoft.EntityFrameworkCore;

namespace Carfaith.Infraestructura.AccesoDatos.EFCore;

public partial class CarfaithDbContext : DbContext
{
    public CarfaithDbContext()
    {
    }

    public CarfaithDbContext(DbContextOptions<CarfaithDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<DetalleOrdenCompra> DetalleOrdenCompras { get; set; }

    public virtual DbSet<DetalleOrdenEgreso> DetalleOrdenEgresos { get; set; }

    public virtual DbSet<DetalleOrdenIngreso> DetalleOrdenIngresos { get; set; }

    public virtual DbSet<DetalleTransferencia> DetalleTransferencia { get; set; }

    public virtual DbSet<LineasDeProducto> LineasDeProductos { get; set; }

    public virtual DbSet<OrdenDeCompra> OrdenDeCompras { get; set; }

    public virtual DbSet<OrdenDeIngreso> OrdenDeIngresos { get; set; }

    public virtual DbSet<OrdenEgreso> OrdenEgresos { get; set; }

    public virtual DbSet<PreciosHistoricos> PreciosHistoricos { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    public virtual DbSet<ProductoProveedor> ProductoProveedors { get; set; }

    public virtual DbSet<Proveedores> Proveedores { get; set; }

    public virtual DbSet<Stock> Stocks { get; set; }

    public virtual DbSet<Transferencias> Transferencias { get; set; }

    public virtual DbSet<Ubicaciones> Ubicaciones { get; set; }

    public virtual DbSet<Usuarios> Usuarios { get; set; }

    /* protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-REELKQG\\SQLEXPRESS;Database=carfaith;User=sa;Password=123456;TrustServerCertificate=True;"); */

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DetalleOrdenCompra>(entity =>
        {
            entity.HasKey(e => e.IdDetalleOrden).HasName("PK__Detalle___05923E9FC7275972");

            entity.ToTable("Detalle_orden_compra");

            entity.Property(e => e.IdDetalleOrden).HasColumnName("Id_detalle_orden");
            entity.Property(e => e.IdOrden).HasColumnName("Id_orden");
            entity.Property(e => e.IdProductoProveedor).HasColumnName("Id_producto_proveedor");
            entity.Property(e => e.PrecioUnitario)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("Precio_unitario");

            entity.HasOne(d => d.IdOrdenNavigation).WithMany(p => p.DetalleOrdenCompras)
                .HasForeignKey(d => d.IdOrden)
                .HasConstraintName("FK__Detalle_o__Id_or__6D0D32F4");

            entity.HasOne(d => d.IdProductoProveedorNavigation).WithMany(p => p.DetalleOrdenCompras)
                .HasForeignKey(d => d.IdProductoProveedor)
                .HasConstraintName("FK__Detalle_o__Id_pr__6E01572D");
        });

        modelBuilder.Entity<DetalleOrdenEgreso>(entity =>
        {
            entity.HasKey(e => e.IdDetalleOrdenEgreso).HasName("PK__Detalle___6B21310D7218CD36");

            entity.ToTable("Detalle_orden_egreso");

            entity.Property(e => e.IdDetalleOrdenEgreso).HasColumnName("Id_detalle_orden_egreso");
            entity.Property(e => e.IdProductoProveedor).HasColumnName("Id_producto_proveedor");
            entity.Property(e => e.OrdenEgresoId).HasColumnName("Orden_egreso_id");
            entity.Property(e => e.UbicacionId).HasColumnName("Ubicacion_id");

            entity.HasOne(d => d.IdProductoProveedorNavigation).WithMany(p => p.DetalleOrdenEgresos)
                .HasForeignKey(d => d.IdProductoProveedor)
                .HasConstraintName("FK__Detalle_o__Id_pr__73BA3083");

            entity.HasOne(d => d.OrdenEgreso).WithMany(p => p.DetalleOrdenEgresos)
                .HasForeignKey(d => d.OrdenEgresoId)
                .HasConstraintName("FK__Detalle_o__Orden__72C60C4A");

            entity.HasOne(d => d.Ubicacion).WithMany(p => p.DetalleOrdenEgresos)
                .HasForeignKey(d => d.UbicacionId)
                .HasConstraintName("FK__Detalle_o__Ubica__74AE54BC");
        });

        modelBuilder.Entity<DetalleOrdenIngreso>(entity =>
        {
            entity.HasKey(e => e.IdDetalleOrdenIngreso).HasName("PK__Detalle___4A2B71A4EC7C2C48");

            entity.ToTable("Detalle_orden_ingreso");

            entity.Property(e => e.IdDetalleOrdenIngreso).HasColumnName("Id_detalle_orden_ingreso");
            entity.Property(e => e.IdProductoProveedor).HasColumnName("Id_producto_proveedor");
            entity.Property(e => e.NumeroLote)
                .HasMaxLength(255)
                .HasColumnName("numero_lote");
            entity.Property(e => e.OrdenIngresoId).HasColumnName("Orden_ingreso_id");
            entity.Property(e => e.PrecioUnitario)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("Precio_unitario");
            entity.Property(e => e.TipoIngreso)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("tipo_ingreso");
            entity.Property(e => e.UbicacionId).HasColumnName("Ubicacion_id");

            entity.HasOne(d => d.IdProductoProveedorNavigation).WithMany(p => p.DetalleOrdenIngresos)
                .HasForeignKey(d => d.IdProductoProveedor)
                .HasConstraintName("FK__Detalle_o__Id_pr__70DDC3D8");

            entity.HasOne(d => d.OrdenIngreso).WithMany(p => p.DetalleOrdenIngresos)
                .HasForeignKey(d => d.OrdenIngresoId)
                .HasConstraintName("FK__Detalle_o__Orden__6FE99F9F");

            entity.HasOne(d => d.Ubicacion).WithMany(p => p.DetalleOrdenIngresos)
                .HasForeignKey(d => d.UbicacionId)
                .HasConstraintName("FK__Detalle_o__Ubica__71D1E811");
        });

        modelBuilder.Entity<DetalleTransferencia>(entity =>
        {
            entity.HasKey(e => e.IdDetalleTransferencia).HasName("PK__Detalle___595FCCF0C909F886");

            entity.ToTable("Detalle_transferencia");

            entity.Property(e => e.IdDetalleTransferencia).HasColumnName("Id_detalle_transferencia");
            entity.Property(e => e.IdProductoProveedor).HasColumnName("Id_producto_proveedor");
            entity.Property(e => e.IdTransferencia).HasColumnName("Id_transferencia");

            entity.HasOne(d => d.IdProductoProveedorNavigation).WithMany(p => p.DetalleTransferencia)
                .HasForeignKey(d => d.IdProductoProveedor)
                .HasConstraintName("FK__Detalle_t__Id_pr__7A672E12");

            entity.HasOne(d => d.IdTransferenciaNavigation).WithMany(p => p.DetalleTransferencia)
                .HasForeignKey(d => d.IdTransferencia)
                .HasConstraintName("FK__Detalle_t__Id_tr__797309D9");
        });

        modelBuilder.Entity<LineasDeProducto>(entity =>
        {
            entity.HasKey(e => e.IdLinea).HasName("PK__Lineas_d__CF744F1B011B9CEA");

            entity.ToTable("Lineas_de_Producto");

            entity.Property(e => e.IdLinea).HasColumnName("Id_linea");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<OrdenDeCompra>(entity =>
        {
            entity.HasKey(e => e.IdOrden).HasName("PK__Orden_de__33F95B581743FAF6");

            entity.ToTable("Orden_de_compra");

            entity.Property(e => e.IdOrden).HasColumnName("Id_orden");
            entity.Property(e => e.ArchivoPdf)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("Archivo_pdf");
            entity.Property(e => e.Estado)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FechaCreacion).HasColumnName("Fecha_creacion");
            entity.Property(e => e.FechaEstimadaEntrega).HasColumnName("Fecha_estimada_entrega");
            entity.Property(e => e.IdProveedor).HasColumnName("Id_proveedor");
            entity.Property(e => e.NumeroOrden)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Numero_orden");

            entity.HasOne(d => d.IdProveedorNavigation).WithMany(p => p.OrdenDeCompras)
                .HasForeignKey(d => d.IdProveedor)
                .HasConstraintName("FK__Orden_de___Id_pr__6C190EBB");
        });

        modelBuilder.Entity<OrdenDeIngreso>(entity =>
        {
            entity.HasKey(e => e.IdOrdenIngreso).HasName("PK__Orden_de__78798AE1D9CE5B46");

            entity.ToTable("Orden_de_ingreso");

            entity.Property(e => e.IdOrdenIngreso).HasColumnName("Id_orden_ingreso");
            entity.Property(e => e.Estado)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.IdOrdenCompra).HasColumnName("Id_orden_compra");
            entity.Property(e => e.OrigenDeCompra)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Origen_de_compra");

            entity.HasOne(d => d.IdOrdenCompraNavigation).WithMany(p => p.OrdenDeIngresos)
                .HasForeignKey(d => d.IdOrdenCompra)
                .HasConstraintName("FK__Orden_de___Id_or__6EF57B66");
        });

        modelBuilder.Entity<OrdenEgreso>(entity =>
        {
            entity.HasKey(e => e.IdOrdenEgreso).HasName("PK__Orden_eg__21C30FEBA07A17C6");

            entity.ToTable("Orden_egreso");

            entity.Property(e => e.IdOrdenEgreso).HasColumnName("Id_orden_egreso");
            entity.Property(e => e.Destino)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Estado)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TipoEgreso)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Tipo_egreso");
        });

        modelBuilder.Entity<PreciosHistoricos>(entity =>
        {
            entity.HasKey(e => e.IdPreciosHistoricos).HasName("PK__PreciosH__0578A7F607777C6B");

            entity.Property(e => e.IdPreciosHistoricos).HasColumnName("Id_precios_historicos");
            entity.Property(e => e.FechaFinalizacion).HasColumnName("Fecha_finalizacion");
            entity.Property(e => e.FechaInicio).HasColumnName("Fecha_inicio");
            entity.Property(e => e.IdProductoProveedor).HasColumnName("Id_producto_proveedor");
            entity.Property(e => e.Precio).HasColumnType("decimal(18, 0)");

            entity.HasOne(d => d.IdProductoProveedorNavigation).WithMany(p => p.PreciosHistoricos)
                .HasForeignKey(d => d.IdProductoProveedor)
                .HasConstraintName("FK__PreciosHi__Id_pr__6B24EA82");
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.IdProducto).HasName("PK__Producto__1D8EFF01EF921852");

            entity.ToTable("Producto");

            entity.Property(e => e.IdProducto).HasColumnName("Id_producto");
            entity.Property(e => e.CodigoProducto)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Codigo_producto");
            entity.Property(e => e.LineaDeProducto).HasColumnName("Linea_de_producto");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.LineaDeProductoNavigation).WithMany(p => p.Productos)
                .HasForeignKey(d => d.LineaDeProducto)
                .HasConstraintName("FK__Producto__Linea___68487DD7");
        });

        modelBuilder.Entity<ProductoProveedor>(entity =>
        {
            entity.HasKey(e => e.IdProductoProveedor).HasName("PK__Producto__A0750A0C29A3249E");

            entity.ToTable("Producto_Proveedor");

            entity.Property(e => e.IdProductoProveedor).HasColumnName("Id_producto_proveedor");
            entity.Property(e => e.IdProducto).HasColumnName("Id_producto");
            entity.Property(e => e.IdProveedor).HasColumnName("Id_proveedor");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.ProductoProveedors)
                .HasForeignKey(d => d.IdProducto)
                .HasConstraintName("FK__Producto___Id_pr__693CA210");

            entity.HasOne(d => d.IdProveedorNavigation).WithMany(p => p.ProductoProveedors)
                .HasForeignKey(d => d.IdProveedor)
                .HasConstraintName("FK__Producto___Id_pr__6A30C649");
        });

        modelBuilder.Entity<Proveedores>(entity =>
        {
            entity.HasKey(e => e.IdProveedor).HasName("PK__Proveedo__6704E5A8BC809F68");

            entity.Property(e => e.IdProveedor).HasColumnName("Id_proveedor");
            entity.Property(e => e.Direccion)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.NombreProveedor)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.PaisOrigen)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.PersonaContacto)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Ruc)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Telefono)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.TipoProveedor)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Stock>(entity =>
        {
            entity.HasKey(e => e.IdStock).HasName("PK__Stock__3A39590A77516DD3");

            entity.ToTable("Stock");

            entity.Property(e => e.IdStock).HasColumnName("id_stock");
            entity.Property(e => e.IdProductoProveedor).HasColumnName("Id_producto_proveedor");
            entity.Property(e => e.IdUbicacion).HasColumnName("Id_ubicacion");

            entity.HasOne(d => d.IdProductoProveedorNavigation).WithMany(p => p.Stocks)
                .HasForeignKey(d => d.IdProductoProveedor)
                .HasConstraintName("FK__Stock__Id_produc__75A278F5");

            entity.HasOne(d => d.IdUbicacionNavigation).WithMany(p => p.Stocks)
                .HasForeignKey(d => d.IdUbicacion)
                .HasConstraintName("FK__Stock__Id_ubicac__76969D2E");
        });

        modelBuilder.Entity<Transferencias>(entity =>
        {
            entity.HasKey(e => e.IdTransferencia).HasName("PK__Transfer__749D659DE0C5161E");

            entity.Property(e => e.IdTransferencia).HasColumnName("Id_transferencia");
            entity.Property(e => e.UbicacionDestinoId).HasColumnName("Ubicacion_destino_id");
            entity.Property(e => e.UbicacionOrigenId).HasColumnName("Ubicacion_origen_id");

            entity.HasOne(d => d.UbicacionDestino).WithMany(p => p.TransferenciaUbicacionDestinos)
                .HasForeignKey(d => d.UbicacionDestinoId)
                .HasConstraintName("FK__Transfere__Ubica__787EE5A0");

            entity.HasOne(d => d.UbicacionOrigen).WithMany(p => p.TransferenciaUbicacionOrigens)
                .HasForeignKey(d => d.UbicacionOrigenId)
                .HasConstraintName("FK__Transfere__Ubica__778AC167");
        });

        modelBuilder.Entity<Ubicaciones>(entity =>
        {
            entity.HasKey(e => e.IdUbicacion).HasName("PK__Ubicacio__CFBBC8617A7EF099");

            entity.Property(e => e.IdUbicacion).HasColumnName("Id_ubicacion");
            entity.Property(e => e.LugarUbicacion)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Lugar_ubicacion");
        });

        modelBuilder.Entity<Usuarios>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__Usuarios__EF59F76266B4A2BA");

            entity.HasIndex(e => e.Email, "UQ__Usuarios__A9D10534609B768C").IsUnique();

            entity.Property(e => e.IdUsuario).HasColumnName("Id_usuario");
            entity.Property(e => e.Contraseña)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.NombreCompleto)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
