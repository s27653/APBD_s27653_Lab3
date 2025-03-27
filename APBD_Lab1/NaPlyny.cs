namespace APBD_Lab1;

public class NaPlyny : Kontener, InterfaceHazard
{
    public bool isDengerous { get;  set; }

    public NaPlyny(int wysokosc, double wlasnaWaga, int glebokosc, double maxLadownosc, bool isDengerous)
    : base(wysokosc, wlasnaWaga, glebokosc, "L", maxLadownosc)
    {
        this.isDengerous = isDengerous;
    }

    public void PowiadomProblem(string kontenerNum, string m)
    {
        Console.WriteLine($"PROBLEM : {kontenerNum}, {m}");
    }
    
//==================================================================    

    public override void Zaladowanie(double m)
    {
        double limit = isDengerous ? 0.5 : 0.9;
        double dozwolonaMasa = maxLadownosc * limit;

        if (aktualnaMasa + m > dozwolonaMasa)
        {
            PowiadomProblem(numerSeryjny, $"Proba zaladowania ponad {limit * 100}% pojemnosci");
            
            throw new OverflowException($"Przekroczono dopuszczalne {limit * 100}% ładownosci (kontener {numerSeryjny}).");
        }

        if (aktualnaMasa + m > maxLadownosc)
        {
            PowiadomProblem(numerSeryjny, "Proba zaladowania ponad max ladownosc");
            throw new OverfillException($"Przekroczono max ładownosc (kontener {numerSeryjny}).");
        }
        
        base.Zaladowanie(m);
    }
}