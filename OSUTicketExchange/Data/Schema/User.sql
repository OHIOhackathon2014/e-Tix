USE [TicketExchange]
GO

/****** Object:  Table [dbo].[COPYUSERS]    Script Date: 10/4/2014 3:46:43 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Email] [nvarchar](50) NULL,
	[Good] [int] NOT NULL,
	[Bad] [int] NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_Good]  DEFAULT ((0)) FOR [Good]
GO

ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_Bad]  DEFAULT ((0)) FOR [Bad]
GO


