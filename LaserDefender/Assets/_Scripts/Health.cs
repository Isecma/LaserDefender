using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] bool isPlayer;
    [SerializeField] int health = 50;
    [SerializeField] int scoreValue = 50;
    [SerializeField] ParticleSystem hitEffect;
    [SerializeField] AudioClip hitSFX;

    [SerializeField] bool applyCameraShake;

    CameraShake cameraShake;
    ScoreKeeper scoreKeeper;

    void Awake()
    {
        cameraShake = Camera.main.GetComponent<CameraShake>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
       DamageDealer damageDealer = collision.GetComponent<DamageDealer>();
        
        if (damageDealer != null)
        {
            TakeDamage(damageDealer.GetDamage());
            PlayHitSFX();
            PlayHitEffect();
            ShakeCamera();
            damageDealer.Hit();
        }
    }

    void TakeDamage(int damageTaken)
    {

        health -= damageTaken;
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (!isPlayer)
        {
            scoreKeeper.ModifyScore(scoreValue);
        }
        Destroy(gameObject);
    }

    void PlayHitEffect()
    {
        ParticleSystem instance = Instantiate(hitEffect, transform.position, Quaternion.identity);
        Destroy(instance.gameObject, instance.main.duration + instance.main.startLifetime.constant);
    }

    void ShakeCamera()
    {
        if (applyCameraShake)
        {
            cameraShake.Play();
        }
    }

    void PlayHitSFX()
    {
        AudioSource.PlayClipAtPoint(hitSFX, transform.position);
    }

    public int GetHealth()
    {
        return health;
    }
}
