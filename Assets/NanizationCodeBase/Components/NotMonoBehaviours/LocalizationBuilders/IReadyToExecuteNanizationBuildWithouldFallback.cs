using Naninovel;

namespace NanizationCodeBase.Components.NotMonoBehaviours.LocalizationBuilders
{
    public interface IReadyToExecuteNanizationBuildWithFallback :
        IHaveLocalizationDocument, IHaveLocalizationKey, IHaveFallback
    {
        UniTask<string> LocalizeAsync();
    }

    public interface IReadyToExecuteNanizationBuild :
        IReadyToExecuteNanizationBuildWithFallback,
        ICanSetFallback<IReadyToExecuteNanizationBuildWithFallback>
    {
    }
}