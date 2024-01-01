using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuHandler : MonoBehaviour
{
    private string username;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartNew()
    {
        // 이름을 입력하면 게임 시작
        username = DataManager.Instance.NameInput.text;

        if (string.IsNullOrEmpty(username))
        {
            Debug.Log("이름 입력");
        }
        else
        {
            SceneManager.LoadScene(0);
        }
    }

    public void Exit()
    {
        // 종료
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit(); // original code to quit Unity player
#endif
    }
}
