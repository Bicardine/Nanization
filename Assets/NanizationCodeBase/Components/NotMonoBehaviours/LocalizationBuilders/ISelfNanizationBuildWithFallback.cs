namespace NanizationCodeBase.Components.NotMonoBehaviours.LocalizationBuilders
{
    public interface ISelfNanizationBuildWithFallback : IHaveFallback
    {
        IReadyToExecuteNanizationBuildWithFallback WithDocument(string documentName);
        
        IReadyToExecuteNanizationBuildWithFallback AsSelfPath();
    }

    public interface ISelfNanizationBuild : ICanSetFallback<ISelfNanizationBuildWithFallback>
    {
        IReadyToExecuteNanizationBuild WithDocument(string documentName);
        
        IReadyToExecuteNanizationBuild AsSelfPath();
    }
}