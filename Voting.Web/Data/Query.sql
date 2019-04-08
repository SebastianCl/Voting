use Shop

select * from Products
select * from Cities


select * from AspNetUsers


select * from AspNetUsers
select * from Cities
select * from Events
select * from Candidates
use Voting

SELECT *
FROM Events
INNER JOIN Candidates
ON Events.Id = Candidates.EventId;


SELECT count(*) FROM Candidates WHERE EventId=0