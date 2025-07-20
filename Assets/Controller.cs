using UnityEngine;
using UnityEngine.EventSystems;

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
    private Transform heldObj;
    private bool clickThrough;


    void Start()
    {
        //rb = GetComponent<Rigidbody>();
    }

    void Update()
    {

            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100f))
            {
                if (hit.collider.CompareTag("Menu"))
                {
                if (!clickThrough)
                {
                    tran.SetClickthrough(true);
                    clickThrough = true;
                }
                    return;
                }
                if (hit.collider.CompareTag("Interactable"))
                {
                    if (!holding)
                    {
                    if (!clickThrough)
                    {
                        tran.SetClickthrough(true);
                        clickThrough = true;
                    }
                }
                    if (Input.GetMouseButtonDown(0))
                    {
                        holding = true;
                        heldObj = hit.collider.gameObject.transform;
                        rb = heldObj.GetComponent<Rigidbody>();
                        rb.isKinematic = true; // disable physics while dragging

                        // Record start mouse position (correct depth)
                        Vector3 mousePos = Input.mousePosition;
                        mousePos.z = cam.WorldToScreenPoint(heldObj.position).z;
                        lastMousePos = cam.ScreenToWorldPoint(mousePos);
                    }
                }
                else
                {
                if (!holding)
                {
                    if (clickThrough)
                    {
                        tran.SetClickthrough(false);
                        clickThrough= false;
                    }
                }
                }
            }
            else
            {
            if (!holding)
            {
                if (clickThrough)
                {
                    tran.SetClickthrough(false);
                    clickThrough = false;
                }
            }

        }

            if (holding)
            {
                // Get current mouse world position at correct depth
                Vector3 mousePos = Input.mousePosition;
                mousePos.z = cam.WorldToScreenPoint(heldObj.position).z;

                // Clamp to screen bounds with padding
                mousePos.x = Mathf.Clamp(mousePos.x, screenPaddingX, Screen.width - screenPaddingX);
                mousePos.y = Mathf.Clamp(mousePos.y, screenPaddingY, Screen.height - screenPaddingY);

                Vector3 currentMousePos = cam.ScreenToWorldPoint(mousePos);

                // Move object to follow mouse
                heldObj.position = currentMousePos;

                // Calculate velocity
                mouseVelocity = (currentMousePos - lastMousePos) / Time.deltaTime;
                lastMousePos = currentMousePos;

                if (Input.GetMouseButtonUp(0))
                {
                    rb.isKinematic = false; // re-enable physics

                    // Apply force in drag direction
                    rb.AddForce(mouseVelocity * 0.3f, ForceMode.Impulse); // Tweak strength if needed
                if (clickThrough)
                {
                    tran.SetClickthrough(false);
                    clickThrough = false;
                }
                holding = false;
                }
            }
    }
}
