create database COMSATS
use COMSATS
DROP DATABASE COMSATS
create table Admin
(
	Admin_ID varchar(255) not null unique,
	Admin_Name varchar(255) not null,
	Admin_Password varchar(8) not null,
	Admin_Age int not null check(Admin_age >= 22),
	Admin_Gender varchar(255) not null,
	Admin_DOB varchar(255) not null,
	Admin_Email varchar(255) not null unique,
	Admin_Address varchar(255) not null,
	Admin_Pic image not null
	primary key (Admin_ID,Admin_Email)
)
drop table Admin
 select Admin_ID,Admin_Name, Admin_Gender,Admin_DOB,Admin_Email,Admin_Address from Admin
 insert into Admin values('Admin@cui','Hashim Ashraf','admin',25,'Male','Wednesday, March 26, 1997','hashimashraf@cuilahore.edu.pk','Green Town Sherkot',(SELECT * FROM OPENROWSET(BULK N'E:\\Semester 4\\DB\Project\\CuOnline Portal\\Images\\Admin.jpg', SINGLE_BLOB) as T1))
 truncate table Admin
 select * from Admin
create table Student
(
	Std_ID varchar(255) not null unique,
	Std_Name varchar(255) not null,
	Std_Password varchar(255) not null,
	Std_Personal_Email varchar(255) not null,
	Std_Official_Email varchar(255) not null unique,
	Std_Department varchar(255) not null,
	Std_Programme varchar(255) not null,
	Std_Gender varchar(255) not null,
	Std_DOB varchar(255) not null,
	Std_Address varchar(255) not null,
	Std_PhoneNo varchar(255) not null,
	Std_Pic image not null,
	primary key (Std_ID,Std_Official_Email)
)
select * from Student where Std_ID = 'FA20-BSE-084'

--
SELECT * FROM STUDENT
drop table Student
DELETE from Student WHERE Std_ID = 'FA20-BSE-009'
truncate table Student
DELETE FROM Student where 
create table Faculty
(
	Fac_ID varchar(255) not null unique,
	Fac_Name varchar(255) not null,
	Fac_Password varchar(8) not null,
	Fac_Personal_Email varchar(255) not null,
	Fac_Official_Email varchar(255) not null unique,
	Fac_Education varchar(255) not null,
	Fac_designation varchar(255) not null,
	Fac_Gender varchar(255) not null,
	Fac_DOB varchar(255) not null,
	Fac_Address varchar(255) not null,
	Fac_PhoneNo varchar(255) not null,
	Fac_Pic image not null,
	primary key (Fac_ID,Fac_Official_Email)
)
alter table Faculty drop column  Fac_Pic
alter table Faculty add Fac_Pic image
select * from Faculty
drop table Faculty
insert into Faculty values ('CS-12','Abid','abid','abid@','cs-12@cui','Master','Assistance','Male','2012-12-23','Lahore','123123123')
insert into Faculty values('CS-13','Farooq','abid','abid@','cs-13@cui','Master','Assistance','Male','2012-12-23','Lahore','123123123'),
('CS-14','Sana','abid','abid@','cs-14@cui','Master','Assistance','Female','2012-12-23','Lahore','123123123')
truncate table Faculty
insert into Faculty values('CS-17','Murtaza','murtaza','abid@','cs-17@cui','Master','Assistance','Male','2012-12-23','Lahore','123123123')

select * from Faculty
create table programs
(
	department varchar(255) not null,
	program varchar(255) not null,
	primary key (department,program)
)
select * from programs --where department = 'Computer Science'
insert into programs values ('Computer Science','BSSE'),('Computer Science','BSCS'),('Electrical Engineering','BSEE'),('Electrical Engineering','BSCE'),('Management Science','BBA'),('Management Science','BSMS'),('Mathematics','BSM'),('Physics','BSPY'),('Pharmacy','PHM'),('Accounting and Finance','BSAF'),('Architecture','BSAR')
create table Courses
(
	Course_Code varchar(255) primary key,
	Course_Name varchar(255) not null,
	Credit_Hours int not null check(Credit_Hours >= 0 AND Credit_Hours <= 4),
	Course_Department varchar(255) not null
)
select Course_Code from Courses --where Course_Code not in (select Course_Code from Assign_Course)
select * from Courses
truncate table Courses
delete from Courses where Course_Code = 'CSE102'
drop table Course

