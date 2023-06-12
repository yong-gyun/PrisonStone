using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UI_Interaction : UI_Base
{
    enum Texts
    {
        InteractionText
    }

    public delegate void OnInteraction();
    public OnInteraction OnInteractionHandler = null;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            if (OnInteractionHandler != null)
            {
                OnInteractionHandler.Invoke();
            }
        }

        transform.rotation = Camera.main.transform.rotation;
    }

    protected override void Init()
    {
        BindText(typeof(Texts));
    }

    public void SetInfo(string text, bool isRed = false)
    {
        GetText((int)Texts.InteractionText).text = text;

        if(isRed)
            GetText((int)Texts.InteractionText).color = Color.red;
    }
}
