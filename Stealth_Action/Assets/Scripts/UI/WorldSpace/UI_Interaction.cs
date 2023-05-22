using System.Collections;
using System.Collections.Generic;
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
        if(Input.GetKeyDown(KeyCode.A))
        {
            if (OnInteractionHandler != null)
            {
                OnInteractionHandler.Invoke();
            }
        }
    }

    protected override void Init()
    {
        BindText(typeof(Texts));
    }

    public void SetInfo(string text, bool openable)
    {
        GetText((int)Texts.InteractionText).text = text;

        if(!openable)
            GetText((int)Texts.InteractionText).color = Color.red;
    }
}