create table Student_Registration
(
	
	Course_Code varchar(255) not null foreign key references Courses(Course_Code) ON DELETE CASCADE,
	Std_ID varchar(255) not null foreign key references Student(Std_ID) ON DELETE CASCADE,
	PRIMARY KEY (Course_Code,Std_ID)
	
)
SELECT * FROM Student_Registration
select Student_Registration.Course_Code,Courses.Course_Name,Courses.Credit_Hours from Student_Registration inner join Courses on Student_Registration.Course_Code = Courses.Course_Code where Std_ID = 'FA20-BSE-084'
DROP TABLE Student_Registration
TRUNCATE TABLE Student_Registration
insert into Student_Registration values ('CSE102','FA20-BSE-084'), ('CSE112','FA20-BSE-084')
select Std_ID from Student_Registration where Course_Code = 'CSE112'
SELECT Student_Registration.Std_ID,Student.Std_Name FROM Student_Registration --inner join Student on Student_Registration.Std_ID = Student.Std_ID where Course_Code = 'CSE112'

delete from Student_Registration where Course_Code = 'CSE102' and Std_ID = 'FA20-BSE-084'
select Student_Registration.Std_ID, Student_Registration.Course_Code, Courses.Course_Name, Courses.Credit_Hours,Courses.Course_Department from Student_Registration inner join Courses on Student_Registration.Course_Code = Courses.Course_Code where Std_ID = 'FA20-BSE-084'
select Student_Registration.Std_ID, Student.std_Name from Student_Registration inner join Student on Student_Registration.Std_ID = Student.Std_ID where Course_Code = 'CSE112'

create table Assign_Course
(
	Fac_ID varchar(255) not null foreign key references Faculty(Fac_ID ) ON DELETE CASCADE,
	Course_Code varchar(255) not null foreign key references Courses(Course_Code) ON DELETE CASCADE,
	PRIMARY KEY (Fac_ID,Course_Code)
)
select Fac_ID from Faculty --where Fac_ID not in (select Fac_ID from Assign_Course)
select Courses.Credit_Hours,Assign_Course.Course_Code from Assign_Course inner join Courses on Assign_Course.Course_Code = Courses.Course_Code
drop table Assign_Course
select Courses.Credit_Hours,Assign_Course.Course_Code from Assign_Course inner join Courses on Assign_Course.Course_Code = Courses.Course_Code where Fac_ID = 'CS-01'
SELECT * from Assign_Course
insert into Assign_Course VALUES ('CS-17','MGT131')
truncate table Assign_Course
delete from Assign_Course where Fac_ID = 'CS-12'
select Fac_ID from Assign_Course
SELECT Assign_Course.Fac_ID,Faculty.Fac_Name,Assign_Course.Course_Code,Courses.Course_Name FROM ((Assign_Course INNER JOIN Faculty ON Faculty.Fac_ID = Assign_Course.Fac_ID) INNER JOIN Courses ON Assign_Course.Course_Code = Courses.Course_Code);
BEGIN TRY
select Std_ID from Student where Std_ID = 'FA20-BSE-084'
END TRY
BEGIN CATCH
  SELECT
    ERROR_NUMBER() AS ErrorNumber,
    ERROR_STATE() AS ErrorState,
    ERROR_SEVERITY() AS ErrorSeverity,
    ERROR_PROCEDURE() AS ErrorProcedure,
    ERROR_LINE() AS ErrorLine,
    ERROR_MESSAGE() AS ErrorMessage;
END CATCH;
GO
update Attendance set Attendance = 'Absent' where Std_ID = 'FA20-BSE-014' AND Lecture_Date = '2022-05-31' and Lecture_start_time = '08:50'

Select * From Courses

