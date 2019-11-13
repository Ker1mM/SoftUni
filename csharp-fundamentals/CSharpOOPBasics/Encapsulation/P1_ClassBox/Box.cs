namespace P1_ClassBox
{
    public class Box
    {
        private double length;
        private double width;
        private double height;

        public Box(double length, double width, double height)
        {
            this.length = length;
            this.width = width;
            this.height = height;
        }

        //Surface Area = 2lw + 2lh + 2wh
        public double SurfaceArea()
        {
            double result = 2 * (this.length * this.width) + 2 * (this.length * this.height) + 2 * (this.width * this.height);
            return result;
        }

        //Lateral Surface Area = 2lh + 2wh
        public double LateralSurfaceArea()
        {
            double result = 2 * (this.length * this.height) + 2 * (this.width * this.height);
            return result;
        }

        //Volume = lwh
        public double Volume()
        {
            double result = this.length * this.height * this.width;
            return result;
        }
    }
}
