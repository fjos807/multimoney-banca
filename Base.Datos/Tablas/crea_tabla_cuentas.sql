USE [multimoney_banca]
GO

CREATE TABLE [dbo].[Cuentas] (
	[Id_Cuenta] int identity(1,1),
	[Numero_IBAN] varchar(22) NOT NULL,
	[Id_Tipo_Cuenta] int NOT NULL,
	[Id_Cliente] int NOT NULL,
	[Moneda] char(3) NOT NULL,
	[Saldo_Disponible] money NOT NULL,
	[Saldo_Bloqueado] money NOT NULL,
	PRIMARY KEY (Id_Cuenta),
	FOREIGN KEY (Id_Tipo_Cuenta) REFERENCES [dbo].[Tipos_de_Cuenta](Id_Tipo_Cuenta),
	FOREIGN KEY (Id_Cliente) REFERENCES [dbo].[Clientes](Id_Cliente)
)

GO