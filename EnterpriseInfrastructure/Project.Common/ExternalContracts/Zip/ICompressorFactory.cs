namespace Project.Common.ExternalContracts.Zip
{
    public interface ICompressorFactory
    {
        ICompressor Get();
    }
}