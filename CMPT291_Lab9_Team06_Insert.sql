/* CMPT 291 Lab 9 - Team 06
   Run this after Create.sql.
   These are just sample records for testing, not actual movie cast data.
   If we need to rerun the inserts, run Create.sql again first.
*/
USE MovieRental_Team06;
GO

INSERT INTO Customer (first_name, last_name, email, phone, is_active) VALUES
(N'Amara', N'Okafor', N'amara.okafor@example.com', '780-555-0101', 1),
(N'Liam', N'Chen', N'liam.chen@example.com', '780-555-0102', 1),
(N'Sofia', N'Patel', N'sofia.patel@example.com', NULL, 1),
(N'Shina', N'Adeshina', N'shina.adeshina@example.com', '780-555-0104', 1),
(N'Jenny', N'Chedder', N'jenny.chedder@example.com', NULL, 1),
(N'Truc', N'Vo', N'truc.vo@example.com', '780-555-0106', 0), -- inactive but has old rentals
(N'Paul', N'Awesome', N'paul.awesome@example.com', NULL, 1); -- no rentals yet

INSERT INTO Employee (first_name, last_name, is_active) VALUES
(N'Jordan', N'Smith', 1), (N'Priya', N'Singh', 1), (N'Amal', N'Ali', 1);

INSERT INTO Movie (title, movie_type, is_available, release_year) VALUES
(N'The Grand Budapest Hotel', 'Comedy', 1, '2014-01-01'),  -- 1
(N'Mad Max: Fury Road', 'Action', 1, '2015-01-01'),       -- 2
(N'Parasite', 'Foreign', 1, '2019-01-01'),                -- 3
(N'The Shawshank Redemption', 'Drama', 0, '1994-01-01'), -- 4: unavailable now
(N'The Greatest Showman', 'Comedy', 1, '2017-01-01'),    -- 5
(N'The Mentalist', 'Drama', 1, '2008-01-01'),            -- 6
(N'Inception', 'Action', 1, '2010-01-01'),               -- 7
(N'Amelie', 'Foreign', 1, '2001-01-01'),                 -- 8
(N'Little Miss Sunshine', 'Comedy', 1, '2006-01-01'),   -- 9
(N'The Pursuit of Happyness', 'Drama', 1, '2006-01-01'),-- 10
(N'The Raid', 'Foreign', 1, '2011-01-01'),               -- 11
(N'The Green Mile', 'Drama', 0, '1999-01-01');          -- 12: unavailable now

INSERT INTO Actor (first_name, last_name, birth_year) VALUES
(N'Ralph', N'Fiennes', 1962), (N'Tom', N'Hardy', 1977),
(N'Song', N'Kang-ho', 1967), (N'Tim', N'Robbins', 1958),
(N'Hugh', N'Jackman', 1968), (N'Simon', N'Baker', 1969),
(N'Leonardo', N'DiCaprio', 1974), (N'Audrey', N'Tautou', 1976),
(N'Steve', N'Carell', 1962), (N'Will', N'Smith', 1968);

-- Added 24 movie/actor pairs so we have enough to test.
INSERT INTO Movie_Actor (movie_id, actor_id, role_name) VALUES
(1,1,N'Lead'),(1,9,N'Support'),(2,2,N'Lead'),(2,7,N'Support'),
(3,3,N'Lead'),(3,8,N'Support'),(4,4,N'Lead'),(4,1,N'Support'),
(5,5,N'Lead'),(5,9,N'Support'),(6,6,N'Lead'),(6,4,N'Support'),
(7,7,N'Lead'),(7,2,N'Support'),(8,8,N'Lead'),(8,3,N'Support'),
(9,9,N'Lead'),(9,5,N'Support'),(10,10,N'Lead'),(10,6,N'Support'),
(11,3,N'Lead'),(11,2,N'Support'),(12,4,N'Lead'),(12,10,N'Support');


