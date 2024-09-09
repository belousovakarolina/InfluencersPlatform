# Nuomonės formuotojų paieškos platforma

Sistemos paskirtis:

Platformoje įmonės gali ieškoti tinkamų nuomonės formuotojų, skaitydami esamus atsiliepimus.

Funkciniai reikalavimai:

* Įmonė gali palikti atsiliepimą
* Įmonė ir nuomonės formuotojai gali perskaityti atsiliepimą

Technologijos:

* Loginiai komponentai realizuojami C# .NET karkasu
* Vaizdinė sąsaja realizuojama su JavaScript React biblioteka
* Loginiai ir vaizdiniai komponentai tarpusavyje bendrauja REST APIs

Objektai:

* Nuomonės formuotojas: primary key id, string name, string email, string password, string description
* Įmonė: primary key id, string name, string emai, string password, string description
* Atsiliepimas: primary key id, external id influencerId, external id companyId, string description, int stars, boolean verified