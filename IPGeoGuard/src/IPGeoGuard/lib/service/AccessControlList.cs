namespace IPGeoGuard.lib.service;
internal class AccessControlList
{
    public HashSet<string>? BlockedList { get; set; }
    public HashSet<string>? PermittedList { get; set; }
    internal bool IsAllowed(string key)
    {
        if (BlockedList == null || BlockedList.Count == 0) return true;

        if (PermittedList != null && PermittedList.Contains(key)) return true;

        if (BlockedList != null && BlockedList.Contains(key)) return false;

        return true;
    }

}