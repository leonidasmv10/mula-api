SELECT 
    UserId,
    AVG(CorrectAnswers) AS AverageCorrectAnswers
FROM 
    GameResults
WHERE 
    CategoryId = 19
GROUP BY 
    UserId
ORDER BY 
    AverageCorrectAnswers DESC;

