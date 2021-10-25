using System;
using UnityEngine;

public class TapPanel : MonoBehaviour
{
    public event Action PanelTaped;
    public event Action PanelEnabled;

    private void Start()
    {
        PanelEnabled?.Invoke();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            PanelTaped?.Invoke();
            gameObject.SetActive(false);
        }
    }
}
