using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityRawInput;

public class Controller : MonoBehaviour
{
    public Camera cam;
    public TransparentWindow tran;

    private Toy activeObj;
    private Toy lastObj;
    private Transform hoverObj;
    private bool clickThrough = false;

    [SerializeField] private Menu menu;
    [SerializeField] private Bin bin;

    void Awake()
    {
        //rb = GetComponent<Rigidbody>();
    }
    private void ToggleClickThrough(bool toggle)
    {
        if (clickThrough == toggle)
            return;
        tran.SetClickthrough(toggle);
        clickThrough = toggle;
    }
    private void Update()
    {
        if (lastObj != null && !Application.isFocused)
        {
            lastObj.active = false;
            lastObj = null;
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (activeObj == null)
                return;
            if (menu.open)
            {
                if (Vector3.Distance(activeObj.transform.position, bin.gameObject.transform.position) < 1)
                {
                    Destroy(activeObj.gameObject);
                    activeObj = null;
                    bin.gameObject.SetActive(false);
                    return;
                }
                bin.gameObject.SetActive(false);
            }
            activeObj.OnRelease();
            activeObj = null;
            return;
        }
        if (Input.GetMouseButtonDown(0)) 
        {
            if (hoverObj == null || activeObj != null)
                return;
            if (hoverObj.TryGetComponent<Toy>(out Toy toy))
            {
                SetHeldObj(toy);
                if (menu.open)
                    bin.gameObject.SetActive(true);
            }
        }
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        LayerMask layer = LayerMask.GetMask("Interactable", "Menu");

        if (Physics.Raycast(ray, out hit, 100f, layer))
        {
            ToggleClickThrough(true);
            hoverObj = hit.collider.transform;
            return;
        }
        hoverObj = null;
        if (activeObj == null)
            ToggleClickThrough(false);
    }
    private void SetHeldObj(Toy toy)
    {
        if (lastObj!= null)
            lastObj.active = false;
        activeObj = toy;
        lastObj = activeObj;
        lastObj.active = true;
        toy.OnClicked();
    }
}
