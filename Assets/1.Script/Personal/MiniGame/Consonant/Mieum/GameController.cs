using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class GameController : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI textTitle;
    [SerializeField]
    private TextMeshProUGUI textTapToPlay;

    [SerializeField]
    private TextMeshProUGUI textCoinCount; //coinȹ�� ����
    private int             cointCount = 0;

    public bool IsGameStart { private set; get; }

    private void Awake()
    {
        IsGameStart = false;

        textTitle.enabled = true;
        textTapToPlay.enabled = true;
        textCoinCount.enabled = false;
    }
    private IEnumerator Start()
    {
        //���콺 ���� ��ư�� ������ ������ �������� �ʰ� ���
        while(true)
        {
            if(Input.GetMouseButtonDown(0))
            {
                IsGameStart = true;

                textTitle.enabled = false;
                textTapToPlay.enabled = false;
                textCoinCount.enabled = true;

                break;
            }
            yield return null;
        }
    }
    public void IncreaseCoinCount()//������ ȹ�������� ȣ��
    {
        cointCount++;//���� ����
        textCoinCount.text = cointCount.ToString();//text�� ���� ����
    }
    public void GameOver()
    {
        //���� ���� �ٽ� �ε�
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
