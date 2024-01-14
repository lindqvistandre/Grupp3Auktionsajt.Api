create table Users (
	UserId int primary key identity(1,1),
	Username nvarchar(50) not null unique,
	Password nvarchar(50) not null
);
go

create table Auctions (
	AuctionId int primary key identity(1,1),
	Title nvarchar(100) not null,
	Description nvarchar(max),
	Price decimal(18,2) not null,
	StartDate datetime not null,
	EndDate datetime not null,
	CreatorUserId int foreign key references Users(UserId)
);
go

create table Bids (
	BidId int primary key identity(1,1),
	AuctionId int foreign key references Auctions(AuctionId),
	UserId int foreign key references Users(UserId),
	BidPrice decimal(18,2) not null,
	BidTimeStamp datetime not null default getdate() -- Later when creating stored procedures, you won't need to specify the BidTimeStamp since it will now on default be "getdate()"
);
go