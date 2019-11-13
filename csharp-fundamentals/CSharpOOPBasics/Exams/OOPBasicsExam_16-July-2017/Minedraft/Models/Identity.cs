public abstract class Identity
{
    private string id;

    public string Id
    {
        get { return Id; }
        protected set { Id = value; }
    }


    protected Identity(string id)
    {
        this.Id = id;
    }
}
