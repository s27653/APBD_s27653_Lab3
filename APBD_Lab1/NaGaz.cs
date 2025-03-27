namespace APBD_Lab1;

public class NaGaz : Kontener, InterfaceHazard
{
    public double cisnienie { get;  set; }

    public NaGaz(int wysokosc, double wagaWlasna, int glebokosc, double maksLadownosc, double cisnienie)
        : base(wysokosc, wagaWlasna, glebokosc, "G", maksLadownosc)
    {
        this.cisnienie = cisnienie;
    }

    public void PowiadomProblem(string kontenerNum, string m)
    {
        Console.WriteLine($"Powiadom problem: {kontenerNum}, {m}");
    }

    public override void Oproznienie()
    {
        double reszta = aktualnaMasa * 0.05;
        aktualnaMasa = reszta;
    }

    public override void Zaladowanie(double m)
    {
        if (aktualnaMasa + m > maxLadownosc)
        {
            PowiadomProblem(numerSeryjny, "Proba przekroczenia max ladownocci w kontenerze na gaz");
            throw new OverflowException($"Przekroczono maksymalna ladownosc (kontener {numerSeryjny})");
        }
        base.Zaladowanie(m);
    }
}