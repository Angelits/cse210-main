using System;

public class Fraction
{
    private int _top;
    private int _bottom;

    public Fraction()
    {
        _top = 1;
        _bottom = 1;
    }

    public Fraction(int wholeNumber)
    {
        _top = wholeNumber;
        _bottom = 1;
    }

    public Fraction(int top, int bottom)
    {
        _top = top;
        _bottom = bottom;
    }

    public string GetFractionString()
    {
        return $"{_top}/{_bottom}";
    }

    public double GetDecimalValue()
    {
        return (double)_top / (double)_bottom;
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        
        Fraction fraction1 = new Fraction();          
        Fraction fraction2 = new Fraction(1);        
        Fraction fraction3 = new Fraction(5);        
        Fraction fraction4 = new Fraction(3, 4);     
        Fraction fraction5 = new Fraction(1, 3);     

        
        
        Console.WriteLine(fraction2.GetFractionString());
        Console.WriteLine(fraction2.GetDecimalValue());
        Console.WriteLine(fraction3.GetFractionString());
        Console.WriteLine(fraction3.GetDecimalValue());
        Console.WriteLine(fraction4.GetFractionString());
        Console.WriteLine(fraction4.GetDecimalValue());
        Console.WriteLine(fraction5.GetFractionString());
        Console.WriteLine(fraction5.GetDecimalValue());
    }
}