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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            DoubleMaxLife();
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            ResetMaxLife();
        }
    }

    public void DoubleMaxLife()
    {
        maxLife *= 2;
        lifeCounter.text = $"{playerLife} | {maxLife}";
    }

    public void ResetMaxLife()
    {
        maxLife = 3;
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
        roundCounter.text = $"Round {round}";
    }
}
