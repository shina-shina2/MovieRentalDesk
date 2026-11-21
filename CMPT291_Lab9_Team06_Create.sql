/* CMPT 291 Lab 9 - Team 06
   Amal Ali and Shina Adeshina

   Run this one first

   We made a few changes based on the Lab 8 feedback:
   - Moved the link dates into Rental since Link was just a 1-to-1 table.
   - Added MaintainMovie and MaintainActor to track employee updates.
   - Added employee_id to Rental so we know who processed it.
   - Added customer_id to reviews so we can check who rented the movie.
   - Actor reviews also keep movie_id so the actor has to be in that movie.
   - Kept Movie_Actor to connect movies and actors in SQL.
   - Named the constraints so they're easier to find.

   The five-active-link rule still needs to be checked by the app.
*/
IF DB_ID(N'MovieRental_Team06') IS NULL
    CREATE DATABASE MovieRental_Team06;
GO
USE MovieRental_Team06;
GO

-- Drop the tables that depend on others first. Also drop the old Link table if it is there.
DROP TABLE IF EXISTS Actor_Review;
DROP TABLE IF EXISTS Movie_Review;
DROP TABLE IF EXISTS MaintainActor;
DROP TABLE IF EXISTS MaintainMovie;
DROP TABLE IF EXISTS Movie_Actor;
DROP TABLE IF EXISTS Link;
DROP TABLE IF EXISTS Rental;
DROP TABLE IF EXISTS Actor;
DROP TABLE IF EXISTS Movie;
DROP TABLE IF EXISTS Employee;
DROP TABLE IF EXISTS Customer;
GO

CREATE TABLE Customer (
  customer_id INT IDENTITY(1,1) NOT NULL,
  first_name NVARCHAR(100) NOT NULL,
  last_name NVARCHAR(100) NOT NULL,
  email NVARCHAR(255) NOT NULL,
  phone VARCHAR(20) NULL,
  is_active BIT NOT NULL CONSTRAINT DF_Customer_IsActive DEFAULT (1),
  CONSTRAINT PK_Customer PRIMARY KEY (customer_id),
  CONSTRAINT UQ_Customer_Email UNIQUE (email)
);

CREATE TABLE Employee (
  employee_id INT IDENTITY(1,1) NOT NULL,
  first_name NVARCHAR(100) NOT NULL,
  last_name NVARCHAR(100) NOT NULL,
  is_active BIT NOT NULL CONSTRAINT DF_Employee_IsActive DEFAULT (1),
  CONSTRAINT PK_Employee PRIMARY KEY (employee_id)
);

CREATE TABLE Movie (
  movie_id INT IDENTITY(1,1) NOT NULL,
  title NVARCHAR(100) NOT NULL,
  movie_type VARCHAR(20) NOT NULL,
  is_available BIT NOT NULL CONSTRAINT DF_Movie_IsAvailable DEFAULT (1),
  release_year DATE NOT NULL,
  CONSTRAINT PK_Movie PRIMARY KEY (movie_id),
  CONSTRAINT CK_Movie_Type CHECK (movie_type IN ('Comedy','Drama','Action','Foreign'))
);

CREATE TABLE Actor (
  actor_id INT IDENTITY(1,1) NOT NULL,
  first_name NVARCHAR(100) NOT NULL,
  last_name NVARCHAR(100) NOT NULL,
  birth_year INT NOT NULL,
  CONSTRAINT PK_Actor PRIMARY KEY (actor_id)
);

-- One movie can have several actors, and an actor can be in several movies.
CREATE TABLE Movie_Actor (
  movie_id INT NOT NULL,
  actor_id INT NOT NULL,
  role_name NVARCHAR(100) NOT NULL,
  CONSTRAINT PK_MovieActor PRIMARY KEY (movie_id, actor_id),
  CONSTRAINT FK_MovieActor_Movie FOREIGN KEY (movie_id) REFERENCES Movie(movie_id),
  CONSTRAINT FK_MovieActor_Actor FOREIGN KEY (actor_id) REFERENCES Actor(actor_id)
);

