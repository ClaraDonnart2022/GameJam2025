using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class SceneManager : MonoBehaviour
{
    public List<bool> isFinished = new List<bool>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Initialiser avec 4 éléments par exemple
        for (int i = 0; i < 4; i++)
        {
            isFinished.Add(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
