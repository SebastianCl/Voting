use Shop

select * from Products
select * from Cities


select * from AspNetUsers



select * from Cities

use Voting

SELECT *
FROM Events
INNER JOIN Candidates
ON Events.Id = Candidates.EventId;


SELECT count(*) FROM Candidates WHERE EventId=0

INSERT INTO Votes VALUES ('6c543cd5-7cd5-45bf-b1df-983347535c85', 1, 1);

select * from Votes
select * from Events
select * from Candidates
select * from AspNetUsers