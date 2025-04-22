-- Creación de la Base de Datos
CREATE DATABASE Inventario2025;
GO

USE Inventario2025;
GO

-- Creación de la tabla de categorías
CREATE TABLE tb_categorias (
    idcategoria INT PRIMARY KEY IDENTITY(1,1),
    nombre_categoria VARCHAR(100) NOT NULL,
    descripcion VARCHAR(255),
    estado BIT DEFAULT 1
);
GO

-- Creación de la tabla de productos
CREATE TABLE tb_productos (
    idproducto INT PRIMARY KEY IDENTITY(1,1),
    nombre VARCHAR(100) NOT NULL,
    descripcion VARCHAR(255),
    precio_unitario DECIMAL(10,2) NOT NULL,
    stock INT NOT NULL DEFAULT 0,
    idcategoria INT REFERENCES tb_categorias(idcategoria),
    fecha_registro DATETIME DEFAULT GETDATE(),
    estado BIT DEFAULT 1
);
GO

-- Inserción de datos de prueba para categorías
INSERT INTO tb_categorias (nombre_categoria, descripcion, estado)
VALUES 
    ('Electrónica', 'Productos electrónicos y gadgets', 1),
    ('Informática', 'Hardware y software para computadoras', 1),
    ('Oficina', 'Artículos y mobiliario de oficina', 1),
    ('Hogar', 'Productos para el hogar', 1),
    ('Deporte', 'Artículos deportivos', 1);
GO

-- Inserción de datos de prueba para productos
INSERT INTO tb_productos (nombre, descripcion, precio_unitario, stock, idcategoria)
VALUES 
    ('Laptop HP 15', 'Laptop con procesador i5, 8GB RAM, 512GB SSD', 2500.00, 15, 2),
    ('Mouse inalámbrico', 'Mouse ergonómico inalámbrico con batería recargable', 89.90, 30, 2),
    ('Televisor Smart TV 50"', 'Televisor LED 4K con sistema operativo Android TV', 1899.00, 8, 1),
    ('Teclado mecánico', 'Teclado mecánico con retroiluminación RGB', 249.90, 12, 2),
    ('Escritorio oficina', 'Escritorio de 140x60cm con cajones', 399.00, 5, 3),
    ('Silla ergonómica', 'Silla con soporte lumbar ajustable', 349.00, 10, 3),
    ('Licuadora 5 velocidades', 'Licuadora de 600W con jarra de vidrio', 159.90, 20, 4),
    ('Balón de fútbol', 'Balón oficial tamaño 5', 79.90, 25, 5);
GO

-- Procedimiento almacenado para listar todos los productos
CREATE PROCEDURE usp_ListarProductos
AS
BEGIN
    SELECT p.idproducto, p.nombre, p.descripcion, p.precio_unitario, p.stock, 
           p.idcategoria, c.nombre_categoria, p.fecha_registro, p.estado
    FROM tb_productos p
    INNER JOIN tb_categorias c ON p.idcategoria = c.idcategoria
    WHERE p.estado = 1
    ORDER BY p.nombre;
END
GO

-- Procedimiento almacenado para listar todas las categorías
CREATE PROCEDURE usp_ListarCategorias
AS
BEGIN
    SELECT idcategoria, nombre_categoria, descripcion, estado
    FROM tb_categorias
    WHERE estado = 1
    ORDER BY nombre_categoria;
END
GO

-- Procedimiento almacenado para agregar un producto
CREATE PROCEDURE usp_AgregarProducto
    @nombre VARCHAR(100),
    @descripcion VARCHAR(255),
    @precio_unitario DECIMAL(10,2),
    @stock INT,
    @idcategoria INT
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION
            INSERT INTO tb_productos (nombre, descripcion, precio_unitario, stock, idcategoria)
            VALUES (@nombre, @descripcion, @precio_unitario, @stock, @idcategoria);
            
            SELECT 'Producto registrado con éxito' AS Mensaje;
        COMMIT TRANSACTION
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION
        SELECT ERROR_MESSAGE() AS Mensaje;
    END CATCH
END
GO

-- Procedimiento almacenado para actualizar un producto
CREATE PROCEDURE usp_ActualizarProducto
    @idproducto INT,
    @nombre VARCHAR(100),
    @descripcion VARCHAR(255),
    @precio_unitario DECIMAL(10,2),
    @stock INT,
    @idcategoria INT
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION
            UPDATE tb_productos
            SET nombre = @nombre,
                descripcion = @descripcion,
                precio_unitario = @precio_unitario,
                stock = @stock,
                idcategoria = @idcategoria
            WHERE idproducto = @idproducto;
            
            IF @@ROWCOUNT > 0
                SELECT 'Producto actualizado correctamente' AS Mensaje;
            ELSE
                SELECT 'No se encontró el producto con el ID especificado' AS Mensaje;
        COMMIT TRANSACTION
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION
        SELECT ERROR_MESSAGE() AS Mensaje;
    END CATCH
END
GO

-- Procedimiento almacenado para obtener un producto por su ID
CREATE PROCEDURE usp_ObtenerProductoPorID
    @idproducto INT
AS
BEGIN
    SELECT p.idproducto, p.nombre, p.descripcion, p.precio_unitario, p.stock, 
           p.idcategoria, c.nombre_categoria, p.fecha_registro, p.estado
    FROM tb_productos p
    INNER JOIN tb_categorias c ON p.idcategoria = c.idcategoria
    WHERE p.idproducto = @idproducto;
END
GO

-- Ejemplo de ejecución de los procedimientos almacenados:
-- EXEC usp_ListarProductos;
-- EXEC usp_ListarCategorias;
-- EXEC usp_ObtenerProductoPorID 1;
-- EXEC usp_AgregarProducto 'Audífonos Bluetooth', 'Audífonos con cancelación de ruido', 199.90, 25, 1;
-- EXEC usp_ActualizarProducto 1, 'Laptop HP 15 Actualizada', 'Laptop con procesador i7, 16GB RAM, 1TB SSD', 2999.00, 10, 2;