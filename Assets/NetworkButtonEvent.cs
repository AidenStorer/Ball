using Mirror;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;

public class NetworkButtonEvent : NetworkBehaviour
{
    public GameObject cube;
    public GameObject target;
    public GameObject setTarget;
    public GameObject canvas;
    public GameObject setCube;
    public UnityEngine.UI.Button myButton;
    public bool aimingSet = false;
    public int aimCheck = 0;
    public float horizontalMin;
    public float horizontalMax;
    public float verticalMin;
    public float verticalMax;
    private bool localCheck = false;

    private void Start()
    {
        if (!isServer && !isLocalPlayer) 
        {
            GameObject newCube = Instantiate(cube, Vector3.zero, Quaternion.identity);
            setCube= newCube;
            GameObject newTarget = Instantiate(target, Vector3.zero, Quaternion.identity);
            setTarget = newTarget;
        }
        if (isServer && isLocalPlayer)
        {
            GameObject newCanvas = Instantiate(canvas, Vector3.zero, Quaternion.identity);
            myButton = newCanvas.GetComponentInChildren<UnityEngine.UI.Button>();
            myButton.onClick.AddListener(SetupAiming);
            UnityEngine.Input.gyro.enabled = true;
        }
    }
    void Update()
    {
        if (!isLocalPlayer) return;

        if (isServer && isLocalPlayer)
        {
            if (aimingSet)
            {
                CmdTriggerEvent();
            }
        }
    }

    [Command]
    void CmdTriggerEvent()
    {
        Debug.Log("Server received event from player!");

        // Send event to all clients
        RpcTriggerEvent(horizontalMin, horizontalMax, UnityEngine.Input.gyro.attitude.z, verticalMin, verticalMax, UnityEngine.Input.gyro.attitude.x, localCheck);
        if (localCheck)
            localCheck = false;
    }

    [ClientRpc]
    void RpcTriggerEvent(float horizontalMinR, float horizontalMaxR, float currentHorizontal, float verticalMinR, float verticalMaxR, float currentVertical, bool checkTarget)
    {
        Debug.Log("All clients triggered event!" + netId);
        if (!isServer)
        {
            Debug.Log(horizontalMinR + " " + horizontalMaxR + " " + currentHorizontal);
            float midpoint = (horizontalMinR + horizontalMaxR) / 2f;
            float halfRange = (horizontalMinR - horizontalMaxR) / 2f;
            float output = ((currentHorizontal - midpoint) / halfRange) * -8.9f;
            float midpointv = (verticalMinR + verticalMaxR) / 2f;
            float halfRangev = (verticalMinR - verticalMaxR) / 2f;
            float outputv = ((currentVertical - midpointv) / halfRangev) * -5f;
            setCube.transform.position = new Vector3(output, outputv, 0);
            if (checkTarget)
            {
                if (Vector3.Distance(setCube.transform.position, setTarget.transform.position) < 0.5f)
                {
                    setTarget.transform.position = new Vector3(Random.Range(-8, 8), Random.Range(-4, 4), 0);
                }
            }
        }
        // Do something here (start game, play animation, spawn object...)
    }
    public void SetupAiming()
    {
        if (aimCheck == 0) 
        {
            horizontalMin = UnityEngine.Input.gyro.attitude.z;
            aimCheck = 1;
            return;
        }
        if (aimCheck == 1)
        {
            horizontalMax = UnityEngine.Input.gyro.attitude.z;
            aimCheck = 2;
            return;
        }
        if (aimCheck == 2)
        {
            verticalMin = UnityEngine.Input.gyro.attitude.x;
            aimCheck = 3;
            return;
        }
        if (aimCheck == 3)
        {
            verticalMax = UnityEngine.Input.gyro.attitude.x;
            aimCheck = 4;
            aimingSet = true;
            return;
        }
        if (aimCheck == 4)
        {
            localCheck = true;
        }
    }
}