# Nuomonės formuotojų paieškos platforma

## Sistemos paskirtis:

Platformoje įmonės gali ieškoti tinkamų nuomonės formuotojų, skaitydami esamus atsiliepimus.

## Funkciniai reikalavimai:

* Įmonė gali palikti atsiliepimą
* Įmonė ir nuomonės formuotojai gali perskaityti atsiliepimą

## Technologijos:

* Loginiai komponentai realizuojami C# .NET karkasu
* Vaizdinė sąsaja realizuojama su JavaScript React biblioteka
* Loginiai ir vaizdiniai komponentai tarpusavyje bendrauja REST APIs

## Objektai:

* Nuomonės formuotojo profilis: primary key id, external id userId, string name, string description, int igFollowersCount, int fbFollowersCount
* Įmonės profilis: primary key id, external id userId, string name, string desfription, float yearlyIncome
* Atsiliepimas: primary key id, external id influencerId, external id companyId, string description, int stars, boolean verified
* Kategorija: primary key id, string name

## Rolės:

* Nuomonės formuotojas: primary key id, string username, string email, string password
* Įmonė: primary key id, string username, string emai, string password
* Administratorius: primary key id, string username, string email, string password