USE [master]
GO
CREATE DATABASE agendaweb;
GO

CREATE TABLE [dbo].[contatos] (
  [ID] [int] IDENTITY (1, 1) NOT NULL,
  [CodUsuario] [int] NOT NULL,
  [nome] [varchar] (100) NOT NULL,
  [telefone] [varchar] (15) NOT NULL,
  [excluido] [bit] NULL,
) ON [PRIMARY];
GO

SELECT [ID], [nome], [email], [senha]
  FROM [dbo].[usuarios]
GO

USE [master]
GO
CREATE LOGIN [usuario] WITH PASSWORD=N'123', DEFAULT_DATABASE=[master], CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF
GO
ALTER SERVER ROLE [bulkadmin] ADD MEMBER [usuario]
GO
ALTER SERVER ROLE [dbcreator] ADD MEMBER [usuario]
GO
ALTER SERVER ROLE [diskadmin] ADD MEMBER [usuario]
GO
ALTER SERVER ROLE [processadmin] ADD MEMBER [usuario]
GO
ALTER SERVER ROLE [securityadmin] ADD MEMBER [usuario]
GO
ALTER SERVER ROLE [serveradmin] ADD MEMBER [usuario]
GO
ALTER SERVER ROLE [setupadmin] ADD MEMBER [usuario]
GO
ALTER SERVER ROLE [sysadmin] ADD MEMBER [usuario]
GO
USE [agendaweb]
GO
CREATE USER [usuario] FOR LOGIN [usuario]
GO