using System;

namespace ClassBox
{
    public class Box
    {
        private double length;
        private double width;
        private double height;

        private double Length
        {
            get => this.length;
            set
            {
                if (value <= 0)
                {
                    Console.WriteLine("Length cannot be zero or negative.");
                }
                this.length = value;
            }
        }

        private double Width
        {
            get => this.width;
            set
            {
                if (value <= 0)
                {
                    Console.WriteLine("Width cannot be zero or negative.");
                }
                this.width = value;
            }
        }

        private double Height
        {
            get => this.height;
            set
            {
                if (value <= 0)
                {
                    Console.WriteLine("Height cannot be zero or negative.");
                }
                this.height = value;
            }
        }

        public Box(double length, double width, double height)
        {
            this.Length = length;
            this.Width = width;
            this.Height = height;
        }

        //Surface Area = 2lw + 2lh + 2wh
        public double SurfaceArea()
        {
            double result = 2 * (this.Length * this.Width) + 2 * (this.Length * this.Height) + 2 * (this.Width * this.Height);
            return result;
        }

        //Lateral Surface Area = 2lh + 2wh
        public double LateralSurfaceArea()
        {
            double result = 2 * (this.Length * this.Height) + 2 * (this.Width * this.Height);
            return result;
        }

        //Volume = lwh
        public double Volume()
        {
            double result = this.Length * this.Height * this.Width;
            return result;
        }
    }
}
