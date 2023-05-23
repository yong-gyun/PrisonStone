using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Fade : UI_Popup
{
    public delegate void FadeHandler();
    public FadeHandler OnFadeHandler;

    enum Images
    {
        Image
    }

    protected override void Init()
    {
        base.Init();

        BindImage(typeof(Images));
        StartCoroutine(CoFade());
    }

    IEnumerator CoFade()
    {
        Image img = GetImage((int)Images.Image);
        float f_time = 4f;
        float t = 0;

        while (img.color.a < 1f)
        {
            Color imgColor = img.color;
            t += Time.deltaTime / f_time;
            imgColor.a = Mathf.Lerp(0, 1, t);
            img.color = imgColor;

            yield return null;
        }

        if (OnFadeHandler != null)
            OnFadeHandler.Invoke();
    }
}
