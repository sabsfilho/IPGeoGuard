namespace IPGeoGuard.lib.storage;
internal class LocalFileStorage : AFileStorage
{
    public LocalFileStorage(string serviceName) : base(serviceName)
    {
    }

    internal override string? Read()
    {
        var fp = GetPath();
        if (File.Exists(fp))
        {
            return File.ReadAllText(fp);
        }
        return null;
    }

    private string GetPath()
    {
        string path = Function.IPGEOGUARD_LOCAL_STORAGE_REPO_NAME;
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        return $"{path}/{GetFileName()}";
    }

    internal override void Persist(string json)
    {
        File.WriteAllText(GetPath(), json);
    }
}