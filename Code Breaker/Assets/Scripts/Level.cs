using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    [SerializeField] int breakableBlocks = 0; //Serialized for debugging purposes
    [SerializeField] int unbreakableBlocks = 0;

    // cached reference
    SceneLoader sceneloader;

    private void Start()
    {
        sceneloader = FindObjectOfType<SceneLoader>();
    }

    public void addOneBreakableBlock()
    {
        breakableBlocks++;
    }

    public void addOneUnbreakableBlock()
    {
        unbreakableBlocks++;
    }

    public void removeOneBreakableBlock()
    {
        breakableBlocks--;
        if(breakableBlocks <= 0)
        {
            sceneloader.LoadNextScene();
        }
    }
}
