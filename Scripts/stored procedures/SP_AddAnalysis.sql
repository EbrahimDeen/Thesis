IF OBJECT_ID('[SP_AddAnalysis]') IS NULL
	EXEC ('CREATE PROCEDURE [SP_AddAnalysis] AS SET NOCOUNT ON;')
GO

ALTER PROCEDURE [SP_AddAnalysis] 
	@IP VARCHAR(24)
	,@CountryName VARCHAR(255)
	,@CityName VARCHAR(255)
	,@ContinentName VARCHAR(255)
	,@DownloadedBy INT
	,@FileId INT
AS
BEGIN
	DECLARE @OwnerId INT = (
			SELECT TOP 1 [UserId]
			FROM [FileMetaData]
			WHERE fileId = @fileId
			)

	INSERT INTO Analysis (
		IP
		,CountryName
		,CityName
		,ContinentName
		,DownloadedBy
		,OwnerId
		,FileId
		,CreatedDate
		)
	VALUES (
		@IP
		,@CountryName
		,@CityName
		,@ContinentName
		,@DownloadedBy
		,@OwnerId
		,@FileID
		,GETDATE()
		)
END
