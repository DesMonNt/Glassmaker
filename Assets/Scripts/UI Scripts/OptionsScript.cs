using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsScript : MonoBehaviour
{
    public GameObject options;

    public GameObject menuButtons;

    public void ChangeButtons()
    {
        menuButtons.SetActive(false);
        options.SetActive(true);
    }
}
