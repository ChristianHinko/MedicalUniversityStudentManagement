﻿CREATE TABLE [dbo].[tblAdmin]
(
	[Id] INT NOT NULL FOREIGN KEY REFERENCES tblLogin(Id) ON DELETE CASCADE,
	[FirstName] VARCHAR(64) NOT NULL,
	[LastName] VARCHAR(64) NOT NULL

)