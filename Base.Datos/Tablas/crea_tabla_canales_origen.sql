USE [multimoney_banca]
GO

CREATE TABLE [dbo].[Canales_Origen] (
	[Id_Canal_Origen] int identity(1,1),
	[Nombre] varchar(50) NOT NULL,
	PRIMARY KEY (Id_Canal_Origen)
)

GO	