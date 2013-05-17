using System.Runtime.Serialization;
using AsbaBank.Core.Commands;

namespace AsbaBank.ApplicationService.Commands
{
    [DataContract]
    public class ListClients : ICommand
    {
    }
}