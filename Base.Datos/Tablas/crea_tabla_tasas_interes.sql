USE [multimoney_banca]
GO

CREATE TABLE [dbo].[Tasas_De_Interes] (
	[Id_Tasa_Interes] int identity(1,1),
	[Porcentaje] decimal NOT NULL,
	PRIMARY KEY (Id_Tasa_Interes)
)

GO