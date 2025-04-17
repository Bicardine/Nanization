namespace NanizationCodeBase.Components.NotMonoBehaviours.LocalizationBuilders
{
    public interface IWithKeyNanizationBuildWithFallback : IHaveLocalizationKey, IHaveFallback
    {
        IReadyToExecuteNanizationBuildWithFallback WithDocument(string documentName);
    }

    public interface IWithKeyNanizationBuild : IHaveLocalizationKey, ICanSetFallback<IWithKeyNanizationBuildWithFallback>
    {
        IReadyToExecuteNanizationBuild WithDocument(string documentName);
    }
}