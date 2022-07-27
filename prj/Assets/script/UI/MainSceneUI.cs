using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainSceneUI : MonoBehaviour
{
    int check;

    public Image[] checkImage = new Image[4];


    private void Start()
    {
        check = 1;
        Cursor.visible = false;
    }

    private void Update()
    {
        MainManage();   //Ű����� checknum����
        CheckImangeOn(check);   //��ư������ Ȱ��ȭ����
        ButtonManage(check);    //��������
    }
    private void MainManage()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        { if (check < 4) check++; }

        else if (Input.GetKeyDown(KeyCode.UpArrow))
        { if (check > 1) check--; }

    }
    private void CheckImangeOn(int num)
    {
        for (int i = 0; i < checkImage.Length; i++)
            checkImage[i].gameObject.SetActive(false);

        checkImage[num - 1].gameObject.SetActive(true);
    }
    private void ButtonManage(int num)
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if(num==1)
            SceneManager.LoadScene("Menu");
            else if(num ==2)
            {
                ExitGame();
            }
            else if(num ==3)
            {
                //���� ����
            }
            
        }
    }    
    private void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(); // ���ø����̼� ����
#endif
    }

}
