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

    internal void SetCountryRestriction(string country, bool block, bool remove)
    {
        var list = serviceItem.CountryList;
        if (list == null) 
        {
            if (remove) return;
            list = new AccessControlList();
            serviceItem.CountryList = list;
        }
        SetRestriction(
            list,
            country,
            block,
            remove
        );
    }
    internal void SetCityRestriction(string city, bool block, bool remove)
    {
        var list = serviceItem.CityList;
        if (list == null) 
        {
            if (remove) return;
            list = new AccessControlList();
            serviceItem.CityList = list;
        }
        SetRestriction(
            list,
            city,
            block,
            remove
        );
    }
    private void SetRestriction(AccessControlList list, string key, bool block, bool remove)
    {
        HashSet<string> lst;
        if (block)
        {
            if (list.BlockedList == null)
            {
                if (remove) return;
                list.BlockedList = new HashSet<string>();
            }
            lst = list.BlockedList;
        }
        else
        {
            if (list.PermittedList == null)
            {
                if (remove) return;
                list.PermittedList = new HashSet<string>();
            }
            lst = list.PermittedList;
        }
        SetRestriction(lst, key, remove);
    }
    private void SetRestriction(HashSet<string> list, string key, bool remove)
    {
        if (remove)
        {
            if (!list.Contains(key)) return;
            list.Remove(key);
        }
        else 
        {
            list.Add(key);
        }
        Persist();
    }

    private static string GetPath(string serviceName)
    {
        return $"{STORAGE_PATH}{serviceName}.json";
    }
    private void Persist()
    {
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