-- Older rentals use set dates. New links use GETDATE() so they are still active.
INSERT INTO Rental (rental_at, charged_amount, customer_id, movie_id, employee_id, created_time, expire_time, manually_expired_at) VALUES
('2026-09-04T09:00:00',5.00,1,1,1,'2026-09-04T09:00:00',DATEADD(HOUR,24,CONVERT(DATETIME2(0),'2026-09-04T09:00:00')),NULL),
('2026-09-06T12:00:00',5.00,2,4,2,'2026-09-06T12:00:00',DATEADD(HOUR,24,CONVERT(DATETIME2(0),'2026-09-06T12:00:00')),'2026-09-06T18:00:00'),
('2026-09-08T15:00:00',5.00,3,2,1,'2026-09-08T15:00:00',DATEADD(HOUR,24,CONVERT(DATETIME2(0),'2026-09-08T15:00:00')),NULL),
('2026-09-11T11:00:00',5.00,4,5,2,'2026-09-11T11:00:00',DATEADD(HOUR,24,CONVERT(DATETIME2(0),'2026-09-11T11:00:00')),NULL),
('2026-09-15T16:00:00',5.00,5,6,3,'2026-09-15T16:00:00',DATEADD(HOUR,24,CONVERT(DATETIME2(0),'2026-09-15T16:00:00')),NULL),
('2026-09-18T19:00:00',5.00,6,12,1,'2026-09-18T19:00:00',DATEADD(HOUR,24,CONVERT(DATETIME2(0),'2026-09-18T19:00:00')),NULL),
('2026-10-03T10:00:00',5.00,1,3,1,'2026-10-03T10:00:00',DATEADD(HOUR,24,CONVERT(DATETIME2(0),'2026-10-03T10:00:00')),NULL),
('2026-10-08T12:30:00',5.00,2,7,2,'2026-10-08T12:30:00',DATEADD(HOUR,24,CONVERT(DATETIME2(0),'2026-10-08T12:30:00')),NULL),
('2026-10-12T14:00:00',5.00,3,8,1,'2026-10-12T14:00:00',DATEADD(HOUR,24,CONVERT(DATETIME2(0),'2026-10-12T14:00:00')),NULL),
('2026-10-17T17:00:00',5.00,4,9,3,'2026-10-17T17:00:00',DATEADD(HOUR,24,CONVERT(DATETIME2(0),'2026-10-17T17:00:00')),NULL),
('2026-10-23T18:00:00',5.00,5,10,2,'2026-10-23T18:00:00',DATEADD(HOUR,24,CONVERT(DATETIME2(0),'2026-10-23T18:00:00')),NULL),
('2026-10-28T20:00:00',5.00,6,11,3,'2026-10-28T20:00:00',DATEADD(HOUR,24,CONVERT(DATETIME2(0),'2026-10-28T20:00:00')),NULL),
(DATEADD(HOUR,-3,GETDATE()),5.00,1,1,1,DATEADD(HOUR,-3,GETDATE()),DATEADD(HOUR,24,DATEADD(HOUR,-3,GETDATE())),NULL),
(DATEADD(HOUR,-3,GETDATE()),5.00,1,2,2,DATEADD(HOUR,-3,GETDATE()),DATEADD(HOUR,24,DATEADD(HOUR,-3,GETDATE())),NULL),
(DATEADD(HOUR,-3,GETDATE()),5.00,1,3,3,DATEADD(HOUR,-3,GETDATE()),DATEADD(HOUR,24,DATEADD(HOUR,-3,GETDATE())),NULL),
(DATEADD(HOUR,-3,GETDATE()),5.00,1,5,1,DATEADD(HOUR,-3,GETDATE()),DATEADD(HOUR,24,DATEADD(HOUR,-3,GETDATE())),NULL),
(DATEADD(HOUR,-3,GETDATE()),5.00,1,7,2,DATEADD(HOUR,-3,GETDATE()),DATEADD(HOUR,24,DATEADD(HOUR,-3,GETDATE())),NULL),
(DATEADD(HOUR,-2,GETDATE()),5.00,2,6,2,DATEADD(HOUR,-2,GETDATE()),DATEADD(HOUR,24,DATEADD(HOUR,-2,GETDATE())),NULL),
(DATEADD(HOUR,-2,GETDATE()),5.00,3,10,1,DATEADD(HOUR,-2,GETDATE()),DATEADD(HOUR,24,DATEADD(HOUR,-2,GETDATE())),NULL),
(DATEADD(HOUR,-1,GETDATE()),5.00,4,9,3,DATEADD(HOUR,-1,GETDATE()),DATEADD(HOUR,24,DATEADD(HOUR,-1,GETDATE())),NULL);

