using UnityEngine;
using UnityEngine.UI;

public class PanelManager : MonoBehaviour
{ 
    [SerializeField] GameObject panelOption;

    public void PanelOpen()
    {
        panelOption.SetActive(true);
    }

    public void PanelClose()
    {
        panelOption.SetActive(false);
    }
}

