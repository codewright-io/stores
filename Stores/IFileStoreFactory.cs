namespace DevKnack.Stores
{
    public interface IFileStoreFactory
    {
        IFileStore? Create(string url);

        IFileStore? CreateInternal(string url);
    }
}
