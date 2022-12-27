using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

//��� ���·� �� ������ ������ ���ΰ�?? : Json�� �̿��Ͽ� ������ �� ���� ....
[System.Serializable]
public class SaveFileFormat
{
    public float volume; 
    public Color color;
}


public class SaveOption : MonoBehaviour
{
    string folderPath = "";
    string fileName = "";

    [SerializeField]
    GameObject audioSource;
    [SerializeField]
    Image image;
    private void Awake()
    {
        folderPath = Application.dataPath +"/";
        fileName = "han_setting.json";
        audioSource = GameObject.Find("Audio Source");
    }

    //������ �Լ�
    public void SaveFile()
    {
        //������ ������ ���Ƿ� ���� : �� �ҷ����� ���� ���� ������ ���� ������ ����
        SaveFileFormat savefileFormat = new SaveFileFormat();
        savefileFormat.volume = audioSource.GetComponent<AudioSource>().volume;
        savefileFormat.color = image.color;

        var saveData = JsonUtility.ToJson(savefileFormat);

        File.WriteAllText(folderPath + fileName, saveData);
        print("���� �Ϸ� ");
    }

    private void OnEnable()
    {
        SaveFile();
    }
}
