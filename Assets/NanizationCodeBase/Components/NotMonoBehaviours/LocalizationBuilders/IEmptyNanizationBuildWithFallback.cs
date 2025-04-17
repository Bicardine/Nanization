namespace NanizationCodeBase.Components.NotMonoBehaviours.LocalizationBuilders
{
    public interface IEmptyNanizationBuildWithFallback : IHaveFallback
    {
        IWithDocumentNanizationBuildWithFallback WithDocument(string document);
        IWithKeyNanizationBuildWithFallback WithKey(string key);
    }

    public interface IEmptyNanizationBuild : ICanSetFallback<IEmptyNanizationBuildWithFallback>
    {
        IWithDocumentNanizationBuild WithDocument(string document);
        IWithKeyNanizationBuild WithKey(string key);
    }
}