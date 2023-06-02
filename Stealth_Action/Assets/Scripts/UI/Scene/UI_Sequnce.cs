using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Sequnce : UI_Base
{
    enum Images
    {
        TopImage,
        BottomImage
    }

    protected override void Init()
    {
        Canvas canvas = gameObject.GetOrAddComponent<Canvas>();
        canvas.sortingOrder = int.MaxValue;
        BindImage(typeof(Images));
    }

    public void SetActive(bool active)
    {
        if(active)
        {
            gameObject.SetActive(true);
            GetImage((int)Images.TopImage).color = Color.black;
            GetImage((int)Images.BottomImage).color = Color.black;
        }
        else
        {
            StartCoroutine(CoDisabled());
        }
    }

    IEnumerator CoDisabled()
    {
        Image topImage = GetImage((int)Images.TopImage);
        Image bottomImage = GetImage((int)Images.BottomImage);

        Color color = Color.black;

        float t = 0;
        float f_time = 1f;

        while (color.a > 0.1f)
        {
            color.a = Mathf.Lerp(1, 0.1f, t);
            t += Time.deltaTime / f_time;
            topImage.color = color;
            bottomImage.color = color;
            yield return null;
        }

        gameObject.SetActive(false);
    }
}