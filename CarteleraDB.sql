-- Ejecutar de una sola vez en una instancia limpia de LocalDB
-- sqlcmd -S "(localdb)\MSSQLLocalDB" -i CarteleraDB.sql
USE master;
GO
IF DB_ID(N'CarteleraDB') IS NOT NULL
BEGIN
    ALTER DATABASE CarteleraDB SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE CarteleraDB;
END
GO
CREATE DATABASE CarteleraDB;
GO
USE CarteleraDB;
GO
CREATE TABLE Funciones (
    Id       INT IDENTITY(1,1) PRIMARY KEY,
    Pelicula NVARCHAR(100)  NOT NULL,
    Sala     NVARCHAR(50)   NOT NULL,
    Precio   DECIMAL(10,2)  NOT NULL
);
GO
INSERT INTO Funciones (Pelicula, Sala, Precio) VALUES (N'Coco', N'Sala 1', 40.00);
INSERT INTO Funciones (Pelicula, Sala, Precio) VALUES (N'Moana', N'Sala 2', 45.00);
GO
