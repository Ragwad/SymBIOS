using UnityEngine;

public partial class GameManager
{
    public enum RectTransforms
    {
        home_1_commencer, home_2_options, home_3_credits, home_4_quitter,
        options_commandes, options_audio,
        commandes_manette, commandes_claviersouris,
        manette, claviersouris,
        audio_general, audio_musique, audio_bruitages,
        slider_general, slider_musique, slider_bruitages,
        slide_general, slide_musique, slide_bruitages,
        pause_reprendre, pause_options, pause_home,
        retour,
        _last_
    }

    public enum Tmps { title, _last_ }

    public readonly RectTransform[] rectTransforms = new RectTransform[(int)RectTransforms._last_];
    public readonly TMPro.TextMeshProUGUI[] tmps = new TMPro.TextMeshProUGUI[(int)Tmps._last_];

    [HideInInspector] public Camera camera_ui;

    [Header("~@ UI @~")]
    public OnValue<RectTransforms> nav_f;

    //------------------------------------------------------------------------------------------------------------------------------

    void InitUI()
    {
        nav_f = new OnValue<RectTransforms>(RectTransforms.home_1_commencer, delegate (RectTransforms value)
        {
            animator.SetFloat((int)Parameters.nav_f, (int)value);
            mouse_hold = false;

            var tmp = tmps[(int)Tmps.title];

            switch (state_base)
            {
                case BaseStates.Init:
                case BaseStates.Home:
                    tmp.text = "";
                    break;

                case BaseStates.Options:
                    switch (value)
                    {
                        case RectTransforms.options_commandes:
                        case RectTransforms.options_audio:
                            tmp.text = "Options";
                            break;

                        case RectTransforms.commandes_manette:
                        case RectTransforms.commandes_claviersouris:
                            tmp.text = "Commandes";
                            break;

                        case RectTransforms.manette:
                            tmp.text = "Manette";
                            break;

                        case RectTransforms.claviersouris:
                            tmp.text = "Clavier";
                            break;

                        case RectTransforms.audio_general:
                        case RectTransforms.audio_musique:
                        case RectTransforms.audio_bruitages:
                            tmp.text = "Audio";
                            break;
                    }
                    break;
            }
        });

        camera_ui = transform.Find("Camera - UI").GetComponent<Camera>();

        tmps[(int)Tmps.title] = transform.Find("UI/rT/-Options-/tmp - Title").GetComponent<TMPro.TextMeshProUGUI>();

        for (RectTransforms rect = 0; rect < RectTransforms._last_; rect++)
            switch (rect)
            {
                case RectTransforms.home_1_commencer:
                case RectTransforms.home_2_options:
                case RectTransforms.home_3_credits:
                case RectTransforms.home_4_quitter:
                    rectTransforms[(int)rect] = (RectTransform)transform.Find("UI/rT/-Home-/rT/Menu/layout/menu " + (rect - RectTransforms.home_1_commencer + 1));
                    break;

                case RectTransforms.options_commandes:
                    rectTransforms[(int)rect] = (RectTransform)transform.Find("UI/rT/-Options-/Options/layout/commandes");
                    break;
                case RectTransforms.options_audio:
                    rectTransforms[(int)rect] = (RectTransform)transform.Find("UI/rT/-Options-/Options/layout/audio");
                    break;

                case RectTransforms.commandes_manette:
                    rectTransforms[(int)rect] = (RectTransform)transform.Find("UI/rT/-Options-/Commandes/layout/manette");
                    break;

                case RectTransforms.commandes_claviersouris:
                    rectTransforms[(int)rect] = (RectTransform)transform.Find("UI/rT/-Options-/Commandes/layout/clavier");
                    break;

                case RectTransforms.audio_general:
                    rectTransforms[(int)rect] = (RectTransform)transform.Find("UI/rT/-Options-/Audio/layout/1 general");
                    break;

                case RectTransforms.audio_musique:
                    rectTransforms[(int)rect] = (RectTransform)transform.Find("UI/rT/-Options-/Audio/layout/2 musique");
                    break;

                case RectTransforms.audio_bruitages:
                    rectTransforms[(int)rect] = (RectTransform)transform.Find("UI/rT/-Options-/Audio/layout/3 bruitages");
                    break;

                case RectTransforms.retour:
                    rectTransforms[(int)rect] = (RectTransform)transform.Find("UI/rT/-Options-/Retour");
                    break;

                case RectTransforms.slider_general:
                    rectTransforms[(int)rect] = (RectTransform)transform.Find("UI/rT/-Options-/Audio/layout/1 general/slide");
                    break;

                case RectTransforms.slider_musique:
                    rectTransforms[(int)rect] = (RectTransform)transform.Find("UI/rT/-Options-/Audio/layout/2 musique/slide");
                    break;

                case RectTransforms.slider_bruitages:
                    rectTransforms[(int)rect] = (RectTransform)transform.Find("UI/rT/-Options-/Audio/layout/3 bruitages/slide");
                    break;

                case RectTransforms.slide_general:
                    rectTransforms[(int)rect] = (RectTransform)transform.Find("UI/rT/-Options-/Audio/layout/1 general/slide/cursor");
                    break;

                case RectTransforms.slide_musique:
                    rectTransforms[(int)rect] = (RectTransform)transform.Find("UI/rT/-Options-/Audio/layout/2 musique/slide/cursor");
                    break;

                case RectTransforms.slide_bruitages:
                    rectTransforms[(int)rect] = (RectTransform)transform.Find("UI/rT/-Options-/Audio/layout/3 bruitages/slide/cursor");
                    break;

                case RectTransforms.pause_reprendre:
                    rectTransforms[(int)rect] = (RectTransform)transform.Find("UI/rT/-Pause-/layout/pause 1");
                    break;

                case RectTransforms.pause_options:
                    rectTransforms[(int)rect] = (RectTransform)transform.Find("UI/rT/-Pause-/layout/pause 2");
                    break;

                case RectTransforms.pause_home:
                    rectTransforms[(int)rect] = (RectTransform)transform.Find("UI/rT/-Pause-/layout/pause 3");
                    break;
            }
    }

