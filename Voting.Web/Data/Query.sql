use Shop

select * from Products
select * from Cities


select * from AspNetUsers


select * from AspNetUsers
select * from Cities
select * from Events
use Voting
select * from Candidates

SELECT *
FROM Events
INNER JOIN Candidates
ON Events.Id = Candidates.EventId;


SELECT count(*) FROM Candidates WHERE EventId=0