USE [multimoney_banca]
GO
/****** Object:  StoredProcedure [dbo].[PROC_CALCULAR_INTERESES_DIARIOS]    Script Date: 6/5/2024 12:00:22 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Frank Benavides Murillo
-- Create date: 04-06-2024
-- Description: Permite generar el calculo de intereses diarios para todas las cuentas
-- =============================================
CREATE PROCEDURE [dbo].[PROC_CALCULAR_INTERESES_DIARIOS]
AS
	DECLARE @v_id_cuenta INT;
	DECLARE @v_fecha_ultimo_calculo DATETIME;
	DECLARE @v_saldo_actual MONEY;
	DECLARE @v_porcentaje_interes DECIMAL;
	DECLARE @v_porcentaje_interes_diario DECIMAL;
	DECLARE @v_interes_diario MONEY;
BEGIN
	BEGIN TRANSACTION;
    SAVE TRANSACTION MySavePoint;
	BEGIN TRY
		SELECT 
			[Id_Cuenta]
		INTO 
			#Tabla_Temporal_Cuentas 
		FROM
			[dbo].[Cuentas]

		WHILE EXISTS (SELECT * FROM #Tabla_Temporal_Cuentas)
			BEGIN

				SELECT
					@v_id_cuenta = (SELECT TOP 1 [Id_Cuenta]
									FROM #Tabla_Temporal_Cuentas)

				SELECT
					TOP 1
					@v_fecha_ultimo_calculo = [Fecha]
				FROM
					[dbo].[Historico_Calculo_Interes]
				WHERE
					[Id_Cuenta] = @v_id_cuenta
				ORDER BY 
					[Id_Historico_Calculo_Interes] DESC

				IF @v_fecha_ultimo_calculo IS NULL OR DATEDIFF(day, @v_fecha_ultimo_calculo,  GETDATE()) > 0
					BEGIN
						SELECT
							@v_saldo_actual = cuentas.[Saldo_Disponible],
							@v_porcentaje_interes = tasas.[Porcentaje]
						FROM
							[dbo].[Cuentas] cuentas
								INNER JOIN
							[dbo].[Tipos_De_Cuenta] tipos_cuentas
									ON cuentas.[Id_Tipo_Cuenta] = tipos_cuentas.[Id_Tipo_Cuenta]
								INNER JOIN
							[dbo].[Tasas_De_Interes] tasas
									ON tipos_cuentas.[Id_Tasa_Interes] = tasas.[Id_Tasa_Interes]
						WHERE
							cuentas.[Id_Cuenta] = @v_id_cuenta

						SET @v_interes_diario = dbo.calcularMontoInteresDiario(@v_porcentaje_interes, @v_saldo_actual);
						
						SET @v_porcentaje_interes_diario = dbo.calcularPorcentajeInteresDiario(@v_porcentaje_interes);

						INSERT INTO
							[dbo].[Historico_Calculo_Interes] (
								[Id_Cuenta],
								[Fecha],
								[Porcentaje_Interes],
								[Saldo_Anterior],
								[Saldo_Siguiente],
								[Monto_Interes]
							)
						VALUES (
							@v_id_cuenta,
							GETDATE(),
							@v_porcentaje_interes_diario,
							@v_saldo_actual,
							@v_saldo_actual + @v_interes_diario,
							@v_interes_diario
						)
						
						UPDATE
							[dbo].[Cuentas]
						SET
							[Saldo_Disponible] = @v_saldo_actual + @v_interes_diario
						WHERE
							[Id_Cuenta] = @v_id_cuenta
				END

				DELETE FROM 
					#Tabla_Temporal_Cuentas
				WHERE
					[Id_Cuenta] = @v_id_cuenta
			END

		DROP TABLE #Tabla_Temporal_Cuentas
		
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