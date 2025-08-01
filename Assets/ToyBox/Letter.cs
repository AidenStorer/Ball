using UnityEngine;
using UnityEngine.UI;

public class Letter : Toy
{
    [SerializeField] private TMPro.TextMeshProUGUI text;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private new void Awake()
    {
        base.Awake();

    }

    // Update is called once per frame
    private void OnGUI()
    {
        if (!holding)
            return;
        Event e = Event.current;
        if (e.type == EventType.KeyDown && e.keyCode.ToString().Length == 1 && char.IsLetter(e.keyCode.ToString()[0]))
        {
            text.text = e.keyCode.ToString();
        }
/*        if (Input.GetKeyDown(KeyCode.B))
        {
            text.text = "B";
        }*/

        return;
    }
}
