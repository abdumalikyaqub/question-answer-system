use dbqasystem

go
create trigger QuestionInsert2
on Question for insert as
if @@ROWCOUNT = 1
begin
	declare @title nvarchar(150)
	select @title = Title from inserted
	if exists(select * from inserted where Title = @title)
	begin
		rollback tran
	end
end

drop trigger TrigDelAnswer

go
create trigger TrigDelQuest
on Question for delete as
begin
	declare @questionId int
	select @questionId = Id from deleted
	delete from Answer where Answer.QuestionId = @questionId
end

go
create trigger TrigDelAnswer
on Answer for delete as
begin
	declare @Id int
	select @Id = Id from deleted
	delete from Vote where Vote.AnswerId = @Id
end

alter table Answer
drop fk_ans_question

alter table Vote
drop fk_vote_answer

delete from Question where Id = 19