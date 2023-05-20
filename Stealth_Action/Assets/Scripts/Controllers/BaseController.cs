using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;

public abstract class BaseController : MonoBehaviour
{
    protected float hp;
    protected float damage;
    [SerializeField] protected float moveSpeed;

    protected abstract void Init();

    protected virtual void Start()
    {
        Init();
    }

    protected virtual void Update()
    {
        UpdateMove();
    }

    protected virtual void UpdateMove() { }
    protected virtual void UpdateAttack() { }
    protected virtual void OnAttacked() { }
}
