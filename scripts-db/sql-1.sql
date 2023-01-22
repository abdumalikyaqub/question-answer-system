create table Answer
(
	Id int identity primary key,
	Body nvarchar(150),
	AuthorId int,
	QuestionId int,
	constraint fk_ans_user foreign key (AuthorId) references Users(Id),
	constraint fk_ans_question foreign key (QuestionId) references Question(Id)
);

ALTER TABLE Answer
Add  constraint fk_ans_question foreign key (QuestionId) references Question(Id)


create table Vote
(
	Id int identity primary key,
	VoterId int,
	AnswerId int,
	VoteStatus int,
	CreatedAt date,
	constraint fk_vote_user foreign key (VoterId) references Users (Id),
	constraint fk_vote_answer foreign key (AnswerId) references Answer (Id),
);

select sum(VoteStatus) as SUMMA from Vote where AnswerId = 12