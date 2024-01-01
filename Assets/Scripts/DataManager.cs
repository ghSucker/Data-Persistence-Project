using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;

public class DataManager : MonoBehaviour
{
    public InputField NameInput;
    public string user;
    public Text BestScoreText;

    public static DataManager Instance; // 싱글톤

    private void Awake() // 싱글톤
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        user = NameInput.text; // 최초 이름 저장

        // 제이슨 파일이 있다면 최고 점수 보이기
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string score = PlayerPrefs.GetString("점수", "");
            BestScoreText.text = score;
        }
            
    }

    // Update is called once per frame
    void Update()
    {
        // 이름 돌려막기
        if (string.IsNullOrEmpty(NameInput.text))
        {
            NameInput.text = user;
        }
        user = NameInput.text;
    }
}
