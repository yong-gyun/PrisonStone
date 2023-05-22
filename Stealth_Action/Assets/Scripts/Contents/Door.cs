using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] bool _needKey;
    [SerializeField] Define.CardKey _type;
    Collider col;
    Animator anim;



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
            //Show UI
            Open();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(CoClose());
            //Close UI
        }
    }

    public void Open()
    {
        anim.SetBool("IsOpen", true);

        Debug.Log("Open");
    }

    IEnumerator CoClose()
    {
        yield return new WaitForSeconds(5f);
        anim.SetBool("IsOpen", false);
        Debug.Log("Close");
    }
}
