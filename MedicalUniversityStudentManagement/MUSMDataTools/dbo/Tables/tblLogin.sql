﻿CREATE TABLE [dbo].[tblLogin]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[Username] VARCHAR(32) NOT NULL,
	[Password] VARCHAR(256) NOT NULL,
	[UserType] INT NOT NULL

)
