USE [IAM]
GO

/****** Object:  StoredProcedure [dbo].[SP_GetFileMetaData]    Script Date: 3/6/2022 9:04:14 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

create procedure [test].[SP_GetFileMetaData] @UserId int
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
  left join [dbo].[FILE] f on f.[File_ID] = fmd.[FileID]
  where f.[User_ID] = @UserId 

END
GO


