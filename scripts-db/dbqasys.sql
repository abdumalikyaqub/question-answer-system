create database dbqasystem

use dbqasystem
--- 1
create table Users
(
	Id int primary key identity,
	FullName nvarchar(150) not null,
	Email nvarchar(150) not null,
	Login nvarchar(50) not null,
	Password nvarchar(50) not null,
	RoleId int not null,
	constraint fk_user_to_roles foreign key(RoleId) references Roles(Id)
);

---- 2
create table Roles
(
	Id int primary key identity,
	TypeName nvarchar(50) not null
);

----- 3
create table Category
(
	Id int primary key identity,
	Name nvarchar(50) not null
);

---- 4
create table Question
(
	Id int primary key identity,
	Title nvarchar(150) not null,
	Body nvarchar(4000) not null,
	AuthorId int,
	CategoryId int,
	CreatedAt datetime,
	constraint fk_question_to_users foreign key (AuthorId) references Users (Id),
	constraint fk_question_to_category foreign key (CategoryId) references Category (Id) 
);


---- 5
create table Answer
(
	Id int identity primary key,
	Body nvarchar(4000) not null,
	AuthorId int not null,
	QuestionId int not null,
	constraint fk_ans_user foreign key (AuthorId) references Users(Id),
	constraint fk_ans_question foreign key (QuestionId) references Question(Id)
);

----- 6
create table Vote
(
	Id int identity primary key,
	VoterId int not null,
	AnswerId int not null,
	Score int,
	constraint fk_vote_user foreign key (VoterId) references Users (Id),
	constraint fk_vote_answer foreign key (AnswerId) references Answer (Id),
);


----- 7
create table Badge
(
	Id int primary key identity,
	Name nvarchar(150)
);

---- 8
create table UserBadges
(
	Id int primary key identity,
	UserId int,
	BadgeId int
	constraint fk_ub_user foreign key (UserId) references Users (Id),
	constraint fk_ub_badge foreign key (BadgeId) references Badge (Id)

);

