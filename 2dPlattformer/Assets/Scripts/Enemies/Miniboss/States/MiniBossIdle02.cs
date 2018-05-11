using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "States/Miniboss/MiniBossIdle02")]
public class MiniBossIdle02 : State {

    public float shieldDownTime;
    public bool shield;
    public Color shieldColor = Color.cyan;
    public Transform shieldTrigger;
    public ShieldDownMiniBoss shieldDownMiniBoss;
    public bool shieldCoroutine;
    private Renderer _bodyRender;
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
   
  

    private int _patrolIndex;
    public LayerMask CollisionLayers;
    private bool _IsElipseAttack = false;
    private bool _IsStartPositionSet;
    private int patrolCount;

    public override void Initialize(Controller owner)
    {
        _controller = (MiniBossController)owner;
        Transform _body = _controller.transform.Find("Body");
        _bodyRender = _body.gameObject.GetComponent<Renderer>();
        shieldTrigger = _controller.transform.Find("SecondeState");
        shieldTrigger = shieldTrigger.transform.Find("ShieldTrigger");
        shieldDownMiniBoss= shieldTrigger.GetComponent<ShieldDownMiniBoss>();
        _patrolIndex = 1;

    }

    public override void Enter()
    {
        _bodyRender.material.color = Color.cyan;
        shieldTrigger.gameObject.SetActive(true);
        _controller.manager.TakeDamage = false;
        shieldCoroutine = false;
        centerPoint = _controller.PatrolPoints[1].transform;
        patrolWaitTime = patrolStartWaitTime;
    }

    public override void Update()
    {
        _controller.UpdateHealth();
        SheildHit();
        if (_IsElipseAttack)
        {
            ElipseAttack();
        }
        else
        {
            PatrolState();
        }
        _controller.GetState<MiniBossIdle01>().ShootRoutine();

        if (_controller.health == 0)
            _controller.TransitionTo<DeathState>();
    }

  

    public override void Exit()
    {
        base.Exit();
    }

    private void SheildHit()
    {
        if (shieldDownMiniBoss.isSheild == true)
        {
            _bodyRender.material.color = Color.cyan;
        }
        else
        {
            if (shieldCoroutine) return;

            _controller.manager.TakeDamage = true;
            _bodyRender.material.color = Color.red;
            _controller.StartCoroutine(ShieldDown());
            shieldCoroutine = true;
        }
    }

    private IEnumerator ShieldDown()
    {
        yield return new WaitForSeconds(shieldDownTime);
        shieldDownMiniBoss.isSheild = true;
        _controller.manager.TakeDamage = false;
        shieldCoroutine = false;
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
        Debug.Log("elipse");
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
            Debug.Log("clockwise" + OrbitProgress);
            if (OrbitProgress > 0.75f)
            {
                _IsElipseAttack = false;
            }
        }
        if (!clockwise)
        {
            OrbitProgress -= Time.deltaTime * orbitSpeed;
            Debug.Log("!clockwise" + OrbitProgress);
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

