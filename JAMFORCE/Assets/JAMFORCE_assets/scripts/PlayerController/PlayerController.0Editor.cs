#if UNITY_EDITOR
using UnityEngine;

public partial class PlayerController
{
    [ContextMenu(nameof(OnValidate))]
    private void OnValidate()
    {
        if (Application.isPlaying && !string.IsNullOrWhiteSpace(json.name))
            json.Save();

        const int length = (int)Prefabs._last_;

        if (prefabs.Length != length)
        {
            var clone = new GameObject[length];

            for (int i = 0; i < Mathf.Min(length, prefabs.Length); i++)
                clone[i] = prefabs[i];

            prefabs = clone;
        }
    }
}
#endif