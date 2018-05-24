using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "States/Miniboss/MiniBossIdle02")]
public class MiniBossIdle02 : State {


    private MiniBossController _controller;

    [Header("Ellipse")]
    public Transform centerPoint;
    public Ellipse ellipse;
    [Range(0.25f, 0.75f)]
    public float OrbitProgress;
    public float orbitSpeed = 0.15f;
    public bool clockwise = false;


    public int speed = 10;
    public float patrolMovementSpeed = 3;
    public float patrolStartWaitTime = 0;
    private float patrolWaitTime;

    public float fireRate = 10;  // The number of bullets fired per second
    private float nextFire;   // The value of Time.time at the last firing moment
    public float rotationSpeed = 5;
    private Transform _aim;

    private int _patrolIndex;
    public LayerMask CollisionLayers;
    private bool _IsElipseAttack = false;
    private bool _IsStartPositionSet;
    private int patrolCount;
    private bool shieldCoroutine;

    public override void Initialize(Controller owner)
    {
        _controller = (MiniBossController)owner;
        _aim = _controller.transform.GetChild(0);
        _patrolIndex = 1;

    }

    public override void Enter()
    {
        _controller.manager.bodyRender.sprite = _controller.manager.bossSprites[1];
        centerPoint = _controller.PatrolPoints[1].transform;
        patrolWaitTime = patrolStartWaitTime;
        shieldCoroutine = false;
        _controller.manager.setTakeDamage(false);
    }

    public override void Update()
    {
        
        if (_controller.manager.sheild.hitShield == true)
            SheildHit();

        if (_IsElipseAttack)
        {
            ElipseAttack();
        }
        else
        {
            PatrolState();
        }

        ShootRoutine();

        if (_controller.manager.currentHealth == 0)
            _controller.TransitionTo<DeathState>();
    }


    private void SheildHit()
    {
        if (shieldCoroutine) return;
        _controller.manager.setTakeDamage(true);
        _controller.manager.bodyRender.sprite = _controller.manager.bossSprites[0];
        _controller.StartCoroutine(ShieldDown());
        shieldCoroutine = true;

    }

    private IEnumerator ShieldDown()
    {
        yield return new WaitForSeconds(_controller.shieldDownTime);
        _controller.manager.sheild.hitShield = false;
        _controller.manager.bodyRender.sprite = _controller.manager.bossSprites[1];
        _controller.manager.setTakeDamage(false);
        shieldCoroutine = false;
    }

    public void ShootRoutine()
    {
        RotateWeapon();
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Shoot();
        }

    }

    private void RotateWeapon()
    {
        Vector3 toTargetVector = _controller.target.position - _aim.transform.position;
        float zRotation = Mathf.Atan2(toTargetVector.y, toTargetVector.x) * Mathf.Rad2Deg;
        _aim.transform.rotation = Quaternion.Lerp(_aim.transform.rotation, Quaternion.Euler(0, 0, zRotation), Time.deltaTime * rotationSpeed);
    }

    private void Shoot()
    {
        Instantiate(_controller.BulletPrefab, _aim.position, _aim.rotation);
    }

    private void PatrolState()
    {
              
        _controller.transform.position = Vector3.MoveTowards(_controller.transform.position, _controller.PatrolPoints[_patrolIndex].position, speed * Time.deltaTime);

        if (Vector2.Distance(_controller.transform.position, _controller.PatrolPoints[_patrolIndex].position) < 0.5f)
        {
            if (patrolWaitTime <= 0)
            {
                patrolCount++;
                _patrolIndex = GetPoint();
                patrolWaitTime = patrolStartWaitTime;
                int rnd = UnityEngine.Random.Range(1, 3);
                if (rnd == 1)
                {
                    if (patrolCount > 2)
                        _IsElipseAttack = true;
                }
            }
            else
            {
                patrolWaitTime -= Time.deltaTime;
               
            }
        }
    }

    private void ElipseAttack()
    {
        Debug.Log(_patrolIndex);
        clockwise = _patrolIndex == 0 ? true : false;
  
       
        if (!_IsStartPositionSet)
        {
            _IsStartPositionSet = true;
            if (clockwise)
            {
                OrbitProgress = 0.25f;
            }
            else
            {
                OrbitProgress = 0.75f;
            }
        }

        if (clockwise)
        {
            OrbitProgress += Time.deltaTime * orbitSpeed;
           // Debug.Log("clockwise" + OrbitProgress);
            if (OrbitProgress > 0.75f)
            {
                _IsElipseAttack = false;
            }
        }
        if (!clockwise)
        {
            OrbitProgress -= Time.deltaTime * orbitSpeed;
           // Debug.Log("!clockwise" + OrbitProgress);
            if (OrbitProgress < 0.25f)
            {
                _IsElipseAttack = false;
            }
        }

        if (_IsElipseAttack)
        {
            Vector3 delta = ellipse.Evaluate(OrbitProgress);
            _controller.transform.position = centerPoint.position + delta;
        }
        else
        {
            _IsStartPositionSet = false; 
            _patrolIndex = GetPoint();
        }

    }

    private int GetPoint()
    {
        if (_patrolIndex == 0)
        {
            _patrolIndex = 2;
            return 2;
        }
        else
        {
            _patrolIndex = 0;
            return 0;
        }
       
    }

}

