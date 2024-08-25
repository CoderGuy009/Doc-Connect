select * from Schedule;
delete Schedule;
insert into Schedule values('D002','Dr. Amit Bhunia','Fri','11:00 AM - 12:00 PM');
SELECT * FROM Schedule WHERE Name = 'Dr. Kushal Aggarwal' AND Day = 'Thursday';
delete from Schedule where Day='Wed' AND Slot='3:00 AM - 4:00 PM';
desc Dependents;
select * from Appointments order by AppointmentId;
Update Appointments set Status='No';
ALTER TABLE Appointments MODIFY Status VARCHAR2(10) DEFAULT 'No' NOT NULL;
UPDATE Appointments SET Status = 'No' WHERE Status IS NULL;

desc Appointments;
commit;
alter table Appointments add Status varchar2(10));
ALTER TABLE Appointments ADD Status VARCHAR2(10);

SELECT Slot FROM Schedule WHERE Name = 'Dr. Kushal Aggarwal' AND Day = 'Wednesday'

alter table Appointments
MODIFY (
    APPOINTMENTDATE DATE NOT NULL,
    DEPENDENTNAME VARCHAR2(100) NOT NULL,
    SPECIALTY VARCHAR2(50) NOT NULL,
    DOCTOR VARCHAR2(100) NOT NULL,
    SLOT VARCHAR2(60) NOT NULL
);

drop Status from Appointments;
ALTER TABLE Appointments DROP COLUMN Status;
UPDATE Appointments SET Status = NULL;


delete from Appointments where appointmentid =10;
