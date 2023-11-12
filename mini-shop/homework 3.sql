Create Table [Sklad]
([ID] int primary key identity,
[Name] varchar(30) not null,
[Type] varchar(50),
[Vendor] varchar(100) not null,
[Quantity] int not null,
[Price] Money,
[Date] date
);
Insert [Sklad] values
('bread','food','terebovlia',56,11.34,CURRENT_TIMESTAMP),
('milk','food','lanivci',18,30.50,CURRENT_TIMESTAMP),
('chesse','food','romanivka',5,69.00,CURRENT_TIMESTAMP),
('soap','health','likarnya1',49,28.63,CURRENT_TIMESTAMP),
('tooth paste','health','likarnya2',73,41.21,CURRENT_TIMESTAMP),
('chair','home','tryskavec',73,41.21,CURRENT_TIMESTAMP);

Select Distinct [Sklad].[Type] from [Sklad];
Select Distinct [Sklad].Vendor from [Sklad];
Select * from [Sklad]
where [Sklad].[Quantity]= (Select Max([Sklad].[Quantity]) from [Sklad]);
Select * from [Sklad]
where [Sklad].[Quantity]= (Select Min([Sklad].[Quantity]) from [Sklad]);
Select * from [Sklad]
where [Sklad].[Price]= (Select Min([Sklad].[Price]) from [Sklad]);
Select * from [Sklad]
where [Sklad].[Price]= (Select Max([Sklad].[Price]) from [Sklad]);

Create Proc FindType @NameType varchar(50)
as
   Select * from [Sklad]
   where [Sklad].[Type]= @NameType;

Exec FindType 'home';

Create Proc FindVendor @NameVendor varchar(100)
as
   Select * from [Sklad]
   where [Sklad].[Vendor]= @NameVendor;

Select * from [Sklad]
where [Sklad].[Date]= (Select MAx([Date]) from [Sklad]);

Select AVG([Sklad].Quantity) from [Sklad];

Create Proc DeleteCol @id_ int
as
   delete from [Sklad] 
   where [ID]= @id_;


