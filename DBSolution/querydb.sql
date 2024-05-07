-- Active: 1707123530557@@127.0.0.1@3306@assessmentdb
-- alter user 'root'@'localhost' identified by 'password';
use assessmentdb;

select * from interviews;
select * from interviewcriterias;
select * from interviewresults;
select questionbank.id,questionbank.title,questionbank.a,questionbank.b,questionbank.c,questionbank.d from questionbank inner join subjects on subjects.id=questionbank.subjectid
where subjects.title="ADVJAVA";
select * from test;
-- Select all questions  beong to ADV JAVA
select * from questionbank where subjectid=(select id from subjects where title ="ADVJAVA");
 
select * from questionbank inner join testquestions on testquestions.questionbankid = questionbank.id where testquestions.testid=1;

INSERT INTO candidateanswers (candidateid, testquestionid, answerkey) VALUES (@candidateId, @testQuestionId, @answerKey);

update candidatetestresults set testendtime =@testendtime where candidateid=@candidateid and testid=@testid;
update candidatetestresults set testendtime ="2015-11-05 14:35:00" where candidateid=2 and testid=1;



select questionbank.id, questionbank.title, subjects.title as subject ,evaluationcriterias.title as criteria
from questionbank, subjects,evaluationcriterias
where questionbank.subjectid=subjects.id and questionbank.evaluationcriteriaid=evaluationcriterias.id
and subjects.id=1 and evaluationcriterias.id=1;
                            
select employees.firstname,employees.lastname,interviews.candidateid from employees
inner join interviews
where employees.id=interviews.candidateid
order by interviews.candidateid asc;

select interviews.candidateid,interviews.smeid,subjectmatterexperts.subjectid,subjects.id,subjects.title
from interviews
inner join subjectmatterexperts
on interviews.smeid = subjectmatterexperts.id
inner join subjects on subjectmatterexperts.subjectid=subjects.id;

select interviews.candidateid,subjects.title
from interviews
inner join subjectmatterexperts
on interviews.smeid = subjectmatterexperts.id
inner join subjects on subjectmatterexperts.subjectid=subjects.id
where interviews.candidateid=2;


 
select interviews.id,interviews.interviewdate,interviews.interviewtime,interviews.smeid,
concat(employees.firstname," ",employees.lastname)as SmeName from interviews
inner join subjectmatterexperts 
on interviews.smeid=subjectmatterexperts.id 
inner join employees
on subjectmatterexperts.employeeid= employees.id
where interviews.id=2;

select interviews.candidateid, concat(employees.firstname," ",employees.lastname)as CandidateName from employees
inner join interviews
on interviews.candidateid = employees.id
where interviews.id=2;

select subjects.id, subjects.title from interviews
inner join subjectmatterexperts
on interviews.smeid = subjectmatterexperts.id
inner join subjects
on subjectmatterexperts.subjectid = subjects.id
where interviews.id=2;

select evaluationcriterias.id, evaluationcriterias.title from interviews
inner join interviewcriterias
on interviews.id = interviewcriterias.interviewid
inner join evaluationcriterias
on interviewcriterias.evaluationcriteriaid = evaluationcriterias.id
where interviews.id=2;
            
update questionbank set answerkey="c" where id = 1;











----------------------------------------------------------------------------------------------------------------




-- this query gives the testdetails where testid=1
select * from tests where id=1;

-- this query gives the tests createddate between "2023-11-05" and "2023-12-05"
select * from tests where creationDate  between "2023-11-05" and "2023-12-05";

-- this query gives the tests created by particular subjectmatterexperts
select * from tests where smeid=1;

select * from testquestions;

-- this query is for add questions into tests
insert into testquestions(testid,questionBankid) values( 1,1);

-- this query is for delete particular questions from tests
Delete from testquestions where testid=1 and questionbankid=3;

-- this query is for changing the duration of particular tests
update tests set duration=@duration where id=@assessmentId;

-- this query is for changing the scheduleddate of particular tests
update tests set scheduleddate=@scheduledDate where id=@assessmentId;

-- get all questions from questionbank
select * from questionbank;

-- this query gives the question where subjectid=1
select questionbank.id as questionid, questionbank.title as question, subjects.title as subject,
 subjects.id as subjectid from questionbank, subjects 
 where questionbank.subjectid=subjects.id and subjects.id=1;
 
