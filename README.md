This Repository is only for GPTW, intention is to get the assignment assess.

Github Repo

https://github.com/saunvid/GPTW.git

Postman collection

https://www.postman.com/saunvid/workspace/saunvid-ws/collection/26826848-12768fde-5ba8-4836-a363-78963f6cf722?action=share&creator=26826848

Script file which includes table, store procedure, is also push to repository for reference GPTW.sql

Task 1:

Made one table with the name “rawdata” with the following columns: ID (Primary key) – Auto increment QuestionID – Number(int) QuestionText – String Score – Number(float)

Task 2:

Made one post API named “/submitdata”, data passed as mentioned { “questionText”: “question 1 text”, “questionID”: 323, “score”: 5 } created 10 entries, beow is the data table.
ID QuestionID QuestionText Score

1 323 question 1 text 4 2 323 question 1 text 5 3 324 question 2 text 4 4 324 question 2 text 5 5 325 question 3 text 3 6 325 question 3 text 5 7 326 question 4 text 1 8 326 question 4 text 2 9 327 question 5 text 3 13 327 question 5 text 4

Task 3:

Made GET API named “/getdata”,called from Postman. The response of this API can look like below: { “positive_score”: X } Where X = ((count of 4s + count of 5s)/(count of 1s + count of 2s + count of 3s + count of 4s + count of 5s))*100. This will be for all question IDs considered together.

              select 
              Score = Case when Score IN (4,5)
              Then SUM(Score)
              ELSE 0
              END,
              TOTAL_SCORE=SUM(Score)
              into #temp
              from rawdata
              GROUP BY Score
              select (cast(sum(Score) as float)/cast(sum(TOTAL_SCORE) as float)) * 100 as positive_score from #temp
              Drop table #temp

**Task 4: **

Made another POST API named “/getdata” that you can call from Postman. The request body can look like below: { “questionIDs”: [323, 324, 325] } The response of this API can look like below: { data: [{questionId: 323, score: X},{questionId: 324, score: X},{questionId: 325, score: X}] } Where X = ((count of 4s + count of 5s)/(count of 1s + count of 2s + count of 3s + count of 4s + count of 5s))*100 for that respective question ID.

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
        from #temp1
        where QuestionID IN (SELECT value FROM STRING_SPLIT(@QuestionID, ','))
        group by QuestionID
        Drop table #temp1

My Response

{ "Message": "Data fetched successfully.", "Success": true, "data": [ { "QuestionID": 323, "QuestionText": null, "Score": 25 }, { "QuestionID": 324, "QuestionText": null, "Score": 25 }, { "QuestionID": 325, "QuestionText": null, "Score": 13.888888888888889 } ], "positive_score": null }
