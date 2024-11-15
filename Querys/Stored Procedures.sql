CREATE PROCEDURE sp_GetUserPersonalStats
    @UserId INT
AS
BEGIN
    SELECT 
        UserId,
        AVG(CorrectAnswers) AS AverageCorrectAnswers
    FROM 
        GameResults
    WHERE 
        UserId = @UserId
    GROUP BY 
        UserId;
END;

CREATE PROCEDURE sp_GetUserCategoryStats
    @UserId INT
AS
BEGIN
    SELECT 
        CategoryId,
        AVG(CorrectAnswers) AS AverageCorrectAnswers
    FROM 
        GameResults
    WHERE 
        UserId = @UserId
    GROUP BY 
        CategoryId;
END;

CREATE PROCEDURE sp_GetGlobalRanking
AS
BEGIN
    SELECT 
        UserId,
        AVG(CorrectAnswers) AS AverageCorrectAnswers
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
        AVG(CorrectAnswers) AS AverageCorrectAnswers
    FROM 
        GameResults
    WHERE 
        CategoryId = @CategoryId
    GROUP BY 
        UserId
    ORDER BY 
        AverageCorrectAnswers DESC;
END;
