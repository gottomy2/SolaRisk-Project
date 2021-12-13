# Solarisk - Computer Game measuring aptitude to take a risk

![](https://user-images.githubusercontent.com/68858742/145803256-4271e535-9947-4a6d-8b15-e05593a3d950.png)

### Developer Guide

#### Uruchamiając projekt należy:
- Zainstalować następujące paczki:  
![](https://user-images.githubusercontent.com/68858742/145803211-8f86f6eb-1a6f-4f03-9e5a-c39135167187.png)


#### Dodając swój projekt do projektu głównego należy:
- Otworzyć katalog projektu głównego oraz katalog projektu do dodania.
- Przejść do assetów, i kolejno dla każdego podfolderu utworzyć folder o nazwie odnoszącej się do wgrywanego projektu np. Asset: Animations -> tworzymy folder "Menu"
- Do nowo utworzonego folderu przenieść wszystkie pliki np. animacje.
- Proces powtórzyć do momentu aż wszystkie assety z naszego projektu zostaną dorzucone.

#### Zapisywanie danych:
Dane w projekcie zapisywane są w postaci parametrów Serializable Object - Database.
Database znajduje się w folderze Assets/data
Jest on utworzony na bazie skryptu GlobalVars znajdującego się w Assets/Scripts/Utils

##### Dodawanie nowych zmiennych globalnych i odwoływanie się do nich:
- Dodajemy publiczną zmienną klasy GlobalVars
- W pliku skryptowym w którym chcemy używać zmiennej globalnej tworzymy zmienną:
> public GlobalVars global;

- Załączamy obiekt "Database" w unity do naszego skryptu - przykład:
![](https://user-images.githubusercontent.com/68858742/145803286-1c22e5cb-23ce-49c3-b414-ae65bddba769.png)

#### Przełączanie między scenami
W celu połączenia wybranych scen można skorzystać z istniejącego skryptu SwitchScene, w sposób następujący:

- Dodajemy skrypt SceneSwitch jako komponent w głównym obiekcie ze sceny.
- Dodajemy scene do Build Settings
- Następnie pod konkretny przycisk odwołujemy się do jednej z funkcji:
> NextScene() - przeniesie do następnej sceny według kolejności w Build Settings  
PreviousScene() - przeniesie do poprzedniej sceny według kolejności w Build Settings  
SceneByPath() - Przeniesie nas do sceny na podstawie ścieżki do sceny - dodawanie do Build Settings w tym wypadku nie jest konieczne.  






