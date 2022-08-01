USE [IAM]
GO

/****** Object:  StoredProcedure [test].[SP_GetPublicFilesMeta]    Script Date: 8/1/2022 4:16:52 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE OR ALTER procedure [test].[SP_GetPublicFilesMeta]
AS
BEGIN
	select fmd.FileId, 
	fmd.FileName, 
	fmd.Ext, 
	fmd.CreatedBy, 
	fmd.FileSize, 
	fmd.CreatedDate, 
	fmd.ModifiedDate, 
	fmd.UserId AS [OwnerId] 
	from [dbo].[FileMetaData] fmd
	  left join [dbo].[Attachments] atch on fmd.[FileId] = atch.ID
	where atch.[isPublic] = 1
END

GO


