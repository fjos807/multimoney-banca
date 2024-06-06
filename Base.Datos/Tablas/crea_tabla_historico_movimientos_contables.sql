USE [multimoney_banca]
GO

CREATE TABLE [dbo].[Historico_Movimientos_Contables] (
	[Id_Historico_Movimiento_Contable] int identity(1,1),
	[Id_Cuenta] int NOT NULL,
	[Id_Tipo_Movimiento] int NOT NULL,
	[Fecha] datetime NOT NULL,
	[Monto] money NOT NULL,
	PRIMARY KEY (Id_Historico_Movimiento_Contable),
	FOREIGN KEY (Id_Cuenta) REFERENCES [dbo].[Cuentas](Id_Cuenta),
	FOREIGN KEY (Id_Tipo_Movimiento) REFERENCES [dbo].[Tipos_De_Movimiento](Id_Tipo_Movimiento)
)

GO	