-- Keeping the link dates here instead of using a separate Link table.
CREATE TABLE Rental (
  rental_id INT IDENTITY(1,1) NOT NULL,
  rental_at DATETIME2(0) NOT NULL,
  charged_amount DECIMAL(8,2) NOT NULL,
  customer_id INT NOT NULL,
  movie_id INT NOT NULL,
  employee_id INT NOT NULL,
  created_time DATETIME2(0) NOT NULL,
  expire_time DATETIME2(0) NOT NULL,
  manually_expired_at DATETIME2(0) NULL,
  CONSTRAINT PK_Rental PRIMARY KEY (rental_id),
  CONSTRAINT UQ_Rental_IdCustomer UNIQUE (rental_id, customer_id),
  CONSTRAINT UQ_Rental_IdCustomerMovie UNIQUE (rental_id, customer_id, movie_id),
  CONSTRAINT FK_Rental_Customer FOREIGN KEY (customer_id) REFERENCES Customer(customer_id),
  CONSTRAINT FK_Rental_Movie FOREIGN KEY (movie_id) REFERENCES Movie(movie_id),
  CONSTRAINT FK_Rental_Employee FOREIGN KEY (employee_id) REFERENCES Employee(employee_id),
  CONSTRAINT CK_Rental_Charge CHECK (charged_amount = 5.00),
  CONSTRAINT CK_Rental_TimeOrder CHECK (expire_time > created_time),
  CONSTRAINT CK_Rental_StartTime CHECK (created_time = rental_at),
  CONSTRAINT CK_Rental_ManualExpiry CHECK (manually_expired_at IS NULL OR
      (manually_expired_at >= created_time AND manually_expired_at <= expire_time))
);

-- Only the customer from this rental should be able to review the movie.
CREATE TABLE Movie_Review (
  movie_review_id INT IDENTITY(1,1) NOT NULL,
  rating INT NOT NULL,
  review_text NVARCHAR(1000) NOT NULL,
  reviewed_at DATETIME2(0) NOT NULL,
  rental_id INT NOT NULL,
  customer_id INT NOT NULL,
  CONSTRAINT PK_MovieReview PRIMARY KEY (movie_review_id),
  CONSTRAINT UQ_MovieReview_Rental UNIQUE (rental_id),
  CONSTRAINT CK_MovieReview_Rating CHECK (rating BETWEEN 1 AND 5),
  CONSTRAINT FK_MovieReview_Renter FOREIGN KEY (rental_id, customer_id)
    REFERENCES Rental(rental_id, customer_id)
);

-- Make sure the customer rented the movie and the actor was in it.
CREATE TABLE Actor_Review (
  actor_review_id INT IDENTITY(1,1) NOT NULL,
  rating INT NOT NULL,
  review_text NVARCHAR(1000) NOT NULL,
  reviewed_at DATETIME2(0) NOT NULL,
  rental_id INT NOT NULL,
  customer_id INT NOT NULL,
  movie_id INT NOT NULL,
  actor_id INT NOT NULL,
  CONSTRAINT PK_ActorReview PRIMARY KEY (actor_review_id),
  CONSTRAINT UQ_ActorReview_RentalActor UNIQUE (rental_id, actor_id),
  CONSTRAINT CK_ActorReview_Rating CHECK (rating BETWEEN 1 AND 5),
  CONSTRAINT FK_ActorReview_RenterMovie FOREIGN KEY (rental_id, customer_id, movie_id)
    REFERENCES Rental(rental_id, customer_id, movie_id),
  CONSTRAINT FK_ActorReview_Cast FOREIGN KEY (movie_id, actor_id)
    REFERENCES Movie_Actor(movie_id, actor_id)
);

-- Track which employee added or updated each movie or actor.
CREATE TABLE MaintainMovie (
  employee_id INT NOT NULL,
  movie_id INT NOT NULL,
  maintained_at DATETIME2(0) NOT NULL,
  maintenance_action VARCHAR(10) NOT NULL,
  CONSTRAINT PK_MaintainMovie PRIMARY KEY (employee_id, movie_id, maintained_at),
  CONSTRAINT FK_MaintainMovie_Employee FOREIGN KEY (employee_id) REFERENCES Employee(employee_id),
  CONSTRAINT FK_MaintainMovie_Movie FOREIGN KEY (movie_id) REFERENCES Movie(movie_id),
  CONSTRAINT CK_MaintainMovie_Action CHECK (maintenance_action IN ('ADD','UPDATE'))
);

CREATE TABLE MaintainActor (
  employee_id INT NOT NULL,
  actor_id INT NOT NULL,
  maintained_at DATETIME2(0) NOT NULL,
  maintenance_action VARCHAR(10) NOT NULL,
  CONSTRAINT PK_MaintainActor PRIMARY KEY (employee_id, actor_id, maintained_at),
  CONSTRAINT FK_MaintainActor_Employee FOREIGN KEY (employee_id) REFERENCES Employee(employee_id),
  CONSTRAINT FK_MaintainActor_Actor FOREIGN KEY (actor_id) REFERENCES Actor(actor_id),
  CONSTRAINT CK_MaintainActor_Action CHECK (maintenance_action IN ('ADD','UPDATE'))
);
GO
PRINT N'Team 06 corrected schema created.';
GO
