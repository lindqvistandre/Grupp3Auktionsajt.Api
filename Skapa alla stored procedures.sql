------------------------------- Users table         -- All correct for this table, Kevin
CREATE PROCEDURE sp_UserLogin
    @Username NVARCHAR(20),
    @Password NVARCHAR(20)
AS
BEGIN
    SELECT UserId
    FROM Users
    WHERE Username = @Username AND Password = @Password
END
GO

--Create User 
CREATE PROCEDURE sp_CreateUser
    @UserName NVARCHAR(20),
    @Password NVARCHAR(20)
AS
BEGIN
    -- Skapa en ny användare i Users-tabellen
    INSERT INTO Users (UserName, Password)
    VALUES (@UserName, @Password);
END

-- Update för User 
CREATE PROCEDURE sp_UpdateUser
    @UserID INT,
    @UserName NVARCHAR(20),
    @Password NVARCHAR(20)
AS
BEGIN
    -- Uppdatera användarinformation i Users-tabellen baserat på användar-ID
    UPDATE Users
    SET
        Username = isnull(@Username, Username),        -- Added in the style of "isnull(@Parameter, Parameter). This means that if the in-parameter is null, then the column in the database will not change.
        Password = isnull(@Password, [Password])       -- This in other words allows you to choose beetween updating one or both parameters using only 1 stored procedure, Kevin.
    WHERE
        UserID = @UserID;
END

-- Föregående kod om det finns...
GO

CREATE PROCEDURE sp_UserLogin
    @Username NVARCHAR(50),
    @Password NVARCHAR(50)
AS
BEGIN
    SELECT UserId
    FROM Users
    WHERE Username = @Username AND Password = @Password
END
GO



--------------------------------Bid table

-- Det går även att ångra dvs ta bort ett bud om auktionen inte är avslutad.

Create procedure sp_DeleteBid(@DeleteByID int)      -- Correct, Kevin
As
Begin
Delete 
From Bids
Where BidId = @DeleteByID
End;


-- stored procedure för lista bid -- 
CREATE PROCEDURE sp_GetBidsForAuction       -- Probably correct, but this will work, Kevin
    @AuctionId INT
AS
BEGIN
    SELECT b.BidId, b.UserId, b.BidPrice, b.BidTimeStamp 
    FROM Bids b 
    WHERE b.AuctionId = @AuctionId
    ORDER BY b.BidPrice DESC;
END;


-- stored procedure för create bid --               -- Correct, Kevin

CREATE PROCEDURE CreateBid
    @AuctionId INT,
    @UserId INT,
    @BidPrice DECIMAL(18, 2)
AS
BEGIN
    INSERT INTO Bids (AuctionId, UserId, BidPrice)
    VALUES (@AuctionId, @UserId, @BidPrice);
END;    


-------------------------------Auction table

-- Create an auction            -- Correct, Kevin
create procedure sp_CreateAuction
	@Title nvarchar(100),
	@Description nvarchar(max),
	@Price decimal(18,2),
	@Days int,
	@CreatorUserId int
as
begin
	declare @StartDate datetime = getdate();
	declare @EndDate datetime = @StartDate + @Days;

	insert into Auctions (Title, Description, Price, StartDate, EndDate, CreatorUserId)
	values (@Title, @Description, @Price, @StartDate, @EndDate, @CreatorUserId);
end;



Create procedure sp_DeleteAuction(@DeleteByID int)
As
Begin
Delete 
From Auctions
Where AuctionID = @DeleteByID
End;




-- Eventuell efterföljande kod...      -- Correct, Kevin

CREATE PROCEDURE UpdateAuctionPrice
    @AuctionId INT,
    @NewPrice DECIMAL(18,2)
AS
BEGIN
    UPDATE Auctions
    SET Price = @NewPrice
    WHERE AuctionId = @AuctionId
END
GO

-- Search auctions                 -- Correct, Kevin
create procedure sp_SearchAuctions  
	@SearchTerm nvarchar (100)
as
begin
	set @SearchTerm = '%'+ @SearchTerm +'%'

	select AuctionId, Title
	from Auctions
	where Title like @SearchTerm
end;






