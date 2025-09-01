using UnityEngine;
using UnityEngine.UI;
using TMPro;

using UnityEngine.Networking;
using System.Collections;
using System.Text;


class AcceptAllCertificatesSignedHandler : CertificateHandler
{
    protected override bool ValidateCertificate(byte[] certificateData)
    {
        return true; // Accept any cert
    }
}
public class PauseMenu : MonoBehaviour
{
    public GameObject pausePanel;
    public Button pauseButton;
    public Button resumeButton;
    public TMP_InputField walletInputField; // input field for wallet
    public Button claimButton; // claim button

    public GameManager gameManager;

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
            int currentScore = int.Parse(gameManager.scoreText.text);
            Debug.Log("Claiming reward for wallet: " + walletAddress + " with score: " + currentScore);

            string jsonData = $"{{\"userAddressFromRequest\":\"{walletInputField.text}\", \"tokenToMint\":\"{currentScore}\"}}";
            StartCoroutine(PostRequestForBackendReward("http://localhost:3000/api/v1/backendrequest", jsonData));
        }
    }

    public IEnumerator PostRequestForBackendReward(string uri, string jsonData)
    {
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonData);
        using (UnityWebRequest webRequest = new UnityWebRequest(uri, "POST"))
        {
            webRequest.uploadHandler = new UploadHandlerRaw(bodyRaw);
            webRequest.downloadHandler = new DownloadHandlerBuffer();
            webRequest.SetRequestHeader("Content-Type", "application/json");

            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.ConnectionError ||
                webRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error: " + webRequest.error);
            }
            else
            {
                Debug.Log("Response: " + webRequest.downloadHandler.text);
            }
        }

    }

}
