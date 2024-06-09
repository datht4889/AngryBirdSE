using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScript1 : MonoBehaviour
{
    public TextMeshProUGUI score;

    // Start is called before the first frame update
    void Start()
    {
        score.text = "" + ScoreScript.scoreValue;
    }

    // Update is called once per frame
    void Update()
    {
    }
}
