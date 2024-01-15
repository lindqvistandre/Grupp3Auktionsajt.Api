-- Det g�r �ven att �ngra dvs ta bort ett bud om auktionen inte �r avslutad.

Create procedure DeleteBid(@DeleteByID int)
As
Begin
Delete 
From Bids
Where BidId = @DeleteByID
End;

--Finns det inga bud p� en auktion skall den kunna tas bort. Det skall �ven g� att
Create procedure DeleteAuction(@DeleteByID int)
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


-- stored procedure f�r bid -- 
CREATE PROCEDURE CreateBid
    @AuctionId INT,
    @UserId INT,
    @BidPrice DECIMAL(18, 2)
AS
BEGIN
    -- Kontrollera om auktionen �r �ppen med en BOOL
    DECLARE @IsOpen BIT;
    SELECT @IsOpen = IsOpen FROM Auctions WHERE AuctionId = @AuctionId;

    IF @IsOpen = 0
    BEGIN
        RAISERROR ('Auktionen �r inte �ppen f�r bud.', 16, 1);
        RETURN;
    END

    -- Kontrollerar att anv�ndaren inte �r skaparen av auktionen
    DECLARE @CreatorId INT;
    SELECT @CreatorId = UserId FROM Auctions WHERE AuctionId = @AuctionId;

    IF @UserId = @CreatorId
    BEGIN
        RAISERROR ('Du kan inte l�gga bud p� din egen auktion.', 16, 1);
        RETURN;
    END

    -- Kontrollera att budet �r h�gre �n det nuvarande h�gsta budet
    DECLARE @HighestBid DECIMAL(18, 2);
    SELECT @HighestBid = ISNULL(MAX(BidPrice), 0) FROM Bids WHERE AuctionId = @AuctionId;

    IF @BidPrice <= @HighestBid
    BEGIN
        RAISERROR ('Budet �r f�r l�gt.', 16, 1);
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
