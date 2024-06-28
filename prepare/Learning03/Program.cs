// Fraction.cs

using System;

public class Fraction
{
    private int numerator;   // Top number of the fraction
    private int denominator; // Bottom number of the fraction

    // Constructor 1: Default constructor, initializes to 1/1
    public Fraction()
    {
        numerator = 1;
        denominator = 1;
    }

    // Constructor 2: Constructor with one parameter (top), initializes denominator to 1
    public Fraction(int top)
    {
        numerator = top;
        denominator = 1;
    }

    // Constructor 3: Constructor with two parameters (top and bottom)
    public Fraction(int top, int bottom)
    {
        if (bottom == 0)
        {
            throw new ArgumentException("Denominator cannot be zero.");
        }
        numerator = top;
        denominator = bottom;
    }

    // Getter and Setter for numerator
    public int Numerator
    {
        get { return numerator; }
        set { numerator = value; }
    }

    // Getter and Setter for denominator
    public int Denominator
    {
        get { return denominator; }
        set
        {
            if (value == 0)
            {
                throw new ArgumentException("Denominator cannot be zero.");
            }
            denominator = value;
        }
    }

    // Method to return the fraction in the form "numerator/denominator"
    public string GetFractionString()
    {
        return $"{numerator}/{denominator}";
    }

    // Method to return the decimal value of the fraction
    public double GetDecimalValue()
    {
        return (double)numerator / denominator;
    }
}
