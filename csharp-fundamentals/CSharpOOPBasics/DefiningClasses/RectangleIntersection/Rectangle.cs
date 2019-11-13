namespace RectangleIntersection
{
    public class Rectangle
    {
        private string id;
        private double width;
        private double height;
        private double x;
        private double y;

        public double Y
        {
            get { return y; }
            set { y = value; }
        }

        public double X
        {
            get { return x; }
            set { x = value; }
        }

        public double Height
        {
            get { return height; }
            set { height = value; }
        }

        public double Width
        {
            get { return width; }
            set { width = value; }
        }

        public string Id
        {
            get { return id; }
            set { id = value; }
        }

        public Rectangle(string id, double width, double height, double x, double y)
        {
            this.Id = id;
            this.Width = width;
            this.Height = height;
            this.X = x;
            this.Y = y;
        }

        public bool Intersect(Rectangle rectangle)
        {
            if (this.X + this.Width < rectangle.X || this.Y - this.height > rectangle.Y)
            {
                return false;
            }

            if (this.X > rectangle.X + rectangle.Width || this.Y < rectangle.Y - rectangle.Height)
            {
                return false;
            }

            return true;
        }
    }
}
