﻿using System.Collections.Generic;
using AsbaBank.ApplicationService;
using AsbaBank.Core;
using AsbaBank.Infrastructure;
using AsbaBank.Infrastructure.CommandScripts;
using AsbaBank.Infrastructure.DataStoreSelector;
using AsbaBank.Infrastructure.UnitOfWork;
using AsbaBank.Presentation.Shell.ShellCommands;
using AsbaBank.Presentation.Shell.SystemCommands;

namespace AsbaBank.Presentation.Shell
{
    public static class Environment
    {
        public static ILog Logger;
        private static readonly Dictionary<string, ICommandBuilder> CommandBuilders;
        private static readonly Dictionary<string, ISystemCommand> SystemCommands;
        private static readonly ScriptRecorder ScriptRecorder;

        static Environment()
        {
            Logger = new ConsoleWindowLogger();
            CommandBuilders = new Dictionary<string, ICommandBuilder>();
            SystemCommands = new Dictionary<string, ISystemCommand>();
            ScriptRecorder = new ScriptRecorder();
            RegsiterSystemCommands();
            RegsiterCommandBuilders();
        }

        public static string GetDataStoreType()
        {
            return "You are using a " + ContextFactory.Context.DataStoreName;
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
            RegisterCommandBuilder(new ListClientsBuilder());
        }

        private static void RegisterCommandBuilder(ICommandBuilder commandBuilder)
        {
            CommandBuilders.Add(commandBuilder.Key.ToUpper(), commandBuilder);
        }

        public static IPublishCommands GetCommandPublisher()
        {
            var commandPublisher = new LocalCommandPublisher();

            var unitOfWork = new UnitOfWork((dynamic)ContextFactory.Context);
            commandPublisher.Subscribe(new ClientService(unitOfWork, Logger));

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