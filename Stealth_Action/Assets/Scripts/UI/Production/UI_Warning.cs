using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Warning : UI_Production
{
    enum Images
    {
        Image
    }

    protected override void Init()
    {
        BindImage(typeof(Images));

        base.Init();
        Managers.Sound.Play("Bgm/Siren", Define.Sound.Bgm);
        Destroy(gameObject, 10f);
        StartCoroutine(CoWarning());
    }

    float _currentTime = 10f;

    IEnumerator CoWarning()
    {
        Image image = GetImage((int)Images.Image);
        float f_time = 1f;
        float time;

        while (_currentTime >= 0)
        {
            time = 0f;
            
            while (image.color.a < 0.6f)
            {
                time += Time.deltaTime / f_time;
                Color color = image.color;
                color.a = Mathf.Lerp(0.1f, 0.6f, time);
                image.color = color;
                _currentTime -= Time.deltaTime;
                yield return null;
            }

            time = 0;

            while (image.color.a > 0.1f)
            {
                time += Time.deltaTime / f_time;
                Color color = image.color;
                color.a = Mathf.Lerp(0.6f, 0.1f, time);
                image.color = color;
                _currentTime -= Time.deltaTime;
                yield return null;
            }
        }

        Managers.Sound.Play("Bgm/Game", Define.Sound.Bgm);
    }
}
