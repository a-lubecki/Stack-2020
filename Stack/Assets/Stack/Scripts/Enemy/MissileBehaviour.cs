using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Pool;
public class MissileBehaviour : MonoBehaviour
{

    Rigidbody rb;
    public Transform target;
    public GameObject dust;
    public GameObject explosionEffect;
    public float speed = 9f, rotationSpeed = 120f, dustWait = .05f;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        StartCoroutine(makingDust());

    }

    void FixedUpdate()
    {
        rb.velocity = transform.up * speed;
        if (target != null)
        {
            Vector3 direction = (Vector3)target.position - rb.position;
            direction.Normalize();
            float angle = Vector3.Cross(direction, transform.up).z;
            rb.angularVelocity = -rotationSpeed * direction;
        }
    }

    IEnumerator makingDust()
    {
        while (gameObject)
        {
            yield return new WaitForSeconds(dustWait);
            GameObject dustTemp = Instantiate(dust, transform.position, dust.transform.rotation);
            Destroy(dustTemp, 2f);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        string tag = other.gameObject.tag;
        if (tag.Equals("Fruits"))
        {
            blowUpPlane(other.gameObject.transform);
        }
        if (tag.Equals("missile"))
        {
            blowUpSelf();
        }
    }



    void blowUpPlane(Transform plane)
    {
        blowUpSelf();
        plane.parent.GetComponent<BlockBehavior>();
        

    }

    void blowUpSelf()
    {
        GameObject tempExplosion = Instantiate(explosionEffect, transform.position, dust.transform.rotation);
        //Destroy(tempExplosion, 1.2f);
        LeanPool.Despawn(gameObject);
        LeanPool.Despawn(tempExplosion);
    }

}
