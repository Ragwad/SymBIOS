using UnityEngine;

public static partial class Util
{
    public enum GamepadKeycodes
    {
        _first_ = KeyCode.JoystickButton0 - 1,
        A, B, X, Y, LB, RB, Se, St, Lb, Rb,
        _last_
    }

    public static bool OnKeysDown(params KeyCode[] keycodes)
    {
        foreach (var key in keycodes)
            if (Input.GetKeyDown(key))
                return true;

        return false;
    }

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