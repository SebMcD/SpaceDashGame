using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowStartCanvas : MonoBehaviour
{
    [SerializeField] private float timeToWait;
    [SerializeField] private CanvasGroup startCanvasGroup;

    bool showCanvas = false;

    void Start()
    {
        startCanvasGroup.alpha = 0;
        StartCoroutine(ShowCanvasAfterTime());
    }

    void Update()
    {
        if (showCanvas)
        {
            if (startCanvasGroup.alpha < 1)
            {
                startCanvasGroup.alpha += Time.deltaTime;
            }
            else if (startCanvasGroup.alpha >= 1)
            {
                showCanvas = false;
            }
        }
    }

    IEnumerator ShowCanvasAfterTime()
    {
        yield return new WaitForSeconds(timeToWait);
        showCanvas = true;
    }
}
