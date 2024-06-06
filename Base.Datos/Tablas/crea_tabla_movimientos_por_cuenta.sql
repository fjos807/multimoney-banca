USE [multimoney_banca]
GO

CREATE TABLE [dbo].[Movimientos_Por_Cuenta] (
	[Id_Movimiento] int identity(1,1),
	[Id_Historico_Transferencia] int NULL,
	[Id_Historico_Movimiento_Contable] int NULL,
	[Id_Tipo_Movimiento] int NOT NULL,
	[Id_Cuenta] int NOT NULL,
	[Descripcion] varchar(100) NOT NULL,
	[Fecha] datetime NOT NULL,
	[Monto] money NOT NULL,
	PRIMARY KEY (Id_Movimiento),
	FOREIGN KEY (Id_Historico_Transferencia) REFERENCES [dbo].[Historico_Transferencias](Id_Historico_Transferencia),
	FOREIGN KEY (Id_Historico_Movimiento_Contable) REFERENCES [dbo].[Historico_Movimientos_Contables](Id_Historico_Movimiento_Contable),
	FOREIGN KEY (Id_Tipo_Movimiento) REFERENCES [dbo].[Tipos_De_Movimiento](Id_Tipo_Movimiento),
	FOREIGN KEY (Id_Cuenta) REFERENCES [dbo].[Cuentas](Id_Cuenta)
)

GO	