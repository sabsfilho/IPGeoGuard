
namespace IPGeoGuard.lib.storage;
internal abstract class AFileStorage
{
    protected string ServiceName { get; private set; }
    internal abstract string? Read();
    internal abstract void Persist(string json);

    public AFileStorage(string serviceName)
    {
        ServiceName = serviceName;
    }
    protected string GetFileName()
    {
        return $"{ServiceName}.json";
    }
}