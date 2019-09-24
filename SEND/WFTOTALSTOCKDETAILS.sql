USE [A2ZACWMS]
GO

/****** Object:  Table [dbo].[WFTOTALSTOCKDETAILS]    Script Date: 2019-03-18 4:56:39 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[WFTOTALSTOCKDETAILS](
	[LNSRL_1] [int] NULL,
	[SORT_SRL2] [int] NULL,
	[SORT_SL3] [int] NULL,
	[Head1] [nvarchar](100) NULL,
	[Head2] [nvarchar](100) NULL,
	[Details1] [nvarchar](100) NULL,
	[DetailsAmt1] [money] NULL,
	[HeadAmt1] [money] NULL,
	[Head2Amt1] [money] NULL,
	[Head3] [nvarchar](100) NULL,
	[Head4] [nvarchar](100) NULL,
	[Details2] [nvarchar](100) NULL,
	[DetailsAmt2] [money] NULL,
	[HeadAmt2] [money] NULL,
	[Head2Amt2] [money] NULL,
	[SL] [int] NULL,
	[LineSL] [int] NULL
) ON [PRIMARY]

GO

