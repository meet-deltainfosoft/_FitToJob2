
create     FUNCTION [dbo].[GetFalseCount]
(@strOKData NVARCHAR(100),
@strAnsData NVARCHAR(100)
)
RETURNS decimal(18,2)
AS
BEGIN
DECLARE @intAlpha INT
DECLARE @intOk decimal(18,2)
SET @intOk =0
SET @intAlpha = len(@strOKData)
BEGIN
IF @intAlpha > 0
BEGIN
	select @intOk = count(*)  from [dbo].[SplitInToRows](@strAnsData,',') b  left join [dbo].[SplitInToRows](@strOKData,',') a on   b.data = a.Data where a.data is null
	IF @intOk > 0
	BEGIN
		select @intOk =1
	END
END
END
RETURN  @intOk
END




GO


