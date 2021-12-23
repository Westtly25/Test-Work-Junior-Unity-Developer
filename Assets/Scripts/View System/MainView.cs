using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class MainView : MonoBehaviour
{
    public event Action<bool> OnShielButtonPressed;

    public void ShielPressed(bool isPressed)
    {
        OnShielButtonPressed?.Invoke(isPressed);
    }
}