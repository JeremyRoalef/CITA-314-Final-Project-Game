using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    TMP_Text enemiesLeftText;

    [SerializeField]
    GameObject winText;

    const string ENEMIES_LEFT_STRING = "Enemies Left: ";
    int enemiesLeft = 0;

    public void AdjustEnemiesLeft(int amount)
    {
        enemiesLeft += amount;

        if (enemiesLeft <= 0)
        {
            winText.SetActive(true);
        }

        enemiesLeftText.text = ENEMIES_LEFT_STRING + enemiesLeft.ToString();
    }

    public void RestartLevelButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitButton()
    {
        Application.Quit();
    }


}
