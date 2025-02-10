using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

// A class that must be extended from if you expect to use it with the ScreenManager
public class BaseScreen : MonoBehaviour
{
    [SerializeField] private bool _hideStack = false;
    public bool HideStack {
        get { return _hideStack; }
        set {}
    }
    [SerializeField] private bool _singleInstance = false;
    public bool SingleInstance {
        get { return _singleInstance; }
        set {}
    }

    public virtual void Show()
    {
        transform.localScale = Vector3.zero;
        transform.DOScale(1, 1).SetEase(Ease.OutElastic);
    }

    public virtual void Hide()
    {
        transform.localScale = Vector3.one;
        transform.DOScale(0, 1)
            .SetEase(Ease.OutElastic)
            .OnComplete(() => {
                ScreenManager.Instance.CloseScreen(this.GetType());
        });
    }
}
