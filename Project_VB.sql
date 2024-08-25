select * from Doctors;

CREATE TABLE Appointments (
    AppointmentID NUMBER PRIMARY KEY,
    AppointmentDate DATE DEFAULT SYSDATE,
    DependentName VARCHAR2(100),
    Specialty VARCHAR2(50),
    Doctor VARCHAR2(100),
    SlotNo NUMBER
);
GRANT CREATE TABLE, CREATE SEQUENCE TO SYS;

CREATE SEQUENCE appointment_seq
    START WITH 1
    INCREMENT BY 1
    NOCACHE
    NOCYCLE;

set serveroutput ON
CREATE OR REPLACE TRIGGER trg_appointments_before_insert
BEFORE INSERT ON Appointments
FOR EACH ROW
BEGIN
    SELECT appointment_seq.NEXTVAL
    INTO :NEW.AppointmentID
    FROM dual;
END;

select * from Schedule;

drop table Appointments;

select * from Appointments;
select * from Users;

insert into Appointments values('9011','26-06-2024','Aniket Sahu','Neurology','Dr. Amit Bhunia',1);
