namespace NanizationCodeBase.Components.NotMonoBehaviours
{
    public interface INanizationSubscriber
    {
        bool IsActive { get; }
        
        void Pause();
        
        void Resume(bool localizeNow = true);
        
        void Localize();
        
        void Unsubscribe();
    }
}