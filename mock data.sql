INSERT INTO Users (UserId, Username, Password)
VALUES 
('1', 'user1', 'psw1'),
('2', 'user2', 'psw2'),
('3', 'user3', 'psw3'),
('4', 'user4', 'psw4'),
('5', 'user5', 'psw5'),
('6', 'user6', 'psw6'),
('7', 'user7', 'psw7'),
('8', 'user8', 'psw8'),
('9', 'user9', 'psw9');


INSERT INTO Auctions(AuctionId, Title, Description, Price, StartDate, EndDate, CreateUserId)
VALUES 
('1', 'BMW 1', '240hk', '100', '2023-01-12 15:15:50','2023-01-25 15:15:50', '1'),
('2', 'Opel 1', '200hk', '150', '2023-01-12 15:15:50','2023-02-15 15:15:50', '1'),
('3', 'Opel 2', '210hk', '150', '2023-01-12 15:15:50','2023-02-16 15:15:50', '2'),
('4', 'Byrå', 'Tung ek byrå', '50', '2023-01-12 15:15:50','2023-02-16 15:15:50', '3');

INSERT INTO Bids(BidId, AuctionId, UserId, BidPrice, BidTimeStamp)
VALUES
-- Auctioner för auction 1 som har startpris 100--
('1', '1', '1', '110', '2023-01-12 15:16:50'),
('2', '1', '2', '120', '2023-01-13 15:16:50'),
('3', '1', '3', '130', '2023-01-14 15:16:50'),
('4', '1', '1', '140', '2023-01-15 15:16:50'),
-- Auctioner för auction 2 som har startpris 150--
('1', '2', '4', '160', '2023-01-12 15:16:50'),
('2', '2', '5', '170', '2023-01-13 15:16:50'),
('3', '2', '6', '180', '2023-01-14 15:16:50'),
('4', '2', '7', '190', '2023-01-15 15:16:50');
