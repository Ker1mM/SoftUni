USE master
CREATE DATABASE School
USE School
GO

DROP DATABASE School
--Problem 1. DONE
CREATE TABLE Students
(
  Id         INT PRIMARY KEY IDENTITY,
  FirstName  NVARCHAR(30) NOT NULL,
  MiddleName NVARCHAR(25),
  LastName   NVARCHAR(30) NOT NULL,
  Age        INT,
  Address    NVARCHAR(50),
  Phone      CHAR(10),
  CONSTRAINT ck_Age CHECK (Age > 0)
)

CREATE TABLE Subjects
(
  Id      INT PRIMARY KEY IDENTITY,
  [Name]  NVARCHAR(20) NOT NULL,
  Lessons INT          NOT NULL
)

CREATE TABLE StudentsSubjects
(
  Id        INT PRIMARY KEY IDENTITY,
  StudentId INT            NOT NULL FOREIGN KEY REFERENCES Students (Id),
  SubjectId INT            NOT NULL FOREIGN KEY REFERENCES Subjects (Id),
  Grade     DECIMAL(15, 2) NOT NULL,
  CONSTRAINT ck_Grade CHECK (Grade >= 2 AND Grade <= 6)
)

CREATE TABLE Exams
(
  Id        INT PRIMARY KEY IDENTITY,
  Date      DATETIME,
  SubjectId INT NOT NULL FOREIGN KEY REFERENCES Subjects (Id)
)

CREATE TABLE StudentsExams
(
  StudentId INT            NOT NULL FOREIGN KEY REFERENCES Students (Id),
  ExamId    INT            NOT NULL FOREIGN KEY REFERENCES Exams (Id),
  Grade     DECIMAL(15, 2) NOT NULL,
  CONSTRAINT pk_StudentsExams PRIMARY KEY (StudentId, ExamId),
  CONSTRAINT ck_Grade_StudentsExams CHECK (Grade >= 2 AND Grade <= 6)
)

CREATE TABLE Teachers
(
  Id        INT PRIMARY KEY IDENTITY,
  FirstName NVARCHAR(20) NOT NULL,
  LastName  NVARCHAR(20) NOT NULL,
  Address   NVARCHAR(20) NOT NULL,
  Phone     CHAR(10),
  SubjectId INT          NOT NULL FOREIGN KEY REFERENCES Subjects (Id)
)

CREATE TABLE StudentsTeachers
(
  StudentId INT NOT NULL FOREIGN KEY REFERENCES Students (Id),
  TeacherId INT NOT NULL FOREIGN KEY REFERENCES Teachers (Id),
  CONSTRAINT pk_StudentsTeachers PRIMARY KEY (StudentId, TeacherId)
)
GO

--Problem 2. DONE
INSERT INTO Teachers(FirstName, LastName, Address, Phone, SubjectId)
VALUES ('Ruthanne', 'Bamb', '84948 Mesta Junction', '3105500146', 6),
       ('Gerrard', 'Lowin', '370 Talisman Plaza', '3324874824', 2),
       ('Merrile', 'Lambdin', '81 Dahle Plaza', '4373065154', 5),
       ('Bert', 'Ivie', '2 Gateway Circle', '4409584510', 4)

INSERT INTO Subjects(Name, Lessons)
VALUES ('Geometry', 12),
       ('Health', 10),
       ('Drama', 7),
       ('Sports', 9)
GO

--Problem 3. DONE
UPDATE StudentsSubjects
SET Grade = 6
WHERE SubjectId IN (1, 2)
  AND Grade >= 5.50
GO

--Problem 4. DONE
DELETE
FROM StudentsTeachers
WHERE TeacherId IN (SELECT id FROM Teachers WHERE Phone LIKE '%72%')

DELETE
FROM Teachers
WHERE Phone LIKE '%72%'

--Problem 5. DONE
SELECT st.FirstName, st.LastName, st.Age
FROM Students st
WHERE st.Age >= 12
ORDER BY FirstName, LastName

