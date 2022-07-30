alter table [dbo].[Attachments]
add isPublic bit not null
CONSTRAINT DefaultValue DEFAULT (0)