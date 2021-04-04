#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

public static partial class Util
{
    [MenuItem("CONTEXT/" + nameof(Transform) + "/" + nameof(GetPath))]
    static void GetPath(MenuCommand command) 
        => Debug.Log(((Transform)command.context).GetPath());
}
#endif