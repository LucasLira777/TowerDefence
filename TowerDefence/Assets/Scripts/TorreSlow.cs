using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorreSlow : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firingPoint;

    [Header("Attributes")]
    [SerializeField] private float targetingRange = 5f;
    [SerializeField] private float bps = 1f;
    [SerializeField] private float slowAmount = 0.5f; // Percentual de redução de velocidade
    [SerializeField] private float slowDuration = 2f; // Duração do efeito slow

    private Transform target;
    private float timeUntilFire;

    public class SlowBullet : MonoBehaviour
    {
        private Transform target;
        private float slowAmount;
        private float slowDuration;

        public void SetTarget(Transform target, float slowAmount, float slowDuration)
        {
            this.target = target;
            this.slowAmount = slowAmount;
            this.slowDuration = slowDuration;
            StartCoroutine(SlowEffect());
            Destroy(gameObject, 2f); // Destroi a bala após 2 segundos
        }

        private IEnumerator SlowEffect()
        {
            if (target.TryGetComponent(out Enemy enemy)) // Certifique-se de que o alvo tem um script Enemy
            {
                enemy.ApplySlow(slowAmount, slowDuration);
            }
            yield return null;
        }
    }

    private void Update()
    {
        if (target == null)
        {
            FindTarget();
            return;
        }

        if (!CheckTargetIsInRange())
        {
            target = null;
        }
        else
        {
            timeUntilFire += Time.deltaTime;

            if (timeUntilFire >= 1f / bps)
            {
                Shoot();
                timeUntilFire = 0f;
            }
        }
    }

    private void Shoot()
    {
        GameObject bulletObj = Instantiate(bulletPrefab, firingPoint.position, Quaternion.identity);
        SlowBullet bulletScript = bulletObj.GetComponent<SlowBullet>();
        bulletScript.SetTarget(target, slowAmount, slowDuration);
    }

    private void FindTarget()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, targetingRange, Vector2.zero, 0f, enemyMask);

        if (hits.Length > 0)
        {
            target = hits[0].transform;
        }
    }

    private bool CheckTargetIsInRange()
    {
        return Vector2.Distance(target.position, transform.position) <= targetingRange;
    }
}