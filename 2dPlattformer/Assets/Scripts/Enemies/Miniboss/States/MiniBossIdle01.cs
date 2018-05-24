using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "States/Miniboss/MiniBossIdle01")]
public class MiniBossIdle01 : State
{
    
  
    public Vector2 aimOffset;
    public float switchHealth;
    public float transitionTime;
    private bool start = false;

    public int shotBeforeAttack = 5;
    private MiniBossController _controller;
    private Transform _aim;
    public float startTime = 3f;
    public bool startOnce = false;

 
    public float fireRate = 10;  // The number of bullets fired per second
    private float nextFire;   // The value of Time.time at the last firing moment

    public int shootcount = 0;
    public float rotationSpeed = 5;
   


    public override void Initialize(Controller owner)
    {
        
        _controller = (MiniBossController)owner;
        _controller.Gravity = -30;
        _aim = _controller.transform.GetChild(0);
        _controller.manager.setTakeDamage(false);


    }

    public override void Enter()
    {
        _controller.manager.bodyRender.sprite = _controller.manager.bossSprites[0];
        if (_controller.timer == _controller.startTime)
        {
            _controller.manager.setTakeDamage(false);
        }
        else
        {
            _controller.manager.setTakeDamage(true);
        }

        shootcount = 0;
    }

    public override void Update()
    {

        _controller.timer -= Time.deltaTime;

        if (_controller.timer <= 0)
        {
           
            if (shootcount < shotBeforeAttack)
            {
                ShootRoutine();
            }
            else
            {
                _controller.TransitionTo<AttackState>();
            }
        }


        if (_controller.collisions.above || _controller.collisions.below)
        {
            _controller.Velocity.y = 0;
        }

        _controller.Velocity.y += _controller.Gravity * Time.deltaTime;
        _controller.Move(_controller.Velocity * Time.deltaTime);

        TransitionToAttack();
    }

    public override void Exit()
    {
        _controller.manager.bodyRender.sprite = _controller.manager.bossSprites[1];
        _controller.manager.setTakeDamage(false);
        shootcount = 0;
    }

    public void ShootRoutine()
    {
        RotateWeapon();

        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            shootcount++;
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

   public void TransitionToAttack()
    {
        if (_controller.manager.currentHealth <= switchHealth)
        {
            _controller.TransitionTo<BetweenIdle01To02>();
        }
    }

    public void Damage()
    {
        _controller.manager.HitDamage(1);
    }

    IEnumerator StartTime()
    {

        yield return new WaitForSeconds(startTime);
        _controller.manager.setTakeDamage(true);
        startOnce = true;
        start = true;
   
    }
}
