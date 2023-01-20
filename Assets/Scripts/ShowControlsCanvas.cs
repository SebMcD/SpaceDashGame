using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShowControlsCanvas : MonoBehaviour
{
    [SerializeField] private CanvasGroup controlsCanvasGroup;
    [SerializeField] private CanvasGroup startCanvas;

    [SerializeField] private GameObject controlsPlayButton;
    [SerializeField] private EventSystem eventSystem;

    void OnEnable()
    {
        controlsCanvasGroup.alpha = 0;
    }

    public void ShowControls()
    {
        startCanvas.alpha = 0;
        StartCoroutine(IncreaseCanvasAlpha(controlsCanvasGroup));
    }

    IEnumerator IncreaseCanvasAlpha(CanvasGroup canvasGroup)
    {
        while (canvasGroup.alpha < 1)
        {
            canvasGroup.alpha += 0.05f;
            yield return new WaitForSeconds(0.05f);
        }
        if (canvasGroup.alpha == 1)
        {
            startCanvas.gameObject.SetActive(false);
            eventSystem.SetSelectedGameObject(controlsPlayButton);
        }
    }
}
