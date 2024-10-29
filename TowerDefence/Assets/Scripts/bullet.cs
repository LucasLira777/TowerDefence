using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float bulletspeed = 5f;

    [SerializeField] private int bulletDamage = 1;
    private Transform target;
    // Start is called before the first frame update

    public void SetTarget(Transform _target)
    {
        target = _target;
    }
    private void FixedUpdate()
    {
        if (!target) { return; }
        Vector2 direction = (target.position - transform.position).normalized;
        rb.velocity = direction * bulletspeed;
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        other.gameObject.GetComponent<Health>().TakeDamage(bulletDamage);
        other.gameObject.GetComponent <Health>().TakeDamage(bulletDamage);
        Destroy(gameObject);
    }
}