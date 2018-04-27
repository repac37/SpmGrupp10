using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider2D))]
public class PlayerController : Controller
{
    
    public float MaxSpeed; //sekund
    public LayerMask CollisionLayers; //CollisionLayers är de lager som karaktären kan kollidera med
    public float Gravity; // Gravity är gravitationen som appliceras på spelaren bestämms i ground
    public Vector2 Velocity; //Velocity är spelarens nuvarande hastighet också i units/sekund
    public float GroundCheckDistance; //GroundCheckDistance kommer användas för kollisionsdetektering
    public float InputMagnitudeToMove; //InputmagnitudeToMove har med input och göra, täcks i GroundState
    public MinMaxFloat SlopeAngles; //SlopeAngles användMaxSpeed kommer vara våran karaktärs topphastighet i units/s för CheckAllowedSlope
    public static float fuel = 3f;

    [HideInInspector]
    public BoxCollider2D Collider; //Collider är spelarens boxcollider

    private void Start()
    {
        Collider = GetComponent<BoxCollider2D>();;
    }

    public RaycastHit2D[] DetectHits(bool addGroundCheck = false)
    {
        //spelarens hastighet
        Vector2 direction = Velocity.normalized;

        //Hur långt spelaren förflyttar på en frame
        float distance = Velocity.magnitude * Time.deltaTime;

        //Player position plus boxcollidens offset;
        Vector2 position = transform.position + (Vector3)Collider.offset;

        //BoxCastAll(start,boxStorlek,angel, riktining, längd, filter f )
        //Skapar en lista med alla träffar
        List<RaycastHit2D> hits = Physics2D.BoxCastAll(position, Collider.size, 0.0f, direction, distance, CollisionLayers).ToList();
        Debug.DrawRay(position, direction, Color.green);
       
        //Skapar en array med alla träffar på väldigt nära avstånd neråt 
        RaycastHit2D[] groundHits = Physics2D.BoxCastAll(position, Collider.size, 0.0f, Vector2.down, GroundCheckDistance, CollisionLayers);
        Debug.DrawRay(position, Vector2.down, Color.red);
        //addera alla träffa neråt från nära avstånd till spelar riktningens Lista
        hits.AddRange(groundHits);

        for (int i = 0; i < hits.Count; i++)
        {
            //Kollar så vi inte har träffat i eller en kollider, och är den det så bytes den ut mot en träff på den första kanten den träffar.
            RaycastHit2D safetyHit = Physics2D.Linecast(position, hits[i].point, CollisionLayers);
            if (safetyHit.collider != null)
            {
                hits[i] = safetyHit;
            }
                
        }
        return hits.ToArray();
    }

    public void SnapToHit(RaycastHit2D hit)
    {
   
        Vector2 vectorToPoint = hit.point - (Vector2)transform.position;
        
        vectorToPoint -= MathHelper.PointOnRectangle(vectorToPoint.normalized, Collider.size);

        Vector3 movement = (Vector3)hit.normal * Vector2.Dot(hit.normal, vectorToPoint.normalized) * vectorToPoint.magnitude;
       
        if (Vector2.Dot(Velocity.normalized, vectorToPoint.normalized) > 0.0f)
        {
            transform.position += movement;
        }
            
    }

}
