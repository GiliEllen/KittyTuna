using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HowToPlayScreen : MonoBehaviour
{
 public MainMenu MainMenu;
    public void GoBack() {
        MainMenu.gameObject.SetActive(true);
    }
}
