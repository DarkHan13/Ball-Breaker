using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZobmieMovement : MonoBehaviour
{
    [SerializeField] private float viewAngle = 90f;
    [SerializeField] private float viewRadius = 10f;
    [SerializeField] private Transform target; 
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float hp = 20;
    [SerializeField] private GameObject bloodSplashPrefab;
    
    private bool _facingRight = true;
    private bool _isWalking;
    private float _moveHorizontal = 0;

    
    private Rigidbody2D _rb;
    private Animator _animator;
    private static readonly int IsWalking = Animator.StringToHash("isWalking");

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();

        _isWalking = true;
    }

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        if (Math.Abs(transform.rotation.y - 1f) < 0.01) _facingRight = false;
    }

    private void FixedUpdate()
    {
        _moveHorizontal = 0f;

        if (CanSeeTarget())
        {
            if (transform.position.x - 0.5f > target.position.x) _moveHorizontal = -1;
            else if (transform.position.x + 0.5f < target.position.x) _moveHorizontal = 1;
        }
        Move(_moveHorizontal);
        FaceDirection(_moveHorizontal);
        
        SetAnimatorValues();
    }

    private void Move(float moveHorizontal)
    {
        Vector2 movement = new Vector2(moveHorizontal * moveSpeed, _rb.velocity.y);
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
    
    private bool CanSeeTarget()
    {
        // Проверяем расстояние между зомби и игроком
        float distanceToTarget = Vector3.Distance(transform.position, target.position);

        // Если расстояние меньше или равно радиусу поля зрения, зомби видит игрока
        if (distanceToTarget <= viewRadius)
        {
            // Проверяем, что игрок находится в поле зрения зомби
            Vector3 directionToTarget = (target.position - transform.position).normalized;
            float angleToTarget = Vector3.Angle(transform.right, directionToTarget);
            
            // Если угол между направлением зомби и игроком меньше половины поля зрения, значит зомби видит игрока
            if (angleToTarget <= viewAngle / 2f)
            {
                return true;
            }
        }

        return false;
    }

    private void Flip()
    {
        _facingRight = !_facingRight;
        transform.Rotate(Vector3.up, 180f);
    }
    
    

    public void GetDamage(float damage, Vector3 pointOfHit, Vector3 from)
    {
        var bloodSplash = Instantiate(bloodSplashPrefab, pointOfHit, Quaternion.identity);
        bloodSplash.GetComponent<ParticleBehaviour>().DestroyMe(1f);
    
        hp -= damage;
        if (hp <= 0) Destroy(gameObject);

        if (from.x < transform.position.x) _moveHorizontal = -1;
        else _moveHorizontal = 1f;
    }

    private void SetAnimatorValues()
    {
        _animator.SetBool(IsWalking, _isWalking);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, viewRadius);
        
        Vector3 rightDir = Quaternion.Euler(0, 0, viewAngle / 2f) * transform.right;
        Vector3 leftDir = Quaternion.Euler(0, 0, -viewAngle / 2f) * transform.right;

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + rightDir * viewRadius);
        Gizmos.DrawLine(transform.position, transform.position + leftDir * viewRadius);
    }
}
