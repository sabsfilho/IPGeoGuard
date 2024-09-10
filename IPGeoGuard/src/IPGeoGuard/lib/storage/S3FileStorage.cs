namespace IPGeoGuard.lib.storage;
internal class S3FileStorage : AFileStorage
{
    public S3FileStorage(string serviceName) : base(serviceName)
    {
    }

    internal override string? Read()
    {
        return 
            S3Control.DownloadSingleFile(
                Function.IPGEOGUARD_S3_STORAGE_REPO_NAME,
                GetFileName(),
                false
            );
    }

    internal override void Persist(string json)
    {
        S3Control.UploadSingleFile(
            Function.IPGEOGUARD_S3_STORAGE_REPO_NAME,
            GetFileName(),
            json
        );
    }
}