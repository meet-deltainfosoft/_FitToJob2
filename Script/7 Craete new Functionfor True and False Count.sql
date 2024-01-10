 

SET QUOTED_IDENTIFIER ON
GO



create FUNCTION [dbo].[SplitInToRows]
(
	@RowData nvarchar(2000),
	@SplitOn nvarchar(5)
)  
RETURNS @RtnValue table 
(
	--Id int identity(1,1),
	Data nvarchar(100)
) 
AS  
BEGIN 
	Declare @Cnt int
	Set @Cnt = 1

	While (Charindex(@SplitOn,@RowData)>0)
	Begin
		Insert Into @RtnValue (data)
		Select 
			Data = ltrim(rtrim(Substring(@RowData,1,Charindex(@SplitOn,@RowData)-1)))

		Set @RowData = Substring(@RowData,Charindex(@SplitOn,@RowData)+1,len(@RowData))
		Set @Cnt = @Cnt + 1
	End
	
	Insert Into @RtnValue (data)
	Select Data = ltrim(rtrim(@RowData))

	Return
END




GO




GO




GO

--drop     FUNCTION [dbo].[GetFalseCount]
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




