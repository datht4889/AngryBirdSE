using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconHandler : MonoBehaviour
{
    [SerializeField] private Image[] _icons;
    [SerializeField] private Color _usedColor = new Color(116, 116, 116);

    public void UseAmmo(int shotNumber)
    {
        for (int i=0; i< _icons.Length; i++)
        {
            if (shotNumber == i+1)
            {
                _icons[i].color = _usedColor;
                return;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
