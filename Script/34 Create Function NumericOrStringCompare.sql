
create   --drop   create
FUNCTION [dbo].[NumericOrStringCompare]
(@QueDataType varchar(10),
@StrQueData VARCHAR(100),
@strAnsData VARCHAR(100)
)
RETURNS tinyint
AS
BEGIN
	DECLARE @intRet tinyint
	DECLARE @intAns decimal(18,4)
	DECLARE @intOk decimal(18,4)
	SET @intAns =0;
	SET @intOk =1;
	set @intRet = 0;
	IF (len(@StrQueData)>0 And Len(@strAnsData)>0  )
		begiN
			 
			set @StrQueData = replace(@StrQueData,'	','');
			set @StrQueData = replace(@StrQueData,ascii(9),'');
			set @StrQueData = replace(@StrQueData,ascii(32),'');
			set @StrQueData = replace(@StrQueData,ascii(13),'');
			set @strAnsData = replace(@strAnsData,'	','');
			set @strAnsData = replace(@strAnsData,ascii(9),'');
			set @strAnsData = replace(@strAnsData,ascii(32),'');
			set @strAnsData = replace(@strAnsData,ascii(13),'');
			--print @StrQueData;
			--print @strAnsData;
			if (@QueDataType = 'NUM')
			BEGIN
				IF (ISNUMERIC(@StrQueData) = 1 and ISNUMERIC(@strAnsData) = 1 )
				BEGIN
					set @intOk  = convert(decimal(18,4),rtrim(ltrim(@StrQueData)));
					set @intAns = convert(decimal(18,4),ltrim(rtrim(@strAnsData)));
			
					IF (@intOk=@intAns) 
						BEGIN
							set @intRet = 1;
						END
					Else 
						BEGIN
							set @intRet = 0;
						END;
				END
				ELSE
				BEGIN
					 if (ltrim(Rtrim(@StrQueData)) = ltrim(Rtrim(@strAnsData))   )
						begin
							set @intRet = 1;
						end
					ELSE
						begin
							set @intRet = 0; 
						end
				END
			END
			ELSE
				BEGIN
					 if (ltrim(Rtrim(@StrQueData)) = ltrim(Rtrim(@strAnsData))   )
						begin
							set @intRet = 1;
						end
					ELSE
						begin
							set @intRet = 0; 
						end
				END
		END
		ELSE if (ltrim(Rtrim(@StrQueData)) = ltrim(Rtrim(@strAnsData))   )
					begin
						set @intRet = 1;
					end
				ELSE
					begin
						set @intRet = 0; 
					end

	Return @intRet;
 END



GO


