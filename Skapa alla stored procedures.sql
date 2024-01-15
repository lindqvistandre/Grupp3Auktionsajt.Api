-- Det går även att ångra dvs ta bort ett bud om auktionen inte är avslutad.

Create procedure DeleteBid(@DeleteByID int)
As
Begin
Delete 
From Bids
Where BidId = @DeleteByID
End

--Finns det inga bud på en auktion skall den kunna tas bort. Det skall även gå att
Create procedure DeleteAuction(@DeleteByID int)
As
Begin
Delete 
From Auctions
Where AuctionID = @DeleteByID
End

