﻿use Shop

select * from Products
select * from Cities


select * from AspNetUsers
DELETE FROM AspNetUsers 
    WHERE id = 'f900a120-8ed8-409e-bd71-8277b27c1e43'

DELETE FROM Votes 
WHERE id = '1'
select * from Cities

use Voting

SELECT *
FROM Events
INNER JOIN Candidates
ON Events.Id = Candidates.EventId;


SELECT count(*) FROM Candidates WHERE EventId=0

INSERT INTO Votes VALUES ('d9148824-294c-485e-bb81-82623f5446c0', 8, 2);

select * from Votes WHERE EventId = 3
select * from Events
select * from Candidates
select * from AspNetUsers


UPDATE aspnetusers
SET EMAILCONFIRMED = 1
WHERE email = 'sebastiancardonaloaiza3435@gmail.com'

DELETE FROM AspNetUsers 
    WHERE email = 'sebastian.cardona@gruponetw.com'
 