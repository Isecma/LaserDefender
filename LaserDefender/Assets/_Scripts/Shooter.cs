using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileLifetime = 5f;
    [SerializeField] float firingRate = 0.2f;

    public bool isFiring;

    Coroutine fireCoroutine;

    void Start()
    {
        
    }

    void Update()
    {
        Fire();
    }

    void Fire()
    {
        if (isFiring && fireCoroutine == null)
        {
            fireCoroutine = StartCoroutine(FireContinously());
        }
        else if (!isFiring && fireCoroutine != null)
        {
            StopCoroutine(fireCoroutine);
            fireCoroutine = null;
        }
        
    }

    IEnumerator FireContinously()
    {
        while (true)
        {
            GameObject projectileInstance = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            Rigidbody2D projectileRigidbody = projectileInstance.GetComponent<Rigidbody2D>();
            projectileRigidbody.velocity = transform.up * projectileSpeed;
            Destroy(projectileInstance, projectileLifetime);
            yield return new WaitForSeconds(firingRate);
        }

    }
}
