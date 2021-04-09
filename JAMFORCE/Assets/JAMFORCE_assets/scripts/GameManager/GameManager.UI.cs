using UnityEngine;

public partial class GameManager
{
    public enum RectTransforms { home_menu_first, home_menu_last = home_menu_first + 3, _last_ }

    public readonly RectTransform[] rectTransforms = new RectTransform[(int)RectTransforms._last_];

    [Header("~@ UI @~")]
    public OnValue<RectTransforms> nav_f;

    //------------------------------------------------------------------------------------------------------------------------------

    void InitUI()
    {
        nav_f = new OnValue<RectTransforms>(RectTransforms.home_menu_first, delegate (RectTransforms value)
        {
            animator.SetFloat((int)Parameters.nav_f, (int)value);
        });

        var rT = transform.Find("UI/rT/-Home-/rT/Menu/layout");

        for (int i = 0; i <= RectTransforms.home_menu_last - RectTransforms.home_menu_first; i++)
            rectTransforms[i + (int)RectTransforms.home_menu_first] = (RectTransform)rT.Find("menu " + (1 + i));
    }

    //------------------------------------------------------------------------------------------------------------------------------

    public bool IsHoverRect(RectTransforms id)
        => RectTransformUtility.RectangleContainsScreenPoint(rectTransforms[(int)id], mouse_pos);

    void UpdateUI()
    {
        switch (state_base)
        {
            case BaseStates.Home:
                {
                    for (RectTransforms rect = RectTransforms.home_menu_first; rect <= RectTransforms.home_menu_last; rect++)
                        if (IsHoverRect(rect))
                        {
                            nav_f.UpdateValue(rect);
                            break;
                        }
                }
                break;
        }
    }
}