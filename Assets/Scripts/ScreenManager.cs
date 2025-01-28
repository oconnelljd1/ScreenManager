using System;
using System.Collections.Generic;
using UnityEngine;

public class ScreenManager : MonoBehaviour
{
    public static ScreenManager Instance;
    [SerializeField] private List<iScreen> ScreenList;
    private List<iScreen> _screenStack = new List<iScreen>();

    public void Awake()
    {
        if(Instance)
            Destroy(gameObject);
        
        Instance = this;
    }

    public T AddScreen<T>() where T : iScreen
    {
        // Find and show the new screen
        foreach (iScreen screen in ScreenList)
        {
            if (screen is T == false) continue;

            // Hide the current screen if there is one
            if (_screenStack.Count > 0 && screen.HideStack)
            {
                foreach(iScreen stack in _screenStack)
                {
                    stack.Hide();
                }
            }

            // Access the GameObject the screen is attached to
            MonoBehaviour screenMonoBehaviour = screen as MonoBehaviour;
            if (screenMonoBehaviour == null)
            {
                // this should never happen but I'm paranoid
                Debug.LogError($"Screen {screen} is not a MonoBehaviour");
                return default(T);
            }
            
            var newScreen = Instantiate(screenMonoBehaviour.gameObject, transform).GetComponent<T>();
            // ^^^ maybe?
            // var newScreen = Instantiate(screen.gameObject);
            _screenStack.Add(screen);
            return newScreen;
            // screen.Show();
        }
        return default(T);
    }

    // I dont like this because I want to close the screen by type
    // would need to pass the screen type to close
    // would also need to move away from stack and swap to list
    // Other screens can close this 
    // public void HideCurrentScreen()
    // {
    //     if (_screenStack.Count > 0)
    //     {
    //         var currentScreen = _screenStack.Pop();
    //         currentScreen.Hide();

    //         // Show the previous screen if there is one
    //         if (_screenStack.Count == 0) return;
            
    //         var previousScreen = _screenStack.Peek();
    //         previousScreen.Show();

    //         if(previousScreen.HideStack == false)
    //         {
    //             var screenArray = _screenStack.ToArray();
    //             for(int i = screenArray.Length-2; i>-1; i--)
    //             {
    //                 screenArray[i].Show();
    //             }
    //         }
    //     }
    // }

    public void CloseScreen(Type screenType)
    {
        if(_screenStack.Count == 0) return;

        for(int i = 0; i < _screenStack.Count; i++)
        {
            var screen = _screenStack[i];
            if (screen.GetType() != screenType) continue;
            _screenStack.RemoveAt(i);
            var screenMonoBehaviour = screen as MonoBehaviour;
            if(screenMonoBehaviour == null)
            {
                Debug.LogError($"Screen {screen} is not a MonoBehaviour");
                return;
            }

            Destroy(screenMonoBehaviour.gameObject);
            break;
        }
    }
}