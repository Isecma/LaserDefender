using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [Header("General")]
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileLifetime = 5f;
    [SerializeField] List<AudioClip> shotsVFXList;

    [Header ("AI")]
    [SerializeField] bool useAI;
    [SerializeField] float minFiringRate = 0.8f;
    [SerializeField] float maxFiringRate = 1.5f;

    [HideInInspector] public bool isFiring;

    Coroutine fireCoroutine;

    void Start()
    {
        if (useAI)
        {
            isFiring = true;
        }
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
            PlayShootingSFX();
            GameObject projectileInstance = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            Rigidbody2D projectileRigidbody = projectileInstance.GetComponent<Rigidbody2D>();
            projectileRigidbody.velocity = transform.up * projectileSpeed;
            Destroy(projectileInstance, projectileLifetime);
            yield return new WaitForSeconds(Random.Range(minFiringRate, maxFiringRate));
        }

    }

    void PlayShootingSFX()
    {
        int randomShotVFXIndex = Random.Range(0, shotsVFXList.Count);
        AudioClip randomShotVFX = shotsVFXList[randomShotVFXIndex];
        AudioSource.PlayClipAtPoint(randomShotVFX, transform.position);
    }
}
