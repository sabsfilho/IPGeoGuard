using System.Text.Json;

namespace IPGeoGuard.lib.service;
internal class ServiceItemHandler
{
    private const string STORAGE_PATH = "/workspaces/Documents/projects/IPGeoGuard/IPGeoGuard/src/IPGeoGuard/repo/";

    private ServiceItem serviceItem;
    public ServiceItemHandler(string serviceName)
    {
        var fp = GetPath(serviceName);
        if (File.Exists(fp))
        {
            string json = File.ReadAllText(fp);
            serviceItem = JsonSerializer.Deserialize<ServiceItem>(json)!;
        }
        else 
        {
            serviceItem = new ServiceItem(serviceName);
        }
    }

    private Dictionary<string, IPStatInfo>? ipStatInfoDic = null;
    private Dictionary<string, IPStatInfo> IpStatInfoDic
    {
        get
        {
            if (ipStatInfoDic == null)
            {
                ipStatInfoDic = new Dictionary<string, IPStatInfo>();
            }
            return ipStatInfoDic;
        }
    }
    internal ServiceStat? GetStatView()
    {
        return serviceItem.Stat;
    }
    internal IPStatInfo GetInfo(string ip, bool incrementHit)
    {
        bool persist = false;

        IPStatInfo info;
        if (!IpStatInfoDic.TryGetValue(ip, out info!))
        {
            info = RequestIPStatInfo(ip);
            IpStatInfoDic.Add(ip, info);
            persist = true;
        }
        if (incrementHit)
        {
            serviceItem.IncrementHit(ip, info);
            persist = true;
        }
        if (persist)
        {
            Persist();
        }
        return info;
    }

    private static string GetPath(string serviceName)
    {
        return $"{STORAGE_PATH}{serviceName}.json";
    }
    private void Persist()
    {
 //{"ActionType":1,"ServiceName":"ServiceName","IP":"0.0.0.0"}

        string json = JsonSerializer.Serialize(serviceItem);

        File.WriteAllText(GetPath(serviceItem.ServiceName), json);
        
    }

    private IPStatInfo RequestIPStatInfo(string ip)
    {
        var x = IP2LocationHandler.RequestGeolocation(ip);

        bool isAllowed = serviceItem.IsAllowed(x.country, x.city);

        var info = new IPStatInfo()
        {
            Allowed = true,
            City = x.city,
            Country = x.country
        };
        return info;
    }

}