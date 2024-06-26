USE [multimoney_banca]
GO
/****** Object:  StoredProcedure [dbo].[PROC_CUENTAS_OBTENER_COMPLETO]    Script Date: 6/3/2024 10:39:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Frank Benavides Murillo
-- Create date: 04-06-2024
-- Description: Registra una nueva transferencia entre cuentas en el sistema
-- =============================================
CREATE PROCEDURE [dbo].[PROC_TRANSFERENCIAS_AGREGAR]
	@arg_id_cuenta_origen INT,
	@arg_id_cuenta_destino INT,
	@arg_monto MONEY,
	@arg_estado CHAR(3) OUT
AS
	DECLARE @v_numero_cuenta_origen INT;
	DECLARE @v_numero_cuenta_destino INT;
	DECLARE @v_saldo_cuenta_origen MONEY;
	DECLARE @v_saldo_cuenta_destino MONEY;
	DECLARE @v_id_historico_transferencia INT;
BEGIN
	BEGIN TRANSACTION;
    SAVE TRANSACTION MySavePoint;
	BEGIN TRY
		SELECT
			@v_numero_cuenta_origen = [Id_Cuenta],
			@v_saldo_cuenta_origen = [Saldo_Disponible]
		FROM
			[dbo].[Cuentas]
		WHERE
			[Id_Cuenta] = @arg_id_cuenta_origen;

		SELECT
			@v_numero_cuenta_destino = [Id_Cuenta],
			@v_saldo_cuenta_destino = [Saldo_Disponible]
		FROM
			[dbo].[Cuentas]
		WHERE
			[Id_Cuenta] = @arg_id_cuenta_destino;

		IF @v_numero_cuenta_origen IS NOT NULL AND @v_numero_cuenta_destino IS NOT NULL
			BEGIN
				IF (@v_saldo_cuenta_origen - @arg_monto) > 0
					BEGIN
						INSERT INTO
							[dbo].[Historico_Transferencias] (
								[Id_Cuenta_Origen],
								[Id_Cuenta_Destino],
								[Fecha],
								[Monto]
							)
						VALUES (
							@v_numero_cuenta_origen,
							@v_numero_cuenta_destino,
							GETDATE(),
							@arg_monto
						)

						SELECT @v_id_historico_transferencia = SCOPE_IDENTITY();

						INSERT INTO
							[dbo].[Movimientos_Por_Cuenta] (
								[Id_Historico_Transferencia],
								[Id_Tipo_Movimiento],
								[Id_Cuenta],
								[Descripcion],
								[Fecha],
								[Monto]
							)
						VALUES (
							@v_id_historico_transferencia,
							1,
							@v_numero_cuenta_origen,
							'Débito por transferencia entre cuentas',
							GETDATE(),
							@arg_monto
						), (
							@v_id_historico_transferencia,
							2,
							@v_numero_cuenta_destino,
							'Crédito por transferencia entre cuentas',
							GETDATE(),
							@arg_monto
						)

						UPDATE
							[dbo].[Cuentas]
						SET
							[Saldo_Disponible] = @v_saldo_cuenta_origen - @arg_monto
						WHERE
							[Id_Cuenta] = @v_numero_cuenta_origen

						UPDATE
							[dbo].[Cuentas]
						SET
							[Saldo_Disponible] = @v_saldo_cuenta_destino + @arg_monto
						WHERE
							[Id_Cuenta] = @v_numero_cuenta_destino

						SELECT @arg_estado = '200';
					END
				ELSE
					BEGIN
						SELECT @arg_estado = 'SNS';
					END
			END
		ELSE
			BEGIN
				SELECT @arg_estado = 'CNE';
			END
		COMMIT;
	END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
        BEGIN
            ROLLBACK TRANSACTION;
			THROW;
        END
    END CATCH
END