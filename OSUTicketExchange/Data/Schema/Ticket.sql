USE [TicketExchange]
GO

/****** Object:  Table [dbo].[Ticket]    Script Date: 10/3/2014 10:16:25 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Ticket](
	[TicketID] [int] IDENTITY(1,1)NOT NULL,
	[SeatID] [int] NOT NULL,
	[GameID] [int] NOT NULL,
	[UserID] [int] NOT NULL,
	[Sold] [bit] NOT NULL,
 CONSTRAINT [PK_Ticket] PRIMARY KEY CLUSTERED 
(
	[TicketID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


