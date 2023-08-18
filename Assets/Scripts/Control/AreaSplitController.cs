using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class AreaSplitController : MonoBehaviour
{
    public Transform 定位;
    public Image 定位背景;
    public Text 文字;
    public Image 装饰圈;
    public GameObject InfoPrefab;
    List<Tween> tweenersList = new List<Tween>();
    GameObject info;
    // Start is called before the first frame update
    void Awake()
    {
        定位 = transform.Find("定位");
        定位背景 = transform.Find("文字背景").GetComponent<Image>();
        文字 = transform.Find("文字背景/Text").GetComponent<Text>();
        装饰圈 = transform.Find("文字背景/Text/Image").GetComponent<Image>();

        info = GameObject.Instantiate(InfoPrefab, 定位背景.transform);
        info.transform.localScale = Vector3.zero;
        Control(false);
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    Control(true);
        //}   
    }
    
    public void Control(bool b)
    {
        定位.DOScale(Vector3.zero, 0);
        定位背景.DOFillAmount(0, 0);
        文字.DOFade(0, 0);
        装饰圈.DOFade(0, 0);
        foreach(Tween tweener in tweenersList)
        {
            tweener.Kill();     
        }      
        if (b)
        {
            tweenersList.Add( 定位.DOScale(new Vector3(2, 4, 2), 0.5f));
            tweenersList.Add(装饰圈.DOFade(1, 0.5f));
            tweenersList.Add(定位背景.DOFillAmount(1, 0.7f));
            tweenersList.Add(文字.DOFade(1, 1));    
        }
    }
    public void ControlInfo(bool b,string _人口,string _面积,string _消费)
    {
        if (b)
        {
            info.transform.DOScale(new Vector3(1,1,1),1);
        }
        else
        {
            info.transform.DOScale(Vector3.zero, 1);          
        }
        info.GetComponent<AreaSplitInfo>().SetInfo(_人口, _面积, _消费); 
    }
}
