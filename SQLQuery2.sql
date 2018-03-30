	USE [DB_SAIR_Notes]
GO

DECLARE	@return_value Int

EXEC	@return_value = [dbo].[getProf]
 
insert into module values(2,'Base de donnée',1,'2018-01-01','2018-03-01')
insert into module values(3,'ASP',1,'2018-01-01','2018-03-01')
insert into module values(4,'BD AVANCE',1,'2018-01-01','2018-03-01')
insert into module values(5,'TEST',1,'2018-01-01','2018-03-01')
insert into module values(6,'DEMO',1,'2018-01-01','2018-03-01')
select * from Module
GO
go
create proc unmodule @codem int
as select * from Module where codeM=@codem
go
create proc unprof @codep int
as select * from Professeur where codeP=@codep
go
create proc unenote @codem int,@codee int
as select * from Note where codeModule=@codem and codeEtudiant=@codee
go
create proc unetudiant @codee int
as select * from Etudiant where codeE=@codee
go
exec module
go
create view v1
as
 select concat(p.nomP,' ',p.prenomP) as 'Prof',
 concat(E.nom,' ',E.prenom) as 'Etudiant',n.note
  as 'Note /20',m.nomM as 'Module concerné'
 from Professeur p
  inner join Module m on p.codeP=m.codeP
  inner join Note n on n.codeModule=m.codeM
  inner join Etudiant e on n.codeEtudiant=e.codeE 
 update Module set codeP =1
 select * from Module
   insert into Note values(1,1,20)
   insert into Note values(2,1,10)
   insert into Note values(3,1,17)
   insert into Note values(4,1,20)
--procedure de selection 
--getDetailsEtudiantB
go
alter proc getDetailsEtudiantB @idE int 
as begin
	select m.nomM,concat(p.nomP,' ',p.prenomP) as prof,n.note,m.dateR from Etudiant e inner join Note   n on n.codeEtudiant=e.codeE
							 inner join Module m on n.codeModule=m.codeM
							 inner join Professeur p on p.codeP=m.codeP
							 where n.codeEtudiant=@idE --and note!=null
end
select *from Etudiant
select *from Professeur
select *from Module
select *from Note

insert into Module values(1,'Base de Donnés I',11,'03-03-2018','04-03-2018')
exec getDetailsEtudiantB 1
--procedures d'insertion
go
alter proc ADDPROF(@CODEPROF int,@NOMPROF varchar(50),
@PRENOMPROF varchar(50), @DIPLOMEPROF varchar(50),@EMAILPROF varchar(50))
as
begin
insert into Professeur values(@CODEPROF, @NOMPROF,
@PRENOMPROF , @DIPLOMEPROF, @EMAILPROF)
select * from Professeur
end
go
alter proc ADDETUD(@CODEET int,@NOM varchar(50),
@PRENOM varchar(50), @DATEN date,@EMAIL varchar(50))
as
begin
insert into Etudiant values(@CODEET, @NOM,
@PRENOM , @DATEN, @EMAIL)
select * from Etudiant
end
go
alter proc ADDMODULE(@CODEM int,@NOMM varchar(50),
@CODEPROF INT, @DATEPREVUE DATE,@DATEREEL DATE)
as
begin
insert into Module values (@CODEM, @NOMM ,
@CODEPROF , @DATEPREVUE ,@DATEREEL )
select * from Module
end
go
alter proc ADDNOTE(@CODEE int,@CODEM int, @Note float) 
as
begin
insert into Note values(@CODEE, @CODEM,@Note)
select * from Note
end
go
--procedures de suppression

go
create proc DELPROF @CP int
as
begin
delete from Professeur where codeP=@CP
select * from Professeur
end
go
alter proc DELETD @CE int
as
begin
delete from Etudiant where codeE=@CE
select * from Etudiant
end
go
alter proc DELMOD @CM int
as
begin
delete from Module where codeM=@CM
select * from Module
end
go
alter proc DELNOTE (@CE int,@CM int)
as
begin
delete from Note where codeEtudiant= @CE and codeModule=@CM
select * from Note
end

