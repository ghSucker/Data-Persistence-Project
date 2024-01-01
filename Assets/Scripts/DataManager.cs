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

    public static DataManager Instance; // �̱���

    private void Awake() // �̱���
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
        user = NameInput.text; // ���� �̸� ����

        // ���̽� ������ �ִٸ� �ְ� ���� ���̱�
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string score = PlayerPrefs.GetString("����", "");
            BestScoreText.text = score;
        }
            
    }

    // Update is called once per frame
    void Update()
    {
        // �̸� ��������
        if (string.IsNullOrEmpty(NameInput.text))
        {
            NameInput.text = user;
        }
        user = NameInput.text;
    }
}
