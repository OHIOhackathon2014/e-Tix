USE [TicketExchange]
GO

/****** Object:  Table [dbo].[TicketBids]    Script Date: 10/3/2014 10:16:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[TicketBids](
	[TicketID] [int] IDENTITY(1,1) NOT NULL,
	[Price] [int] NOT NULL,
	[UserID] [int] NOT NULL
) ON [PRIMARY]

GO


