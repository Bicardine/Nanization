namespace NanizationCodeBase.Components.NotMonoBehaviours.LocalizationBuilders
{
    public interface IReadyToExecuteNanizationBuildWithFallback :
        IReadyToLocalizeBuild, IHaveLocalizationDocument, IHaveLocalizationKey, IHaveFallback
    {
    }

    public interface IReadyToExecuteNanizationBuild :
        IReadyToLocalizeBuild, IHaveLocalizationDocument, IHaveLocalizationKey, ICanSetFallback<IReadyToExecuteNanizationBuildWithFallback>
    {
    }
}