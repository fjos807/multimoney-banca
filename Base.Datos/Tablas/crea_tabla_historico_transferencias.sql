USE [multimoney_banca]
GO

CREATE TABLE [dbo].[Historico_Transferencias] (
	[Id_Historico_Transferencia] int identity(1,1),
	[Id_Cuenta_Origen] int NOT NULL,
	[Id_Cuenta_Destino] int NOT NULL,
	[Fecha] datetime NOT NULL,
	[Monto] money NOT NULL,
	PRIMARY KEY (Id_Historico_Transferencia),
	FOREIGN KEY (Id_Cuenta_Origen) REFERENCES [dbo].[Cuentas](Id_Cuenta),
	FOREIGN KEY (Id_Cuenta_Destino) REFERENCES [dbo].[Cuentas](Id_Cuenta)
)

GO	