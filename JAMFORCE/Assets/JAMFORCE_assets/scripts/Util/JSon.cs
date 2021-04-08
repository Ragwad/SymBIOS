using System.IO;
using UnityEngine;

public abstract class JSon
{
    public string name;

    //------------------------------------------------------------------------------------------------------------------------------

    public static bool Read<T>(string name, ref T json) where T : JSon
    {
        string path = Path.Combine(Application.streamingAssetsPath, name);

        if (File.Exists(path))
        {
            json = JsonUtility.FromJson<T>(File.ReadAllText(path));
            json.name = name;
            json.Save();
            return true;
        }
        else
        {
            json.name = name;
            json.Save();
            return false;
        }
    }

    public void Save()
    {
        string path = Path.Combine(Application.streamingAssetsPath, name);

        if (!Directory.Exists(Application.streamingAssetsPath))
            Directory.CreateDirectory(Application.streamingAssetsPath);

        File.WriteAllText(path, JsonUtility.ToJson(this, true));
    }
}