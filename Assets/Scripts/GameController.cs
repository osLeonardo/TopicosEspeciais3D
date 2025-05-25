using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class GameController : MonoBehaviour
{
    public int playerLife = 3;
    public int maxLife = 3;
    public TextMeshProUGUI lifeCounter;
    public TextMeshProUGUI ammoCounter;
    public TextMeshProUGUI roundCounter;
    public static GameController Instance { get; private set; }
    public AudioSource doubleAudioSource;
    public AudioSource regenAudioSource;
    public AudioSource roundStartAudioSource;

    public void DoubleMaxLife()
    {
        doubleAudioSource.Play();
        maxLife *= 2;
        lifeCounter.text = $"{playerLife} | {maxLife}";
    }

    public void RegenLife()
    {
        regenAudioSource.Play();
        playerLife = maxLife;
        lifeCounter.text = $"{playerLife} | {maxLife}";
    }

    public void KillPlayer()
    {
        playerLife = 0;
        UpdateLife(playerLife);
        Debug.LogWarning("Morreu");
    }

    public void UpdateLife(int life)
    {
        lifeCounter.text = $"{life} | {maxLife}";
    }

    public void UpdateAmmo(int current, int max)
    {
        ammoCounter.text = $"{current} | {max}";
    }

    public void UpdateRound(int round)
    {
        roundStartAudioSource.Play();
        roundCounter.text = $"Round {round}";
    }
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
    }
}
