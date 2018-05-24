using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour {

    public float gravity = 20;
    public bool isGrounded;
    public LayerMask layerMask;
    private Vector2 _velocity;
    private BoxCollider2D _collider;
    private Bounds _bounds;
    private Vector2 _raycastOrigin;

    private void Awak()
    {
        _collider = GetComponent<BoxCollider2D>();
        _bounds = _collider.bounds;
        _raycastOrigin = new Vector2(_bounds.size.x * 0.5f, _bounds.min.y);
       
    }


    void Update () {
        Vector3 downCheck = transform.TransformDirection(Vector3.down * 0.1f);
      
        Debug.DrawRay(new Vector2(transform.position.x, transform.position.y - (_bounds.size.y * 0.5f)), Vector2.down, Color.red,5f);

        // Note this first Debug is just to see your raycast you can delete it after your sure its working right.
        if (!isGrounded)
        {


            Debug.Log(this.gameObject.transform.position);
            RaycastHit2D hit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + (_bounds.size.y * 0.5f)), Vector2.down, 5f, layerMask);

            if (hit)
            {

                isGrounded = true;
                transform.position = hit.point - new Vector2(transform.position.x, transform.position.y + (_bounds.size.y * 0.5f));
            }
            else
            {
                transform.Translate(Vector2.down * gravity * Time.deltaTime);
                isGrounded = false;
            }
        }

    }
    
}
