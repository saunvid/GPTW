USE [Test]
GO
/****** Object:  Table [dbo].[rawdata]    Script Date: 11-04-2023 19:59:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[rawdata](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[QuestionID] [int] NULL,
	[QuestionText] [nvarchar](max) NULL,
	[Score] [int] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[USP_ADD_QUESTION]    Script Date: 11-04-2023 19:59:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
	-- =============================================
	-- Author:		<Author,,Name>
	-- Create date: <Create Date,,>
	-- Description:	<Description,,>
	-- =============================================
	CREATE PROCEDURE [dbo].[USP_ADD_QUESTION] (
		@sss NVARCHAR(MAX) = NULL,  
		@QuestionID NVARCHAR(MAX) = NULL,   
		@QuestionText NVARCHAR(MAX) = Null,
		@Score int = Null,
		@TYPE NVARCHAR(20) = NULL)

		

	AS
	BEGIN
	SET NOCOUNT ON;
	IF @TYPE = 'ADD_QUESTION'
			BEGIN
				INSERT INTO rawdata
							(QuestionID,
							 QuestionText,
							 Score)
				VALUES     ( @QuestionID,
							 @QuestionText,
							 @Score)

				SELECT * from rawdata;
			END
			
-----------------------------------------------------------------

			IF @TYPE = 'Get_All_Score'
			BEGIN
				select 
				Score = Case when Score IN (4,5)
				Then SUM(Score)
			ELSE 0
			END,
	  TOTAL_SCORE=SUM(Score)
	  into #temp
	  from rawdata
	  GROUP BY Score
	  --27/36
	  select (cast(sum(Score) as float)/cast(sum(TOTAL_SCORE) as float)) * 100 as positive_score from #temp

	  Drop table #temp
	   END
	   --EXEC USP_ADD_QUESTION @Type="Get_All_Score"

------------------------------------------------------------------------------------------------

	   IF @TYPE = 'Get_Particular_Score'
		BEGIN
	select QuestionID,
	  Score = Case when Score IN (4,5)
	  Then SUM(Score)
	  ELSE 0
	  END,
	  TOTAL_SCORE=SUM(Score)
	  into #temp1
	  from rawdata
	  GROUP BY Score,QuestionID
	  select QuestionID,
	  cast(sum(Score) as float) / (select sum(TOTAL_SCORE) from #temp1 )*100 as Score
	  --case when QuestionID=323 then cast(sum(score) as float) / (select sum(TOTAL_SCORE) from #temp1 )*100
		 --  when QuestionID=324 then cast(sum(score) as float) / (select sum(TOTAL_SCORE) from #temp1 )*100
		 --  when QuestionID=325 then cast(sum(score) as float) / (select sum(TOTAL_SCORE) from #temp1 )*100
		 --  else 0 end as Score
  
	  from #temp1
	  --where QuestionID IN (323, 324)
	  --where QuestionID IN (select * from split_string(@QuestionID, ','))
	  where QuestionID IN (SELECT value FROM STRING_SPLIT(@QuestionID, ','))
	 group by QuestionID

Drop table #temp1
	  --select * from split_string(@QuestionID, ',')
--EXEC USP_ADD_QUESTION @Type="Get_Particular_Score"

END
END
GO
