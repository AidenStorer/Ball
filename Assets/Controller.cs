using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityRawInput;

public class Controller : MonoBehaviour
{
    public Camera cam;
    public TransparentWindow tran;

    public float screenPaddingX = 50f; // How far inside the screen horizontally
    public float screenPaddingY = 50f; // How far inside the screen vertically

    private bool holding = false;
    private Vector3 lastMousePos;
    private Vector3 mouseVelocity;
    private Rigidbody rb;
    private Transform hoverObj;
    private bool clickThrough = false;

    void Awake()
    {
        //rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        RawInput.Start();
        RawInput.OnKeyDown += OnClick;
        RawInput.OnKeyUp += OnRelease;
        RawInput.WorkInBackground = true;
        RawInput.InterceptMessages = false;
    }

    private void OnDisable()
    {
        RawInput.OnKeyDown -= OnClick;
        RawInput.OnKeyUp -= OnRelease;
        RawInput.Stop();
    }

    void FixedUpdate()
    {
        if (holding)
        {
            Debug.Log("Holding");
            Debug.Log(lastMousePos);
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = cam.WorldToScreenPoint(hoverObj.position).z;
            mousePos.x = Mathf.Clamp(mousePos.x, screenPaddingX, Screen.width - screenPaddingX);
            mousePos.y = Mathf.Clamp(mousePos.y, screenPaddingY, Screen.height - screenPaddingY);
            Vector3 currentMousePos = cam.ScreenToWorldPoint(mousePos);
            Vector3 calculateSpeed = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));

            hoverObj.position = currentMousePos;

            if (calculateSpeed != lastMousePos)
            {
                mouseVelocity = (calculateSpeed - lastMousePos) / Time.fixedDeltaTime;
                lastMousePos = calculateSpeed;
            }
            return;
        }
        //ToggleClickThrough(false, null);
    }
    private void ToggleClickThrough(bool toggle, Transform obj)
    {
        if (clickThrough == toggle)
            return;
        Debug.Log("ClickSet");
        tran.SetClickthrough(toggle);
        clickThrough = toggle;
        if (obj == null)
        {
            holding= false;
            hoverObj = null;
            if (rb != null)
                rb.isKinematic = false;
            rb = null;
            return;
        }
        hoverObj = obj;
        if (hoverObj.TryGetComponent<Rigidbody>(out Rigidbody rig))
        {
            rb = rig;
            rb.isKinematic = true;
            holding = true;
            return;
        }
        holding = false;
        hoverObj = null;
        if (rb != null)
            rb.isKinematic = false;
        rb = null;
    }
    private void OnClick(RawKey key)
    {
        if (key.Equals(RawKey.LeftButton))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            LayerMask layer = LayerMask.GetMask("Interactable", "Menu");

            if (Physics.Raycast(ray, out hit, 100f, layer))
            {
                Debug.Log("Raycast");
                ToggleClickThrough(true, hit.collider.transform);
                return;
            }
            ToggleClickThrough(false, null);
        }
    }

    private void OnRelease(RawKey key)
    {
        if (key.Equals(RawKey.LeftButton))
        {
            if (!holding)
                return;
            Debug.Log("Released");
            rb.isKinematic = false;
            rb.AddForce(mouseVelocity * 0.5f, ForceMode.Impulse);
            ToggleClickThrough(false, null);
        }
    }
}
