using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnLoadCallback : MonoBehaviour
{
    private void Start()
    {
        Loader.Load(Loader.Scene.Game);
    }
}
