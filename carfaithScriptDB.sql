-- Tabla: Usuarios
CREATE TABLE Usuarios (
    Id_usuario INT PRIMARY KEY IDENTITY,
    NombreCompleto NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100) NOT NULL UNIQUE,
    Contraseña NVARCHAR(255) NOT NULL
);

-- Tabla: Proveedores
CREATE TABLE Proveedores (
    Id_proveedor INT PRIMARY KEY IDENTITY,
    NombreProveedor NVARCHAR(100) NOT NULL,
    PaisOrigen NVARCHAR(100),
    TipoProveedor NVARCHAR(20) CHECK (TipoProveedor IN ('local', 'nacional', 'internacional')),
    Telefono NVARCHAR(20),
    Email NVARCHAR(100),
    PersonaContacto NVARCHAR(100),
    FechaRegistro DATE,
    Ruc NVARCHAR(20),
    Direccion NVARCHAR(200),
    Estado BIT NOT NULL -- 1 = activo, 0 = inactivo
);

-- Tabla: Lineas_de_Producto
CREATE TABLE Lineas_de_Producto (
    Id_linea INT PRIMARY KEY IDENTITY,
    Nombre NVARCHAR(100) NOT NULL,
    Descripcion NVARCHAR(255)
);

-- Tabla: Producto
CREATE TABLE Producto (
    Id_producto INT PRIMARY KEY IDENTITY,
    Codigo_producto NVARCHAR(50) NOT NULL,
    Nombre NVARCHAR(100) NOT NULL,
    Linea_de_producto INT NOT NULL,
    FOREIGN KEY (Linea_de_producto) REFERENCES Lineas_de_Producto(Id_linea)
);

-- Tabla: Producto_Proveedor (relación muchos a muchos)
CREATE TABLE Producto_Proveedor (
    Id_producto_proveedor INT PRIMARY KEY IDENTITY,
    Id_prodcuto INT NOT NULL,
    Id_proveedor INT NOT NULL,
    FOREIGN KEY (Id_prodcuto) REFERENCES Producto(Id_producto),
    FOREIGN KEY (Id_proveedor) REFERENCES Proveedores(Id_proveedor)
);

-- Tabla: Precios_Historicos
CREATE TABLE PreciosHistoricos (
    Id_precios_historicos INT PRIMARY KEY IDENTITY,
    Id_producto INT NOT NULL,
    Id_proveedor INT NOT NULL,
    Precio DECIMAL(10, 2) NOT NULL,
    Fecha_inicio DATE NOT NULL,
    Fecha_finalizacion DATE,
    FOREIGN KEY (Id_producto) REFERENCES Producto(Id_producto),
    FOREIGN KEY (Id_proveedor) REFERENCES Proveedores(Id_proveedor)
);

-- Tabla: Orden_de_compra
CREATE TABLE Orden_de_compra (
    Id_orden INT PRIMARY KEY IDENTITY,
    Numero_orden NVARCHAR(50) NOT NULL,
    Id_proveedor INT NOT NULL,
    Archivo_pdf NVARCHAR(255),
    Estado NVARCHAR(50),
    Fecha_creacion DATE NOT NULL,
    Fecha_estimada_entrega DATE,
    FOREIGN KEY (Id_proveedor) REFERENCES Proveedores(Id_proveedor)
);

-- Tabla: Orden_de_ingreso
CREATE TABLE Orden_de_ingreso (
    Id_orden_ingreso INT PRIMARY KEY IDENTITY,
    Id_orden_compra INT NOT NULL,
    Tipo_ingreso NVARCHAR(50) CHECK (Tipo_ingreso IN ('Compra local', 'Compra nacional', 'Internacional')),
    Fecha DATE NOT NULL,
    Estado NVARCHAR(50),
    FOREIGN KEY (Id_orden_compra) REFERENCES Orden_de_compra(Id_orden)
);

-- Tabla: Ubicaciones
CREATE TABLE Ubicaciones (
    Id_ubicacion INT PRIMARY KEY IDENTITY,
    Lugar_ubicacion NVARCHAR(100) NOT NULL
);

-- Tabla: Detalle_orden_ingreso
CREATE TABLE Detalle_orden_ingreso (
    Id_detalle_orden_ingreso INT PRIMARY KEY IDENTITY,
    Orden_ingreso_id INT NOT NULL,
    Producto_id INT NOT NULL,
    Cantidad INT NOT NULL,
    Precio_unitario DECIMAL(10, 2) NOT NULL,
    Ubicacion_id INT NOT NULL,
    FOREIGN KEY (Orden_ingreso_id) REFERENCES Orden_de_ingreso(Id_orden_ingreso),
    FOREIGN KEY (Producto_id) REFERENCES Producto(Id_producto),
    FOREIGN KEY (Ubicacion_id) REFERENCES Ubicaciones(Id_ubicacion)
);

-- Tabla: Orden_egreso
CREATE TABLE Orden_egreso (
    Id_orden_egreso INT PRIMARY KEY IDENTITY,
    Tipo_egreso NVARCHAR(50) CHECK (Tipo_egreso IN ('venta', 'traslado')),
    Fecha DATE NOT NULL,
    Destino NVARCHAR(100),
    Estado NVARCHAR(50)
);

-- Tabla: Detalle_orden_egreso
CREATE TABLE Detalle_orden_egreso (
    Id_detalle_orden_egreso INT PRIMARY KEY IDENTITY,
    Orden_egreso_id INT NOT NULL,
    Producto_id INT NOT NULL,
    Cantidad INT NOT NULL,
    Ubicacion_id INT NOT NULL,
    FOREIGN KEY (Orden_egreso_id) REFERENCES Orden_egreso(Id_orden_egreso),
    FOREIGN KEY (Producto_id) REFERENCES Producto(Id_producto),
    FOREIGN KEY (Ubicacion_id) REFERENCES Ubicaciones(Id_ubicacion)
);

-- Tabla: Stock (clave compuesta)
CREATE TABLE Stock (
    Id_producto INT NOT NULL,
    Id_ubicacion INT NOT NULL,
    Cantidad INT NOT NULL,
    PRIMARY KEY (Id_producto, Id_ubicacion),
    FOREIGN KEY (Id_producto) REFERENCES Producto(Id_producto),
    FOREIGN KEY (Id_ubicacion) REFERENCES Ubicaciones(Id_ubicacion)
);

-- Tabla: Transferencias
CREATE TABLE Transferencias (
    Id_transferencia INT PRIMARY KEY IDENTITY,
    Fecha DATE NOT NULL,
    Ubicacion_origen_id INT NOT NULL,
    Ubicacion_destino_id INT NOT NULL,
    FOREIGN KEY (Ubicacion_origen_id) REFERENCES Ubicaciones(Id_ubicacion),
    FOREIGN KEY (Ubicacion_destino_id) REFERENCES Ubicaciones(Id_ubicacion)
);

-- Tabla: Detalle_transferencia
CREATE TABLE Detalle_transferencia (
    Id_detalle_transferencia INT PRIMARY KEY IDENTITY,
    Id_transferencia INT NOT NULL,
    Id_producto INT NOT NULL,
    Cantidad INT NOT NULL,
    FOREIGN KEY (Id_transferencia) REFERENCES Transferencias(Id_transferencia),
    FOREIGN KEY (Id_producto) REFERENCES Producto(Id_producto)
);