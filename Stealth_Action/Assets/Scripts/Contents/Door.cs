using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] bool _needKey;
    [SerializeField] Define.CardKey _type;
    Collider col;
    Animator anim;
    UI_Interaction _interactionUI;
    Vector3 offset = new Vector3(2.25f, 5, 0);

    private void Start()
    {
        col = GetComponent<Collider>();
        string name = transform.parent.name;

        switch(name)
        {
            case "WhiteDoor":
                _type = Define.CardKey.White;
                break;
            case "RedDoor":
                _type = Define.CardKey.Red; 
                break;
            case "BlueDoor":
                _type = Define.CardKey.Blue;
                break;
            case "GreenDoor":
                _type = Define.CardKey.Green;
                break;
            case "YellowDoor":
                _type = Define.CardKey.Yellow;
                break;
        }

        anim = GetComponentInParent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            _interactionUI = Managers.UI.MakeWorldSpaceUI<UI_Interaction>();
            _interactionUI.transform.position = offset + transform.position;

            if (Managers.Game.KeyInventory[_type] > 0 || !_needKey)
            {
                _interactionUI.SetInfo("¿­·Á¶ó Âü±ú", false);
                _interactionUI.OnInteractionHandler += OnOpen;
                Debug.Log(Managers.Game.KeyInventory[_type]);
                return;
            }

            _interactionUI.SetInfo("¿­·Á¶ó Âü±ú", true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Managers.Resource.Destroy(_interactionUI.gameObject);
            StartCoroutine(CoClose());
        }
    }

    public void OnOpen()
    {
        anim.SetBool("IsOpen", true);
        Managers.Game.KeyInventory[_type]--;
    }

    IEnumerator CoClose()
    {
        yield return new WaitForSeconds(5f);
        anim.SetBool("IsOpen", false);
    }
}
