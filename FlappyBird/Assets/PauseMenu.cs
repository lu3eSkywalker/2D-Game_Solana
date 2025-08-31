using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PauseMenu : MonoBehaviour
{
    public GameObject pausePanel;
    public Button pauseButton;
    public Button resumeButton;
    public TMP_InputField walletInputField; // input field for wallet
    public Button claimButton; // claim button

    private void Start()
    {
        pausePanel.SetActive(false);

        pauseButton.onClick.AddListener(PauseGame);
        resumeButton.onClick.AddListener(ResumeGame);
        claimButton.onClick.AddListener(ClaimReward);
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        pausePanel.SetActive(true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        pausePanel.SetActive(false);
    }

    public void ClaimReward()
    {
        string walletAddress = walletInputField.text;
        if (string.IsNullOrEmpty(walletAddress))
        {
            Debug.Log("Please enter a wallet address.");
        }
        else
        {
            Debug.Log("Claiming reward for wallet: " + walletAddress);
        }
    }
}
