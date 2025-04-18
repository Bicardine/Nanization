using Naninovel;

namespace NanizationCodeBase.Components.NotMonoBehaviours.LocalizationBuilders
{
    public interface IReadyToLocalizeBuild
    {
        UniTask<string> LocalizeAsync();
    }
}