using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextMeshLoadData : MonoBehaviour
{
    TextMeshPro textmeshPro;
    void Start()
    {
        textmeshPro = this.gameObject.GetComponent<TextMeshPro>();
    }
}
