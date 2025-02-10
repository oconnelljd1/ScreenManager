using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class DemoButton : MonoBehaviour
{
    [SerializeField] private string _screenType;

    // adding this to cut down on runtime overhead
    private MethodInfo _addScreenMethod;

    private void Awake()
    {
        _addScreenMethod = typeof(ScreenManager).GetMethod("AddScreen");
    }

    public void OnClick()
    {
        Type type = Type.GetType(_screenType);
        // ScreenManager.Instance.AddScreen<type>();
        // var method = typeof(ScreenManager).GetMethod("AddScreen").MakeGenericMethod(type);
        var genericMethod = _addScreenMethod.MakeGenericMethod(type);
        genericMethod.Invoke(ScreenManager.Instance, null);
    }
}