select * from Professeur
go
--procs de mise à jour
 alter proc UPDPROF @CODEPROF int,@NOMPROF varchar(50),@PRENOMPROF varchar(50),@DIPLOMEPROF varchar(50),@EMAILPROF varchar(50)
 as
  begin
	update Professeur
	set codeP=@CODEPROF,		nomP=@NOMPROF,		prenomP=@PRENOMPROF,		diplome=@DIPLOMEPROF,		Contact=@EMAILPROF
	select * from Professeur
 end
 go
 create proc UPDMOD  @CM int,
					 @NOMM varchar(50),
					 @CP int,
					 @DATEP date,
					 @DATER date
 as
  begin
	update Module 
	set  
	codeM=@CM,
	nomM=@NOMM,
	codeP=@CP,
	dateP=@DATEP,
	dateR=@DATER
	where codeM=@CM
	select * from Module
 end

 go
 create proc UPDETU  @CE int,
					 @NOME varchar(50),
					 @PNOME int,
					 @DATEN date,
					 @EMAIL date
 as
  begin
	update Etudiant 
	set  
	codeE=@CE,
	nom=@NOME,
	prenom=@PNOME,
	dateNaissance=@DATEN,
	email=@EMAIL
	where codeE=@CE
	select * from Etudiant
 end

 go




 go
 create proc UPDNOTE @CM int,@CE int,@NOTE float
 as
  begin
	update Note
	set codeEtudiant=@CE,codeModule=@cm,note=@NOTE
	where codeEtudiant=@CE and codeModule=@cm
	select * from Note
 end
 go
 exec UPDNOTE 1,1,20
 go
----------------------------
create proc ReportingP1 
as
select m.*,p.nomP,p.prenomP 
from module m inner join Professeur p
 on p.codeP= m.codeP
 --------------------------
 go
 create proc R1
 as select count(codeE) from Etudiant
 exec R1
 go
 create proc R2
 as select m.nomM as MODULE,count(n.codeEtudiant) TOTAL_ETUDIANT
  from Module m inner join Note n on m.codeM=n.codeModule
  group by m.nomM
  exec R2
go
alter PROC R3
AS BEGIN
	SELECT P.nomP+' '+P.prenomP as Nom, COUNT(M.codeM) AS [Nombre Module]
	FROM Professeur P INNER JOIN Module M ON P.codeP = M.codeP 
	GROUP BY P.nomP,P.prenomP 
END

EXEC R3

go
-- R4
create PROC R4
AS BEGIN
	SELECT E.nom + ' ' + E.prenom AS [Nom Etudiant], AVG(N.Note) AS [Moyenne Generale]
	FROM Etudiant E INNER JOIN Note N ON E.codeE = N.codeEtudiant
	GROUP BY E.nom, E.prenom
END

EXEC R4


--R5
CREATE PROC R5
AS BEGIN
	SELECT E.nom + ' ' + E.prenom AS [Nom Etudiant], AVG(N.Note) AS [Moyenne Générale]
	FROM Etudiant E INNER JOIN Note N ON E.codeE = N.codeEtudiant
	GROUP BY E.nom + ' ' + E.prenom HAVING AVG(N.Note) >= 10
END

EXEC R5


--R6
CREATE PROC R6
AS BEGIN
	SELECT M.* FROM Module M WHERE M.dateR > M.dateP
END

EXEC R6

--R7
CREATE PROC R7
AS BEGIN
	SELECT E.nom, MAX(N.Note) AS [Max Note]
	FROM Etudiant E INNER JOIN Note N ON E.codeE = N.CodeEtudiant
	 GROUP BY E.nom
END

EXEC R7


--R8
CREATE PROC R8
AS BEGIN
	SELECT M.nomM, MAX(N.Note) AS [Max Note]
	FROM Module M INNER JOIN Note N ON M.codeM = N.codeModule
	 GROUP BY M.nomM
END

--------------
insert into Note values
(1,'M01',12),
('E00001','M02',13),
('E00001','M03',7),
('E00001','M04',15),
('E00001','M05',5),
('E00002','M01',15),
('E00002','M02',9),
('E00002','M03',15),
('E00002','M04',13),
('E00002','M05',8),
('E00003','M01',8),
('E00003','M02',17),
('E00003','M03',4),
('E00003','M04',12),
('E00003','M05',7),
('E00004','M01',18),
('E00004','M02',19),
('E00004','M03',9),
('E00004','M04',11),
('E00004','M05',6)

select * from Note Module