--  this query gives the questions where subjectid=1 and criteriaid=1;
select questionbank.id, questionbank.title, subjects.title as subject ,evaluationcriterias.title as criteria
from questionbank, subjects,evaluationcriterias
where questionbank.subjectid=subjects.id and questionbank.evaluationcriteriaid=evaluationcriterias.id
and subjects.id=1 and evaluationcriterias.id=1;

-- this query is for changing the answerkey of particular question
update questionbank set answerkey="a" where id =1;

-- this query gives the question where questionid=1
select * from questionbank where id=1;

-- this query is for updating a question
update questionbank set title=@title,a=@a,b=@b,c=@c,d=@d,answerkey=@answerKey where id =1;

-- this query is for changing evaluationcriteriaid and subjectid where questionid=1
update questionbank set evaluationcriteriaid=1 ,subjectid=1 where id =1;

-- this query give the score of candidate where candidateid=1 and testid=3;
select score from candidatetestresults where candidateid=1 and testid=3;

-- get the candidatetestresultdetails
select candidatetestresults.testid,candidatetestresults.score,candidatetestresults.candidateid
,employees.firstname,employees.lastname,subjects.title as subject
from candidatetestresults
inner join employees
on employees.id=candidatetestresults.candidateid
inner join tests
on candidatetestresults.testid=tests.id
inner join subjects
on tests.subjectid=subjects.id
where candidatetestresults.testid=1;

-- get the appearedcandidates for testid=1
select candidatetestresults.testid, candidatetestresults.candidateid, employees.firstname, 
employees.lastname from candidatetestresults 
inner join employees on employees.id= candidatetestresults.candidateid
where candidatetestresults.testid=1;

-- get the passedcandidates for testid=1
select tests.id,candidatetestresults.candidateid,candidatetestresults.score,tests.passinglevel,employees.firstname,employees.lastname
from tests
inner join candidatetestresults on tests.id=candidatetestresults.testid
inner join employees on candidatetestresults.candidateid=employees.id
where candidatetestresults.score >= tests.passinglevel AND tests.id=1;

-- get the failedcandidates for testid=1
select tests.id,candidatetestresults.candidateid,candidatetestresults.score,tests.passinglevel,employees.firstname,employees.lastname
from tests
inner join candidatetestresults on tests.id=candidatetestresults.testid
inner join employees on candidatetestresults.candidateid=employees.id
where candidatetestresults.score < tests.passinglevel AND tests.id=1;

-- get the candidateanswers and correctanswers
select candidateanswers.testquestionid,testquestions.testid,
candidateanswers.answerkey as candidateanswer,questionbank.answerkey
from  candidateanswers
INNER JOIN   testquestions  on testquestions.questionbankid=candidateanswers.testquestionid
INNER JOIN   questionbank on questionbank.id=testquestions.questionbankid
where candidateanswers.candidateid=1 and testquestions.testid=1;

-- get candidatestresultdetails
select candidatetestresults.testid,candidatetestresults.score,candidatetestresults.candidateid
,employees.firstname,employees.lastname,subjects.title as subject
from candidatetestresults inner join employees on employees.id=candidatetestresults.candidateid
inner join tests on candidatetestresults.testid=tests.id
inner join subjects on tests.subjectid=subjects.id
where candidatetestresults.testid=1;

-- get appearedcandidates
select candidatetestresults.testid, candidatetestresults.candidateid, 
employees.firstname, employees.lastname
from candidatetestresults 
inner join employees 
on employees.id= candidatetestresults.candidateid
where candidatetestresults.testid=1;

-- to calculate score of given candidateid and testid;
call spcandidatetestresult(2,1,@pscore) ;
select(@pscore);

-- give the candidatetestresultdetails
CALL spcandidatetestresultdetails(3,2, @pcorrectAnswers, @pincorrectAnswers,@pskippedQuestions);
select @pcorrectAnswers,@pincorrectAnswers,@pskippedQuestions;

-- get interviewdetails where interviewid=2
call spinterviewdetails(2);

-- Update passing level by testid
update tests set passinglevel = 5 where id=1;
update tests set passinglevel=@passinglevel where id =@id;

-- get test results by passing subjectid=1
select tests.id,tests.subjectid,candidatetestresults.candidateid,employees.firstname,employees.lastname,candidatetestresults.score
from tests
inner join candidatetestresults
on tests.id=candidatetestresults.testid
inner join employees
on candidatetestresults.candidateid=employees.id
where tests.subjectid=1;







