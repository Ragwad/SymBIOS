#if UNITY_EDITOR
using UnityEngine;

public partial class PlayerController
{
    [ContextMenu(nameof(OnValidate))]
    private void OnValidate()
    {
        if (Application.isPlaying && !string.IsNullOrWhiteSpace(json.name))
            json.Save();
    }
}
#endif