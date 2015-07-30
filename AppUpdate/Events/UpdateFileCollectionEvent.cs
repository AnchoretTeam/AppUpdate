using AppUpdate.Core;
using Microsoft.Practices.Prism.PubSubEvents;

namespace AppUpdate.Events
{
    public sealed class UpdateFileCollectionEvent: PubSubEvent<IUpdateFileCollection>
    {
    }
}
