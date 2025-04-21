using System;
using Naninovel;

namespace NanizationCodeBase.Components.NotMonoBehaviours.LocalizationBuilders
{
    public interface INanizationBuild : IHaveLocalizationDocument, IHaveLocalizationKey
    {
        INanizationSubscriber Subscribe(Action<string> callback, bool localizeNow = true);
        UniTask<string> LocalizeAsync();
    }
}