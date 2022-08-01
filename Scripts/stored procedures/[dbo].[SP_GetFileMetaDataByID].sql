IF OBJECT_ID('[SP_GetFileMetaDataByID]') IS NULL
	EXEC ('CREATE PROCEDURE [SP_GetFileMetaDataByID] AS SET NOCOUNT ON;')
GO

ALTER procedure [dbo].[SP_GetFileMetaDataByID] @UserId int,@FileId int
AS  
BEGIN  
  
SELECT fmd.[FileId]  
      ,fmd.[FileName]  
      ,fmd.[Ext]  
      ,fmd.[CreatedBy]  
      ,fmd.[FileSize]  
      ,fmd.[CreatedDate]  
      ,fmd.[ModifiedDate]  
  FROM [dbo].[FileMetaData] fmd  
  where [UserId] = @UserId  
  AND [Id] = @FileId
  
END 