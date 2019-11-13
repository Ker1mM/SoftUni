using Logger.Architecture.Interfaces;

namespace Logger.Architecture.Layouts
{
    public class SimpleLayout : ILayout
    {
        public string Format => "{0} - {1} - {2}";
    }
}
