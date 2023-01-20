using UnityEngine;
using System.Collections;

public class SpawnStars : MonoBehaviour
{
    [SerializeField] MeshRenderer starPrefab;
    [SerializeField] Vector2 starRadiusMinMax;
    [SerializeField] int numOfStars = 500;
    [SerializeField] float starfieldRadius = 500f;
    [SerializeField] Vector2 brightnessMinMax;

    float calibrationDistance = 2000f;
    Camera cam;

    void Start()
    {
        cam = Camera.main;

        if(cam)
        {
            float starDistance = cam.farClipPlane - starRadiusMinMax.y;
            float starScale = starDistance / calibrationDistance;

            for (int i = 0; i < numOfStars; i++)
            {
                MeshRenderer star = Instantiate(starPrefab, Random.onUnitSphere * starfieldRadius, Quaternion.identity, transform);
                float randomTValueForLerp = Mathf.Min(1, Random.value);
                star.transform.localScale = Vector3.one * Mathf.Lerp(starRadiusMinMax.x, starRadiusMinMax.y, randomTValueForLerp) * starScale;
                star.material.color = Color.Lerp(Color.black, star.material.color, Mathf.Lerp(brightnessMinMax.x, brightnessMinMax.y, randomTValueForLerp));
            }
            //Instead of below LateUpdate, as camera not moving
            transform.position = cam.transform.position;
        }
    }

    //Only needed if camera moves
    /*void LateUpdate()
    {
        if(cam != null)
        {
            transform.position = cam.transform.position;
        }
    }*/
}