IF OBJECT_ID('[dbo].[SP_SetFilePublic]') IS NULL
	EXEC ('CREATE PROCEDURE [dbo].[SP_SetFilePublic] AS SET NOCOUNT ON;')
GO

ALTER procedure [dbo].SP_SetFilePublic
@FileId int
AS
BEGIN
	update [dbo].[Attachments]
	set [isPublic] = 1
	where [ID] = @FileId
END