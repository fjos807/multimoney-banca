USE [multimoney_banca]
GO

CREATE TABLE [dbo].[Tipos_De_Movimiento] (
	[Id_Tipo_Movimiento] int identity(1,1),
	[Nombre] varchar(50) NOT NULL,
	PRIMARY KEY (Id_Tipo_Movimiento)
)

GO	
