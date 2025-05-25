using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DeathSequence : MonoBehaviour
{
    public Image fadeImage;
    public float fadeDuration = 2f;
    public string deathSceneName = "DeathScreen";

    public void StartDeathSequence() => StartCoroutine(DeathRoutine());

    private System.Collections.IEnumerator DeathRoutine()
    {
        Time.timeScale = 0f;

        var elapsed = 0f;
        var color = fadeImage.color;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.unscaledDeltaTime;
            color.a = Mathf.Clamp01(elapsed / fadeDuration);
            fadeImage.color = color;
            yield return null;
        }

        var wait = 0f;
        while (wait < fadeDuration)
        {
            wait += Time.unscaledDeltaTime;
            yield return null;
        }

        Time.timeScale = 1f;
        SceneManager.LoadScene(deathSceneName);
    }
}