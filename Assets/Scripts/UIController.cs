using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject levelUI;
    [SerializeField] private GameObject startPanel;
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject losePanel;
    [SerializeField] private TextMeshProUGUI currentLevelText;
    [SerializeField] private TextMeshProUGUI nextLevelText;

    [SerializeField] private Image[] stageImages;

    public static Action levelFailed;

    private int currentLevel = 1;
    private int currentStage = 0;

    private Color orangeColor;

    void Start()
    {
        Time.timeScale = 0;
        startPanel.SetActive(true);

        orangeColor = new Color(1, 0.3882353f, 0.1882353f);

        levelFailed += LevelFailed;

        ContainerControl.gatesUp += NextStage;
    }

    private void Update()
    {
        if (startPanel.activeSelf && Input.GetMouseButton(0))
        {
            startPanel.SetActive(false);
            levelUI.SetActive(true);

            Time.timeScale = 1;
        }
    }

    private void LevelFailed()
    {
        Time.timeScale = 0;
        losePanel.SetActive(true);
    }

    private void LevelComplete()
    {
        Time.timeScale = 0;
        winPanel.SetActive(true);
    }

    private void LevelUp()
    {
        currentLevel++;

        currentLevelText.text = currentLevel.ToString();
        nextLevelText.text = (currentLevel + 1).ToString();

        for (int i = 0; i < stageImages.Length; i++)
        {
            stageImages[i].color = Color.white;
        }

        currentStage = 0;
    }

    private void NextStage()
    {
        Image currentStageImage = stageImages[currentStage];
        
        if(currentStageImage != null)
            currentStageImage.color = orangeColor;

        currentStage++;

        if(currentStage >= 3)
        {
            StartCoroutine(WaitForNextLevel());
        }
    }

    private IEnumerator WaitForNextLevel()
    {
        yield return new WaitForSeconds(1f);

        LevelUp();

        yield return new WaitForSeconds(0.5f);

        LevelComplete();
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(0);
        startPanel.SetActive(false);
    }

    public void NextLevel()
    {
        winPanel.SetActive(false);
        Time.timeScale = 1;
    }
}
