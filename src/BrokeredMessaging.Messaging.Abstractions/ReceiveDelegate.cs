using System.Threading.Tasks;

namespace BrokeredMessaging.Abstractions
{
    /// <summary>
    /// A function that can process a received message.
    /// </summary>
    /// <param name="context">The <see cref="ReceiveContext"/> for the recieved message.</param>
    /// <returns>A task that represents the process of the message.</returns>
    public delegate Task ReceiveDelegate(ReceiveContext context);
}
