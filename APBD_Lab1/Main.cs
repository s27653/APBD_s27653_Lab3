namespace APBD_Lab1
{
    class Program
    {
        static List<Kontener> wolneKontenery = new List<Kontener>();
        static List<Statek> statki = new List<Statek>();

        static void Main(string[] args)
        {
            bool exit = false;

            while (!exit)
            {
                Menu();
                string input = Console.ReadLine();
                Console.WriteLine();

                switch (input)
                {
                    case "1":
                        StworzKontener();
                        break;
                    case "2":
                        ZaladujKontener();
                        break;
                    case "3":
                        ZaladujKontenerNaStatek();
                        break;
                    case "4":
                        ListaKontenerow();
                        break;
                    case "5":
                        WyjmijKontener();
                        break;
                    case "6":
                        OproznijKontener();
                        break;
                    case "7":
                        ZastapKontener();
                        break;
                    case "8":
                        WymienKontenerZStatkiem();
                        break;
                    case "9":
                        KontenerInfo();
                        break;
                    case "10":
                        StatekInfo();
                        break;
                    case "11":
                        StworzStatek();
                        break;
                    case "0":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Niepoprawny wybór. Spróbuj ponownie.");
                        break;
                }
                if (!exit)
                {
                    Console.WriteLine("\nNaciśnij ENTER <- aby kontynuowac");
                    Console.ReadLine();
                    Console.Clear();
                }
            }
        }

        static void Menu()
        {
            Console.WriteLine("====== MENU APLIKACJI ======");
            Console.WriteLine("1  - Stwórz kontener");
            Console.WriteLine("2  - Załaduj ładunek do kontenera");
            Console.WriteLine("3  - Załaduj kontener na statek");
            Console.WriteLine("4  - Załaduj listę kontenerów na statek");
            Console.WriteLine("5  - Usuń kontener ze statku");
            Console.WriteLine("6  - Rozładuj kontener");
            Console.WriteLine("7  - Zastąp kontener na statku");
            Console.WriteLine("8  - Przenieś kontener między statkami");
            Console.WriteLine("9  - Wypisz informacje o kontenerze");
            Console.WriteLine("10 - Wypisz informacje o statku i jego ładunku");
            Console.WriteLine("11 - Stwórz kontenerowiec");
            Console.WriteLine("0  - Wyjście");
            Console.Write("Twój wybór: ");
        }

        static void StworzKontener()
        {
            Console.WriteLine("Wybierz typ kontenera:");
            Console.WriteLine("1 - Kontener na płyny");
            Console.WriteLine("2 - Kontener na gaz");
            Console.WriteLine("3 - Kontener chłodniczy");
            Console.Write("Twój wybór: ");
            string typ = Console.ReadLine();

            Console.Write("Podaj wysokość: ");
            int wysokosc = int.Parse(Console.ReadLine());
            Console.Write("Podaj wagę własną: ");
            double wagaWlasna = double.Parse(Console.ReadLine());
            Console.Write("Podaj głębokość: ");
            int glebokosc = int.Parse(Console.ReadLine());
            Console.Write("Podaj maksymalną ładowność: ");
            double maksLadownosc = double.Parse(Console.ReadLine());

            Kontener nowyKontener = null;
            switch (typ)
            {
                case "1":
                    Console.Write("Czy ładunek jest niebezpieczny? (tak/nie): ");
                    bool isDangerous = Console.ReadLine().ToLower() == "tak";
                    nowyKontener = new NaPlyny(wysokosc, wagaWlasna, glebokosc, maksLadownosc, isDangerous);
                    break;
                case "2":
                    Console.Write("Podaj ciśnienie: ");
                    double cisnienie = double.Parse(Console.ReadLine());
                    nowyKontener = new NaGaz(wysokosc, wagaWlasna, glebokosc, maksLadownosc, cisnienie);
                    break;
                case "3":
                    NaChlod.Wyswietl();
                    Console.Write("Podaj rodzaj produktu: ");
                    string produkt = Console.ReadLine();
                    Console.Write("Podaj temperaturę w kontenerze: ");
                    double temp = double.Parse(Console.ReadLine());
                    nowyKontener = new NaChlod(wysokosc, wagaWlasna, glebokosc, maksLadownosc, produkt, temp);
                    break;
                default:
                    Console.WriteLine("Niepoprawny typ kontenera.");
                    return;
            }

            if (nowyKontener != null)
            {
                wolneKontenery.Add(nowyKontener);
                Console.WriteLine($"Utworzono kontener o numerze seryjnym: {nowyKontener.numerSeryjny}");
            }
        }

        static void ZaladujKontener()
        {
            Console.Write("Podaj numer seryjny kontenera: ");
            string numer = Console.ReadLine();
            var kontener = wolneKontenery.Find(k => k.numerSeryjny.Equals(numer, StringComparison.OrdinalIgnoreCase));
            if (kontener == null)
            {
                Console.WriteLine("Nie znaleziono kontenera!");
                return;
            }
            Console.Write("Podaj masę ładunku do załadowania : ");
            double masa = double.Parse(Console.ReadLine());
            try
            {
                kontener.Zaladowanie(masa);
                Console.WriteLine("Ładunek został załadowany.");
            }
            catch (OverfillException e)
            {
                Console.WriteLine($"Błąd: {e.Message}");
            }
        }

        static void ZaladujKontenerNaStatek()
        {
            if (statki.Count == 0)
            {
                Console.WriteLine("Brak statków. Najpierw stwórz kontenerowiec (opcja 11).");
                return;
            }
            Console.Write("Podaj numer seryjny kontenera do załadowania : ");
            string numer = Console.ReadLine();
            var kontener = wolneKontenery.Find(k => k.numerSeryjny.Equals(numer, StringComparison.OrdinalIgnoreCase));
            if (kontener == null)
            {
                Console.WriteLine("Nie znaleziono kontenera!");
                return;
            }
            Console.WriteLine("Wybierz statek :");
            for (int i = 0; i < statki.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {statki[i]}");
            }
            int index = int.Parse(Console.ReadLine()) - 1;
            if (index < 0 || index >= statki.Count)
            {
                Console.WriteLine("Niepoprawny wybór statku.");
                return;
            }
            try
            {
                statki[index].DodajKontener(kontener);
                wolneKontenery.Remove(kontener);
                Console.WriteLine("Kontener został załadowany na statek.");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Błąd: {e.Message}");
            }
        }

        static void ListaKontenerow()
        {
            if (statki.Count == 0)
            {
                Console.WriteLine("Brak statków. Najpierw stwórz kontenerowiec (opcja 11).");
                return;
            }
            Console.WriteLine("Wybierz statek:");
            for (int i = 0; i < statki.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {statki[i]}");
            }
            int index = int.Parse(Console.ReadLine()) - 1;
            if (index < 0 || index >= statki.Count)
            {
                Console.WriteLine("Niepoprawny wybór statku.");
                return;
            }
            foreach (var kontener in wolneKontenery)
            {
                try
                {
                    statki[index].DodajKontener(kontener);
                    Console.WriteLine($"Załadowano kontener {kontener.numerSeryjny} na statek.");
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Błąd przy załadunku kontenera {kontener.numerSeryjny}: {e.Message}");
                }
            }
        }

        static void WyjmijKontener()
        {
            if (statki.Count == 0)
            {
                Console.WriteLine("Brak statków.");
                return;
            }
            Console.WriteLine("Wybierz statek:");
            for (int i = 0; i < statki.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {statki[i]}");
            }
            int index = int.Parse(Console.ReadLine()) - 1;
            if (index < 0 || index >= statki.Count)
            {
                Console.WriteLine("Niepoprawny wybór statku.");
                return;
            }
            Console.Write("Podaj numer seryjny kontenera do usunięcia: ");
            string numer = Console.ReadLine();
            var kontener = statki[index].Kontenery.FirstOrDefault(k => k.numerSeryjny.Equals(numer, StringComparison.OrdinalIgnoreCase));
            if (kontener == null)
            {
                Console.WriteLine("Nie znaleziono kontenera na statku.");
                return;
            }
            statki[index].Usuwanie(kontener);
            Console.WriteLine("Kontener został usunięty ze statku.");
        }

        static void OproznijKontener()
        {
            Console.Write("Podaj numer seryjny kontenera do rozładunku: ");
            string numer = Console.ReadLine();
            var kontener = wolneKontenery.Find(k => k.numerSeryjny.Equals(numer, StringComparison.OrdinalIgnoreCase));
            if (kontener == null)
            {
                Console.WriteLine("Nie znaleziono kontenera.");
                return;
            }
            kontener.Oproznienie();
            Console.WriteLine("Kontener został rozładowany.");
        }

        static void ZastapKontener()
        {
            if (statki.Count == 0)
            {
                Console.WriteLine("Brak statków.");
                return;
            }
            Console.WriteLine("Wybierz statek:");
            for (int i = 0; i < statki.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {statki[i]}");
            }
            int index = int.Parse(Console.ReadLine()) - 1;
            if (index < 0 || index >= statki.Count)
            {
                Console.WriteLine("Niepoprawny wybór statku.");
                return;
            }
            Console.Write("Podaj numer seryjny kontenera do zastąpienia: ");
            string numer = Console.ReadLine();
            var staryKontener = statki[index].Kontenery.FirstOrDefault(k => k.numerSeryjny.Equals(numer, StringComparison.OrdinalIgnoreCase));
            if (staryKontener == null)
            {
                Console.WriteLine("Nie znaleziono kontenera na statku.");
                return;
            }
            Console.WriteLine("Tworzymy nowy kontener do zastąpienia:");
            StworzKontener();
            var nowyKontener = wolneKontenery.Last();
            statki[index].Usuwanie(staryKontener);
            try
            {
                statki[index].DodajKontener(nowyKontener);
                Console.WriteLine("Kontener został zastąpiony.");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Błąd przy zastępowaniu kontenera: {e.Message}");
            }
        }

        static void WymienKontenerZStatkiem()
        {
            if (statki.Count < 2)
            {
                Console.WriteLine("Potrzebne są co najmniej dwa statki.");
                return;
            }
            Console.WriteLine("Wybierz statek źródłowy:");
            for (int i = 0; i < statki.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {statki[i]}");
            }
            int indexSource = int.Parse(Console.ReadLine()) - 1;
            if (indexSource < 0 || indexSource >= statki.Count)
            {
                Console.WriteLine("Niepoprawny wybór statku źródłowego.");
                return;
            }
            Console.Write("Podaj numer seryjny kontenera do przeniesienia: ");
            string numer = Console.ReadLine();
            var kontener = statki[indexSource].Kontenery.FirstOrDefault(k => k.numerSeryjny.Equals(numer, StringComparison.OrdinalIgnoreCase));
            if (kontener == null)
            {
                Console.WriteLine("Nie znaleziono kontenera na statku źródłowym.");
                return;
            }
            Console.WriteLine("Wybierz statek docelowy:");
            for (int i = 0; i < statki.Count; i++)
            {
                if (i == indexSource) continue;
                Console.WriteLine($"{i + 1}. {statki[i]}");
            }
            int indexDest = int.Parse(Console.ReadLine()) - 1;
            if (indexDest < 0 || indexDest >= statki.Count)
            {
                Console.WriteLine("Niepoprawny wybór statku docelowego.");
                return;
            }
            try
            {
                statki[indexSource].Usuwanie(kontener);
                statki[indexDest].DodajKontener(kontener);
                Console.WriteLine("Kontener został przeniesiony.");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Błąd przy przenoszeniu kontenera: {e.Message}");
            }
        }
        
        //omg mam już dość

        static void KontenerInfo()
        {
            if (wolneKontenery.Count == 0)
            {
                Console.WriteLine("Brak dostępnych kontenerów.");
                return;
            }
            Console.WriteLine("Dostępne kontenery:");
            foreach (var k in wolneKontenery)
            {
                Console.WriteLine(k);
            }
            Console.Write("Podaj numer seryjny kontenera: ");
            string numer = Console.ReadLine();
            var kontener = wolneKontenery.Find(k => k.numerSeryjny.Equals(numer, StringComparison.OrdinalIgnoreCase));
            if (kontener == null)
            {
                Console.WriteLine("Nie znaleziono kontenera.");
                return;
            }
            Console.WriteLine(kontener);
        }

        static void StatekInfo()
        {
            if (statki.Count == 0)
            {
                Console.WriteLine("Brak statków.");
                return;
            }
            Console.WriteLine("Wybierz statek:");
            for (int i = 0; i < statki.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {statki[i]}");
            }
            int index = int.Parse(Console.ReadLine()) - 1;
            if (index < 0 || index >= statki.Count)
            {
                Console.WriteLine("Niepoprawny wybór statku.");
                return;
            }
            Console.WriteLine(statki[index]);
            Console.WriteLine("Lista kontenerów na statku:");
            foreach (var k in statki[index].Kontenery)
            {
                Console.WriteLine(k);
            }
        }

        static void StworzStatek()
        {
            Console.Write("Podaj maksymalną prędkość statku ): ");
            double predkosc = double.Parse(Console.ReadLine());
            Console.Write("Podaj maksymalną liczbę kontenerów: ");
            int liczba = int.Parse(Console.ReadLine());
            Console.Write("Podaj maksymalną wagę wszystkich kontenerów (w tonach): ");
            double waga = double.Parse(Console.ReadLine());

            var statek = new Statek(predkosc, liczba, waga);
            statki.Add(statek);
            Console.WriteLine("Stworzono nowy statek.");
        }
    }
}
