namespace P2_BookShop
{
    public class GoldenEditionBook : Book
    {
        public override decimal Price
        {
            get => base.Price;
            set
            {
                base.Price = value * 1.30m;
            }
        }

        public GoldenEditionBook(string author, string title, decimal price)
            : base(author, title, price)
        {
            base.type = "GoldenEditionBook";
        }

    }
}
