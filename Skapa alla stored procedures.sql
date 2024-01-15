-- Det går även att ångra dvs ta bort ett bud om auktionen inte är avslutad.

Create procedure DeleteBid(@DeleteByID int)
As
Begin
Delete 
From Bids
Where BidId = @DeleteByID
End;

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
	declare @EndDate datetime;
	set @EndDate = getdate() + @Days;

	insert into Auctions (Title, Description, Price, StartDate, EndDate, CreatorUserId)
	values (@Title, @Description, @Price, getdate(), @EndDate, @CreatorUserId);
end;


-- stored procedure för bid -- 
CREATE PROCEDURE sp_CreateBid
    @AuctionId INT,
    @UserId INT,
    @BidPrice DECIMAL(18, 2)
AS
BEGIN
    -- Kontrollera om auktionen är öppen med en BOOL
    DECLARE @IsOpen BIT;
    SELECT @IsOpen = IsOpen FROM Auctions WHERE AuctionId = @AuctionId;

    IF @IsOpen = 0
    BEGIN
        RAISERROR ('Auktionen är inte öppen för bud.', 16, 1);
        RETURN;
    END

    -- Kontrollerar att användaren inte är skaparen av auktionen
    DECLARE @CreatorId INT;
    SELECT @CreatorId = UserId FROM Auctions WHERE AuctionId = @AuctionId;

    IF @UserId = @CreatorId
    BEGIN
        RAISERROR ('Du kan inte lägga bud på din egen auktion.', 16, 1);
        RETURN;
    END

    -- Kontrollera att budet är högre än det nuvarande högsta budet
    DECLARE @HighestBid DECIMAL(18, 2);
    SELECT @HighestBid = ISNULL(MAX(BidPrice), 0) FROM Bids WHERE AuctionId = @AuctionId;

    IF @BidPrice <= @HighestBid
    BEGIN
        RAISERROR ('Budet är för lågt.', 16, 1);
        RETURN;
    END

    -- Skapar budet
    INSERT INTO Bids (AuctionId, UserId, BidPrice)
    VALUES (@AuctionId, @UserId, @BidPrice);
END;

-- Create an auction,
create procedure sp_CreateAuction
	@Title nvarchar(100),
	@Description nvarchar(max),
	@Price decimal(18,2),
	@Days int,
	@CreatorUserId int
as
begin
	declare @EndDate datetime;
	set @EndDate = getdate() + @Days;

	insert into Auctions (Title, Description, Price, StartDate, EndDate, CreatorUserId)
	values (@Title, @Description, @Price, getdate(), @EndDate, @CreatorUserId);
end;

-- visar bids om pågående auction, alternativ 

CREATE PROCEDURE sp_GetBidsForAuction
    @AuctionId INT
AS
BEGIN
-- Kontrollera om auktionen är öppen eller stängd
DECLARE @IsOpen BIT;
SELECT @IsOpen = IsOpen FROM Auctions WHERE AuctionId = @AuctionId;
IF @IsOpen = 1
BEGIN
    -- Om auktionen är öppen, visa alla bud
    SELECT b.BidId, b.UserId, b.BidPrice, b.BidTimeStamp 
    FROM Bids b 
    WHERE b.AuctionId = @AuctionId
    ORDER BY b.BidPrice DESC;
END
ELSE
BEGIN
    -- Om auktionen är stängd, visa endast det högsta budet
    SELECT TOP 1 b.BidId, b.UserId, b.BidPrice, b.BidTimeStamp 
    FROM Bids b 
    WHERE b.AuctionId = @AuctionId
    ORDER BY b.BidPrice DESC;
END
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