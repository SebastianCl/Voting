use Shop

select * from Countries
select * from Cities





select * from AspNetUsers
select * from Cities

select * from Events
select * from Candidates

SELECT *
FROM Events
INNER JOIN Candidates
ON Events.Id = Candidates.EventId;

use Voting
SELECT count(*) FROM Candidates WHERE EventId=0