using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingTextManager : MonoBehaviour
{
    public GameObject textContainer;
    public GameObject textPrefab;


    private List<FloatingText> floatingTexts = new List<FloatingText>();

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }


    private void Update()
    {
        foreach (FloatingText txt in floatingTexts)
            txt.UpdateFloatingText();


    }

    public void Show(string msg, int fontSize, Color color, Vector3 position, Vector3 motion, float duration)
    {
        FloatingText floatingText = GetFloatingText();


        floatingText.txt.text = msg; //manually changing text in txt component
        floatingText.txt.fontSize = fontSize;
        floatingText.txt.color = color;

        //very important to translate world space to screen space due to text moving in pixels
        floatingText.go.transform.position = Camera.main.WorldToScreenPoint(position);

        floatingText.motion = motion;
        floatingText.duration = duration;


        floatingText.Show();
    }
   


    private FloatingText GetFloatingText()
    {
        FloatingText txt = floatingTexts.Find(t => !t.active); //could be for loop to check for not active and return it


        if (txt == null)//not find
        {
            txt = new FloatingText();
            txt.go = Instantiate(textPrefab);
            txt.go.transform.SetParent(textContainer.transform);
            txt.txt = txt.go.GetComponent<Text>();

            floatingTexts.Add(txt);
        }

        return txt;
    }

}
