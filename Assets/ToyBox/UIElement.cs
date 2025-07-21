using UnityEngine;

public class UIElement : MonoBehaviour
{
    public GameObject element;
    [SerializeField] private Controller controller;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Spawn()
    {
        GameObject obj = Instantiate(element);
        controller.AddToList(obj.GetComponent<Toy>());
    }
}
