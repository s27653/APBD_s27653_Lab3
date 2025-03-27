namespace APBD_Lab1;

using System;
using System.Collections.Generic;
using System.Linq;

public class Statek
{
    private List<Kontener> kontenery = new List<Kontener>();

    public double maxPredkosc { get;  set; }
    public int maxIlosc { get;  set; }
    public double maxWagaWszystkich { get;  set; }

    public IReadOnlyList<Kontener> Kontenery => kontenery.AsReadOnly();

    public Statek(double maxPredkosc, int maxIlosc, double maxWagaWszystkich)
    {
        this.maxPredkosc = maxPredkosc;
        this.maxIlosc = maxIlosc;
        this.maxWagaWszystkich = maxWagaWszystkich;
    }

//=================================================================    
    
    public void DodajKontener(Kontener kontener)
    {
        if (kontenery.Count >= maxIlosc)
        {
            throw new Exception("Przekroczono max liczbe kontenerow");
        }

        //w tonach
        double aktualnaWaga = Laczna(); // do zrobienia
        
        double wagaNowego = (kontener.wlasnaWaga + kontener.aktualnaMasa) / 1000;
        
        if (aktualnaWaga + wagaNowego > maxWagaWszystkich)
        {
            throw new Exception("Przekroczono max laczna wage kontenerów");
        }
        
        kontenery.Add(kontener);
    }
    
//=================================================================

    public void Usuwanie(Kontener kontener)
    {
        kontenery.Remove(kontener);
    }
    
//=================================================================

    public double Laczna()
    {
        return kontenery.Sum(k => (k.wlasnaWaga + k.aktualnaMasa) / 1000);
    }
    
//=================================================================

    public override string ToString()
    {
        return $"Kontenerowiec:\n" +
               $"- Max predkosc : {maxPredkosc}\n" +
               $"- Ilosc kontntenerow zaladowanych : {kontenery.Count}/{maxIlosc}\n" +
               $"- Laczna waga kontenerow : {Laczna():F2}/{maxWagaWszystkich}\n";  //do naprawy
    }
}