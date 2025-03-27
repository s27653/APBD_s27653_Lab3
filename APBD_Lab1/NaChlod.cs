namespace APBD_Lab1;

using System.Collections.Generic;

public class NaChlod : Kontener
{
    private static Dictionary<string, double> minTemp = new Dictionary<string, double>
    {
        //z pdf
        { "Banany", 13.3 },
        { "Czekolada", 18 },
        { "Ryba", 2 },
        { "Mieso", -15 },
        { "Lody", -18 },
        { "Pizza", -30 },
        { "Ser", 7.2 },
        { "Schab", 5 },
        { "Maslo", 20.5 },
        { "Jaja", 19 }
    };
    
    public string rodzajProduktu { get;  set; }
    public double tempKontenera { get;  set; }

    //do zrobienia
    public NaChlod(int wysokosc, double wagaWlasna, int glebokosc, double maksLadownosc, string rodzajProduktu, double tempKontenera)
        : base(wysokosc, wagaWlasna, glebokosc, "C", maksLadownosc)
    {
        rodzajProduktu = rodzajProduktu;

        if (minTemp.ContainsKey(rodzajProduktu))
        {
            double min = minTemp[rodzajProduktu];
            if (tempKontenera < min)
            {
                throw new Exception(
                    $"Temperatura kontenera ({tempKontenera}°C) " +
                    $"nie może być niższa niż {min} dla produktu {rodzajProduktu}.");
            }            
        }
        else
        {
            throw new Exception($"Nieznany produkt: {rodzajProduktu}");
        }
        
        this.tempKontenera = tempKontenera;
    }
    
    public static void Wyswietl()
    {
        Console.WriteLine("Dostępne produkty i wymagane minimalne temperatury : \n");
        foreach (var produkt in minTemp)
        {
            Console.WriteLine($"{produkt.Key}: {produkt.Value}");
        }
    }

    public override string ToString()
    {
        return base.ToString() + $", Produkt : {rodzajProduktu}, Temp: {tempKontenera}";
    }
}