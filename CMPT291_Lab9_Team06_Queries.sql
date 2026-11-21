/* CMPT 291 Lab 9 - Team 06
   Run this after the create and insert files.
   First we check the rules, then run the five reports.
   For most checks, an empty result means nothing broke the rule.
*/
USE MovieRental_Team06;
GO

/* BR-10: Check if anyone has more than 5 active links.
   Manually expired links don't count. */
SELECT c.customer_id, c.first_name, c.last_name, COUNT(*) AS unexpired_links
FROM Customer AS c JOIN Rental AS r ON r.customer_id = c.customer_id
WHERE r.expire_time > GETDATE() AND r.manually_expired_at IS NULL
GROUP BY c.customer_id, c.first_name, c.last_name
HAVING COUNT(*) > 5;
GO

/* BR-10: Customer 1 should have exactly 5 active links. */
DECLARE @at_limit_customer_id INT = 1;
SELECT c.customer_id, c.first_name, c.last_name,
 COUNT(r.rental_id) AS unexpired_links
FROM Customer AS c LEFT JOIN Rental AS r
 ON r.customer_id = c.customer_id AND r.expire_time > GETDATE()
 AND r.manually_expired_at IS NULL
WHERE c.customer_id = @at_limit_customer_id
GROUP BY c.customer_id, c.first_name, c.last_name;
GO

/* Check movie reviews. The person reviewing should be the renter.
   The foreign key should prevent any mismatches. */
SELECT mr.movie_review_id, mr.rental_id, mr.customer_id
FROM Movie_Review AS mr LEFT JOIN Rental AS r
 ON r.rental_id=mr.rental_id AND r.customer_id=mr.customer_id
WHERE r.rental_id IS NULL;
GO

/* Check actor reviews. The customer needs to have rented the movie,
   and the actor needs to be listed in its cast. */
SELECT ar.actor_review_id, ar.rental_id, ar.customer_id, ar.movie_id, ar.actor_id
FROM Actor_Review AS ar
LEFT JOIN Rental AS r ON r.rental_id=ar.rental_id
 AND r.customer_id=ar.customer_id AND r.movie_id=ar.movie_id
LEFT JOIN Movie_Actor AS ma ON ma.movie_id=ar.movie_id AND ma.actor_id=ar.actor_id
WHERE r.rental_id IS NULL OR ma.movie_id IS NULL;
GO

/* Check the amount owed. We calculate it from the $5 rentals
   instead of storing another balance column. */
SELECT c.customer_id, c.first_name, c.last_name,
 COUNT(r.rental_id) AS rental_count,
 CAST(COALESCE(SUM(r.charged_amount),0) AS DECIMAL(10,2)) AS balance_owed
FROM Customer AS c LEFT JOIN Rental AS r ON r.customer_id=c.customer_id
GROUP BY c.customer_id, c.first_name, c.last_name
ORDER BY c.customer_id;
GO

/* BR-13: Check for active links to unavailable movies.
   Older expired rentals are fine. */
SELECT r.rental_id, r.movie_id, m.title
FROM Rental AS r JOIN Movie AS m ON m.movie_id=r.movie_id
WHERE m.is_available=0 AND r.expire_time > GETDATE()
 AND r.manually_expired_at IS NULL;
GO

/* BR-15: Inactive customers shouldn't have active links. */
SELECT c.customer_id, r.rental_id
FROM Customer AS c JOIN Rental AS r ON r.customer_id=c.customer_id
WHERE c.is_active=0 AND r.expire_time > GETDATE()
 AND r.manually_expired_at IS NULL;
GO

/* Check the link dates, including any early manual expirations. */
SELECT rental_id, created_time, expire_time, manually_expired_at
FROM Rental
WHERE created_time<>rental_at OR expire_time<=created_time
 OR (manually_expired_at IS NOT NULL AND
    (manually_expired_at<created_time OR manually_expired_at>expire_time));
GO

/* Check that the movie and actor maintenance records point to
   real employees and items. The foreign keys should handle this too. */
