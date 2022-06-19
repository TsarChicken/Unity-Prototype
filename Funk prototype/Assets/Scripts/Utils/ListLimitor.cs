using System.Collections.Generic;
public static class ListLimitor 
{
    public static bool Add<T>(this List<T> list, T item)
    {
        if (list.Count == list.Capacity)
        {
            return false;
        }

        list.Add(item);
        return true;
    }
    public static bool CanAdd<T>(this List<T> list)
    {
        return list.Count < list.Capacity;
    }
}
