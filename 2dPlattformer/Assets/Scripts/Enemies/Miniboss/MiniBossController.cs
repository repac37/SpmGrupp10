using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class MiniBossController : Controller {

    private float health;

    public float Gravity = -20; // Gravity är gravitationen som appliceras på spelaren bestämms i ground
    public Vector2 Velocity; //Velocity är spelarens nuvarande hastighet också i units/sekund

    private const float SkinWidth = 0.015f;
    [HideInInspector]
    public BoxCollider2D Collider;
    private RaycastOrigins _raycastOrigins;

    public int horizontalRayCount = 4;
    public int verticalRayCount = 4;
    public LayerMask collisionMask;
    public CollisionInfo collisions;
    public Transform[] AttackPoints;
    public Transform[] PatrolPoints;
    public BossManager manager;
    public Transform target;
    public Transform BulletPrefab;

    public float shieldDownTime = 3f;

    private Transform startPos;

    public float startTime = 2f;
    public float timer;

    private float _horizontalRaySpacing;
    private float _verticalRaySpacing;
    private float meleCountdown;
    private float meleCountdownStart = 3;

    private void Start()
    {
       
        Collider = GetComponent<BoxCollider2D>();
        CalculateRaySpacing();
        startPos = transform;
        GameObject tmp = GameObject.FindGameObjectWithTag("Player");
        target = tmp.transform;
    }

    private void OnEnable()
    {
        timer = startTime;
    }
    private void OnDisable()
    {
        timer = startTime;
    }

    public void Move(Vector2 velocity)
    {
        UpdateRaycastOrigins();
        collisions.Reset();

        if (velocity.x != 0)
        {
            HorizontalCollisions(ref velocity);// ref gör så att du kan ändra velocity i andra funktioner som i C skicka referense
        }
        if (velocity.y != 0)
        {
            VerticalCollisions(ref velocity);
        }

        transform.Translate(velocity);
    }

    private void HorizontalCollisions(ref Vector2 velocity)
    {
        float directionX = Mathf.Sign(velocity.x);  // retunerar +1 om positive, noll vid 0 och -1 vi negative 
        float rayLength = Mathf.Abs(velocity.x) + SkinWidth; // Mathf.Abs tvingar värder att bli positivt
        for (int i = 0; i < verticalRayCount; i++)
        {
            Vector2 rayOrigin = (directionX == -1) ? _raycastOrigins.bottomLeft : _raycastOrigins.bottomRight;
            rayOrigin += Vector2.up * (_horizontalRaySpacing * i);
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * directionX, rayLength, collisionMask);

            Debug.DrawRay(rayOrigin, Vector2.right * directionX * rayLength, Color.red);

            if (hit)
            {
                velocity.x = (hit.distance - SkinWidth) * directionX;
                rayLength = hit.distance;

                collisions.left = directionX == -1;
                collisions.right = directionX == 1;
            }
        }
    }

    private void VerticalCollisions(ref Vector2 velocity)
    {
        float directionY = Mathf.Sign(velocity.y);  // retunerar +1 om positive, noll vid 0 och -1 vi negative 
        float rayLength = Mathf.Abs(velocity.y) + SkinWidth; // Mathf.Abs tvingar värder att bli positivt

        for (int i = 0; i < verticalRayCount; i++)
        {
            Vector2 rayOrigin = (directionY == -1) ? _raycastOrigins.bottomLeft : _raycastOrigins.topLeft;
            rayOrigin += Vector2.right * (_verticalRaySpacing * i + velocity.x);
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up * directionY, rayLength, collisionMask);

            Debug.DrawRay(rayOrigin, Vector2.up * directionY * rayLength, Color.red);

            if (hit)
            {
                velocity.y = (hit.distance - SkinWidth) * directionY;
                rayLength = hit.distance;

                collisions.below = directionY == -1;
                collisions.above = directionY == 1;
            }
        }
    }

    private void UpdateRaycastOrigins()
    {
        Bounds bounds = GetSkinWidth();

        _raycastOrigins.bottomLeft = new Vector2(bounds.min.x, bounds.min.y);
        _raycastOrigins.bottomRight = new Vector2(bounds.max.x, bounds.min.y);
        _raycastOrigins.topLeft = new Vector2(bounds.min.x, bounds.max.y);
        _raycastOrigins.topRight = new Vector2(bounds.max.x, bounds.max.y);
    }

    private void CalculateRaySpacing()
    {
        Bounds bounds = GetSkinWidth();

        horizontalRayCount = Mathf.Clamp(horizontalRayCount, 2, int.MaxValue);
        verticalRayCount = Mathf.Clamp(verticalRayCount, 2, int.MaxValue);

        _horizontalRaySpacing = bounds.size.y / (horizontalRayCount - 1);
        _verticalRaySpacing = bounds.size.x / (verticalRayCount - 1);
    }

    private Bounds GetSkinWidth()
    {
        Bounds bounds = Collider.bounds;
        bounds.Expand(SkinWidth * -2);
        return bounds;
    }

    private struct RaycastOrigins
    {
        public Vector2 topLeft, topRight;
        public Vector2 bottomLeft, bottomRight;
    }

    public struct CollisionInfo
    {
        public bool above, below;
        public bool left, right;

        public void Reset()
        {
            above = below = false;
            left = right = false;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerManager>().Damage();
            PlayerController p = collision.gameObject.GetComponent<PlayerController>();
        }
        meleCountdown = meleCountdownStart;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            meleCountdown -= Time.deltaTime;
            if (meleCountdown <= 0)
            {
                collision.gameObject.GetComponent<PlayerManager>().Damage();
                meleCountdown = meleCountdownStart;
            }
        }
    }
}
