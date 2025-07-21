using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityRawInput;

public class Controller : MonoBehaviour
{
    public Camera cam;
    public TransparentWindow tran;

    private Toy activeObj;
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
    private void ToggleClickThrough(bool toggle)
    {
        if (clickThrough == toggle)
            return;
        tran.SetClickthrough(toggle);
        clickThrough = toggle;
    }
    private void SetHeldObj(Toy toy)
    {
        activeObj = toy;
        toy.OnClicked();
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
                ToggleClickThrough(true);
                if (hit.collider.TryGetComponent<Toy>(out Toy toy))
                {
                    SetHeldObj(toy);
                }
                return;
            }
            ToggleClickThrough(false);
        }
    }

    private void OnRelease(RawKey key)
    {
        if (key.Equals(RawKey.LeftButton))
        {
            if (activeObj == null)
                return;
            activeObj.OnRelease();
            activeObj= null;
        }
    }
}
