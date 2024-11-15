SELECT 
    UserId,
    AVG(CorrectAnswers) AS AverageCorrectAnswers
FROM 
    GameResults
WHERE 
    UserId = 1
GROUP BY 
    UserId;


SELECT 
    CategoryId,
    AVG(CorrectAnswers) AS AverageCorrectAnswers
FROM 
    GameResults
WHERE 
    UserId = 1
GROUP BY 
    CategoryId;