using UnityEngine;
using UnityEngine.EventSystems;

public class EnsureButtonSelected : MonoBehaviour
{
    // ensures that a button is selected for controller+keyboard UI navigation
    private GameObject lastSelect;
    private void Update()
    {
        if (EventSystem.current == null) return;

        if (EventSystem.current.currentSelectedGameObject == null)
        {
            EventSystem.current.SetSelectedGameObject(lastSelect);
        }
        else
        {
            lastSelect = EventSystem.current.currentSelectedGameObject;
        }
    }
}
