using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class GameController : MonoBehaviour
{
    public int playerLife = 3;
    public int maxLife = 3;
    public int currentRound = 1;
    public TextMeshProUGUI lifeCounter;
    public TextMeshProUGUI ammoCounter;
    public TextMeshProUGUI roundCounter;
    public DeathSequence deathSequence;
    public AudioSource upgradeAudioSource;
    public AudioSource regenAudioSource;
    public AudioSource roundStartAudioSource;
    
    public static GameController Instance { get; private set; }
    public static int LastRound { get; private set; }

    public void UpgradeMaxLife()
    {
        upgradeAudioSource.Play();
        maxLife += 1;
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
        LastRound = currentRound;
        playerLife = 0;
        UpdateLife(playerLife);
        deathSequence.StartDeathSequence();
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
        currentRound = round;
        roundStartAudioSource.Play();
        roundCounter.text = $"Round {round}";
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
