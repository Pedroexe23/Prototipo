create Database Prototipo 
use Prototipo 
drop table Registro
create table Registro (
Fk_RUT Varchar (10),
Fk_Id_Documento int
);

delete from Personas
 Drop Database prototipo
Drop Table Registro
Drop Table Personas
Drop Table Documento





create table Registro (
id_registro int identity(0,1) Primary key ,
Fk_RUT Varchar (10),
Fk_Id_Documento int
);

create table Personas(
Nombre Varchar (21),
Apellido Varchar (41),
Rut Varchar (10) primary key


);

Create table Documento(
Id_Documento int primary key ,
Archivo  Varchar (60),
Tamaño Bigint ,
Tipo Varchar (50),
Fecha Date
);


 insert into Personas (Rut, Nombre, Apellido) values ('25235487-8','Gabriel','Gallardo') ;
  insert into Personas (Rut, Nombre, Apellido) values ('18452987-2','Susana','Jimenez') ;
   insert into Personas (Rut, Nombre, Apellido) values ('26448245-9','Maximiliano','Cantaro') ;
    insert into Personas (Rut, Nombre, Apellido) values ('7589542-3','Esteban','Sepulveda') ;
	 insert into Personas (Rut, Nombre, Apellido) values ('14258658-4','Alejandra','Aragon') ;
	  insert into Personas (Rut, Nombre, Apellido) values ('17258625-7','Patricia','Torres') ;
	   insert into Personas (Rut, Nombre, Apellido) values ('5213789-6','Luis','Martinez') ;
  insert into Personas (Rut, Nombre, Apellido) values ('20587925-6','Roman','Flores') ;
  insert into Registro (	Fk_RUT,	Fk_Id_Documento) values('14258658-4', 1 );



select * from Personas
select * from Registro
select * from Documento
