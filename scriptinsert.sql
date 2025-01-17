USE [InsertUserPassword]
GO
/****** Object:  Table [dbo].[GetUsersAvls]    Script Date: 4/29/2021 10:39:23 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GetUsersAvls](
	[Aplicacion] [nvarchar](50) NOT NULL,
	[clientid] [nvarchar](50) NULL,
	[userlogin] [nvarchar](50) NULL,
	[userpassword] [nvarchar](50) NULL,
	[IdAplicacion] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_GetUsersAvls] PRIMARY KEY CLUSTERED 
(
	[IdAplicacion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[GetUsersAvls] ON 

INSERT [dbo].[GetUsersAvls] ([Aplicacion], [clientid], [userlogin], [userpassword], [IdAplicacion]) VALUES (N'startfleet', N'cliente1', N'disern000', N'Mobiletec@SUME', 6)
INSERT [dbo].[GetUsersAvls] ([Aplicacion], [clientid], [userlogin], [userpassword], [IdAplicacion]) VALUES (N'fleet', N'20843', N'Administrator', N'Syracuse%2344', 8)
SET IDENTITY_INSERT [dbo].[GetUsersAvls] OFF
/****** Object:  StoredProcedure [dbo].[USP_GetUserChenengo]    Script Date: 4/29/2021 10:39:23 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[USP_GetUserChenengo]
	
	@Aplicacion nvarchar(50)

AS
BEGIN

	SET NOCOUNT ON;

    
	SELECT [clientid],[userlogin],[userpassword]
	from [dbo].[GetUsersAvls]
	where[Aplicacion]=@Aplicacion
	  

END
GO
/****** Object:  StoredProcedure [dbo].[USP_GetUserID]    Script Date: 4/29/2021 10:39:23 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[USP_GetUserID]
	
	@Aplicacion nvarchar(50)
AS
BEGIN

	SET NOCOUNT ON;

    
	SELECT [Parametro],[Valor]
	from [dbo].[GetUsersAvls]
	where[Aplicacion]=@Aplicacion
	  

END
GO
