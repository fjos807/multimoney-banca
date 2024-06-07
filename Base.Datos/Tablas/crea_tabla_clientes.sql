USE [multimoney_banca]
GO

CREATE TABLE [dbo].[Clientes] (
	[Id_Cliente] int identity(1,1) NOT NULL,
	[Tipo_Identificacion] smallint NOT NULL,
	[Identificacion] varchar(12) NOT NULL,
	[Nombre] varchar(100) NOT NULL,
	[Primer_Apellido] varchar(100) NOT NULL,
	[Segundo_Apellido] varchar(100) NULL,
	[Telefono] varchar(25) NOT NULL,
	[Correo_Electronico] varchar(150) NOT NULL,
	PRIMARY KEY (Id_Cliente)
)

GO