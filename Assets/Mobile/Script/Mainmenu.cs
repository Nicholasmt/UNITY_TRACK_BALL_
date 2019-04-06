using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class levelData
{
    public levelData(string levelName)
    {

    string data = PlayerPrefs.GetString(levelName);
        if (data == "")
            return;

        string [] allData = data.Split('&');
        BestTime = float.Parse(allData[0]);
        SilverTime = float.Parse(allData[1]);
        GoldTime = float.Parse(allData[2]);
         
    }

        public float BestTime { set; get; }
        public float SilverTime { set; get;}
        public float GoldTime { set; get; }

}

public class Mainmenu : MonoBehaviour {

    public GameObject levelButtonPrefabs;
     
    public GameObject levelButtoncontainer;
  
    public GameObject shopButtonprefab;
    public GameObject shopitemcontainer;
    public Material playermaterial;
    public Text currencyText;

    private Transform cameraTransform;
    private Transform cameraDesiredLookAt;
    private bool nextlevelLocked;

    private int[] costs = { 0,150,200,250,
                            300,400,450,500,
                            600,670,700,900,
                            1000,1500,2000,3000};

    private void Start() {

        // PlayerPrefs.DeleteAll();
        
        Changeplayerskin(Game.Instance.currentSkinIndex);
        cameraTransform = Camera.main.transform;
        currencyText.text = "Coins:" + Game.Instance.currency.ToString();

        Sprite[] thumbnails = Resources.LoadAll<Sprite>("levels");

        foreach (Sprite thumbnail in thumbnails)  {
            GameObject container = Instantiate(levelButtonPrefabs) as GameObject;
            container.GetComponent<Image>().sprite = thumbnail;
            container.transform.SetParent(levelButtoncontainer.transform, false);

            levelData level = new levelData(thumbnail.name);

             
            string minutes = ((int) level.BestTime / 60).ToString("00");
           string seconds = (level.BestTime % 60).ToString("00.00");

            container.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = (level.BestTime != 0.0f) ?
            minutes + ":" + seconds 
            : "Not Completed";
            container.transform.GetChild(1).GetComponent<Image>().enabled = nextlevelLocked;
            container.GetComponent<Button>().interactable = !nextlevelLocked;
            if (level.BestTime == 0.0f)
            {
                nextlevelLocked = true;

            }

            string sceneName = thumbnail.name;
            container.GetComponent<Button>().onClick.AddListener(() => Loadlevel(sceneName));
              
        }

        int textureindex = 0;
        Sprite[] textures = Resources.LoadAll<Sprite>("player");
          foreach(Sprite texture in textures) { 

            GameObject container = Instantiate(shopButtonprefab) as GameObject;
            container.GetComponent<Image>().sprite = texture;
            container.transform.SetParent(shopitemcontainer.transform, false);

            int index = textureindex;
            container.GetComponent<Button>().onClick.AddListener(() => Changeplayerskin(index));
            container.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = costs[index].ToString();
          // container.transformer.GetComponetInchildren<text>()

        if ((Game.Instance.skinAvaibility & 1 << index) == 1 << index)
           {
              container.transform.GetChild (0).gameObject.SetActive (false);
          }

            textureindex++;
        }

    } 

    private void Update() {

        if (cameraDesiredLookAt != null) {
            cameraTransform.rotation = Quaternion.Slerp(cameraTransform.rotation, cameraDesiredLookAt.rotation, 4 * Time.deltaTime);
         }

       }
       
    private void Loadlevel(string sceneName) {

        SceneManager.LoadScene(sceneName);

       }

     
    public void LookAtMenu(Transform menuTransform) {

        //Camera.main.transform.LookAt(menuTransform.position);
        cameraDesiredLookAt = menuTransform;


    }
      //newgame button
    public void Newgame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("1_training");

    }

    //public void startGame()
   // {
      //  Time.timeScale = 1;
      //  SceneManager.LoadScene("Main Menu");

   // }

    // Exit Game Button
    public void QuitGame()
    {

 Application.Quit();

        }



    private  void Changeplayerskin(int index) {

        if((Game.Instance.skinAvaibility & 1 << index) == 1 << index)
        {
          

       
        float x = (index % 4) * 0.25f;
        float y = ((int) index / 4) * 0.25f;
        if (y == 0.0f)
            y = 0.75f;
        else if (y == 0.25f)
            y = 0.5f;
        else if (y == 0.50f)
            y = 0.25f;
        else if (y == 0.75f)
            y = 0f;

        playermaterial.SetTextureOffset("_MainTex", new Vector2(x,y));

        Game.Instance.currentSkinIndex = index;
        Game.Instance.Save();
        }
        else{
            // you don't the skin do you want to buy it?
             int Cost = costs[index];
             

            if(Game.Instance.currency >= Cost) {

                Game.Instance.currency -= Cost;
                Game.Instance.skinAvaibility += 1 << index;
                Game.Instance.Save();
                shopitemcontainer.transform.GetChild(index).GetChild(0).gameObject.SetActive(false);
                currencyText.text = "Currency:" + Game.Instance.currency.ToString();
                Changeplayerskin(index);
            }
        }
    }
    

 }
	 
	 

