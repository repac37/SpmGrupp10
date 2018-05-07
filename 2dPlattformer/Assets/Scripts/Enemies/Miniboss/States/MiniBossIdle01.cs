﻿using System;
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
 

    public WeaponData weapon;
    private MiniBossController _controller;
    private Transform _aim;
    public float startTime = 3f;

 
    public float fireRate = 10;  // The number of bullets fired per second
    public float nextFire;   // The value of Time.time at the last firing moment

    public int shootcount = 0;
   
        


    public override void Initialize(Controller owner)
    {
  
        _controller = (MiniBossController)owner;
        _controller.Gravity = -30;
        _aim = _controller.transform.GetChild(0);
        _controller.manager.TakeDamage = false;

    }

    public override void Enter()
    {
        _controller.StartCoroutine(StartTime());
       

    }

    public override void Update()
    {

   
        _controller.UpdateHealth();
        if (start)
        {
            if (shootcount < 4)
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
        shootcount = 0;
    }


    public void ShootRoutine()
    {
        
        Vector3 toTargetVector = _controller.target.position - _aim.transform.position;
        float zRotation = Mathf.Atan2(toTargetVector.y, toTargetVector.x) * Mathf.Rad2Deg;
        _aim.transform.rotation = Quaternion.Euler(new Vector3(0, 0, zRotation));

        //Vector3 direction = _controller.target.position- _aim.transform.position;
        //Quaternion rotation = Quaternion.LookRotation(direction);
        //_aim.transform.rotation = rotation;
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            shootcount++;
            Shoot();
        }

        //if (!_shootOne && !_shootTwo)
        //{
        //    _aim.transform.Rotate((Vector3.forward * 5) * Time.deltaTime);

        //    if (_aim.transform.rotation.eulerAngles.z >= 3.5f)
        //    {
        //        _shootOne = true;
        //        Shoot();
        //    }
        //}

        //if (_shootOne && !_shootTwo)
        //{
        //    _aim.transform.Rotate(-(Vector3.forward * 5) * Time.deltaTime);
        //    if (_aim.transform.rotation.eulerAngles.z <= 356.5f && _aim.transform.rotation.eulerAngles.z > 3.5)
        //    {
        //        _shootTwo = true;
        //        Shoot();
        //    }
        //}

        //if (_shootOne && _shootTwo)
        //{ 
        //    if (_aim.transform.rotation.eulerAngles.z != 0)
        //    {
        //        _aim.transform.Rotate((Vector3.forward * 5) * Time.deltaTime);
        //        if (_aim.transform.rotation.eulerAngles.z > 359.8 || _aim.transform.rotation.eulerAngles.z < 0.2f)
        //        {
        //            _aim.transform.rotation = Quaternion.identity;
        //        }
        //    }

        //}
    }

    private void Shoot()
    {
       Instantiate(weapon.bullet, _aim.position, _aim.rotation);
        
    }
 

    //IEnumerator IsShooting()
    //{
    //    yield return new WaitForSeconds(transitionTime/3);
    //    _isShooting = true;
    //}

   public void TransitionToAttack()
    {
        if (_controller.health <= switchHealth)
        {
            _controller.TransitionTo<BetweenIdle01To02>();
        }
    }

    public void Damage()
    {
        _controller.health--;
    }

    IEnumerator StartTime()
    {

        yield return new WaitForSeconds(startTime);
        _controller.manager.TakeDamage = true;
        start = true;
   
    }
}
