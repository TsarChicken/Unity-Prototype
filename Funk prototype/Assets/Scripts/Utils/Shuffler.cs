using System.Collections.Generic;

public static class Shuffler
{
    public static void Shuffle<T>(this IList<T> list)
    {
        System.Random random = new System.Random();
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = random.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }

    public static  void Shuffle<T>(this T[] array)
    {
        System.Random random = new System.Random();
        int n = array.Length;
        while (n > 1)
        {
            int k = random.Next(n--);
            T value = array[n];
            array[n] = array[k];
            array[k] = value;
        }
    }


}
