using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
//���� �ҷ����� ���

public class LoadFile : MonoBehaviour
{
    string folderPath;
    string fileName;
    [SerializeField]
    Slider imageSlider, volumeSlider;
    [SerializeField]
    AudioSource AudioVolum;
    [SerializeField]
    Image color;


    private void Awake()
    {
        folderPath = Application.dataPath + "/";
        fileName = "han_setting.json";
    }

    public void LoadData()
    {
        if(File.Exists(folderPath+fileName))
        {
            var fileData = File.ReadAllText(folderPath+fileName);
            var fileData2 = JsonUtility.FromJson<SaveFileFormat>(fileData);
            //����� ó��
            AudioVolum.volume = fileData2.volume;
            volumeSlider.value = fileData2.volume;
            //�̹��� ó��
            imageSlider.value = fileData2.color.a;
            color.color = fileData2.color;
        }
    }
    public void OnEnable()
    {
        LoadData();
    }
}
