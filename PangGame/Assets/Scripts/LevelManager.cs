using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class LevelManager : MonoBehaviour
{
    public GameObject LargeBall, LargestBall, BigGreenBall, youWonText;
    public static int LevelCounter = 0;
    public Text LevelText;

    private bool allowLvl1 = true, allowLvl2 = true, allowLvl3 = true;

    // In this function we are choosing the level of the game by looking on how mach balls are destroyed.
    // Ofcurse that this could have bin done in a more elegant way, By creating an enemy spawn class.
    // But this is a small game + i wanted to add animations and other things for the game that will make the game better.
    public void NextLevel()
    {
        if (LevelCounter == 3 && allowLvl1)
        {
            ScoreManagerScript.AddScore();
            ScoreManagerScript.UpdateNewScore();
            LevelText.text = "Level: 2";
            LevelCounter = 0;
            allowLvl1 = false;
            LargeBall.SetActive(true);
        }
        else if (LevelCounter == 7 && allowLvl2)
        {
            ScoreManagerScript.AddScore();
            ScoreManagerScript.UpdateNewScore();
            LevelText.text = "Level: 3";
            LargestBall.SetActive(true);
            LevelCounter = 0;
            allowLvl2 = false;
        }
        else if (LevelCounter == 15 && allowLvl3)
        {
            ScoreManagerScript.AddScore();
            ScoreManagerScript.UpdateNewScore();
            LevelText.text = "Level: 4";
            BigGreenBall.SetActive(true);
            LevelCounter = 0;
            allowLvl3 = false;
            youWonText.SetActive(true);

            this.Wait(2f, () =>
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
            });

        }
    }


    private void Update()
    {
        NextLevel();
    }

    void Start()
    {
        youWonText.SetActive(false);
        LevelText = LevelText.GetComponent<Text>();
        LevelText.text = "Level: 1";
    }
}
