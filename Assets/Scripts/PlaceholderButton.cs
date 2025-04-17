using UnityEngine;
using TMPro;
public class PlaceholderButton : MonoBehaviour
{
    public GameObject text;
    public GameObject button;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void OnClick()
    {
        text.SetActive(true);
        button.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