    //------------------------------------------------------------------------------------------------------------------------------

    public bool IsHoverRect(RectTransforms rect)
        => RectTransformUtility.RectangleContainsScreenPoint(rectTransforms[(int)rect], mouse_pos, camera_ui);

    public bool IsHoverRect(RectTransforms rect, out Vector2 local)
        => RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransforms[(int)rect], mouse_pos, camera_ui, out local);

    void UpdateUI()
    {
        // retour
        if (mouse_down && state_base == BaseStates.Options && IsHoverRect(RectTransforms.retour))
            switch (nav_f.value)
            {
                case RectTransforms.options_commandes:
                case RectTransforms.options_audio:
                    animator.CrossFadeInFixedTime((int)BaseStates.Home, 0, (int)Layers.Base);
                    nav_f.UpdateValue(RectTransforms.home_2_options);
                    break;

                case RectTransforms.commandes_manette:
                case RectTransforms.commandes_claviersouris:
                    nav_f.UpdateValue(RectTransforms.options_commandes);
                    break;

                case RectTransforms.manette:
                    nav_f.UpdateValue(RectTransforms.commandes_manette);
                    break;
                case RectTransforms.claviersouris:
                    nav_f.UpdateValue(RectTransforms.commandes_claviersouris);
                    break;

                case RectTransforms.audio_general:
                case RectTransforms.audio_musique:
                case RectTransforms.audio_bruitages:
                    nav_f.UpdateValue(RectTransforms.options_audio);
                    break;
            }

        bool AutoNav(RectTransforms rect_a, RectTransforms rect_b)
        {
            for (RectTransforms rect = rect_a; rect <= rect_b; rect++)
                if (IsHoverRect(rect))
                {
                    nav_f.UpdateValue(rect);
                    return true;
                }

            return false;
        }

        switch (state_base)
        {
            case BaseStates.Home:
                if (AutoNav(RectTransforms.home_1_commencer, RectTransforms.home_4_quitter) && mouse_down)
                {
                    switch (nav_f.value)
                    {
                        case RectTransforms.home_1_commencer:
                            scene = Scenes.Level1;
                            animator.CrossFadeInFixedTime((int)BaseStates.toLoad, 0, (int)Layers.Base);
                            break;

                        case RectTransforms.home_2_options:
                            animator.CrossFadeInFixedTime((int)BaseStates.Options, 0, (int)Layers.Base);
                            nav_f.UpdateValue(RectTransforms.options_commandes);
                            break;

                        case RectTransforms.home_3_credits:
                            break;

                        case RectTransforms.home_4_quitter:
                            Application.Quit();
                            break;
                    }

                    return;
                }
                break;

            case BaseStates.Options:
                switch (nav_f.value)
                {
                    case RectTransforms.options_commandes:
                    case RectTransforms.options_audio:
                        if (IsHoverRect(RectTransforms.options_commandes))
                            nav_f.UpdateValue(mouse_down ? RectTransforms.commandes_manette : RectTransforms.options_commandes);
                        else if (IsHoverRect(RectTransforms.options_audio))
                            nav_f.UpdateValue(mouse_down ? RectTransforms.audio_general : RectTransforms.options_audio);
                        break;

                    case RectTransforms.commandes_manette:
                    case RectTransforms.commandes_claviersouris:
                        if (IsHoverRect(RectTransforms.commandes_manette))
                            nav_f.UpdateValue(mouse_down ? RectTransforms.manette : RectTransforms.commandes_manette);
                        else if (IsHoverRect(RectTransforms.commandes_claviersouris))
                            nav_f.UpdateValue(mouse_down ? RectTransforms.claviersouris : RectTransforms.commandes_claviersouris);
                        break;

                    case RectTransforms.audio_general:
                    case RectTransforms.audio_musique:
                    case RectTransforms.audio_bruitages:
                        if (AutoNav(RectTransforms.audio_general, RectTransforms.audio_bruitages) && mouse_hold)
                        {
                            RectTransform rect;
                            Vector2 local;
                            Vector3 volumes = audio_volumes.value;

                            if (IsHoverRect(RectTransforms.slider_general))
                            {
                                if (IsHoverRect(RectTransforms.slider_general, out local))
                                {
                                    rect = rectTransforms[(int)RectTransforms.slide_general];
                                    volumes.x = .5f + local.x / rectTransforms[(int)RectTransforms.slider_general].rect.width;
                                    rect.anchorMin = rect.anchorMax = new Vector2(volumes.x, .5f);
                                }
                            }
                            else if (IsHoverRect(RectTransforms.slider_musique))
                            {
                                if (IsHoverRect(RectTransforms.slider_musique, out local))
                                {
                                    rect = rectTransforms[(int)RectTransforms.slide_musique];
                                    volumes.x = .5f + local.x / rectTransforms[(int)RectTransforms.slider_musique].rect.width;
                                    rect.anchorMin = rect.anchorMax = new Vector2(volumes.x, .5f);
                                }
                            }
                            else if (IsHoverRect(RectTransforms.slider_bruitages))
                            {
                                if (IsHoverRect(RectTransforms.slider_bruitages, out local))
                                {
                                    rect = rectTransforms[(int)RectTransforms.slide_bruitages];
                                    volumes.x = .5f + local.x / rectTransforms[(int)RectTransforms.slider_bruitages].rect.width;
                                    rect.anchorMin = rect.anchorMax = new Vector2(volumes.x, .5f);
                                }
                            }

                            audio_volumes.UpdateValue(volumes);
                        }
                        break;
                }
                break;

            case BaseStates.Pause:
                if (AutoNav(RectTransforms.pause_reprendre, RectTransforms.pause_home) && mouse_down)
                    switch (nav_f.value)
                    {
                        case RectTransforms.pause_reprendre:
                            animator.CrossFadeInFixedTime((int)BaseStates.Gameplay, 0, (int)Layers.Base);
                            break;

                        case RectTransforms.pause_options:
                            break;

                        case RectTransforms.pause_home:
                            scene = Scenes.Home;
                            animator.CrossFadeInFixedTime((int)BaseStates.toLoad, 0, (int)Layers.Base);
                            StartCoroutine(ELoadScene());
                            break;
                    }
                break;
        }
    }
}