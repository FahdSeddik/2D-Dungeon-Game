using UnityEngine;
using UnityEngine.UI;

public class FloatingText //no need to inherit from monobehaviour
{
    public bool active;
    public GameObject go;
    public Text txt;
    public Vector3 motion;
    public float duration;
    public float lastShown;



    public void Show()
    {
        active = true;
        lastShown = Time.time;
        go.SetActive(active);
    }

    public void Hide()
    {
        active = false;
        go.SetActive(active);


    }

    //no need for unity update hence we didnt inherit from monobehaviour
    public void UpdateFloatingText()
    {
        if (!active)
            return;

        if (Time.time - lastShown > duration) //if time displayed more than duration then hide
            Hide();


        go.transform.position += motion * Time.deltaTime;

    }
}
