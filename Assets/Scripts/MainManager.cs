using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;


public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public GameObject GameOverText;
    
    private bool m_Started = false;
    private bool m_GameOver = false;
    private int m_Points;

    public Text BestScoreText;
    private int bestScore = 0;

    private string userName;
    private string bestUser;

    // Start is called before the first frame update
    void Start()
    {
        // 플레이 시 벽돌 생성
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        
        int[] pointCountArray = new [] {1,1,2,2,5,5};
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }
    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space)) // 스페이스바 누르면 시작
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver) // 게임오버 후 재시작
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }

        userName = DataManager.Instance.user; // 이름 받기
        Load();
        bestPoint();
    }

    // 점수 업데이트
    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score : {m_Points}";
    }

    // 높은 점수 업데이트
    void bestPoint()
    {
        if (m_Points > bestScore)
        {
            bestScore = m_Points;
            bestUser = userName;
            BestScoreText.text = $"Best Score : {bestUser} : {bestScore}";
            Save();
        }
    }

    // 게임 오버
    public void GameOver()
    {
        m_GameOver = true;
        GameOverText.SetActive(true);
    }

    // JsonUtility
    [System.Serializable]
    class SaveData
    {
        public int score;
        public string name;
    }

    // 데이터 저장
    public void Save()
    {
        SaveData data = new SaveData();
        data.score = bestScore;
        data.name = bestUser;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    // 데이터 로드
    public void Load()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            bestScore = data.score;
            bestUser = data.name;

            BestScoreText.text = $"Best Score : {bestUser} : {bestScore}";
            PlayerPrefs.SetString("점수", BestScoreText.text);
            PlayerPrefs.Save();
        }
    }
}
