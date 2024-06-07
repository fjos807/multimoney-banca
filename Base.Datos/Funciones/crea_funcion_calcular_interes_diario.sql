USE [multimoney_banca]
GO

CREATE FUNCTION calcularMontoInteresDiario(@TasaInteres decimal, @Saldo money)
RETURNS money
AS

BEGIN
    DECLARE @v_nuevo_saldo money;

	SELECT @v_nuevo_saldo =((@TasaInteres / 100) / 365) * @Saldo
    RETURN @v_nuevo_saldo;
END;
