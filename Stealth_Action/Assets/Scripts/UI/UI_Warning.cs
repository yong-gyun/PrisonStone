using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Warning : MonoBehaviour
{
    Image image;

    void Start()
    {
        GameObject go = transform.Find("Image").gameObject;
        image = go.GetComponent<Image>();
    
        Destroy(gameObject, 10f);
        StartCoroutine(CoWarning());
    }

    IEnumerator CoWarning()
    {
        float f_time = 1f;
        float time;

        while (true)
        {
            time = 0f;

            while (image.color.a < 0.6f)
            {
                time += Time.deltaTime / f_time;
                Color color = image.color;
                color.a = Mathf.Lerp(0.1f, 0.6f, time);
                image.color = color;
                yield return null;
            }

            time = 0;

            while (image.color.a > 0.1f)
            {
                time += Time.deltaTime / f_time;
                Color color = image.color;
                color.a = Mathf.Lerp(0.6f, 0.1f, time);
                image.color = color;
                yield return null;
            }
        }
    }
}
