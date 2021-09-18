use [Person];

create table People (
	ID int identity primary key not null,
	LastName nvarchar(50) not null,
	FirstName nvarchar(50) not null,
	MiddleName nvarchar(50) null,
	BirthDate datetime not null
)