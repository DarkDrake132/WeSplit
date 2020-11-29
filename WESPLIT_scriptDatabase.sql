create database WESPLIT

use WESPLIT

CREATE TABLE JOURNEY
(
	id int,
	_location nvarchar(max),
	title nvarchar(max),
	onGoingOrUsedToGo int,
	imageLink nvarchar(max),
	primary key (id)
)

CREATE TABLE IMAGE_DESTINATION
(
	id int,
	stt int,
	imageLink nvarchar(max),
	primary key(id, stt),
	foreign key(id) references JOURNEY(id)
)

CREATE TABLE MEMBER 
(
	id int,
	stt int,
	_name nvarchar(max),
	primary key(id, stt),
	foreign key(id) references JOURNEY(id)
)

CREATE TABLE EXPENSE
(
	id int,
	stt int,
	spending_number int,
	object_pay nvarchar(max),
	cost int,
	primary key(id, stt, spending_number),
	foreign key(id, stt) references MEMBER(id, stt)
)