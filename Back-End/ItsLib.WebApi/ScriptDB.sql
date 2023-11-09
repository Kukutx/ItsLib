INSERT INTO dbo.AspNetRoles (
	   [Id]
      ,[Name]
      ,[NormalizedName]
      ,[ConcurrencyStamp]
)
VALUES 
(NEWID(), 'Admin', 'ADMIN', null),
(NEWID(), 'User', 'USER', null)

INSERT INTO dbo.AspNetUsers(
	   [Id]
      ,[Discriminator]
      ,[Name]
      ,[Surname]
      ,[DateOfBirth]
      ,[FiscalCode]
      ,[LoyaltyCardCode]
      ,[IsDisabled]
      ,[RefreshToken]
      ,[RefreshTokenExpiryTime]
      ,[UserName]
      ,[NormalizedUserName]
      ,[Email]
      ,[NormalizedEmail]
      ,[EmailConfirmed]
      ,[PasswordHash]
      ,[SecurityStamp]
      ,[ConcurrencyStamp]
      ,[PhoneNumber]
      ,[PhoneNumberConfirmed]
      ,[TwoFactorEnabled]
      ,[LockoutEnd]
      ,[LockoutEnabled]
      ,[AccessFailedCount]
)
VALUES
(NEWID(), 'User', 'Master', 'Master', CURRENT_TIMESTAMP, 'Master', '25704088326769', 0, null, CURRENT_TIMESTAMP, 'master@master.com', 'MASTER@MASTER.COM', 'master@master.com', 'MASTER@MASTER.COM', 0, 'AQAAAAIAAYagAAAAEKNzaJA38ilt7+MdzEI1GTG6DI+2THK1nnRVW/RoTfS3wPF8uY/0DdNudiBceKGqfQ==', 'ROQ2E27WUJ5VZVL47QIEFV7A5PPVAFQD', NEWID(), null, 0, 0, null, 1, 0),
(NEWID(), 'User', 'Utente', 'Utente', CURRENT_TIMESTAMP, 'Utente', '25704088326777', 0, null, CURRENT_TIMESTAMP, 'utente@utente.com', 'UTENTE@UTENTE.COM', 'utente@utente.com', 'UTENTE@UTENTE.COM', 0, 'AQAAAAIAAYagAAAAECG0OPZMI/TsUrdQ7KI8YoeyVL0mfAM4BbGh2UyZ1j9ApdlrZuW5M6UM9COEpzq+nQ==', 'HMAMZ4SCTOH3JJ6EQ523FESX5JZ72PMK', NEWID(), null, 0, 0, null, 1, 0)

INSERT INTO dbo.AspNetUserRoles(
       [UserId]
      ,[RoleId]
)
VALUES
((SELECT Id FROM dbo.AspNetUsers WHERE UserName = 'master@master.com'), (SELECT Id FROM dbo.AspNetRoles WHERE Name = 'Admin')),
((SELECT Id FROM dbo.AspNetUsers WHERE UserName = 'utente@utente.com'), (SELECT Id FROM dbo.AspNetRoles WHERE Name = 'User'))


INSERT INTO  dbo.Category(
	   [CategoryId]
      ,[Name]
      ,[IsDisabled]
      ,[Color]
      ,[Icon]
      ,[Img]
)
Values
(NEWID(), 'Libri', 0, 'Green','menu_book', 'https://assets.entrepreneur.com/content/3x2/2000/publish-a-book-and-prosper-as-a-small-biz-owner.jpg'),
(NEWID(), 'Giochi', 0, 'Blue', 'stadia_controller', 'https://th.bing.com/th/id/OIP._Qat1vbX-JaPXyPj-yrlMQAAAA?pid=ImgDet&rs=1'),
(NEWID(), 'Casa', 0, 'Yellow', 'cottage', 'https://th.bing.com/th/id/OIP._yC174sEBWELImhmt0sS8gHaE2?w=253&h=180&c=7&r=0&o=5&dpr=1.3&pid=1.7'),
(NEWID(), 'Auto', 0, 'Black', 'directions_car', 'https://th.bing.com/th/id/OIP.1Gacl72ZzuAThVIjs-om_AHaE8?w=262&h=180&c=7&r=0&o=5&dpr=1.3&pid=1.7')


