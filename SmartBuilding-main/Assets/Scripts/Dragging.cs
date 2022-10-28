using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Dragging : MonoBehaviour
{
    [SerializeField]
    private InputAction mouseClick;
    [SerializeField]
    private float mouseDragPhysicSpeed = 10;
     [SerializeField]
    private float mouseDragSpeed = .1f;

    private Camera mainCamera;
    private WaitForFixedUpdate waitForFixedUpdate;
    private Vector3 velocity = Vector3.zero;

    public GameObject asset,targetParent;
    public GameObject[] targets;

    public Collider targetCollider;

    private CameraMoveMent cmm;

    private void Start()
    {
        cmm = GameObject.Find("Main Camera").GetComponent<CameraMoveMent>();
        asset = GameObject.FindGameObjectWithTag("Draggable");
    }

    private void Awake()
    {

        mainCamera = Camera.main;
    }

    private void OnEnable()
    {
        mouseClick.Enable();
        mouseClick.performed += MousePressed;
    }
     private void OnDisable()
    {
        mouseClick.performed -= MousePressed;
        mouseClick.Disable();
    }

    private void Update()
    {
        targets = GameObject.FindGameObjectsWithTag("Grid");
         foreach( var target in targets)
         {
            if(Input.GetMouseButtonUp(0))
            {
                    cmm.IsLock = false;
                    asset.transform.SetParent(null);
                    target.GetComponent<Collider>().enabled = true;
            }
              if(Input.GetMouseButtonDown(0))
            {
                    target.GetComponent<Collider>().enabled = false;
            }
         }
    }
    private void MousePressed(InputAction.CallbackContext context)
    {
        asset.transform.SetParent(null);
        Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit;
         if(Physics.Raycast(ray, out hit))
        {
            if(hit.collider != null && (hit.collider.gameObject.CompareTag("Draggable") || hit.collider.
                gameObject.layer == LayerMask.NameToLayer("Draggable")))
            {
                StartCoroutine(DragUpdate(hit.collider.gameObject));
            }
        }
    }

    private  IEnumerator DragUpdate(GameObject clickObject)
        {
            cmm.IsLock = true;
            float initialDistance = Vector3.Distance(clickObject.transform.position, mainCamera.transform.position);
            clickObject.TryGetComponent<Rigidbody>(out var rb);
            while (mouseClick.ReadValue<float>() != 0)
            {
                Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
                if(rb != null)
                {
                    Vector3 direction = ray.GetPoint(initialDistance) - clickObject.transform.position;
                    rb.velocity = direction * mouseDragPhysicSpeed;
                    yield return new WaitForFixedUpdate();
                }
                else{
                    clickObject.transform.position = Vector3.SmoothDamp(clickObject.transform.position, ray.
                        GetPoint(initialDistance), ref velocity, mouseDragSpeed);
                        yield return null;
                }
            }
        }

        void OnTriggerEnter(Collider other)
        {
             foreach( var target in targets)
         {
                target.GetComponent<Collider>().enabled = false;
                targetParent = other.gameObject;
                asset.transform.SetParent(targetParent.transform, true);  
                 Debug.Log("Parent");
         }
        }

        void OnTriggerExit(Collider other)
       {
           asset.transform.SetParent(null);
        }
}
