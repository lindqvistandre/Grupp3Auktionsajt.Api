-- Det g�r �ven att �ngra dvs ta bort ett bud om auktionen inte �r avslutad.

Create procedure sp_DeleteBid(@DeleteByID int)
As
Begin
Delete 
From Bids
Where BidId = @DeleteByID
End;

--Finns det inga bud p� en auktion skall den kunna tas bort. Det skall �ven g� att
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


-- stored procedure f�r lista bid -- 
CREATE PROCEDURE sp_GetBidsForAuction
    @AuctionId INT
AS
BEGIN
    SELECT b.BidId, b.UserId, b.BidPrice, b.BidTimeStamp 
    FROM Bids b 
    WHERE b.AuctionId = @AuctionId
    ORDER BY b.BidPrice DESC;
END;

-- stored procedure f�r create bid --

CREATE PROCEDURE CreateBid
    @AuctionId INT,
    @UserId INT,
    @BidPrice DECIMAL(18, 2)
AS
BEGIN
    INSERT INTO Bids (AuctionId, UserId, BidPrice)
    VALUES (@AuctionId, @UserId, @BidPrice);
END;    


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

-- F�reg�ende kod om det finns...
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

-- Eventuell efterf�ljande kod...

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