-- Det går även att ångra dvs ta bort ett bud om auktionen inte är avslutad.

Create procedure DeleteBid(@DeleteByID int)
As
Begin
Delete 
From Bids
Where BidId = @DeleteByID
End;

--Finns det inga bud på en auktion skall den kunna tas bort. Det skall även gå att
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

