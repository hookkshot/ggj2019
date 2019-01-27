using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    [SerializeField]
    private Screen[] screens;

    public void SwitchScreen(Screen screen)
    {
        foreach (var item in screens)
        {
            item.gameObject.SetActive(false);
        }

        screen.gameObject.SetActive(true);
    }
}
