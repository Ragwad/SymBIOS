using UnityEngine;

public partial class PlayerManager
{
    public enum Rects { viseur, _last_ }

    public readonly RectTransform[] rects = new RectTransform[(int)Rects._last_];

    //------------------------------------------------------------------------------------------------------------------------------

    void InitUI()
    {
        rects[(int)Rects.viseur] = (RectTransform)transform.Find("UI/Viseur");
    }

    //------------------------------------------------------------------------------------------------------------------------------

    void UpdateUI()
    {
        var aim_rT = rects[(int)Rects.viseur];
        aim_rT.anchorMin = aim_rT.anchorMax = mouse_pos;
    }
}