using System;
using System.Collections;
using System.Collections.Generic;
using CB.Core;
using Newtonsoft.Json;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BlockchainManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField nickInputField;
    [SerializeField] private TMP_Text[] playerNames = new TMP_Text[10];
    [SerializeField] private TMP_Text[] playerScores = new TMP_Text[10];

    [SerializeField] private TMP_Text resultText;

    // variables
    private string abi = "[{\"inputs\":[{\"internalType\":\"string\",\"name\":\"user\",\"type\":\"string\"},{\"internalType\":\"uint256\",\"name\":\"score\",\"type\":\"uint256\"}],\"name\":\"addScore\",\"outputs\":[{\"internalType\":\"bool\",\"name\":\"\",\"type\":\"bool\"}],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"inputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"constructor\"},{\"inputs\":[{\"internalType\":\"uint256\",\"name\":\"\",\"type\":\"uint256\"}],\"name\":\"leaderboard\",\"outputs\":[{\"internalType\":\"string\",\"name\":\"user\",\"type\":\"string\"},{\"internalType\":\"uint256\",\"name\":\"score\",\"type\":\"uint256\"}],\"stateMutability\":\"view\",\"type\":\"function\"}]";
    private string contract = "0x81a9043a29aa9e29BFA9D1A89c6f247523FE2b89";

    // cached references
    private GameStatus gameStatus;

    void Start()
    {
        gameStatus = FindObjectOfType<GameStatus>();
        InitTable();
    }

    public void InitTable()
    {
        for(int i = 0; i < 10; i++)
        {
            playerNames[i].text = "undefined";
            playerScores[i].text = "-1";
        }
    }

    async public void SubmitScore()
    {
        if(string.IsNullOrEmpty(nickInputField.text) || string.IsNullOrWhiteSpace(nickInputField.text)) 
        {
            resultText.text = "Invalid nickname, insert a valid one";
            return;
        }

        if(nickInputField.text.Length < 3 || nickInputField.text.Length > 6)
        {
            resultText.text = "Nickname must be between 3 and 6 letters";
            return;
        }
        
        // Debug.Log("Submit score for player: " + nickInputField.text + ", with score: " + FindObjectOfType<GameStatus>().GetScore().ToString());
        
        string method = "addScore";
        string score = FindObjectOfType<GameStatus>().GetScore().ToString();
        string args = "[\"" + nickInputField.text + "\",\"" + score + "\"]";
        string value = "0";
        string gas = "";
        // print("Args submit score: " + args);
        try
        {
            string response = await Web3GL.SendContract(method, abi, contract, args, value, gas);
            // Debug.Log("Submit Score response: " + response);
            resultText.text = args + ", " + response;
        } catch (Exception e)
        {
            Debug.LogException(e, this);
            resultText.text = args + ", " + e;
        }
    }

    async public void RetrieveRankTable()
    {
        // Debug.Log("Retrieve from blockchain the table info");
        string chain = "binance";
        string network = "testnet";
        string method = "leaderboard";

       for(int i = 0; i < 10; i ++)
       {
            string args = "[\"" + i + "\"]";
            // print("Args retrieve table: " + args);
            string response = "";
            try
            {
                response = await EVM.Call(chain, network, contract, abi, method, args);
                // print("Retrieved table response: " + response);
                var pObject = JsonConvert.DeserializeObject<MyInfo>(response);
                // if(string.IsNullOrEmpty(pObject.User) || string.IsNullOrWhiteSpace(pObject.User)) continue;

                playerNames[i].text = pObject.User;
                playerScores[i].text = pObject.Score.ToString();

                resultText.text = args + ", " + response;
                
            } catch(Exception e) 
            {
                Debug.LogException(e, this);
                resultText.text = args + ", " + e;
            }
            
            
       }
    } 
}

class MyInfo
{
    public string User {get; set;}
    public int Score {get; set;}
}