create table Attendance
(
	Course_Code varchar(255) not null  foreign key references Courses(Course_Code),
	Std_ID varchar(255) not null  foreign key references Student(Std_ID),
	Lecture_note varchar(255) not null,
	Lecture_Date date not null,
	Lecture_start_time time not null,
	Lecture_end_time time not null,
	Attendance varchar(255) not null,
	primary key (Course_Code,Std_ID,Lecture_start_time,Lecture_end_time)
)
select * from Attendance where Std_ID = 'FA20-BSE-084' AND Course_Code = 'cse112'
update Attendance set Attendance = 'Absent' where Std_ID = 'FA20-BSE-084' AND Course_Code = 'cse112'and Lecture_note = 'UML'
create table Lab_Attendance
(
	Course_Code varchar(255) not null  foreign key references Courses(Course_Code) ON DELETE CASCADE,
	Std_ID varchar(255) not null  foreign key references Student(Std_ID) ON DELETE CASCADE,
	Lecture_note varchar(255) not null,
	Lecture_Date date not null,
	Lecture_start_time time not null,
	Lecture_end_time time not null,
	Attendance varchar(255) not null,
	primary key (Course_Code,Std_ID,Lecture_start_time,Lecture_end_time)
)
select * from Lab_Attendance

drop table Attendance
update Attendance set Attendance = 'Present' where Std_ID = 'FA20-BSE-014' AND Lecture_Date = '2022-05-31' and Lecture_start_time = '08:50'
insert into Attendance values ('CSE112','FA20-BSE-084','UML',cast (getdate() as date),'04:56','09:07','present')
select * from Attendance
create table Deliverable
(
	Course_Code varchar(255) not null foreign key references Courses(Course_Code) ON DELETE CASCADE,
	Assignment_Name varchar(255) not null,
	Assignment_deadline date not null,
	Assignment_file_name varchar(255) not null,
	Assignment_ext varchar(255) not null,
	Assignment_file varbinary(max) not null,
	primary key(Course_Code,Assignment_file_name)
)
select * from Deliverable --where Course_Code = 'CSE112' and Assignment_Name = 'Assignment-1'
insert into Deliverable values ('CSE112','Assignment-1','2022-12-9','Desktop',null)
drop table Deliverable
select Assignment_Name,Assignment_deadline,Assignment_file from Deliverable where Course_Code = 'CSE112'
create table Whole_Semester_Deliverables
(
	Assignments varchar(255) not null,
	Quizes varchar(255) not null,
	Mid varchar(255) ,
	Final varchar(255) ,
	Lab_Assignments varchar(255) not null,
	Lab_Mid varchar(255) ,
	Lab_Final varchar(255) 
)
insert into Whole_Semester_Deliverables values ('Assignment-1','Quiz-1','MID-Term Exam','Final Exam','Lab Assignment-1','Lab MID-Term Exam','Lab Final Exam')
insert into Whole_Semester_Deliverables values ('Assignment-2','Quiz-2',null,null,'Lab Assignment-2',null,null)
insert into Whole_Semester_Deliverables values ('Assignment-3','Quiz-3',null,null,'Lab Assignment-3',null,null)
insert into Whole_Semester_Deliverables values ('Assignment-4','Quiz-4',null,null,'Lab Assignment-4',null,null)
TRUNCATE TABLE Whole_Semester_Deliverables
select * from Whole_Semester_Deliverables

drop table Whole_Semester_Deliverables
create table Assignments_Marks
(
	Course_Code varchar(255) not null  foreign key references Courses(Course_Code) ON DELETE CASCADE,
	Std_ID varchar(255) not null  foreign key references Student(Std_ID) ON DELETE CASCADE,
	Assignments_Marks_Id varchar(255),
	Assignments_Marks float check (Assignments_Marks >= 0),
	Assignments_total_Marks float check (Assignments_total_Marks >= 0)
	primary key(Course_Code,Std_ID,Assignments_Marks_Id)
)
truncate table Assignments_Marks
select Assignments_Marks_Id,Assignments_Marks,Assignments_total_Marks from Assignments_Marks where Course_Code = 'CSE112' and Std_ID = 'FA20-BSE-084'
select Assignments_total_Marks from Assignments_Marks where Assignments_Marks_Id = 'Assignment-1' and Course_Code = 'CSE112'
update Assignments_Marks set Assignments_Marks = 14 where Std_ID = 'FA20-BSE-084' AND Course_Code = 'CSE112' and  Assignments_Marks_Id = 'Assignment-1'

