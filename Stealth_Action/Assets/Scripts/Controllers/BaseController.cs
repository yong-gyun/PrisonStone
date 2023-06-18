using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseController : MonoBehaviour
{
    [SerializeField] protected float _minSpeed;
    [SerializeField] protected float _maxSpeed;
    [SerializeField] protected float _moveSpeed;

    public abstract void Init();

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
