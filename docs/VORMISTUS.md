# Dokumentatsiooni vormistusnõuded

Allikas: *Tallinna Tehnoloogiakolledži kirjalike tööde koostamise ja vormistamise juhend* (2026).
Selle faili järgi koostatakse `docs/` kaustas olev projekti dokumentatsioon.

## Tiitelleht (juhendi lisa 1)

```
TALLINNA TEHNOLOOGIAKOLLEDŽ
<Õppekava või osakond>

RETSEPTID JA TOIDUPLAAN
<Töö liik: Lõpuprojekt (Programmeerimine ja Hajusrakendused)>

Õpilane: Igor Aleksejev, <õpperühm>
Juhendajad: Marina Oleinik, Irina Merkulova

Tallinn 2026
```

- Tiitellehel leheküljenumbrit ei kuvata, kuid see arvestatakse numeratsiooni hulka.
- Pealkirja lõppu punkti ei panda.

## Töö osad

1. Tiitelleht
2. Sisukord (automaatne, leheküljenumbritega; pealkirjad samas sõnastuses nagu tekstis)
3. Sissejuhatus: teema, olulisus, eesmärk, ülesanded, käsitluse piirid, kasutatud meetodid/allikad, töö ülesehitus. Tulemusi ei esitata.
4. Põhiosa (nummerdatud peatükid), projekti puhul näiteks: analüüs ja nõuded, arhitektuur, andmebaas, API, mobiilirakendus, testimine. Iga peatükk on tervik ja seotud eesmärgiga.
5. Kokkuvõte: kas eesmärk täideti, tulemused, järeldused, edasiarendused. Uusi allikaid ega argumente ei lisata.
6. Kasutatud allikad: ainult tekstis viidatud allikad, tähestikulises järjekorras, APA 7.
7. Lisad: igaüks uuelt lehelt, „Lisa 1. Pealkiri", nummerdatud viitamise järjekorras.

Sisukorda, sissejuhatust, kokkuvõtet ja sellele järgnevaid osi **ei nummerdata**.

## Lehekülg ja tekst (juhendi tabel 6)

| Element | Nõue |
|---|---|
| Lehekülg | A4, püstpaigutus |
| Veerised | vasak 3 cm; ülal, all, paremal 2,5 cm |
| Põhitekst | Times New Roman 12 pt, kogu töös sama kirjatüüp |
| Reavahe | 1,5; tabelites, allkirjades, plokktsitaatides ja allikaloendis 1,0 |
| Joondus | rööpjoondus; pealkirjad, loetelud ja allikaloend vasakule |
| Lõigud | esimese rea taane 0 cm; lõigu järel 6 pt; tühja lõiku ei lisata |
| 1. taseme pealkiri | Bold, SUURTÄHTEDEGA, 16 pt, vasakul; peatükk algab uuelt lehelt; ees ja järel 12 pt |
| 2. taseme pealkiri | Bold, 14 pt |
| 3. taseme pealkiri | Bold, 12 pt |
| Pealkirjad | automaatne mitmetasandiline numeratsioon (1., 1.1.); lõppu punkti ei panda; lühendeid ei kasutata |
| Leheküljenumber | all paremal; tiitellehel ei kuvata |
| Tekstivärv | must |
| Fail | DOCX tööprotsessis, PDF esitamisel; failinimi `Perenimi_Eesnimi_Rühm_Tööliik` |

## Tabelid, joonised, kood

- Tabeli pealkiri tabeli **kohal**: `Tabel 1. Pealkiri` (vasakul, lõpus punkti pole).
- Joonise allkiri joonise **all**: `Joonis 1. Pealkiri`.
- Tabelid ja joonised nummerdatakse eraldi, läbivalt; paigutatakse pärast tekstiviidet ja joondatakse vasakule.
- Programmikood: monospace, eelistatult Consolas 10 pt, süntaksi esiletõstmisega; igale koodinäitele viidatakse tekstis. Üle lehekülje pikkune kood läheb lisasse. Kogu kood on GitHubis, töös näidatakse ainult tähtsamad osad.

## Keel ja stiil

- Akadeemiline, objektiivne stiil; umbisikuline vorm („töös analüüsitakse", „rakenduses kasutati").
- Ühtsed terminid; võõrkeelne termin ilma eestikeelse vasteta kaldkirjas; eesti termin + sulgudes võõrkeelne vaste, nt pöördumine (*request*).
- Lühend: esmakordselt täisnimetus ja sulgudes lühend, nt Representational State Transfer (REST).
- Programmi- ja süsteeminimed püstkirjas suure algustähega (Visual Studio, GitHub, SQLite).
- Arvud 0–9 sõnadega, suuremad numbritega; kuupäevad kujul 02.02.2026.

## Viitamine (APA 7)

- Tekstisisene viide: (Autor, aasta) või Autor (aasta); autorita veebilehel organisatsioon ja (s.a.).
- Näide allikaloendis: `Microsoft. (s.a.). ASP.NET Core documentation. https://learn.microsoft.com/aspnet/core/`

## Tehisaru kasutamine (juhendi peatükk 6)

Kui tehisaru on töö sisu või koostamisprotsessi olulisel määral mõjutanud, tuleb selle kasutamine töös avalikustada: tööriista nimetus, kasutamise ajavahemik, eesmärk ning kuidas väljundit kasutati ja kontrolliti. Õpilane peab suutma selgitada töö käiku, põhjendada valikuid ja tõendada oma panust.

## Kaitsmine (juhendi peatükk 7)

- Ettekanne 5–10 minutit: eesmärgid, probleem, valiku põhjendus, järeldused, ettepanekud, **isiklik panus**.
- Seejärel küsimused ja lõppsõna. Kõik kaitsmisel kasutatavad materjalid peavad olema töös kajastatud.
