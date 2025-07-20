using System;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.XR;

public class TransparentWindow : MonoBehaviour
{

    // Import Windows API functions
    [DllImport("user32.dll")] private static extern IntPtr GetActiveWindow();
    [DllImport("Dwmapi.dll")] private static extern uint DwmExtendFrameIntoClientArea(IntPtr hWnd, ref MARGINS margins);
    [DllImport("user32.dll")] private static extern int SetWindowLong(IntPtr hWnd, int nIndex, uint dwNewLong);
    [DllImport("user32.dll", SetLastError = true)]
    private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

    private static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);

    const int GWL_EXSTYLE = -20;
    const uint WS_EX_LAYERED = 0x00080000;
    const uint WS_EX_TRANSPARENT = 0x00000020;
    private bool clickthroughCheck;
    private IntPtr hWnd;
    private struct MARGINS
    {
        public int cxLeftWidth, cxRightWidth, cyTopHeight, cyBottomHeight;
    }


    private void Start()
    {
#if !UNITY_EDITOR
        hWnd = GetActiveWindow();

         //Set the window to always be on top
        SetWindowPos(hWnd, HWND_TOPMOST, 0, 0, 0, 0, 0);

        SetWindowLong(hWnd, GWL_EXSTYLE, WS_EX_LAYERED | WS_EX_TRANSPARENT);

        MARGINS margins = new MARGINS { cxLeftWidth = -1 };
        DwmExtendFrameIntoClientArea(hWnd, ref margins);
#endif
    }
    private void Update()
    {
#if !UNITY_EDITOR
        //bool isOverUI = Physics2D.OverlapPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition)) != null;
        //SetClickthrough(!isOverUI);
#endif
    }

    public void SetClickthrough(bool clickthrough)
    {
        if (!clickthrough)
        {
            SetWindowLong(hWnd, GWL_EXSTYLE, WS_EX_LAYERED | WS_EX_TRANSPARENT);
        }
        else
        {
            SetWindowLong(hWnd, GWL_EXSTYLE, WS_EX_LAYERED);
        }
    }

}