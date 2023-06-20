using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_Loading : UI_Popup
{
    enum Texts
    {
        PercentText,
    }

    enum Sliders
    {
        ProgressSlider
    }

    protected override void Init()
    {
        base.Init();
        BindText(typeof(Texts));
        BindSlider(typeof(Sliders));
    }

    public void LoadScene(Define.Scene type)
    {
        StartCoroutine(CoLoadScene(Managers.Scene.GetSceneName(type)));
        Managers.Sound.Stop();
    }

    IEnumerator CoLoadScene(string sceneName)
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(sceneName);
        op.allowSceneActivation = false;
        float t = 0f;

        Slider progressBar = GetSlider((int)Sliders.ProgressSlider);
        TMP_Text percentText = GetText((int)Texts.PercentText);

        while (!op.isDone)
        {
            yield return null;

            t += Time.deltaTime;
            
            if (op.progress < 0.9f)
            {
                progressBar.value = Mathf.Lerp(progressBar.value, op.progress * 10, t);
                percentText.text = $"{(int)(op.progress * 100)}%";

                if (progressBar.value >= op.progress * 10)
                {
                    t = 0f;
                }
            }
            else
            {
                progressBar.value = Mathf.Lerp(progressBar.value, 10f, t);
                percentText.text = $"{(int)(op.progress * 100)}%";
                
                if (progressBar.value == 10f)
                {
                    percentText.text = $"{100}%";
                    op.allowSceneActivation = true;
                    Managers.UI.ClosePopupUI();
                    yield break;
                }
            }
        }
    }
}
