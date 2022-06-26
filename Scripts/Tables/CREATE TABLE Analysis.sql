CREATE TABLE Analysis (
	ID INT IDENTITY(1, 1) NOT NULL
	,IP VARCHAR(24) NULL
	,CountryName VARCHAR(255) NULL
	,CityName VARCHAR(255) NULL
	,ContinentName VARCHAR(255) NULL
	,DownloadedBy INT NULL
	,OwnerId VARCHAR(50) NULL
	,FileId INT NULL
	,[CreatedDate] DATETIME NOT NULL
	,[ModifiedDate] DATETIME NULL
	)


