# Reflektion — Musiklistappen

**Namn:** [Emma Löwing]
**Datum:** [2026-10-31]

---

## Vad var svårast?

Jag tyckte det svåraste var att förstå hur Program.cs, DbHandler och Song samarbetar.

---

## Vad lärde du dig?

Jag lärde mig att koppla C#-program till min egen SQLite-databas, att läsa databasrader och skapa Song-objekt av dem samt att testa en funktion i taget och göra commits efter fungerande delar. 

---

## Hur använde du ADO.NET i praktiken — vad kändes naturligt och vad var förvirrande?

Jag använde ADO.NET för att öppna databasanslutningen, skapa kommandon och köra SQL med parametrar. ExecuteReader() läser låtar, ExecuteScalar() kontrollerar om album finns och ExecuteNonQuery() lägger till eller tar bort data. Jag använde using för att frigöra resurser automatiskt.

SQL var bekant, men jag behövde stöd för att förstå hur delarna samarbetar och hur databasrader blir C#-objekt.

---

## Varför ska man använda parameteriserade queries?

Parameteriserade queries gör att användarens inmatning behandlas som data och inte som SQL-kod. Det skyddar mot SQL injection. Om man i stället klistrar ihop SQL med användarinmatning kan någon manipulera frågan och exempelvis läsa eller radera data. Parameternamnen i SQL-frågan måste matcha namnen i `AddWithValue`.


---

## Om du hade mer tid — vad hade du lagt till eller gjort annorlunda?

Om jag hade mer tid så skulle jag utveckla appen så att albumets namn och id visas när användaren lägger till en låt, jag skulle förhindra tomma titlar och längder som är noll eller negativa. Jag skulle även vilja lägga till sökning på låttitel.

---

## (VG) Varför delade du upp koden som du gjorde?

Program.cs ansvarar för meny, inmatning, bekräftelse och utskrifter. DbHandler ansvarar för anslutningar, SQL och databasoperationer. Song håller informationen om en låt. Uppdelningen gör koden tydligare och hjälper mig hitta var ändringar ska göras. Databasmetoder kan återanvändas utan att menylogiken skrivs om. Om allt låg i samma fil skulle användargränssnitt och databaskod blandas, vilket skulle göra felsökning och underhåll svårare.

