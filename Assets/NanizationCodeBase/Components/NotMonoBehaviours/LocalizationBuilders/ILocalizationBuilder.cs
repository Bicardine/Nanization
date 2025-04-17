namespace NanizationCodeBase.Components.NotMonoBehaviours.LocalizationBuilders
{
    public interface ILocalizationBuilder
    {
        IWithKeyNanizationBuild WithDocument(string documentName);
        IReadyToExecuteNanizationBuild WithKey(string key);
    }
}