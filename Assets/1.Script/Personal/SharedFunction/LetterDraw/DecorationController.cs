using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DecorationController : MonoBehaviour
{
    [SerializeField]
    Sprite active;

    bool optional = true;
    int curIndex;
    private void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<Image>().sprite = active;
            transform.GetChild(i).gameObject.AddComponent<DecorationHit>();
        }
        CleanDecoration();
        StartCoroutine(Progress());
    }

    private void OnEnable()
    {
        optional = true;
    }

    private void OnDisable()
    {
        optional = false;
    }

    IEnumerator Progress()
    {
        while (optional)
        {
            for (int i = curIndex; i < transform.childCount; i++)
            {
                
                if (i < curIndex) i = curIndex;
                transform.GetChild(i).GetComponent<Image>().color = Color.white;
                yield return new WaitForSeconds(0.5f);
            }
            CleanDecoration();
        }
        yield break;
    }

    public void CleanDecoration()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<Image>().color = new Color(0, 0, 0, 0);
        }
    }

    public void CleanDeco()
    {
        for(int i = curIndex; i >= 0; i--)
        {
            transform.GetChild(i).GetComponent<Image>().color = new Color(0, 0, 0, 0);
        }
    }

    public void SetIndex(int i)
    {
        curIndex = i;
        CleanDeco();
    }

    
}
