namespace NanizationCodeBase.Components.NotMonoBehaviours.LocalizationBuilders
{
    public interface INanizationBuild
    {
        IWithDocumentNanizationBuildWithFallback WithDocument(string documentName);
        IWithKeyNanizationBuild WithKey(string key);
    }
}