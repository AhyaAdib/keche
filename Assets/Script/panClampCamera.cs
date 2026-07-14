using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class panClampCamera : MonoBehaviour
{
    public float speed;
    [SerializeField]
    private Camera cam;

    [SerializeField]
    private GameObject camPoint;
    private Vector3 dragOrigin;

    [SerializeField]
    private float zoomStep, minCamSize, maxCamSize;
    public float minX, maxX, minY, maxY;
    public Slider zoomSlider;

    [SerializeField]
    private CinemachineVirtualCamera vcam;

    // Start is called before the first frame update
    void Start()
    {
        if (zoomSlider != null)
        {
            zoomSlider.minValue = minCamSize;
            zoomSlider.maxValue = maxCamSize;
            zoomSlider.value = vcam.m_Lens.OrthographicSize;
            zoomSlider.onValueChanged.AddListener(OnZoomSliderChanged);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        // {
        //     Vector2 distPos = Input.GetTouch(0).deltaPosition;

        //     transform.Translate(-distPos.x * speed, -distPos.y * speed, 0);

        //     //boundaries
        //     transform.position = new Vector3(
        //         Mathf.Clamp(transform.position.x, -40f, 40f),
        //         Mathf.Clamp(transform.position.y, 0, 0),
        //         Mathf.Clamp(transform.position.z, -25f, 50f)
        //     );

        // }

        if(Input.touchCount == 2)
        {
            Touch touchI = Input.GetTouch(0);
            Touch touchJ = Input.GetTouch(1);

            Vector2 touchPrevI = touchI.position - touchI.deltaPosition;
            Vector2 touchPrevJ = touchJ.position - touchJ.deltaPosition;

            float prevMagnitude = (touchPrevI - touchPrevJ).magnitude;
            float currMagnitude = (touchI.position - touchJ.position).magnitude;

            float diff = currMagnitude - prevMagnitude;


            Zoom(diff * speed);
        } else
            PanCamera();

        camPoint.transform.position = new Vector2(cam.transform.position.x, cam.transform.position.y);
        cam.transform.position = new Vector3(cam.transform.position.x, cam.transform.position.y, -10f);
    }

    private void PanCamera()
    {
        if(Input.GetMouseButtonDown(0))
        {
            dragOrigin = cam.ScreenToWorldPoint(Input.mousePosition);
        }

        if(Input.GetMouseButton(0))
        {
            Vector3 diff = dragOrigin - cam.ScreenToWorldPoint(Input.mousePosition);

            // Debug.LogWarning(diff);
            cam.transform.position += diff;
            cam.transform.position = new Vector3(cam.transform.position.x, cam.transform.position.y, -10f);
            
            cam.transform.position = new Vector3(
                Mathf.Clamp(cam.transform.position.x, minX, maxX),
                Mathf.Clamp(cam.transform.position.y, minY, maxY),
                cam.transform.position.z
            );
        }
    }
    private void OnZoomSliderChanged(float value)
    {
        ZoomTo(value);
    }

    public void Zoom(float increment)
    {
        float newSize = Mathf.Clamp(vcam.m_Lens.OrthographicSize - increment, minCamSize, maxCamSize);
        ZoomTo(newSize);
    }

    private void ZoomTo(float newSize)
    {
        vcam.m_Lens.OrthographicSize = newSize;
        if (zoomSlider != null)
        {
            zoomSlider.value = newSize;
        }
    }
}
