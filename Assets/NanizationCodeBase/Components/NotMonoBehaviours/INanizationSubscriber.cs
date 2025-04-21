using System;
using NanizationCodeBase.Model.Data;

namespace NanizationCodeBase.Components.NotMonoBehaviours
{
    public interface INanizationSubscriber : IHaveId<Guid>, IDisposable
    {
        bool IsActive { get; }
        
        void Pause();
        
        void Resume(bool localizeNow = true);
        
        void Localize();
    }
}