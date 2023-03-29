# bfkdo-eu-praxisprojekt

## Programmierrichtlinien

Um eine konstante Codequalität zu bieten, müssen zu jedem Zeitpunkt der Entwicklung folgende Punkte im Code berücksichtigt werden:

*   Jede Methode, Klasse, Eigenschaft, Feld und Konstruktor erfordert einen XML-Kommentar. Dieser wird durch den Compiler erfordert und wird auch bei Fehlen durch eine Compiler-Warnung angezeigt. Der Kommentar muss wie folgt aussehen:
    

```xml
/// <summary>
/// Genaue Beschreibung, welchen Sinn dieser Punkt hat
/// </summary>
```

*   Des Weiteren gelten folgende Benenunngsregeln für Variablen:
    
    *   Klassen: Müssen mit einem Großbuchstaben anfangen
        
        ```
        public class TestClass
        {
        }
        ```
        
    *   Methoden: Müssen mit einem Großbuchstaben anfangen und beschreiben, was diese Methode macht
        
        ```
        public void ConvertStringToInt(string value)
        {
        }
        ```
        
    *   Parameter: Sowohl bei Methoden als auch Konstruktoren müssen die Parameter kleingeschrieben werden
        
    *   Eigenschaften: Eigenschaften müssen mit einem Großbuchstaben beginnen und wenn möglich einen privaten Setter haben. Ebenso ist es erforderlich den Eigenschaften einen Standardwert hinzuzufügen, um eventuellen NullRefrenceExceptions vorzubeugen
        
        ```
        public string Filter { get; private set; } = string.Empty;
        ```
        
    *   Felder: Felder müssen immer mit einem \_ gefolgt von einem Kleinbuchstaben beginnen. Die Felder sollten außerdem, wenn es möglich ist, auf “readonly” gesetzt werden.
        
        ```
        private readonly ILogger _logger;
        ```
        
*   Folgende Reihenfolge muss in einer Klasse eingehalten werden:
    
    *   Klasse
        
    *   Felder
        
    *   Konstruktoren
        
    *   Eigenschaften
        
    *   Methoden
        
*   Zur Verbesserung der Leserlichkeit sollen #regions eingesetzt werden. Diese ermöglichen das Auf- und Zuklappen einzelner Code-Blöcke und ermöglichen es überladene Klassen übersichtlicher zu gestalten.
    

Es wird um strikte Einhaltung der Richtlinien gebeten, da die Nichteinhaltung beim Kompilieren zu Compiler-Warnungen führt und die zur Verfügung gestellten Github Actions keinerlei Programme mit Compiler-Warnungen publisht.

