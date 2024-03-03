using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public static GameManager Instance
    {
        get { return _instance; }
    }

    #region 변수
    public Button[] lvlButtons;
    private bool isMenu = false;
    private bool isSetting = false;
    #endregion

    #region 오브젝트
    public GameObject menuSet;
    public GameObject Settings;
    public GameObject player;
    public GameObject Stage;
    #endregion
    // Start is called before the first frame update
    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        int levelAt = PlayerPrefs.GetInt("levelAt", 2);
        for(int i =0; i < lvlButtons.Length; i++)
        {
            if(i+2>levelAt)
                lvlButtons[i].interactable = false;
        }
        GameLoad();
    }

    // Update is called once per frame
    void Update()
    {
        GameMenu();
    }

    private void GameMenu()
    {

        
        if (Input.GetButtonDown("Cancel"))
        {
            if (menuSet.activeSelf)
            {
                menuSet.SetActive(false);
                Settings.SetActive(false);
            }
            else if (Settings.activeSelf)
            {
                Settings.SetActive(false);
            }
            else
                menuSet.SetActive(true); 
        }

    }

    public void GameSave()
    {
        PlayerPrefs.SetFloat("PlayerX", player.transform.position.x);
        PlayerPrefs.SetFloat("PlayerY", player.transform.position.y);
        PlayerPrefs.SetFloat("PlayerZ", player.transform.position.z);
        PlayerPrefs.Save();

        menuSet.SetActive(false);
    }
    public void GameLoad()
    {

        if (!PlayerPrefs.HasKey("PlayerX"))
            return;
        float x = PlayerPrefs.GetFloat("PlayerX");
        float y = PlayerPrefs.GetFloat("PlayerY");
        float z = PlayerPrefs.GetFloat("PlayerZ");

        player.transform.position = new Vector3(x, y, z);
    }

    public void GameExit()
    {
        Application.Quit();
    }

    public void GameSettings()
    {
        Settings.SetActive(true);
        menuSet.SetActive(false);
    }
    public void Settings_Menu()
    {
        Settings.SetActive(false);
        menuSet.SetActive(true);
    }

    public void GameStart()
    {
        Stage.SetActive(true);
    }
}
