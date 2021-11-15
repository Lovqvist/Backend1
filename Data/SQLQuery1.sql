CREATE TABLE Products (
Id int not null identity primary key,
Image nvarchar(max) not null,
Name nvarchar(max) not null,
ShortDescription nvarchar(max) not null,
LongDescription nvarchar(max),
Price money not null,
InStock bit not null
)



CREATE TABLE Users (
Id int not null identity primary key,
FirstName nvarchar(50) not null,
LastName nvarchar(50) not null,
Email varchar(100) not null unique,
Password nvarchar(100) not null
)
GO



CREATE TABLE Orders (
Id int not null identity primary key,
UserId int not null references Users(id),
OrderDate datetimeoffset not null,
Status nvarchar(15) not null
)
GO



CREATE TABLE OrderLines (
OrderId int not null references Orders(Id),
ProductId int not null references Products(Id),
Quantity int not null default 1,
UnitPrice money not null default 0



primary key (OrderId, ProductId)
)