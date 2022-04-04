using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CB.Core
{
    public class Paddle : MonoBehaviour
    {
        // configuration paramaters
        [SerializeField] float minX = 1f;
        [SerializeField] float maxX = 15f;
        [SerializeField] float screenWidthInUnits = 16f;

        //configuration
        [SerializeField] Vector2 initialPosition = new Vector2(8f, 0.4f);

        private bool lockPaddle = false;

        // cached references
        GameStatus theGameStatus;
        Ball theBall;

        // Use this for initialization
        void Start()
        {
            theGameStatus = FindObjectOfType<GameStatus>();
            theBall = FindObjectOfType<Ball>();
            transform.position = initialPosition;

            theGameStatus?.CheckIsMenu();
        }

        // Update is called once per frame
        void Update()
        {
            if(lockPaddle) return;

            UpdatePaddlePos();

            if(theGameStatus == null)
            {
                theGameStatus = FindObjectOfType<GameStatus>();
                theGameStatus?.CheckIsMenu();
            }
        }

        private void UpdatePaddlePos()
        {
            Vector2 paddlePos = new Vector2(transform.position.x, transform.position.y);
            paddlePos.x = Mathf.Clamp(GetXPos(), minX, maxX);
            transform.position = paddlePos;
        }

        private float GetXPos()
        {
            if (theGameStatus.IsAutoPlayEnabled())
                return theBall.transform.position.x;
            else
                return Input.mousePosition.x / Screen.width * screenWidthInUnits;
        }

        public void ToggleLockPaddle(bool toggle)
        {
            lockPaddle = toggle;
        }

        public void ResetPaddlePos()
        {
            transform.position = initialPosition;
        }
    }
}
