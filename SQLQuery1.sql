create table Professeur(
codeP int,
nomP varchar(50),
prenomP varchar(50),
diplome varchar(50),
Contact varchar(50)
)
create table Etudiant(
codeE int,
nom varchar(50),
prenom varchar(50),
dateNaissance date,
email varchar(50)
)
create table Module(
codeM int,
nomM varchar(50),
codeP int,
dateP date,
dateR date
)
drop table note
create table Note(
codeE int not null,
codeM int not null,
note float
)
alter table Note add constraint  notePk primary key(codeE,CodeM)