SELECT mm.employee_id, mm.movie_id
FROM MaintainMovie AS mm
LEFT JOIN Employee AS e ON e.employee_id=mm.employee_id
LEFT JOIN Movie AS m ON m.movie_id=mm.movie_id
WHERE e.employee_id IS NULL OR m.movie_id IS NULL;
GO
SELECT ma.employee_id, ma.actor_id
FROM MaintainActor AS ma
LEFT JOIN Employee AS e ON e.employee_id=ma.employee_id
LEFT JOIN Actor AS a ON a.actor_id=ma.actor_id
WHERE e.employee_id IS NULL OR a.actor_id IS NULL;
GO

/* Report 1: Money made from rentals each month.
   Uses GROUP BY, SUM, and a date filter. */
DECLARE @from_date DATE = '2026-09-01';
SELECT YEAR(r.rental_at) AS rental_year,
 MONTH(r.rental_at) AS rental_month,
 COUNT(*) AS rental_count,
 CAST(SUM(r.charged_amount) AS DECIMAL(10,2)) AS monthly_income
FROM Rental AS r
WHERE r.rental_at >= @from_date
GROUP BY YEAR(r.rental_at),MONTH(r.rental_at)
ORDER BY rental_year,rental_month;
GO

/* Report 2: Customers with the most rentals.
   Uses GROUP BY and an EXISTS subquery. Ties get the same rank. */
SELECT DENSE_RANK() OVER (ORDER BY COUNT(r.rental_id) DESC) AS activity_rank,
 c.customer_id,c.first_name,c.last_name,COUNT(r.rental_id) AS rental_count,
 CAST(SUM(r.charged_amount) AS DECIMAL(10,2)) AS total_spent
FROM Customer AS c JOIN Rental AS r ON r.customer_id=c.customer_id
WHERE EXISTS (SELECT 1 FROM Rental AS rr WHERE rr.customer_id=c.customer_id)
GROUP BY c.customer_id,c.first_name,c.last_name
ORDER BY activity_rank,c.customer_id;
GO

/* Report 3: Movies rented the most.
   Uses GROUP BY and COUNT. Ties share a rank, even if a movie has 0 rentals. */
SELECT DENSE_RANK() OVER (ORDER BY COUNT(r.rental_id) DESC) AS popularity_rank,
 m.movie_id,m.title,m.movie_type,COUNT(r.rental_id) AS rental_count
FROM Movie AS m LEFT JOIN Rental AS r ON r.movie_id=m.movie_id
GROUP BY m.movie_id,m.title,m.movie_type
ORDER BY popularity_rank,m.title;
GO

/* Report 4: Rentals for a chosen movie type.
   Joins four tables. We can change @movie_type later in the app. */
DECLARE @movie_type VARCHAR(20) = 'Drama';
SELECT r.rental_id,r.rental_at,
 c.first_name+N' '+c.last_name AS customer_name,
 m.title AS movie_title,m.movie_type,
 e.first_name+N' '+e.last_name AS processed_by,
 r.charged_amount,
 CASE WHEN r.manually_expired_at IS NOT NULL THEN 'Manually expired'
      WHEN r.expire_time<=GETDATE() THEN 'Expired'
      ELSE 'Unexpired' END AS link_status
FROM Rental AS r
JOIN Customer AS c ON c.customer_id=r.customer_id
JOIN Movie AS m ON m.movie_id=r.movie_id
JOIN Employee AS e ON e.employee_id=r.employee_id
WHERE m.movie_type=@movie_type
ORDER BY r.rental_at DESC,r.rental_id;
GO

/* Report 5: Employees who processed the most rentals.
   Uses GROUP BY and COUNT. Ties get the same rank. */
SELECT DENSE_RANK() OVER (ORDER BY COUNT(r.rental_id) DESC) AS employee_rank,
 e.employee_id,e.first_name,e.last_name,
 COUNT(r.rental_id) AS rentals_processed,
 CAST(COALESCE(SUM(r.charged_amount),0) AS DECIMAL(10,2)) AS rental_revenue
FROM Employee AS e LEFT JOIN Rental AS r ON r.employee_id=e.employee_id
GROUP BY e.employee_id,e.first_name,e.last_name
ORDER BY employee_rank,e.employee_id;
GO