--Problem 6. DONE
SELECT CONCAT(st.FirstName, ' ', st.MiddleName, ' ', st.LastName) AS [Full Name],
       st.Address
FROM Students st
WHERE st.Address LIKE '%road%'
ORDER BY FirstName, LastName, Address

--Problem 7. DONE
SELECT st.FirstName, st.Address, st.Phone
FROM Students st
WHERE st.MiddleName IS NOT NULL
  AND st.Phone LIKE '42%'
ORDER BY FirstName

--Problem 8. DONE
SELECT st.FirstName, st.LastName, COUNT(stt.TeacherId) AS TeachersCount
FROM Students st
       JOIN StudentsTeachers stt ON st.Id = stt.StudentId
GROUP BY firstname, lastname

--Problem 9. DONE
SELECT CONCAT(t.FirstName, ' ', t.LastName) AS FullName,
       CONCAT(sb.Name, '-', sb.Lessons)     AS Sunjects,
       COUNT(stt.StudentId)                 AS Students
FROM Teachers t
       JOIN Subjects sb ON t.SubjectId = sb.Id
       JOIN StudentsTeachers stt ON t.id = stt.TeacherId
GROUP BY FirstName, LastName, sb.Name, sb.Lessons
ORDER BY Students DESC

--Problem 10. DONE
SELECT CONCAT(st.FirstName, ' ', st.LastName) AS [Full Name]
FROM Students st
WHERE st.Id NOT IN (SELECT StudentId FROM StudentsExams)
ORDER BY [Full Name]

--Problem 11. DONE
SELECT TOP 10 tc.FirstName,
              tc.LastName,
              COUNT(stt.StudentId) AS StudentsCount
FROM Teachers tc
       JOIN StudentsTeachers stt ON tc.Id = stt.TeacherId
GROUP BY firstname, lastname
ORDER BY StudentsCount DESC, FirstName, LastName

--Problem 12. DONE
SELECT TOP 10 st.FirstName                                      AS [First Name],
              st.LastName                                       AS [Last Name],
              cast(ROUND(AVG(stex.Grade), 2) AS NUMERIC(36, 2)) AS Grade
FROM Students st
       JOIN StudentsExams stex ON st.Id = stex.StudentId
GROUP BY FirstName, LastName
ORDER BY Grade DESC, FirstName, LastName

--Problem 13.
SELECT a.FirstName, a.LastName, a.Grade
FROM (SELECT st.FirstName,
             st.LastName,
             stsb.Grade,
             ROW_NUMBER() over (PARTITION BY stsb.StudentId ORDER BY stsb.Grade DESC) AS Rank
      FROM Students st
             JOIN StudentsSubjects stsb ON st.Id = stsb.StudentId) AS a
WHERE a.Rank = 2
ORDER BY a.FirstName, a.LastName

--Problem 14.
SELECT CONCAT(st.FirstName, ' ', CASE WHEN st.MiddleName IS NOT NULL THEN st.MiddleName + ' ' END,
              st.LastName) AS [Full Name]
FROM Students st
WHERE st.Id NOT IN (SELECT stsb.StudentId FROM StudentsSubjects stsb)
ORDER BY [Full Name]

--Problem 15.
SELECT st.Id,
       CONCAT(st.FirstName, '', st.LastName)             AS [Student Full Name],
       cast(ROUND(AVG(stex.Grade), 2) AS NUMERIC(36, 2)) AS Grade
FROM Students st
       JOIN StudentsExams stex ON stex.StudentId = st.Id
GROUP BY FirstName, LastName, st.Id
ORDER BY Grade DESC

SELECT CONCAT(tc.FirstName, ' ', tc.LastName) AS [Teacher Full Name],
       sb.Name                                AS [Subject Name],
       a.[Student Full Name],
       a.Grade
