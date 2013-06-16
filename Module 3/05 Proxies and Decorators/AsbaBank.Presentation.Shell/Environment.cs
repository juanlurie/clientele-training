using System.Collections.Generic;
using System.Data.Entity;
using System.Security.Principal;
using AsbaBank.ApplicationService;
using AsbaBank.Core;
using AsbaBank.Core.Commands;
using AsbaBank.DataModel;
using AsbaBank.Infrastructure.CommandProxy;
using AsbaBank.Infrastructure.CommandPublishers;
using AsbaBank.Infrastructure.CommandScripts;
using AsbaBank.Infrastructure.EntityFramework;
using AsbaBank.Infrastructure.Loggers;
using AsbaBank.Presentation.Shell.ShellCommands;
using AsbaBank.Presentation.Shell.SystemCommands;

namespace AsbaBank.Presentation.Shell
{
    public static class Environment
    {
        private static readonly Dictionary<string, ICommandBuilder> CommandBuilders;
        private static readonly Dictionary<string, ISystemCommand> SystemCommands;
        private static readonly ScriptRecorder ScriptRecorder;
        private static readonly IContextFactory ContextFactory;
        private static CurrentUserSession currentUserSession;

        static Environment()
        {
            CommandBuilders = new Dictionary<string, ICommandBuilder>();
            SystemCommands = new Dictionary<string, ISystemCommand>();
            ScriptRecorder = new ScriptRecorder();
            RegsiterSystemCommands();
            RegsiterCommandBuilders();

            Database.SetInitializer(new AsbaContextInitializer());
            ContextFactory = new ContextFactory<AsbaContext>("AsbaBank");
            currentUserSession = new CurrentUserSession(new GenericIdentity(System.Environment.UserDomainName));
        }

        public static IEnumerable<ICommandBuilder> GetShellCommands()
        {
            return CommandBuilders.Values;
        }

        public static IEnumerable<ISystemCommand> GetSystemCommands()
        {
            return SystemCommands.Values;
        }

        public static ICommandBuilder GetShellCommand(string command)
        {
            return CommandBuilders[command.ToUpper()];
        }

        private static void RegsiterSystemCommands()
        {
            RegsiterSystemCommand(new RecordScript());
            RegsiterSystemCommand(new SaveScript());
            RegsiterSystemCommand(new RunScript());
            RegsiterSystemCommand(new ListScripts());
        }

        private static void RegsiterSystemCommand(ISystemCommand command)
        {
            SystemCommands.Add(command.Key.ToUpper(), command);
        }

        private static void RegsiterCommandBuilders()
        {
            RegisterCommandBuilder(new RegisterClientBuilder());
            RegisterCommandBuilder(new UpdateClientAddressBuilder());
        }

        private static void RegisterCommandBuilder(ICommandBuilder commandBuilder)
        {
            CommandBuilders.Add(commandBuilder.Key.ToUpper(), commandBuilder);
        }

        public static void SetCurrentUserRole(params UserRole[] role)
        {
            var roles = role;
            currentUserSession = new CurrentUserSession(new GenericIdentity(System.Environment.UserDomainName), roles);
        }

        public static IPublishCommands GetCommandPublisher()
        {
            var proxy = new CommandPublisherProxy(currentUserSession);
            var retryDecorator = new CommandPublisherRetryDecorator(proxy);
            var commandPublisher = new CommandPublisherLoggerDecorator(retryDecorator, new ConsoleWindowLogger());

            var unitOfWork = new EntityFrameworkUnitOfWork(ContextFactory);

            commandPublisher.Subscribe(new ClientService(unitOfWork));

            return commandPublisher;
        }

        public static ScriptPlayer GetScriptPlayer()
        {
            return new ScriptPlayer(GetCommandPublisher());
        }

        public static ScriptRecorder GetScriptRecorder()
        {
            return ScriptRecorder;
        }

        public static bool IsSystemCommand(string command)
        {
            return SystemCommands.ContainsKey(command.ToUpper());
        }

        public static ISystemCommand GetSystemCommand(string command)
        {
            return SystemCommands[command.ToUpper()];
        }
    }
}