namespace IPGeoGuard.lib.storage;
internal static class FileStorageHandler
{
    public static AFileStorage? GetFileStorage(string serviceName)
    {
        if (Function.USE_LOCAL_FILE_STORAGE_CACHE)
        {
            return new LocalFileStorage(serviceName);
        }
        if (Function.USE_S3_STORAGE_CACHE)
        {
            return new S3FileStorage(serviceName);
        }
        return null;
    }
}