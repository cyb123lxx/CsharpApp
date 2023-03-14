using System;

interface IShape
{
    double GetArea();
    bool IsValid();
}

abstract class Shape : IShape
{
    public abstract double GetArea();
    public abstract bool IsValid();
}

class Rectangle : Shape
{
    private double width;
    private double height;

    public Rectangle(double width, double height)
    {
        this.width = width;
        this.height = height;
    }

    public override double GetArea()
    {
        return width * height;
    }

    public override bool IsValid()
    {
        return width > 0 && height > 0;
    }

    public double Width
    {
        get { return width; }
        set { width = value; }
    }

    public double Height
    {
        get { return height; }
        set { height = value; }
    }
}

class Square : Shape
{
    private double side;

    public Square(double side)
    {
        this.side = side;
    }

    public override double GetArea()
    {
        return side * side;
    }

    public override bool IsValid()
    {
        return side > 0;
    }

    public double Side
    {
        get { return side; }
        set { side = value; }
    }
}

class Triangle : Shape
{
    private double a;
    private double b;
    private double c;

    public Triangle(double a, double b, double c)
    {
        this.a = a;
        this.b = b;
        this.c = c;
    }

    public override double GetArea()
    {
        double p = (a + b + c) / 2;
        return Math.Sqrt(p * (p - a) * (p - b) * (p - c));
    }

    public override bool IsValid()
    {
        return a + b > c && a + c > b && b + c > a;
    }

    public double A
    {
        get { return a; }
        set { a = value; }
    }

    public double B
    {
        get { return b; }
        set { b = value; }
    }

    public double C
    {
        get { return c; }
        set { c = value; }
    }
}

class ShapeFactory
{
    private static Random random = new Random();

    public static Shape CreateShape()
    {
        int type = random.Next(3);

        switch (type)
        {
            case 0:
                return new Rectangle(random.NextDouble() * 10, random.NextDouble() * 10);
            case 1:
                return new Square(random.NextDouble() * 10);
            case 2:
                return new Triangle(random.NextDouble() * 10, random.NextDouble() * 10, random.NextDouble() * 10);
            default:
                throw new Exception("Invalid shape type.");
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        double totalArea = 0;

        for (int i = 0; i < 10; i++)
        {
            Shape shape = ShapeFactory.CreateShape();

            if (shape.IsValid())
            {
                totalArea += shape.GetArea();
            }
        }

        Console.WriteLine("Total area of 10 random shapes: " + totalArea);
    }
}
