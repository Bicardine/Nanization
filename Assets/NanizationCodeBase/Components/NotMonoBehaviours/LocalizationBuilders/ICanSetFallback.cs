namespace NanizationCodeBase.Components.NotMonoBehaviours.LocalizationBuilders
{
    public interface ICanSetFallback<T> 
    {
        T SetFallback(string fallback);
    }
}