INSERT INTO dbo.Product([ProductId]
      ,[CategoryId]
      ,[Name]
      ,[IntroductoryPrice]
      ,[DateAdded]
      ,[AdditionalData]
      ,[IsDisabled])
VALUES
(NEWID(), (SELECT CategoryId FROM dbo.Category WHERE Name = 'Libri'), 'Harry Potter', 12.1, CURRENT_TIMESTAMP, 'img', 0),
(NEWID(), (SELECT CategoryId FROM dbo.Category WHERE Name = 'Libri'), 'Il Piccolo Principe', 16.1, CURRENT_TIMESTAMP, 'img', 0),
(NEWID(), (SELECT CategoryId FROM dbo.Category WHERE Name = 'Giochi'), 'Minecraft', 18.1, CURRENT_TIMESTAMP, 'img', 0),
(NEWID(), (SELECT CategoryId FROM dbo.Category WHERE Name = 'Giochi'), 'GTA6', 59.1, CURRENT_TIMESTAMP, 'img', 0),
(NEWID(), (SELECT CategoryId FROM dbo.Category WHERE Name = 'Casa'), 'Stendino', 9.99, CURRENT_TIMESTAMP, 'img', 0),
(NEWID(), (SELECT CategoryId FROM dbo.Category WHERE Name = 'Casa'), 'Presine', 9.99, CURRENT_TIMESTAMP, 'img', 1)





INSERT INTO dbo.ProductUser([UserId]
      ,[ProductId]
      ,[InWishList]
      ,[IsUsed]
      ,[IsDisabled]
      ,[Review]
      ,[ReviewTitle]
      ,[DateAdded]
      ,[LastModifiedDate])
values
((SELECT Id from dbo.AspNetUsers WHERE UserName = 'master@master.com'), (SELECT ProductId from dbo.Product WHERE Name = 'Stendino'), 0, 0, 0, null, null, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP ),
((SELECT Id from dbo.AspNetUsers WHERE UserName = 'master@master.com'), (SELECT ProductId from dbo.Product WHERE Name = 'GTA6'), 0, 0, 0, null, null, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP ),
((SELECT Id from dbo.AspNetUsers WHERE UserName = 'master@master.com'), (SELECT ProductId from dbo.Product WHERE Name = 'Harry Potter'), 0, 0, 0, null, null, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP ),
((SELECT Id from dbo.AspNetUsers WHERE UserName = 'utente@utente.com'), (SELECT ProductId from dbo.Product WHERE Name = 'GTA6'), 0, 0, 0, null, null, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP ),
((SELECT Id from dbo.AspNetUsers WHERE UserName = 'utente@utente.com'), (SELECT ProductId from dbo.Product WHERE Name = 'Harry Potter'), 0, 0, 0, null, null, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP )





INSERT INTO dbo.DiscountCode(
       [DiscountCodeId]
      ,[Code]
      ,[Discount]
      ,[IsDisabled]
)
VALUES
(NEWID(), 'AcEfssdW', 15, 0),
(NEWID(), 'DaswPDr', 18, 0),
(NEWID(), 'PDCSertE', 20, 0),
(NEWID(), 'PDNAEser', 22, 0),
(NEWID(), 'PDEANCRt', 25, 0)

INSERT INTO dbo.UserDiscountCode(
       [DiscountCodeId]
      ,[UserId]
      ,[IsDisabled]
      ,[IsUsed]
)
VALUES
((SELECT DiscountCodeId FROM dbo.DiscountCode WHERE Code = 'AcEfssdW'), (SELECT Id from dbo.AspNetUsers WHERE UserName = 'master@master.com'), 0, 0),
((SELECT DiscountCodeId FROM dbo.DiscountCode WHERE Code = 'DaswPDr'), (SELECT Id from dbo.AspNetUsers WHERE UserName = 'master@master.com'), 0, 0),
((SELECT DiscountCodeId FROM dbo.DiscountCode WHERE Code = 'PDEANCRt'), (SELECT Id from dbo.AspNetUsers WHERE UserName = 'master@master.com'), 0, 0),
((SELECT DiscountCodeId FROM dbo.DiscountCode WHERE Code = 'PDCSertE'), (SELECT Id from dbo.AspNetUsers WHERE UserName = 'utente@utente.com'), 0, 0)

