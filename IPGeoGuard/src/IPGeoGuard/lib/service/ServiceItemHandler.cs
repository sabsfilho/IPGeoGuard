using System.Text.Json;
using IPGeoGuard.lib.storage;

namespace IPGeoGuard.lib.service;
internal class ServiceItemHandler
{
    private ServiceItem serviceItem;
    private AFileStorage? fileStorage;
    public ServiceItemHandler(string serviceName)
    {
        fileStorage = FileStorageHandler.GetFileStorage(serviceName);
        if (fileStorage != null)
        {
            string? json = fileStorage.Read();
            if (json != null)
            {
                serviceItem = JsonSerializer.Deserialize<ServiceItem>(json)!;
                return;
            }
        }

        serviceItem = new ServiceItem(serviceName);
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
    private void Persist()
    {
        if (fileStorage == null) return;

        string json = JsonSerializer.Serialize(serviceItem);
        fileStorage.Persist(json);
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