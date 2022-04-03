CREATE procedure test.SP_AddFile
@Data varbinary(max),
@Name varchar(max),
@Ext varchar(10),
@UserId int
AS
Begin

	BEGIN TRY
		BEGIN TRAN
			Insert into dbo.Attachments([File], Name, Ext) 
			Values(@Data, @Name, @Ext);
	
			DECLARE @FileID int = @@IDENTITY
	
			INSERT INTO [dbo].[FileMetaData]
				   ([FileId]
				   ,[FileName]
				   ,[Ext]
				   ,[CreatedBy]
				   ,[FileSize]
				   ,[CreatedDate]
				   ,[ModifiedDate])
			 VALUES
				   (@FileID
				   ,@Name
				   ,@Ext
				   ,@UserId
				   ,0
				   ,GETDATE()
				   ,GETDATE()
				   )


			select @FileID;
		COMMIT;
	END TRY
	BEGIN CATCH
	 ROLLBACK;
	 SELECT  
            ERROR_NUMBER() AS ErrorNumber  
            ,ERROR_SEVERITY() AS ErrorSeverity  
            ,ERROR_STATE() AS ErrorState  
            ,ERROR_PROCEDURE() AS ErrorProcedure  
            ,ERROR_LINE() AS ErrorLine  
            ,ERROR_MESSAGE() AS ErrorMessage;
	END CATCH
End