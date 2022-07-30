create procedure [dbo].SP_SetFilePublic
@FileId int
AS
BEGIN
	update [dbo].[Attachments]
	set [isPublic] = 1
	where [ID] = @FileId
END