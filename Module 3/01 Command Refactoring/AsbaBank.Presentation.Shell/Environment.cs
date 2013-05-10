using AsbaBank.Infrastructure;
using AsbaBank.Presentation.Shell.Factories;

namespace AsbaBank.Presentation.Shell
{
    public static class Environment
    {
        private static readonly InMemoryDataStore DataStore;
        public static readonly ILog Logger;
        public static readonly CommandFactory CommandFactory;

        static Environment()
        {
            DataStore = new InMemoryDataStore();
            Logger = new ConsoleWindowLogger();
            CommandFactory = new CommandFactory();
        }

        public static void ExecuteCommand(string[] split)
        {
            CommandFactory.ExecuteCommand(split);
        }

        public static IUnitOfWork GetUnitOfWork()
        {
            return new InMemoryUnitOfWork(DataStore);
        }
    }
}