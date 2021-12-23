using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeView : MonoBehaviour
{
    [SerializeField] private ViewFader viewFader;
    
    private void Awake() => Initialize();

    private void OnEnable()
    {
        viewFader.OnFadeInConpletedEvent += ActivateViewContent;
        viewFader.OnFadeOutConpletedEvent += DisableViewContent;

        GamePause.PauseGame(true);
        StartCoroutine(viewFader.FadeIn());
    }

    private void OnDisable()
    {
        viewFader.OnFadeInConpletedEvent -= ActivateViewContent;
        viewFader.OnFadeOutConpletedEvent -= DisableViewContent;
    }

    private void Initialize()
    {
        viewFader = GetComponent<ViewFader>();
    }

    public void ActivateViewContent()
    {
        StartCoroutine(viewFader.FadeAway());
    }

    private void DisableViewContent()
    {
        GamePause.PauseGame(false);
        this.gameObject.SetActive(false);
    }
}   