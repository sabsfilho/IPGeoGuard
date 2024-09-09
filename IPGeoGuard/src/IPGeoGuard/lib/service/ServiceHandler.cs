namespace IPGeoGuard.lib.service;
internal class ServiceHandler
{
    private object locker = new object();
    private Dictionary<string, ServiceItemHandler>? serviceItemDic = null;
    private Dictionary<string, ServiceItemHandler> ServiceItemDic
    {
        get
        {
            if (serviceItemDic == null)
            {
                serviceItemDic = new Dictionary<string, ServiceItemHandler>();
            }
            return serviceItemDic;
        }
    }

    internal IPStatInfo GetInfo(string serviceName, string ip, bool incrementHit)
    {
        lock(locker)
        {
            ServiceItemHandler serviceItemHandler;
            if (!ServiceItemDic.TryGetValue(serviceName, out serviceItemHandler!))
            {
                serviceItemHandler = new ServiceItemHandler(serviceName);
                ServiceItemDic.Add(serviceName, serviceItemHandler);
            }

            var info = serviceItemHandler.GetInfo(ip, incrementHit);

            return info;
        }
    }

    private static ServiceHandler? instance = null;
    public static ServiceHandler Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new ServiceHandler();
            }
            return instance;
        }
    }

}