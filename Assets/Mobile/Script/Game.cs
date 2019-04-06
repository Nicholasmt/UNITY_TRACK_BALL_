using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{

    private static Game instance;
    public static Game Instance { get { return instance; } }

    public int currentSkinIndex = 0;
    public int currency = 0;
    public int skinAvaibility = 1;

    private void Awake()
    {
        instance = this;

        DontDestroyOnLoad(gameObject);

        if (PlayerPrefs.HasKey("currentSkin"))
        {
            currentSkinIndex = PlayerPrefs.GetInt("currentSkin");
            currency = PlayerPrefs.GetInt("currency");
            skinAvaibility = PlayerPrefs.GetInt("SkinAvaibility");
        }
        else
        {
            Save();

        }
    }
 public void Save()
    {

        PlayerPrefs.SetInt("currentSkin", currentSkinIndex);
        PlayerPrefs.SetInt("currency", currency);
        PlayerPrefs.SetInt("SkinAvaibility", skinAvaibility);
    }
}
