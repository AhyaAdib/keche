using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nearestTarget : MonoBehaviour
{
    public LayerMask targetLayer; // Layer of the objects you want to approach
    public float approachSpeed = 5.0f; // peed at which the object approaches the target
    public static nearestTarget instance;
    public bool isSearchNear = true;

    public Transform target; // Reference to the target object

    void Start()
    {
        instance = this;
        StartCoroutine(FindNearestTarget());
    }

    void Update()
    {
        
    }

    IEnumerator FindNearestTarget()
    {
        while(true)
        {
            if(isSearchNear)
            {
                yield return new WaitForSeconds(.5f);
                
                Collider2D[] targets = Physics2D.OverlapCircleAll(transform.position, Mathf.Infinity, targetLayer);
                float closestDistance = Mathf.Infinity;
                Transform nearestTarget = null;

                foreach (Collider2D potentialTarget in targets)
                {
                    float distance = Vector2.Distance(transform.position, potentialTarget.transform.position);
                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        nearestTarget = potentialTarget.transform;
                    }
                }

                target = nearestTarget;
            } else {
                target = GameObject.FindGameObjectWithTag("Player").transform;
            }
        }
    }
}
