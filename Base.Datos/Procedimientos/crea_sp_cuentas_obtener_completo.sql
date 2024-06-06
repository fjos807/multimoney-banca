USE [multimoney_banca]
GO
/****** Object:  StoredProcedure [dbo].[PROC_CUENTAS_OBTENER_COMPLETO]    Script Date: 6/6/2024 12:14:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Frank Benavides Murillo
-- Create date: 03-06-2024
-- Description: Obtiene toda la información de una cuenta
-- =============================================
CREATE PROCEDURE [dbo].[PROC_CUENTAS_OBTENER_COMPLETO]
	@arg_tipo_identificacion INT,
	@arg_identificacion VARCHAR(12),
	@arg_id_cuenta INT,
	@arg_estado char(3) out
AS
	DECLARE @v_numero_cuenta INT;
BEGIN
	SELECT
		@v_numero_cuenta = [Id_Cuenta]
	FROM
		[dbo].[Cuentas] cuentas
			INNER JOIN
		[dbo].[Clientes] clientes
			ON cuentas.[Id_Cliente] = clientes.[Id_Cliente]
	WHERE
		cuentas.[Id_Cuenta] = @arg_id_cuenta
			AND
		clientes.[Tipo_Identificacion] = @arg_tipo_identificacion
			AND
		clientes.[Identificacion] = @arg_identificacion

	IF @v_numero_cuenta IS NOT NULL
		BEGIN
			SELECT
				(
					SELECT
						[Id_Cuenta] AS 'idCuenta',
						[Numero_IBAN] AS 'numeroIBAN',
						[Moneda] AS 'moneda',
						[Saldo_Disponible] AS 'saldoDisponible',
						[Saldo_Bloqueado] AS 'saldoBloqueado'
					FROM
						[dbo].[Cuentas]
					WHERE
						[Id_Cuenta] = @v_numero_cuenta
					FOR
						JSON AUTO, WITHOUT_ARRAY_WRAPPER
				) AS 'cuenta',
				(
					SELECT
						movimientos.[Nombre] AS 'nombre',
						movimientos_cuenta.[Descripcion] AS 'descripcion',
						movimientos_cuenta.[Fecha] AS 'fecha',
						movimientos_cuenta.[Monto] AS 'monto'
					FROM
						[dbo].[Movimientos_Por_Cuenta] movimientos_cuenta
							INNER JOIN
						[dbo].[Tipos_De_Movimiento] movimientos
							ON movimientos_cuenta.[Id_Movimiento] = movimientos.[Id_Tipo_Movimiento]
					WHERE
						[Id_Cuenta] = @v_numero_cuenta
					FOR
						JSON PATH
				) AS 'movimientos',
				(
					SELECT
						[Fecha] AS 'fecha',
						[Porcentaje_Interes] AS 'porcentajeInteres',
						[Monto_Interes] AS 'montoInteres',
						[Saldo_Anterior] AS 'saldoAnterior',
						[Saldo_Siguiente] AS 'saldoSiguiente'
					FROM
						[dbo].[Historico_Calculo_Interes]
					WHERE
						[Id_Cuenta] = @v_numero_cuenta
					FOR
						JSON AUTO
				) AS 'intereses'

			SELECT @arg_estado = '200';
		END
	ELSE
		BEGIN
			SELECT @arg_estado = 'CNE';
		END
END