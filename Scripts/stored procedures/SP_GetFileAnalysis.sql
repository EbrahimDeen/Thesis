IF OBJECT_ID('[SP_GetFileAnalysis]') IS NULL
	EXEC ('CREATE PROCEDURE [SP_GetFileAnalysis] AS SET NOCOUNT ON;')
GO
ALTER PROCEDURE [SP_GetFileAnalysis]
@FileId int,
@UserId int
AS
BEGIN
    select  ID
            ,IP
            ,CountryName
            ,CityName
            ,ContinentName
            ,DownloadedBy
            ,OwnerId
            ,FileId
    from Analysis
    WHERE FileID = @FileID
    AND OwnerId = @UserId
End