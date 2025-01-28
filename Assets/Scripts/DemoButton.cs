using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoButton : MonoBehaviour
{
    [SerializeField] private MonoBehaviour _screenType;
    
    public void OnClick()
    {
        Type type = _screenType.GetType();
        ScreenManager.Instance.AddScreen<type>();
    }
}
