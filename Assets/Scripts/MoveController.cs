using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour
{
    public float moveSpeed = 2f;
    public Transform shootPoint;
    public GameObject bulletPrefab;
    public bool hasPistol = false;
    

    private bool _isWalking;
    private bool _isShooting;
    private bool _facingRight = true;
    private Rigidbody2D _rb;
    private Animator _animator;
    private static readonly int IsWalking = Animator.StringToHash("isWalking");

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }
    
    private void Update()
    {
        if (_isShooting) return;
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        Move(moveHorizontal);
        FaceDirection(moveHorizontal);

        if (hasPistol && Input.GetKeyDown(KeyCode.Space))
        {
            StartShoot();
        }
        

        SetAnimatorValues();
    }

    private void Move(float moveHorizontal)
    {
        Vector2 movement = new Vector2(moveHorizontal * moveSpeed, _rb.velocity.y);
        if (moveHorizontal != 0) _isWalking = true;
        else _isWalking = false;
        _rb.velocity = movement;
    }

    private void FaceDirection(float moveHorizontal)
    {
        if (moveHorizontal > 0 && !_facingRight)
        {
            Flip();
        }
        else if (moveHorizontal < 0 && _facingRight)
        {
            Flip();
        }
    }

    public void Death()
    {
        _rb.velocity = Vector2.zero;
        _rb.bodyType = RigidbodyType2D.Kinematic;
        _animator.Play("cop_zombie_1");
        enabled = false;
    }

    private void Flip()
    {
        _facingRight = !_facingRight;
        transform.Rotate(Vector3.up, 180f);
    }

    private void StartShoot()
    {
        _isShooting = true;
        _animator.Play("shoot");
    }

    private void Shoot()
    {
        Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
    }

    private void EndShoot()
    {
        _isShooting = false;
        _animator.Play("idle");
    }
    
    private void SetAnimatorValues()
    {
        _animator.SetBool(IsWalking, _isWalking);
    }

    
}
