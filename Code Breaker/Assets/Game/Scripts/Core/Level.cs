using System.Collections;
using System.Collections.Generic;
using CB.SceneControl;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CB.Core
{
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
            if (breakableBlocks <= 0)
            {
                Cursor.visible = false;
                FindObjectOfType<GameStatus>().AddLife();
                sceneloader.LoadNextScene();
            }
        }
    }
}
