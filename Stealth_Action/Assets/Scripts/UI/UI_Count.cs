using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_Count : MonoBehaviour
{
    TMP_Text countText;
    PlayerController player;

    private void Start()
    {
        countText = transform.Find("CountText").GetComponent<TMP_Text>();
        player = GameManager.Instance.Player.GetComponent<PlayerController>();
    }

    private void Update()
    {
        countText.text = $"{player.currentCount} / {player.maxCount}";
    }
}
