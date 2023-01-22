--create database testqa;
use testqa

create table Users
(
	Id int primary key identity,
	FullName nvarchar(50) not null,
	Email nvarchar(50) not null,
	Password nvarchar(50)

);

create table Category
(
	Id int primary key identity,
	Name nvarchar(50) not null,

);

create table Question
(
	Id int primary key identity,
	Title nvarchar(50) not null,
	Body nvarchar(50) not null,
	AuthorId int,
	CategoryId int,
	constraint fk_question_to_users foreign key (AuthorId) references Users (Id),
	constraint fk_question_to_category foreign key (CategoryId) references Category (Id) 
);

insert into Category(Name)
values
('CSS'),
('JS'),
('NET Core')

insert into Users(FullName, Email, Password)
values
('Mike', 'mike@bk.ru', 'qwuder76'),
('Jake', 'jake@bk.ru', 'qwuder76'),
('Nike', 'nike@bk.ru', 'qwuder76')

insert into Question(Title, Body, AuthorId, CategoryId) values
('q1', 't1', 1, 1),
('q2', 't2', 2, 2),
('q3', 't3', 3, 3)

select * from Users
select * from Category
select * from Question

use testqa

create table Roles
(
	Id int primary key identity,
	Name nvarchar(50) not null,
);

create table UserRoles
(
	Id int primary key identity,
	UserId int not null,
	RoleId int not null,
	constraint fk_ur_user foreign key(UserId) references Users (Id),
	constraint fk_ur_roles foreign key(RoleId) references Roles (Id),
);

create table Badge
(
	Id int primary key identity,
	Name nvarchar(50) not null,
	
);

create table UserBadges
(
	Id int primary key identity,
	UserId int not null,
	BadgeId int not null,
	constraint fk_ub_user foreign key(UserId) references Users(Id),
	constraint fk_ub_badge foreign key(BadgeId) references Badge(Id),
);

alter table Users
add Login nvarchar(10) not null default 'root';

--alter table Vote
--drop column CraetedAt

alter table Users
add Role nvarchar(10) not null default 'user';