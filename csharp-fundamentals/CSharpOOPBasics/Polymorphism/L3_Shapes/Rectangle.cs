public class Rectangle : Shape
{
    private double Height;
    private double Width;

    public Rectangle(double height, double width)
    {
        Height = height;
        Width = width;
    }

    public override double CalculateArea()
    {
        return Height * Width;
    }

    public override double CalculatePerimeter()
    {
        return 2 * (Height + Width);
    }

    public override string Draw()
    {
        return base.Draw() + "Rectangle";
    }
}
