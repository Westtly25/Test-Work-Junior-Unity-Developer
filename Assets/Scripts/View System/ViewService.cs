using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Плохое решение, но в данном случае для быстрого результат сделал так.
public class ViewService : MonoBehaviour, IViewService
{
    [SerializeField] private MainView mainView;
    [SerializeField] private ExiteView exiteView;
    [SerializeField] private FadeView fadeView;

    public MainView MainView => mainView;

    public void Initialize()
    {

    }

    public void ActivateFader()
    {
        fadeView.gameObject.SetActive(true);
    }
}
