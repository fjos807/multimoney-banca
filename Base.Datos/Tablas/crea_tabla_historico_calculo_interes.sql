USE [multimoney_banca]
GO

CREATE TABLE [dbo].[Historico_Calculo_Interes] (
	[Id_Historico_Calculo_Interes] int identity(1,1),
	[Id_Cuenta] int NOT NULL,
	[Fecha] datetime NOT NULL,
	[Porcentaje_Interes] decimal(10, 7) NOT NULL,
	[Monto_Interes] money NOT NULL,
	[Saldo_Anterior] money NOT NULL,
	[Saldo_Siguiente] money NOT NULL,
	PRIMARY KEY (Id_Historico_Calculo_Interes),
	FOREIGN KEY (Id_Cuenta) REFERENCES [dbo].[Cuentas](Id_Cuenta)
)

GO	