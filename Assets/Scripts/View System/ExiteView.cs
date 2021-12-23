using UnityEngine;
using UnityEngine.UI;

public class ExiteView : MonoBehaviour
{
    [SerializeField] private Button continueButton;
    [SerializeField] private Button exiteButton;
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
        continueButton.onClick.AddListener(() => Continue());
        exiteButton.onClick.AddListener(() => QuiteGame());

        viewFader = GetComponent<ViewFader>();
    }

    private void ActivateViewContent()
    {
        continueButton.gameObject.SetActive(true);
        exiteButton.gameObject.SetActive(true);
    }

    private void DisableViewContent()
    {
        this.gameObject.SetActive(false);
    }

    private void Continue()
    {
        GamePause.PauseGame(false);
        StartCoroutine(viewFader.FadeAway());
    }

    private void QuiteGame()
    {
        Application.Quit();
    }
}
