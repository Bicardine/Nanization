using System;

namespace NanizationCodeBase.Components.NotMonoBehaviours.LocalizationBuilders
{
    public interface IReadyToExecuteNanizationBuildWithFallback :
        INanizationBuild, IHaveLocalizationDocument, IHaveLocalizationKey, IHaveFallback
    {
    }

    public interface IReadyToExecuteNanizationBuild :
        INanizationBuild, ICanSetFallback<IReadyToExecuteNanizationBuildWithFallback>
    {
    }
}