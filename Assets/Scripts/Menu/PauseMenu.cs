using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private Button ResumeButton;
    [SerializeField] private Button RestartButton;
    [SerializeField] private Button QuitButton;

    private void Start()
    {
        ResumeButton.onClick.AddListener(HandleResumeCLick);
        RestartButton.onClick.AddListener(HandleRestartCLick);
        QuitButton.onClick.AddListener(HandleQuitCLick);
    }

    void HandleResumeCLick()
    {
        GameManager.Instance.TogglePause();
    }

    void HandleRestartCLick()
    {
        GameManager.Instance.RestartGame();
    }

    void HandleQuitCLick()
    {
        GameManager.Instance.QuitGame();
    }
}
