use Shop

select * from Products
select * from Cities


select * from AspNetUsers
DELETE FROM AspNetUsers 
    WHERE id = 'f900a120-8ed8-409e-bd71-8277b27c1e43'


select * from Cities

use Voting

SELECT *
FROM Events
INNER JOIN Candidates
ON Events.Id = Candidates.EventId;


SELECT count(*) FROM Candidates WHERE EventId=0

INSERT INTO Votes VALUES ('9b7dee15-ac18-4874-a5fa-a42a46ded689', 8, 2);

select * from Votes WHERE EventId = 3
select * from Events
select * from Candidates
select * from AspNetUsers