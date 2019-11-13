using DungeonsAndCodeWizards.Enums;
using DungeonsAndCodeWizards.Functionality;
using DungeonsAndCodeWizards.Models.Characters;

namespace DungeonsAndCodeWizards
{
    public class StartUp
    {
        // DO NOT rename this file's namespace or class name.
        // However, you ARE allowed to use your own namespaces (or no namespaces at all if you prefer) in other classes.
        public static void Main()
        {
            DungeonMaster dm = new DungeonMaster();
            Engine engine = new Engine(dm);

            engine.Run();
        }
    }
}