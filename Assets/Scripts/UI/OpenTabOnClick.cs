using UnityEngine;
using UnityEngine.EventSystems;    
public class OpenTabOnClick : MonoBehaviour
{
    public GameObject linkedTab;

    // Opens the associated tab. (Should be called by the tabButton when clicked.)
    public void Clicked()
    { 
        GetComponentInParent<TabManager>().SetTab(linkedTab);
    }
}