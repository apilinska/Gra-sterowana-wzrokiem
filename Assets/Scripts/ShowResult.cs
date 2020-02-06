using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowResult : MonoBehaviour
{

    public Text Result;

    void Start()
    {
        setResult();
    }

    void setResult() {
        Result.text = MemoryController.GetResult();
    }
}