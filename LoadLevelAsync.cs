﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevelAsync : MonoBehaviour
{
    private void Start()
    {
        Screen.SetResolution(1100, 700, false);
        Invoke("Load", 2);
    }
    void Load()
    {
        SceneManager.LoadSceneAsync(1);
    }
}