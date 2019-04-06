using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class LevelManager : MonoBehaviour {
    private static LevelManager instance;
    public static LevelManager Instance { get { return instance; } }
    public GameObject PauseMenu;
    public Transform respawnSpoint;
    public Text timerText;
    public Text endTimerText;
    public Text LevelAward;

    private GameObject Player;
    private  float LevelDuration;
    private const float Time_before_start = 3.0f;

    private float startTime;
    public float silverTime;
    public float goldTime;
    public GameObject EndMenu;

    private void Start()
    {
        //PlayerPrefs.DeleteAll();
        startTime = Time.time;
        instance = this; 
        PauseMenu.SetActive(false);
        EndMenu.SetActive(false);
        Player = GameObject.FindGameObjectWithTag("Player");
        Player.transform.position = respawnSpoint.position;

    }

    private  void Update()
    {
        if (Player.transform.position.y < -10.0f)
           Death();

        if(Time.time - startTime < Time_before_start)
            return;

        LevelDuration = Time.time - (startTime + Time_before_start);
        string minutes = ((int)LevelDuration  / 60).ToString("00");
        string seconds = (LevelDuration % 60).ToString("00.00");

        timerText.text =  minutes +  ":" + seconds;

    } 

    public void TogglePauseMenu()

    {
        PauseMenu.SetActive(!PauseMenu.activeSelf);
        Time.timeScale = (PauseMenu.activeSelf) ? 0 : 1;
    }

    public void RestartLevel()
    {
        Time.timeScale = 1;
       SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
     
public void Tomenu()
    {
        Time.timeScale = 1;
       SceneManager.LoadScene("Main Menu");

    }	
    

    public void Victory()
    {

        foreach(Transform t in EndMenu.transform.parent)
        {
            t.transform.gameObject.SetActive(false);
        }

        EndMenu.SetActive(true);
       Rigidbody rigid =  Player.GetComponent<Rigidbody>();
       rigid.constraints = RigidbodyConstraints.FreezePosition;
        string minutes = ((int)LevelDuration / 60).ToString("00");
        string seconds = (LevelDuration % 60).ToString("00.00");

        endTimerText.text = minutes + ":" + seconds;
        
        if (LevelDuration < goldTime)
        {
            Game.Instance.currency += 50;
            endTimerText.color = Color.yellow;
            LevelAward.text = "GoldTime: Earned 60 Coins";
            LevelAward.color = Color.yellow;
        }
        else if (LevelDuration < silverTime)
        {
            Game.Instance.currency += 25;
            endTimerText.color = Color.gray;
            LevelAward.text = "SilverTime: Earned 25 Coins";
            LevelAward.color = Color.cyan;
        }

        else
        {
            Game.Instance.currency += 10;
            endTimerText.color =  new Color (0.8f, 0.5f, 0.2f, 1.0f);
            LevelAward.text = "BronzeTime: Earned 10 Coins";
            LevelAward.color = new Color(0.8f, 0.5f, 0.2f, 1.0f);
        }

        Game.Instance.Save();
       

        string saveString = "";
        levelData level = new levelData(SceneManager.GetActiveScene().name);
        saveString += (level.BestTime > LevelDuration || level.BestTime == 0.0f) ? LevelDuration.ToString(): level.BestTime.ToString();
        saveString += '&';
        saveString += silverTime.ToString();
        saveString += '&';
        saveString += goldTime.ToString();
        PlayerPrefs.SetString(SceneManager.GetActiveScene().name, saveString);
         
       // SceneManager.LoadScene ("Main Menu");




    }


   // private static string NewMethod(float duration, string saveString)
   // {
       // saveString += duration.ToString();
       // return saveString;
   // }

   public void Death()
    {
       // Player.transform.position = respawnSpoint.position;
        //Rigidbody rigid = Player.GetComponent<Rigidbody>();
        //rigid.velocity = Vector3.zero;
        //rigid.angularVelocity = Vector3.zero;
        RestartLevel(); 

    }
       
}