FROM Teachers tc
       JOIN Subjects sb ON sb.Id = tc.SubjectId
       JOIN StudentsTeachers sttc ON sttc.TeacherId = tc.id
       JOIN (SELECT st.Id,
                    CONCAT(st.FirstName, '', st.LastName)             AS [Student Full Name],
                    cast(ROUND(AVG(stex.Grade), 2) AS NUMERIC(36, 2)) AS Grade
             FROM Students st
                    JOIN StudentsExams stex ON stex.StudentId = st.Id
             GROUP BY FirstName, LastName, st.Id) a ON sttc.StudentId = a.id
ORDER BY [Subject Name], [Teacher Full Name], Grade DESC

--Problem 16. DONE
SELECT sb.Name,
       AVG(stsb.Grade)
FROM Subjects sb
       JOIN StudentsSubjects stsb ON sb.id = stsb.SubjectId
GROUP BY sb.Name, sb.Id
ORDER BY sb.Id

--Problem 17.
SELECT a.Quarter, a.SubjectName, SUM(a.StudentsCount) FROM (
              SELECT CASE
         WHEN DATEPART(MONTH, e.Date) BETWEEN 1 AND 3 THEN 'Q1'
         WHEN DATEPART(MONTH, e.Date) BETWEEN 4 AND 6 THEN 'Q2'
         WHEN DATEPART(MONTH, e.Date) BETWEEN 7 AND 9 THEN 'Q3'
         WHEN DATEPART(MONTH, e.Date) BETWEEN 10 AND 12 THEN 'Q4'
         WHEN e.Date IS NULL THEN 'TBA'
         END                 AS Quarter,
       sb.Name               AS SubjectName,
       COUNT(stex.StudentId) AS StudentsCount
FROM Exams e
       JOIN StudentsExams stex ON stex.ExamId = e.Id AND stex.Grade >= 4
       JOIN Subjects sb ON sb.Id = e.SubjectId
GROUP BY sb.Name, e.Date
                ) as a
GROUP BY a.Quarter, SubjectName
ORDER BY Quarter

--Problem 18. DONE
CREATE FUNCTION udf_ExamGradesToUpdate(@studentId INT, @grade DECIMAL(15, 2))
  RETURNS VARCHAR(MAX) AS
BEGIN
  IF NOT EXISTS(SELECT 1 FROM Students WHERE Id = @studentId)
    BEGIN
      RETURN ('The student with provided id does not exist in the school!')
    end

  IF (@grade > 6)
    BEGIN
      RETURN ('Grade cannot be above 6.00!')
    end

  DECLARE @GradeCount INT = (SELECT COUNT(stex.Grade)
                             FROM StudentsExams stex
                             WHERE stex.StudentId = @studentId AND
                                   stex.Grade BETWEEN @grade AND @grade+0.50)

  DECLARE @StudentName NVARCHAR(50) = (SELECT FirstName
                                       FROM Students
                                       WHERE Id = @studentId)

  RETURN CONCAT('You have to update ',@GradeCount,' grades for the student ', @StudentName)
end
GO

--Problem 19. DONE
CREATE PROCEDURE usp_ExcludeFromSchool(@StudentId INT) AS
  BEGIN
      IF NOT EXISTS(SELECT 1 FROM Students WHERE Id = @StudentId)
    BEGIN
      RAISERROR ('This school has no student with the provided id!', 16, 1)
    end

    DELETE FROM StudentsExams
    WHERE StudentId = @StudentId

    DELETE FROM StudentsSubjects
    WHERE StudentId = @StudentId

    DELETE FROM StudentsTeachers
    WHERE StudentId = @StudentId

    DELETE FROM Students
    WHERE Id = @StudentId
  end

--Problem 20.
CREATE TABLE ExcludedStudents
(
  StudentId INT PRIMARY KEY NOT NULL,
  StudentName NVARCHAR(100) NOT NULL
)

CREATE TRIGGER tr_ExcludedStudents ON Students AFTER DELETE AS
  BEGIN
    INSERT INTO ExcludedStudents(StudentId, StudentName)
    SELECT Id, CONCAT(FirstName, ' ', LastName) FROM deleted
  end

