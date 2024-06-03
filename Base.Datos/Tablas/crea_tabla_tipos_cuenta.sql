USE [multimoney_banca]
GO

CREATE TABLE [dbo].[Tipos_De_Cuenta] (
	[Id_Tipo_Cuenta] int identity(1,1),
	[Nombre] varchar(100) NOT NULL,
	[Id_Tasa_Interes] int NOT NULL,
	PRIMARY KEY (Id_Tipo_Cuenta),
	FOREIGN KEY (Id_Tasa_Interes) REFERENCES [dbo].[Tasas_De_Interes](Id_Tasa_Interes)
)

GO
