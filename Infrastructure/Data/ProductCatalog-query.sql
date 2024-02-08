DROP TABLE Products
DROP TABLE Manufacturers
DROP TABLE Categories

CREATE TABLE Categories (
	Id int not null identity primary key,
	Name nvarchar(100) not null unique
)

CREATE TABLE Manufacturers (
	Id int not null identity primary key,
	Name nvarchar(100) not null unique
)

CREATE TABLE Products (
	ArticleNumber int not null identity primary key,
	Title nvarchar(200) not null,
	Description nvarchar(max) null,
	price money not null,
	CategoryId int not null references Categories(Id),
	ManufacturerId int not null references Manufacturers(Id)
)
