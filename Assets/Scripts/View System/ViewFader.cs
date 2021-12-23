using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ViewFader : MonoBehaviour
{
    [SerializeField] private Image viewBackgroundImage;

    public event Action OnFadeInConpletedEvent;
    public event Action OnFadeOutConpletedEvent;

    [SerializeField] private Color fadeColor;

    public IEnumerator FadeAway()
    {
        for (float i = 1; i >= 0; i -= Time.unscaledDeltaTime )
        {
            viewBackgroundImage.color = new Color(fadeColor.r, fadeColor.g, fadeColor.b, i);
            yield return null;
        }

        OnFadeOutConpletedEvent?.Invoke();
    }

    public IEnumerator FadeIn()
    {
        for (float i = 0; i <= 1; i += Time.unscaledDeltaTime )
        {
            viewBackgroundImage.color = new Color(fadeColor.r, fadeColor.g, fadeColor.b, i);
            yield return null;
        }

        OnFadeInConpletedEvent?.Invoke();
    }
}
