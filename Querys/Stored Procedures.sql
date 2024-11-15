CREATE PROCEDURE sp_GetUserPersonalStats
    @UserId INT
AS
BEGIN
    SELECT 
        UserId,
        COUNT(*) AS TotalGamesPlayed,
        CAST(AVG(CorrectAnswers * 1.0) AS FLOAT) AS AverageCorrectAnswers,
        CAST((SUM(CorrectAnswers) * 1.0 / (COUNT(*) * 10)) * 100 AS FLOAT) AS SuccessRate
    FROM 
        GameResults
    WHERE 
        UserId = @UserId
    GROUP BY 
        UserId;
END;



EXEC sp_GetUserPersonalStats @UserId = 1

CREATE PROCEDURE sp_GetUserCategoryStats
    @UserId INT
AS
BEGIN
    SELECT 
        CategoryId,
        CAST(AVG(CorrectAnswers * 1.0) AS FLOAT) AS AverageCorrectAnswers
    FROM 
        GameResults
    WHERE 
        UserId = @UserId
    GROUP BY 
        CategoryId;
END;

EXEC sp_GetUserCategoryStats @UserId = 1

CREATE PROCEDURE sp_GetGlobalRanking
AS
BEGIN
    SELECT 
        UserId,
        CAST(AVG(CorrectAnswers * 1.0) AS FLOAT) AS AverageCorrectAnswers
    FROM 
        GameResults
    GROUP BY 
        UserId
    ORDER BY 
        AverageCorrectAnswers DESC;
END;


CREATE PROCEDURE sp_GetCategoryRanking
    @CategoryId INT
AS
BEGIN
    SELECT 
        UserId,
        CAST(AVG(CorrectAnswers * 1.0) AS FLOAT) AS AverageCorrectAnswers
    FROM 
        GameResults
    WHERE 
        CategoryId = @CategoryId
    GROUP BY 
        UserId
    ORDER BY 
        AverageCorrectAnswers DESC;
END;

