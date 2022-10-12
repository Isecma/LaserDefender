using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIDisplay : MonoBehaviour
{
    GameObject player;
    Health health;
    int maxHealth;
    ScoreKeeper scoreKeeper;

    TextMeshProUGUI scoreText;
    Slider healthSlider;

    
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        health = player.GetComponent<Health>();
        maxHealth = health.GetHealth();
        scoreText = FindObjectOfType<TextMeshProUGUI>();  
        healthSlider = FindObjectOfType<Slider>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();

    }

    
    void Update()
    {
        UpdateHealthSlider();
        UpdateScore();
    }

    void UpdateHealthSlider()
    {
        if (player != null)
        { 
            int currentHealth = health.GetHealth();
            healthSlider.value = currentHealth / (float)maxHealth;
        }
        else if (player == null)
        {
            healthSlider.value = 0;
        }
    }

    void UpdateScore()
    {
        scoreText.text = scoreKeeper.GetCurrentScore().ToString("000000000");
    }
}
