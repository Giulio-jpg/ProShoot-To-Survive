using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] protected float bulletSpeed;
    [SerializeField] public Vector3 dir;

    public float DamageDealt;

    [SerializeField] protected Rigidbody rb;

    [SerializeField] protected float lifetime;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    protected virtual void FixedUpdate()
    {
        StandardMovement();
    }

    private void Update()
    {
        lifetime -= Time.deltaTime;
        if (lifetime <= 0)
        {
            Destroy(gameObject);
        }
    }

    protected void StandardMovement()
    {
        rb.velocity = dir * bulletSpeed * Time.fixedDeltaTime;
    }
    protected void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);

    }
}
