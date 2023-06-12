using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Crosshair : UI_Production
{
    enum Images
    {
        Crosshair
    }

    private void Awake()
    {
        BindImage(typeof(Images));
        GetImage((int)Images.Crosshair).color = new Color(1, 1, 1, 0);
    }

    public void OnShow()
    {
        StartCoroutine(CoChangeAlphaByColor(false));
    }

    public void OnClose()
    {
        StartCoroutine(CoChangeAlphaByColor(true));
    }

    IEnumerator CoChangeAlphaByColor(bool isFade)
    {
        Color color = GetImage((int)Images.Crosshair).color;
        float t = 0;
        float targetTime = 0.1f;

        if(isFade)
        {
            while(true)
            {
                if (color.a == 0f)
                    break;

                float a = Mathf.Lerp(1f, 0f, t);
                t += Time.deltaTime / targetTime;
                color.a = a;

                GetImage((int)Images.Crosshair).color = color;
                yield return null;
            }

            Managers.UI.CloseProduction(gameObject);
        }
        else
        {
            while (true)
            {
                if (color.a == 1f)
                    break;

                float a = Mathf.Lerp(0f, 1f, t);
                t += Time.deltaTime / targetTime;
                color.a = a;

                GetImage((int)Images.Crosshair).color = color;
                yield return null;
            }
        }
    }
}