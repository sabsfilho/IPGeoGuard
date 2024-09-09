
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

    private ServiceItemHandler GetServiceItemHandler(string serviceName)
    {
        ServiceItemHandler serviceItemHandler;
        if (!ServiceItemDic.TryGetValue(serviceName, out serviceItemHandler!))
        {
            serviceItemHandler = new ServiceItemHandler(serviceName);
            ServiceItemDic.Add(serviceName, serviceItemHandler);
        }
        return serviceItemHandler;
    }

    internal IPStatInfo GetInfo(string serviceName, string ip, bool incrementHit)
    {
        lock(locker)
        {
            ServiceItemHandler serviceItemHandler = GetServiceItemHandler(serviceName);

            var info = serviceItemHandler.GetInfo(ip, incrementHit);

            return info;
        }
    }

    internal ServiceStat? GetView(string serviceName)
    {
        ServiceItemHandler serviceItemHandler = GetServiceItemHandler(serviceName);
        return serviceItemHandler.GetStatView();
    }
    internal void SetCountryRestriction(string serviceName, string country, bool block)
    {
        ServiceItemHandler serviceItemHandler = GetServiceItemHandler(serviceName);
        serviceItemHandler.SetCountryRestriction(country, block, false);
    }
    internal void DeleteCountryRestriction(string serviceName, string country)
    {
        ServiceItemHandler serviceItemHandler = GetServiceItemHandler(serviceName);
        serviceItemHandler.SetCountryRestriction(country, false, true);
        serviceItemHandler.SetCountryRestriction(country, true, true);
    }
    internal void SetCityRestriction(string serviceName, string city, bool block)
    {
        ServiceItemHandler serviceItemHandler = GetServiceItemHandler(serviceName);
        serviceItemHandler.SetCityRestriction(city, block, false);
    }
    internal void DeleteCityRestriction(string serviceName, string city)
    {
        ServiceItemHandler serviceItemHandler = GetServiceItemHandler(serviceName);
        serviceItemHandler.SetCityRestriction(city, false, true);
        serviceItemHandler.SetCityRestriction(city, true, true);
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