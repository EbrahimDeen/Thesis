USE [IAM]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetFileMetaData]    Script Date: 8/1/2022 4:19:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE OR ALTER procedure [dbo].[SP_GetFileMetaData] @UserId int
AS
BEGIN

SELECT fmd.[FileId]
      ,fmd.[FileName]
      ,fmd.[Ext]
      ,fmd.[CreatedBy]
      ,fmd.[FileSize]
      ,fmd.[CreatedDate]
      ,fmd.[ModifiedDate]
	  ,fmd.UserId as OwnerId
  FROM [dbo].[FileMetaData] fmd
  left join [dbo].[Attachments] f on f.[ID] = fmd.[FileID]
  where fmd.UserId = @UserId

END