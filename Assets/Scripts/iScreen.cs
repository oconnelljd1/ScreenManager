using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface iScreen
{
    public bool HideStack {get; set;}
    public bool SingleInstance {get; set;}
    public void Show();

    public void Hide();
}
