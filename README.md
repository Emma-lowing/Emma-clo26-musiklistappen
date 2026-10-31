# Musiklistappen — [Emma Löwing]


---

## Vad appen gör

Det är en konsolapp för Taylor Swifts musik. Mer specifikt för albumen Fearless, Red, 1989, folklore och Midnights. I appen kan användaren visa, lägga till och ta bort låtar. Appen visar också låtar tillsammans med deras album.

## Så kör du den
Du behöver .NET 10 SDK installerat.

Klona projektet och kör följande i terminalen:

```bash
git clone https://github.com/Emma-lowing/Emma-clo26-musiklistappen.git
cd Emma-clo26-musiklistappen/MusikApp
dotnet build
dotnet run
```

Databasfilen `musik.db` ska ligga i mappen `MusikApp`, tillsammans med `appsettings.json`. Anslutningssträngen i `appsettings.json` ska vara `Data Source=musik.db`.

## Så ser huvudmenyn ut

<img width="804" height="670" alt="image" src="https://github.com/user-attachments/assets/43580d07-2509-4167-9505-ea4e11db3a69" /> 

---

## Mina designval (VG)

1. **Hur designade du din databas? Varför valde du just den strukturen?**

Jag delade upp databasen i fem tabeller: Artists, Albums, Songs och SongGenres. Det minskar upprepningen av information och gör att databasen är lättare att underhålla. Artisterns namn sparas exempelvis i Artists istället för att upprepas för varja låt, och albuminformationen sparas i album. Tabellerna kopplas ihop med primärnycklar och främmande nycklar. SongGenrer är en kopplingstabell som gör att en låt kan tillhöra flera genrer och att en genre kan innehålla flera låtar. Då behöcer jag inte lagra flera genrer i samma cell. Uppdelningen stöder normalisering och gör att informationen kan ändras på ett ställe.

2. **Vilket SQL-krav valde du (JOIN eller subquery) och varför passade det din app?**

Jag valde att använda JOIN för att visa låtar tillsammans med deras album. Låttiteln finns i tabllen Songs och albumtiteln finns i Albums. Genom att koppla Songs.album_id till Albums.id kan appen hämta båda titlarna i samma SQL fråga. Det passar min app eftersom användaren kan se vilket album varje låt tillhör, istället för att bara se ett album-ID. Funktionen finns som ett eget alternativ i huvudmenyn.

3. **Vad skulle du gjort annorlunda om du byggde om den från scratch?**

Om jag byggde om appen från början skulle jag göra det enklare att välja album när en låt läggs till. Nu behöver användaren känna till albumets ID. Jag skulle istället visa en lista med albumens namn och ID ditekt i appen. Jag skulle också förbättra valideringen så att titeln inte kan vara tom och längden måste vara större än noll. Det skulle göra appen mer användarvänlig och minska risken för att felaktiga uppgifter sparas.
