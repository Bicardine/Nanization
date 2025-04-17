namespace NanizationCodeBase.Components.NotMonoBehaviours
{
    public interface INanizationSubscriber
    {
        void Pause();
        
        void Resume(bool localizeNow = true);
        
        bool IsActive { get; }
        
        void Localize();
        
        void Unsubscribe();
    }
}