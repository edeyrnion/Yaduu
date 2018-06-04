using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    // Track the Animation Component
    // Track the AnimationClips for fade in/out
    // Function that can receive animation events
    // Functions to play fade in/out animations

    [SerializeField] private Animation _mainMenuAnimator;
    [SerializeField] private AnimationClip _fadeOutAnimation;
    [SerializeField] private AnimationClip _fadeInAnimation;

    private void Start()
    {
        GameManager.Instance.OnGameStateChanged.AddListener(HandleGameStateChanged);
    }

    public void OnFadeOutComplete()
    {
        Debug.LogWarning("FadeOut Complete");
    }

    public void OnFadeInComplete()
    {
        Debug.LogWarning("FadeIn Complete");

        UIManager.Instance.SetDummyCameraActiv(true);
    }

    void HandleGameStateChanged(GameManager.GameState currentState, GameManager.GameState previousState)
    {
        if (previousState == GameManager.GameState.PREGAME && currentState == GameManager.GameState.RUNNING)
        {
            FadeOut();
        }
    }

    public void FadeIn()
    {
        _mainMenuAnimator.Stop();
        _mainMenuAnimator.clip = _fadeInAnimation;
        _mainMenuAnimator.Play();
    }

    public void FadeOut()
    {
        UIManager.Instance.SetDummyCameraActiv(false);

        _mainMenuAnimator.Stop();
        _mainMenuAnimator.clip = _fadeOutAnimation;
        _mainMenuAnimator.Play();
    }
}
