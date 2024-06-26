USE [multimoney_banca]
GO
/****** Object:  StoredProcedure [dbo].[PROC_MOVIMIENTOS_CONTABLES_AGREGAR]    Script Date: 6/4/2024 12:55:01 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Frank Benavides Murillo
-- Create date: 03-06-2024
-- Description: Registra un nuevo retiro o deposito en el sistema
-- =============================================
CREATE PROCEDURE [dbo].[PROC_MOVIMIENTOS_CONTABLES_AGREGAR]
	@arg_id_cuenta INT,
	@arg_tipo_movimiento VARCHAR(50),
	@arg_monto MONEY,
	@arg_estado CHAR(3) OUT
AS
	DECLARE @v_numero_cuenta INT;
	DECLARE @v_saldo_actual MONEY;
	DECLARE @v_nuevo_saldo MONEY;
	DECLARE @v_id_tipo_movimiento INT;
	DECLARE @v_id_historico_movimiento_contable INT;
BEGIN
	BEGIN TRANSACTION;
    SAVE TRANSACTION MySavePoint;
	BEGIN TRY
		SELECT
			@v_numero_cuenta = [Id_Cuenta],
			@v_saldo_actual = [Saldo_Disponible]
		FROM
			[dbo].[Cuentas]
		WHERE
			[Id_Cuenta] = @arg_id_cuenta;

		IF @v_numero_cuenta IS NOT NULL
			BEGIN
				SELECT
					@v_id_tipo_movimiento = [Id_Tipo_Movimiento]
				FROM
					[dbo].[Tipos_De_Movimiento]
				WHERE
					[Nombre] LIKE @arg_tipo_movimiento

				IF @v_id_tipo_movimiento IS NOT NULL
					BEGIN
						SELECT @v_nuevo_saldo = CASE 
													WHEN (@v_id_tipo_movimiento = 1) THEN (@v_saldo_actual - @arg_monto) 
													ELSE (@v_saldo_actual + @arg_monto)
												END

						IF @v_nuevo_saldo > 0
							BEGIN

								INSERT INTO 
									[dbo].[Historico_Movimientos_Contables] (
										[Id_Cuenta],
										[Fecha],
										[Id_Tipo_Movimiento],
										[Monto]
									)
								VALUES (
									@v_numero_cuenta,
									GETDATE(),
									@v_id_tipo_movimiento,
									@arg_monto
								)

								SELECT @v_id_historico_movimiento_contable = SCOPE_IDENTITY();

								INSERT INTO
									[dbo].[Movimientos_Por_Cuenta] (
										[Id_Historico_Movimiento_Contable],
										[Id_Tipo_Movimiento],
										[Id_Cuenta],
										[Descripcion],
										[Fecha],
										[Monto]
									)
								VALUES (
									@v_id_historico_movimiento_contable,
									@v_id_tipo_movimiento,
									@arg_id_cuenta,
									CASE
										WHEN (@v_id_tipo_movimiento = 1) THEN 'Retiro de dinero' 
										ELSE 'Deposito de dinero'
									END,
									GETDATE(),
									@arg_monto
								)

								UPDATE
									[dbo].[Cuentas]
								SET
									[Saldo_Disponible] = @v_nuevo_saldo
								WHERE
									[Id_Cuenta] = @v_numero_cuenta

								SELECT @arg_estado = '200';

							END
						ELSE
							BEGIN
								SELECT @arg_estado = 'SNF';
							END
					END
				ELSE
					BEGIN
					SELECT @arg_estado = 'MNE';
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