using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoveMent : MonoBehaviour
{
    [SerializeField]
    private float maxZoom = 20f;
    [SerializeField]
    private float minZoom = 3f;
    public float distance = 11;
    private float zoomSpeed = 1.1f;
    private float rotateSpeed = 4;
    [SerializeField]
    private Vector3 originPosCamera;
    [SerializeField]
    private Vector3 originRotCamera;
    public Transform target;
    [SerializeField]
    private bool isLock;


    // Start is called before the first frame update
    void Start()
    {
        IsLock = false;
        transform.localPosition = originPosCamera;
        transform.eulerAngles = originRotCamera;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLock)
        {
            if (Input.GetMouseButton(0)) { cameraRotate(); }
        }
        if (Input.GetAxis("Mouse ScrollWheel") != 0f)
        {
            cameraZoom();
        }
    }
    private void cameraZoom()
    {
        if (Input.GetAxis("Mouse ScrollWheel") < 0f) // backwards
        {
            if (distance > minZoom)
            {
                distance -= zoomSpeed;
                transform.localPosition *= zoomSpeed;
            }
        }
        else if (Input.GetAxis("Mouse ScrollWheel") > 0f) // forward 
        {
            if (distance < maxZoom)
            {
                distance += zoomSpeed;
                transform.localPosition /= zoomSpeed;
            }
        }
        distance = Mathf.Round(distance);
    }

    private void cameraRotate()
    {
        transform.RotateAround(target.position, transform.up, Input.GetAxis("Mouse X") * rotateSpeed);

        if (!(Input.GetAxis("Mouse Y") < 0 && transform.eulerAngles.x > 89))
        {
            transform.RotateAround(target.position, transform.right, -Input.GetAxis("Mouse Y") * rotateSpeed);
        }
        if (transform.eulerAngles.x > 88.5f)
        {
            rotateSpeed = 0.1f;
        }
        else if (transform.eulerAngles.x > 85f)
        {
            rotateSpeed = 0.5f;
        }
        else { rotateSpeed = 4; }

        float y = transform.eulerAngles.y;
        float x = transform.eulerAngles.x;
        //lock camera rotate 0-90 degree
        if (x > 90)
        {
            x = 0;
            //camera doesn't move down
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        }

        transform.eulerAngles = new Vector3(x, y, 0);
    }

    public void resetPositionCamera()
    {
        transform.position = originPosCamera;
        transform.eulerAngles = originRotCamera;
        distance = 11;
    }

    public void topDownView()
    {
        Vector3 newPos = new Vector3(originPosCamera.x, originPosCamera.z, originPosCamera.y);
        transform.position = newPos;
        Vector3 newRot = new Vector3(88, originRotCamera.y, 0);
        transform.eulerAngles = newRot;
    }

    public bool IsLock
    {
        get { return isLock; }
        set { isLock = value; }
    }
}
