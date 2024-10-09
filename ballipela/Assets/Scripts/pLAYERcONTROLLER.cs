using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class pLAYERcONTROLLER : MonoBehaviour
{
    public float speed;
    public TMP_Text ScoreText;
    public TMP_Text WinText;
    public GameObject Wall;
    private Rigidbody rb;
    public int Score;
    public int WinScore = 5;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Score = 0;
        SetScoreText(ScoreText);
        //WinText.text = "";
    }

    private void SetScoreText(TMP_Text ScoreText)
    {
        ScoreText.text = string.Format("Score: {0}", Score.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        // Movement Logic
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        rb.AddForce(movement*speed);  

        if(Input.GetKeyDown(KeyCode.R))
        {
            RestartLevel();
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            QuitGame();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // If player touches coin
        if (other.gameObject.CompareTag("Coin"))
        {
            other.gameObject.SetActive(false);

            Score++;

            SetScoreText(ScoreText);

            if (Score >= WinScore)
            {
                //WinText.text = "Win win win, GIGA WIN! Press R to restart or ESC to quit";
                Wall.gameObject.SetActive(false);
                //RestartLevel();
            }
        }

        //if (other.gameObject.CompareTag("Danger"))
        //{
        //    RestartLevel();
        //}
    }

    private void RestartLevel()
    {
        Application.LoadLevel(Application.loadedLevel);
    }

    private void QuitGame()
    {
        Application.Quit();
    }
}
