#if UNITY_EDITOR
using UnityEngine;

public partial class GameManager
{
    [ContextMenu(nameof(OnValidate))]
    private void OnValidate()
    {
        if (Application.isPlaying && !string.IsNullOrWhiteSpace(json.name))
            json.Save();
    }

    [ContextMenu(nameof(Getlayers))]
    void Getlayers()
    {
        string log = "public enum UserLayers\n{\n";

        for (int i = 0; i < 32; i++)
        {
            var name = LayerMask.LayerToName(i);

            if (!string.IsNullOrWhiteSpace(name))
                log += name + " = " + i + ",\n";
        }

        log += "\n}";

        Debug.Log(log);
    }
}
#endif