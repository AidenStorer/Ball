using UnityEngine;
using DentedPixel;

public class Menu : MonoBehaviour
{
    bool open;
    [SerializeField] private Transform arrow;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        open = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OpenMenu()
    {
        if (!open)
        {
            arrow.rotation = Quaternion.Euler(0, 0, 180);
            LeanTween.move(this.gameObject, new Vector3(-10, 0, -1), 0.5f);
            open = true;
        }
        else
        {
            arrow.rotation = Quaternion.Euler(0, 0, 0);
            LeanTween.move(this.gameObject, new Vector3(-17.45f, 0, -1), 0.5f);
            open = false;
        }
    }
}
