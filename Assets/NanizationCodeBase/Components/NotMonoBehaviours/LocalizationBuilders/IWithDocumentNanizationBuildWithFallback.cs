namespace NanizationCodeBase.Components.NotMonoBehaviours.LocalizationBuilders
{
    public interface IWithDocumentNanizationBuildWithFallback : IHaveLocalizationDocument, IHaveFallback
    {
        IReadyToExecuteNanizationBuildWithFallback WithKey(string key);
    }

    public interface IWithDocumentNanizationBuild : IHaveLocalizationDocument, ICanSetFallback<IWithDocumentNanizationBuildWithFallback>
    {
        IReadyToExecuteNanizationBuild WithKey(string key);
    }
}