use dbqasystem

---- 1
go
create procedure crud_user
(
	@query int,
	@Id int = 0,
	@FullName nvarchar(150) = null,
	@Email nvarchar(150) = null,
	@Login nvarchar(50) = null,
	@Password nvarchar(50) = null,
	@RoleId int = 0
) as
begin
	if(@query = 1)
		begin
		insert into Users(FullName, Email, Login, Password, RoleId)
		values(@FullName, @Email, @Login, @Password, @RoleId)
		end
	if(@query = 2)
		begin
		select * from Users
		end
	if(@query = 3)
		begin
		update Users set
		FullName = @FullName,
		Email = @Email,
		Login = @Login,
		Password = @Password,
		RoleId = @RoleId
		where Users.Id = @Id
		end
	if(@query = 4)
		begin
		delete from Users where Id = @Id
		end
end

----call crud_user
execute crud_user @query = 2

-----------------
--------------------  2
---------------------

go
create procedure crud_category
(
	@query int,
	@Id int = 0,
	@Name nvarchar(50) = null
) as
begin
	if(@query = 1)
		begin
		insert into Category(Name)
		values(@Name)
		end
	if(@query = 2)
		begin
		select * from Category
		end
	if(@query = 3)
		begin
		update Category set
		Name = @Name
		where Id = @Id
		end
	if(@query = 4)
		begin
		delete from Category where Id = @Id
		end
end

-----------------
------------------- 3
---------------------


go
create procedure crud_userbadges
(
	@query int,
	@Id int = 0,
	@UserId int = 0,
	@BadgeId int = 0
) as
begin
	if(@query = 1)
		begin
		insert into UserBadges(UserId, BadgeId)
		values(@UserId, @BadgeId)
		end
	if(@query = 2)
		begin
		select * from UserBadges
		end
	if(@query = 3)
		begin
		update UserBadges set
		UserId = @UserId,
		BadgeId = @BadgeId
		where Id = @Id
		end
	if(@query = 4)
		begin
		delete from UserBadges where Id = @Id
		end
end

------------------
--------------- 4
------------------

go
create procedure crud_badge
(
	@query int,
	@Id int = 0,
	@Name nvarchar(150) = null
) as
begin
	if(@query = 1)
		begin
		insert into Badge(Name)
		values(@Name)
		end
	if(@query = 2)
		begin
		select * from Badge
		end
	if(@query = 3)
		begin
		update Badge set
		Name = @Name
		where Id = @Id
		end
	if(@query = 4)
		begin
		delete from Badge where Id = @Id
		end
end

---------------
------------------ 5
-----------------------

go
create procedure crud_vote
(
	@query int,
	@Id int = 0,
	@VoterId int = 0,
	@AnswerId int = 0,
	@Score int = 0
) as
begin
	if(@query = 1)
		begin
		insert into Vote(VoterId, AnswerId, Score)
		values(@VoterId, @AnswerId, @Score)
		end
	if(@query = 2)
		begin
		select * from Vote
		end
	if(@query = 3)
		begin
		update Vote set
		Score = @Score
		where Id = @Id
		end
	if(@query = 4)
		begin
		delete from Vote where Id = @Id
		end
end

-----------
----------- 6
-----------------------

go
create procedure crud_question
(
	@query int,
	@Id int = 0,
	@Title nvarchar(150) = null,
	@Body nvarchar(4000) = null,
	@AuthorId int = 0,
	@CategoryId int = 0,
	@CretedAt datetime = 0
) as
begin
	if(@query = 1)
		begin
		insert into Question(Title, Body, AuthorId, CategoryId, CreatedAt)
		values(@Title, @Body, @AuthorId, @CategoryId, @CretedAt)
		end
	if(@query = 2)
		begin
		select * from Question
		end
	if(@query = 3)
		begin
		update Question set
		Title = @Title,
		Body = @Body,
		AuthorId = @AuthorId,
		CategoryId = @CategoryId,
		CreatedAt = @CretedAt
		where Id = @Id
		end
	if(@query = 4)
		begin
		delete from Question where Id = @Id
		end
end

-------------
------------------- 7
-------------------


go
create procedure crud_answer
(
	@query int,
	@Id int = 0,
	@Body nvarchar(4000) = null,
	@AuthorId int = 0,
	@QuestionId int = 0
) as
begin
	if(@query = 1)
		begin
		insert into Answer(Body, AuthorId, QuestionId)
		values(@Body, @AuthorId, @QuestionId)
		end
	if(@query = 2)
		begin
		select * from Answer
		end
	if(@query = 3)
		begin
		update Answer set
		Body = @Body,
		AuthorId = @AuthorId,
		QuestionId = @QuestionId
		where Id = @Id
		end
	if(@query = 4)
		begin
		delete from Answer where Id = @Id
		end
end

-----------------
----------------- Func 1
----------------------

go
create function Rating(@userid int)
returns int
as
begin
	declare @score int
	select @score = SUM(Score) from Vote
	where Vote.VoterId = @userid
	return @score
end

----- call
select dbo.Rating(21) as Rating from Vote