-- These reviews use the customer IDs from the matching rentals.
INSERT INTO Movie_Review (rating, review_text, reviewed_at, rental_id, customer_id) VALUES
(5,N'Excellent movie.','2026-09-05T08:00:00',1,1),
(4,N'Enjoyed the story.','2026-09-07T08:00:00',2,2),
(5,N'Great action.','2026-09-09T08:00:00',3,3),
(4,N'Funny and entertaining.','2026-09-12T08:00:00',4,4),
(4,N'Interesting mystery.','2026-09-16T08:00:00',5,5),
(5,N'Fantastic direction.','2026-10-04T08:00:00',7,1),
(4,N'Impressive visuals.','2026-10-09T08:00:00',8,2);

-- Actor reviews need to match both the rental and the movie cast.
INSERT INTO Actor_Review (rating, review_text, reviewed_at, rental_id, customer_id, movie_id, actor_id) VALUES
(5,N'Strong lead performance.','2026-09-05T08:30:00',1,1,1,1),
(4,N'Memorable acting.','2026-09-07T08:30:00',2,2,4,4),
(5,N'Excellent action performance.','2026-09-09T08:30:00',3,3,2,2),
(4,N'Wonderful singing.','2026-09-12T08:30:00',4,4,5,5),
(5,N'Great supporting performance.','2026-10-09T08:30:00',8,2,7,2);

-- Different employees can work on the same movie or actor.
INSERT INTO MaintainMovie (employee_id, movie_id, maintained_at, maintenance_action) VALUES
(1,1,'2026-08-10T09:00:00','ADD'),(2,1,'2026-08-11T10:00:00','UPDATE'),
(2,2,'2026-08-10T10:00:00','ADD'),(1,3,'2026-08-10T11:00:00','ADD'),
(3,4,'2026-08-10T12:00:00','ADD'),(3,5,'2026-08-11T09:00:00','ADD'),
(1,6,'2026-08-11T10:00:00','ADD'),(2,7,'2026-08-11T11:00:00','ADD'),
(3,8,'2026-08-11T12:00:00','ADD'),(1,9,'2026-08-12T09:00:00','ADD'),
(2,10,'2026-08-12T10:00:00','ADD'),(3,11,'2026-08-12T11:00:00','ADD'),
(1,12,'2026-08-12T12:00:00','ADD'),(2,4,'2026-11-01T09:00:00','UPDATE');

INSERT INTO MaintainActor (employee_id, actor_id, maintained_at, maintenance_action) VALUES
(1,1,'2026-08-15T09:00:00','ADD'),(2,1,'2026-08-17T10:00:00','UPDATE'),
(2,2,'2026-08-15T10:00:00','ADD'),(3,3,'2026-08-15T11:00:00','ADD'),
(1,4,'2026-08-15T12:00:00','ADD'),(2,5,'2026-08-16T09:00:00','ADD'),
(3,6,'2026-08-16T10:00:00','ADD'),(1,7,'2026-08-16T11:00:00','ADD'),
(2,8,'2026-08-16T12:00:00','ADD'),(3,9,'2026-08-17T09:00:00','ADD'),
(1,10,'2026-08-17T10:00:00','ADD'),(2,3,'2026-08-18T10:00:00','UPDATE');
GO

-- Count the rows in every table to make sure the inserts worked.
SELECT table_name, row_count FROM (
 SELECT 'Customer' AS table_name, COUNT(*) AS row_count FROM Customer
 UNION ALL SELECT 'Employee', COUNT(*) FROM Employee
 UNION ALL SELECT 'Movie', COUNT(*) FROM Movie
 UNION ALL SELECT 'Actor', COUNT(*) FROM Actor
 UNION ALL SELECT 'Movie_Actor', COUNT(*) FROM Movie_Actor
 UNION ALL SELECT 'Rental', COUNT(*) FROM Rental
 UNION ALL SELECT 'Movie_Review', COUNT(*) FROM Movie_Review
 UNION ALL SELECT 'Actor_Review', COUNT(*) FROM Actor_Review
 UNION ALL SELECT 'MaintainMovie', COUNT(*) FROM MaintainMovie
 UNION ALL SELECT 'MaintainActor', COUNT(*) FROM MaintainActor
) AS counts ORDER BY table_name;
GO
