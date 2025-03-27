namespace APBD_Lab1;
using System;

public class OverfillException : Exception {
    public OverfillException(string n) : base(n)
    {
    }
}

//================================================================

public abstract class Kontener{
    public double aktualnaMasa {get; set;}
    
    public int wysokosc {get; set;} // centymetry
    public double wlasnaWaga {get; set;} // kilogramy
    public int glebokosc {get; set;} // centymetry
    
    public double maxLadownosc {get; set;}
    
    public string typSeryjny {get; set;}
    
    public string numerSeryjny {get; set;}

    private static int licznik = 1;

    public Kontener( int wysokosc, double wlasnaWaga, int glebokosc, string typSeryjny, double maxLadownosc)
    {
        this.wysokosc = wysokosc;
        this.wlasnaWaga = wlasnaWaga;
        this.glebokosc = glebokosc;
        this.typSeryjny = typSeryjny;
        this.maxLadownosc = maxLadownosc;
        aktualnaMasa = 0;

        
        this.numerSeryjny = $"KON-{typSeryjny}-{licznik}";
        licznik++;
    }

    public virtual void Oproznienie() 
    {
        aktualnaMasa = 0;
    }

    public virtual void Zaladowanie(double m) 
    {
        if (aktualnaMasa + m > maxLadownosc)
        {
            throw new OverfillException(
                $"Nie mozna zaladowac wiecej niż przewizywana masa ({m})");
        }
        aktualnaMasa += m;
    }

    public override string ToString()
    {
        return $"Kontener {numerSeryjny}:\n Aktualny ladunek: {aktualnaMasa} kg \n Max ladownosc: {maxLadownosc} kg";
    }
}
