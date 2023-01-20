using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeFrameRate : MonoBehaviour
{
    [SerializeField]
    private int frameRate = 60;

    //Change framerate as soon as game starts
    private void Awake()
    {
        Application.targetFrameRate = frameRate;
    }
}
