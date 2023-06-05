using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppManager : MonoBehaviour
{
    private List<UIAnimation> openedApps = new List<UIAnimation>();

    public void Opened(UIAnimation openedApp)
    {
        openedApps.Add(openedApp);
    }

    public void Back()
    {
        if (openedApps.Count > 0)
        {
            openedApps[openedApps.Count - 1].Move();
            openedApps.RemoveAt(openedApps.Count - 1);
        }
    }

    public void CloseAll()
    {
        for (int i = openedApps.Count - 1; i >= 0; i--)
        {
            openedApps[i].Move();
        }
        openedApps.Clear();
    }
}
