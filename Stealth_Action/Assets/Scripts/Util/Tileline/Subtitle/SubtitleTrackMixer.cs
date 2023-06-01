using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;

public class SubtitleTrackMixer : PlayableBehaviour
{
    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        TMP_Text text = playerData as TMP_Text;
        string currentText = "";
        Color currentColor = Color.white;
        float currentAlpha = 0f;

        if (!text)
            return;

        int inputCount = playable.GetInputCount();

        for (int i = 0; i < inputCount; i++)
        {
            float inputWeight = playable.GetInputWeight(i);

            if(inputWeight > 0f)
            {
                ScriptPlayable<SubtitleBehaviour> inputPlayable = (ScriptPlayable<SubtitleBehaviour>) playable.GetInput(i);

                SubtitleBehaviour subtitleBehaviour = inputPlayable.GetBehaviour();
                currentText = subtitleBehaviour.Text;
                currentColor = subtitleBehaviour.TextColor;
                currentAlpha = inputWeight;
            }
        }

        text.text = currentText;
        currentColor.a = currentAlpha;
        text.color = currentColor;
    }
}
