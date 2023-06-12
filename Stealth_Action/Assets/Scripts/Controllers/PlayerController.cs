using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : BaseController
{
    public int MaxCount { get; private set; } = 4;
    public int CurrentCount { get; private set; } = 0;
    public bool IsActionable { get; set; }

    Animator _anim;
    Transform _firePos;
    Rigidbody _rb;
    float _currentTime = 0;
    [SerializeField] bool _isAiming;

    protected override void Start()
    {

    }

    public override void Init()
    {
        _moveSpeed = 7.5f;
        _firePos = gameObject.FindChild("FirePos", true).transform;
        CurrentCount = MaxCount;

        _rb = GetComponent<Rigidbody>();
        _anim = GetComponentInChildren<Animator>();

    }

    protected override void Update()
    {
        if (Managers.Sequnce.IsCinematic || !IsActionable)
            return;

        UpdateAttack();

        if(CurrentCount < MaxCount)
        {
            _currentTime += Time.deltaTime;

            if(_currentTime >= 4f)
            {
                _currentTime = 0;
                CurrentCount++;
            }
        }

        _isAiming = Input.GetMouseButton(1);
        UpdateMove();
    }

    protected override void UpdateMove()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 dir = (transform.right * horizontal) + (transform.forward * vertical);

        if (dir != Vector3.zero)
        {
            if (Physics.Raycast(transform.position + Vector3.up, dir, 2f, LayerMask.GetMask("Wall")))
            {
                _anim.SetFloat("Speed", 0);
                return;
            }

            transform.position += dir.normalized * _moveSpeed * Time.deltaTime;
            //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), _rotSpeed * Time.deltaTime);
        }
        else
        {
            _rb.velocity = Vector3.zero;
        }

        if(_isAiming)
        {
            _anim.SetBool("Aiming", true);
        }
        else
        {
            float speed = _rb.velocity.normalized.magnitude;
            float curSpeed = Mathf.Clamp(speed, 0f, 0.5f);
            float velocityX = dir.x;
            float velocityZ = dir.z;

            if (Input.GetKey(KeyCode.LeftShift))
                curSpeed = 1f;

            _anim.SetFloat("Speed", curSpeed);
            _anim.SetFloat("VelocityX", velocityX);
            _anim.SetFloat("VelocityZ", velocityZ);
            _anim.SetBool("Aiming", false);
        }
    }

    protected override void UpdateAttack()
    {
        if (CurrentCount == 0)
            return;

        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                GameObject go = Managers.Resource.Instantiate("PlayerBullet", _firePos.position, Quaternion.identity);
                Bullet bullet = go.GetComponent<Bullet>();
                bullet.Init(hit.point);
            }
            //∏∂√Î√— πﬂªÁ
        }
    }
}