using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public static GameManager Instance
    {
        get { return _instance; }
    }

    #region 변수
    //public Button[] lvlButtons;
    private bool isMenu = false;
    private bool isSetting = false;
    public bool isTimer = false; // 타이머가 활성화되었는지 여부
    private float startTime; // 타이머 시작 시간
    private float stopTime; // 타이머 멈춘 시간
    #endregion

    #region 오브젝트
    public GameObject menuSet;
    public GameObject Settings;
    public GameObject player;
    public GameObject Stage;
    #region 타이머오브젝트
    private List<float> timerRecords = new List<float>(); // 타이머 기록 리스트
    public Text rankText; // 랭킹을 표시할 텍스트
    public Text timerText; // UI에 표시될 텍스트
    #endregion
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
        /*int levelAt = PlayerPrefs.GetInt("levelAt", 2);
        for(int i =0; i < lvlButtons.Length; i++)
        {
            if(i+2>levelAt) //원래 2넣엇음
                lvlButtons[i].interactable = false;
        }*/
        GameLoad();
        LoadTimerRecords(); // 게임 시작 시 기록 로드
    }

    // Update is called once per frame
    void Update()
    {
        GameMenu();
        if (isTimer)
        {
            float elapsedTime = Time.time - startTime;
            UpdateTimerUI(elapsedTime);
        }
    }

    #region 게임 메뉴, 세이브 등등 관리
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
    #endregion

    #region 타이머
    // 타이머 시작
    public void StartTimer()
    {
        // 타이머가 이미 실행 중인 경우에는 무시
        if (isTimer)
            return;

        startTime = Time.time;
        isTimer = true;
    }

    // 타이머 중지
    public void StopTimer()
    {
        // 타이머가 실행 중이 아닌 경우에는 무시
        if (!isTimer)
            return;

        stopTime = Time.time;
        isTimer = false;
        SaveTimerRecord();
    }

    // 타이머 기록 저장
    private void SaveTimerRecord()
    {
        float elapsedTime = stopTime - startTime;

        timerRecords.Add(elapsedTime);
        timerRecords = timerRecords.OrderBy(x => x).ToList(); // 기록을 오름차순으로 정렬

        // 상위 랭킹에 해당하는 텍스트 업데이트
        string rankTextContent = "";
        for (int i = 0; i < Mathf.Min(timerRecords.Count, 10); i++)
        {
            int minutes = (int)(timerRecords[i] / 60);
            int seconds = (int)(timerRecords[i] % 60);

            rankTextContent += string.Format("{0}. {1:D2}:{2:D2}\n", i + 1, minutes, seconds);
        }

        rankText.text = rankTextContent;
    }

    // UI에 타이머 정보 업데이트
    private void UpdateTimerUI(float elapsedTime)
    {
        int minutes = (int)(elapsedTime / 60);
        int seconds = (int)(elapsedTime % 60);

        timerText.text = string.Format("{0:D2}:{1:D2}", minutes, seconds);
    }

    private void OnApplicationQuit()
    {
        SaveTimerRecords(); // 게임 종료 시 기록 저장
    }

    private void LoadTimerRecords()
    {
        string filePath = Application.persistentDataPath + "/timerRecords.dat";

        if (File.Exists(filePath))
        {
            try
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(filePath, FileMode.Open);
                timerRecords = (List<float>)bf.Deserialize(file);
                file.Close();
            }
            catch (System.Exception e)
            {
                Debug.LogError("Failed to load timer records: " + e.Message);
            }
        }
    }

    // 타이머 기록 저장
    private void SaveTimerRecords()
    {
        string filePath = Application.persistentDataPath + "/timerRecords.dat";

        try
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(filePath);
            bf.Serialize(file, timerRecords);
            file.Close();
        }
        catch (System.Exception e)
        {
            Debug.LogError("Failed to save timer records: " + e.Message);
        }
    }
    #endregion
}
