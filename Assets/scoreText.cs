using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scoreText : MonoBehaviour
{
    private Text obj;
    public GameObject f;

    void Start() 
    {
        obj = GetComponent<Text>();
    }
    void Update()
    {
        obj.text = "score: " + f;
    }
}
