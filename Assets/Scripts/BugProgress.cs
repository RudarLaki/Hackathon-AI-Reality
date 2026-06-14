using System.Collections.Generic;

public static class BugProgress
{
    public static HashSet<string> discovered = new HashSet<string>();

    public static void Discover(string id)
    {
        if (!string.IsNullOrEmpty(id))
            discovered.Add(id);
    }

    public static bool IsDiscovered(string id)
    {
        return discovered.Contains(id);
    }
}