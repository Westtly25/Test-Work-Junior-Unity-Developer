using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateChanger : MonoBehaviour
{
    [SerializeField] private Color normalColor;
    [SerializeField] private Color invincibleColor;
    [SerializeField] private bool isInvincible = false;
    [SerializeField] private Material meshMaterial;

    public bool IsInvincible => isInvincible;

    private bool timeActive = false;

    private IEnumerator countTimer;
    private MainView mainView;

    public void Initialize(IViewService viewService)
    {
        isInvincible = false;
        timeActive = false;

        mainView = viewService.MainView;
        mainView.OnShielButtonPressed += ChangintState;

        meshMaterial = GetComponentInChildren<MeshRenderer>().sharedMaterial;
        meshMaterial.color = normalColor;
    }

    private void OnDisable() => mainView.OnShielButtonPressed -= ChangintState;

    private void ChangintState(bool isPressed)
    {
        if (isPressed & timeActive == false)
        {
            countTimer = Countdown(2);
            StartCoroutine(countTimer);
        }
        else if(isPressed == false)
        {
            StopCoroutine(countTimer);
            timeActive = false;
            ChangeState(false);
        }
    }

    private void ChangeState(bool isChanged)
    {
        if (isChanged)
        {
            isInvincible = isChanged;
            meshMaterial.color = invincibleColor;
        }
        else
        {
            isInvincible = false;
            meshMaterial.color = normalColor;
        }
    }

    private IEnumerator Countdown (int seconds)
    {
        int counter = seconds;
        timeActive = true;
        while (counter > 0)
        {
            ChangeState(true);
            yield return new WaitForSeconds (1);
            counter--;
        }

        ChangeState(false);
        timeActive = false;
    }
}
