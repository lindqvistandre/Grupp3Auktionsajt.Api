------------------------------- Users table
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

--Create User 
CREATE PROCEDURE sp_CreateUser
    @UserName NVARCHAR(255),
    @Password NVARCHAR(255)
AS
BEGIN
    -- Skapa en ny användare i Users-tabellen
    INSERT INTO Users (UserName, Password)
    VALUES (@UserName, @Password);
END

-- Update för User 
CREATE PROCEDURE sp_UpdateUser
    @UserID INT,
    @UserName NVARCHAR(255),
    @Password NVARCHAR(255)
    -- Lägg till andra parametrar om det behövs
AS
BEGIN
    -- Uppdatera användarinformation i Users-tabellen baserat på användar-ID

    UPDATE Users
    SET
        Password = @Password
        -- Uppdatera andra kolumner om det behövs
    WHERE
        UserID = @UserID;

    -- Du kan lägga till fler uppdateringar här om det behövs för andra kolumner.

END

-- Föregående kod om det finns...
GO

CREATE PROCEDURE UserLogin
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

Create procedure sp_DeleteBid(@DeleteByID int)
As
Begin
Delete 
From Bids
Where BidId = @DeleteByID
End;


-- stored procedure för lista bid -- 
CREATE PROCEDURE sp_GetBidsForAuction
    @AuctionId INT
AS
BEGIN
    SELECT b.BidId, b.UserId, b.BidPrice, b.BidTimeStamp 
    FROM Bids b 
    WHERE b.AuctionId = @AuctionId
    ORDER BY b.BidPrice DESC;
END;


-- stored procedure för create bid --

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


--Finns det inga bud på en auktion skall den kunna tas bort. Det skall även gå att
Create procedure sp_DeleteAuction(@DeleteByID int)
As
Begin
Delete 
From Auctions
Where AuctionID = @DeleteByID
End;

-- Create an auction,
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

-- Eventuell efterföljande kod...

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

-- Search auctions

create procedure sp_SearchAuctions
	@SearchTerm nvarchar (100)
as
begin
	set @SearchTerm = '%'+ @SearchTerm +'%'

	select AuctionId, Title
	from Auctions
	where Title like @SearchTerm
end;






