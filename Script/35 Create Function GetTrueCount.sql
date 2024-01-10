


create        FUNCTION [dbo].[GetTrueCount]
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
	if @intAlpha > 0
	BEGIN
		if [dbo].[GetFalseCount](@strOKData,@strAnsData) > 0
		begin
			select @intOk = 0
		end
		else
		begin
			select @intOk = count(*)  from [dbo].[SplitInToRows](@strOKData,',') a left join [dbo].[SplitInToRows](@strAnsData,',') b on a.Data = b.data where b.data is not null
		END
		END
	END
RETURN  @intOk
END


GO


