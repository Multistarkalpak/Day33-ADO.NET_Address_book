

select * from address_book
use addressbook_service

create database addressbook_service;

create table address_book(First_Name varchar(200) ,
Last_Name varchar(200) ,
Address varchar(200),
City varchar(200),
State varchar(130),
Zip varchar(60),
Phone_number varchar(100),
Email varchar(200),
BookName varchar(200) ,
AddressbookType varchar(200));

insert into address_book values('kalpak','chincholkar','a-colony','Bengaluru','karnataka','45634','34987','afd@gmail.com','shree','professional'),
('Gurpreet','singh','B-colony','Mysore','karnataka','45634','34987','abd@gmail.com','shree','Family'),
('Shivraj','Readdy','c-colony','wizag','Andrapradesh','45634','34987','aed@gmail.com','shree','Friends');

CREATE PROCEDURE addressProcedure
@first_name varchar(20) ,
@last_name varchar(20) ,
@address varchar(20),
@city varchar(20),
@state varchar(13),
@zip varchar(6),
@phone_number varchar(10),
@email varchar(20),
@bookname varchar(20) ,
@addressbooktype varchar(20)
as
insert into address_book values(@first_name,@last_name,@address,@city,@state,@zip,@phone_number,
    @email,@bookname,@addressbooktype);
go