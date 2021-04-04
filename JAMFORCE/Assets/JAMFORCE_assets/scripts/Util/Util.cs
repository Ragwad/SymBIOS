using UnityEngine;

public static partial class Util
{
    public static string GetPath(this Transform transform)
    {
        string path = "";

        do
        {
            path = transform.name + "/" + path;
            transform = transform.parent;
        }
        while (transform != null);

        return path;
    }
}