using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreenUI : MonoBehaviour
{
    public TextMeshProUGUI roundsText;

    private void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        roundsText.text = $"You survived {GameController.LastRound} rounds";
    }

    public void OnRestart() => SceneManager.LoadScene("SampleScene");

    public void OnQuit() => Application.Quit();
}