namespace AsbaBank.Presentation.Shell.Interfaces
{
    public interface IShellCommand
    {
        string Usage { get; }
        string Key { get; }
        ICommand Build(string[] args);
    }
}