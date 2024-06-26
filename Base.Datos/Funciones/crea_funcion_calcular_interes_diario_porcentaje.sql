USE [multimoney_banca]
GO
/****** Object:  UserDefinedFunction [dbo].[calcularPorcentajeInteresDiario]    Script Date: 6/5/2024 12:04:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE FUNCTION [dbo].[calcularPorcentajeInteresDiario](@TasaInteres decimal)
RETURNS decimal(10, 7)
AS

BEGIN
    DECLARE @v_porcentaje decimal(10, 7);

	SELECT @v_porcentaje = ((@TasaInteres / 100) / 365)
    RETURN @v_porcentaje;
END;
