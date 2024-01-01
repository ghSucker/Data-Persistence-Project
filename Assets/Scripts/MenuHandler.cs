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
        // �̸��� �Է��ϸ� ���� ����
        username = DataManager.Instance.NameInput.text;

        if (string.IsNullOrEmpty(username))
        {
            Debug.Log("�̸� �Է�");
        }
        else
        {
            SceneManager.LoadScene(0);
        }
    }

    public void Exit()
    {
        // ����
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit(); // original code to quit Unity player
#endif
    }
}
