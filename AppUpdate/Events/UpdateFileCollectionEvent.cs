using AppUpdate.Core;
using AppUpdate.Core.Models;
using Microsoft.Practices.Prism.PubSubEvents;

namespace AppUpdate.Events
{
    public sealed class UpdateFileCollectionEvent: PubSubEvent<IUpdateFileCollection>
    {
    }
}