insert into Assignments_Marks values ('CSE102','FA20-BSE-078','Assignment-1',34,50)
insert into Assignments_Marks values ('CSE102','FA20-BSE-084','Assignment-2',34,50)
select Assignments from Whole_Semester_Deliverables where Assignments not in (select Assignments_Marks_Id from Assignments_Marks  where Course_Code = 'CSE102') 

create table Lab_Assignments_Marks
(
	Course_Code varchar(255) not null  foreign key references Courses(Course_Code) ON DELETE CASCADE,
	Std_ID varchar(255) not null  foreign key references Student(Std_ID) ON DELETE CASCADE,
	Lab_Assignments_Marks_Id varchar(255),
	Lab_Assignments_Marks float check (Lab_Assignments_Marks >= 0),
	Lab_Assignments_total_Marks float check (Lab_Assignments_total_Marks >= 0)
	primary key(Course_Code,Std_ID,Lab_Assignments_Marks_Id)
)
drop table Lab_Assignments_Marks

create table Quizes_Marks
(
	Course_Code varchar(255) not null  foreign key references Courses(Course_Code) ON DELETE CASCADE,
	Std_ID varchar(255) not null  foreign key references Student(Std_ID) ON DELETE CASCADE,
	Quizes_Marks_Id varchar(255),
	Quizes_Marks float check (Quizes_Marks >= 0),
	Quizes_total_Marks float check (Quizes_total_Marks >= 0)
	primary key(Course_Code,Std_ID,Quizes_Marks_Id)
)
drop table Quizes_Marks
select * from Quizes_Marks
create table Mid_Marks
(
	Course_Code varchar(255) not null  foreign key references Courses(Course_Code) ON DELETE CASCADE,
	Std_ID varchar(255) not null  foreign key references Student(Std_ID) ON DELETE CASCADE,
	Mid_Marks_Id varchar(255),
	Mid_Marks float check (Mid_Marks >= 0),
	Mid_total_Marks float check (Mid_total_Marks >= 0)
	primary key(Course_Code,Std_ID,Mid_Marks_Id)
)
drop table Mid_Marks
create table Final_Marks
(
	Course_Code varchar(255) not null  foreign key references Courses(Course_Code) ON DELETE CASCADE,
	Std_ID varchar(255) not null  foreign key references Student(Std_ID) ON DELETE CASCADE,
	Final_Marks_Id varchar(255),
	Final_Marks float check (Final_Marks >= 0),
	Final_total_Marks float check (Final_total_Marks >= 0)
	primary key(Course_Code,Std_ID,Final_Marks_Id)
)
drop table Final_Marks

create table Lab_Mid_Marks
(
	Course_Code varchar(255) not null  foreign key references Courses(Course_Code) ON DELETE CASCADE,
	Std_ID varchar(255) not null  foreign key references Student(Std_ID) ON DELETE CASCADE,
	Lab_Mid_Marks_Id varchar(255),
	Lab_Mid_Marks float check (Lab_Mid_Marks >= 0),
	Lab_Mid_total_Marks float check (Lab_Mid_total_Marks >= 0)
	primary key(Course_Code,Std_ID,Lab_Mid_Marks_Id)
)
drop table Lab_Mid_Marks

create table Lab_Final_Marks
(
	Course_Code varchar(255) not null  foreign key references Courses(Course_Code) ON DELETE CASCADE,
	Std_ID varchar(255) not null  foreign key references Student(Std_ID) ON DELETE CASCADE,
	Lab_Final_Marks_Id varchar(255),
	Lab_Final_Marks float check (Lab_Final_Marks >= 0),
	Lab_Final_total_Marks float check (Lab_Final_total_Marks >= 0)
	primary key(Course_Code,Std_ID,Lab_Final_Marks_Id)
)
drop table Lab_Final_Marks
update Marks set Quiz_Marks = 23,Quiz_total__Marks=50 where Course_Code = 'CSE112'
select * from Marks
insert into Marks values('CSE112','FA20-BSE-084',12,20,null,null,null,null,null,null,null,null,null,null,null,null)
insert into Marks values('CSE112','FA20-BSE-084',null,null,14,20,null,null,null,null,null,null,null,null,null,null)
update Marks values('CSE112','FA20-BSE-084',null,null,14,20,null,null,null,null,null,null,null,null,null,null)