using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb; // Componente Rigidbody2D da bala para controlar o movimento
    [SerializeField] private float bulletspeed = 5f; // Velocidade da bala

    [SerializeField] private int bulletDamage = 1; // Dano que a bala causa ao atingir um alvo
    private Transform target; // Alvo que a bala ir� seguir

    // M�todo para definir o alvo da bala
    public void SetTarget(Transform _target)
    {
        target = _target;
    }

    private void FixedUpdate()
    {
        // Verifica se h� um alvo definido
        if (!target) { return; }

        // Calcula a dire��o em dire��o ao alvo e move a bala nessa dire��o
        Vector2 direction = (target.position - transform.position).normalized;
        rb.velocity = direction * bulletspeed;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        // Aplica dano ao objeto que a bala colide, se ele tiver o componente de sa�de
        other.gameObject.GetComponent<Health>().TakeDamage(bulletDamage);
        // Destroi a bala ap�s a colis�o
        Destroy(gameObject);
    }
}