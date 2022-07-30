create procedure [test].SP_GetPublicFilesMeta
AS
BEGIN
	select * from [dbo].[Attachments]
	where [isPublic] = 1
END