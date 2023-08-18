using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ScaleControl : MonoBehaviour
{
    public Transform 男性柱状图;
    public Transform 女性柱状图;
    public Image 文字背景;
    public Text 文字;
    public Image 装饰圈;
    public Image 地名背景;
    public Text 地名;
    public GameObject 底座特效;

    public GameObject InfoPrefab;

    List<Tween> tweens = new List<Tween>(); 

    GameObject Info;
    // Start is called before the first frame update
    void Start()
    {
        男性柱状图 = transform.Find("男性柱状图");
        女性柱状图 = transform.Find("女性柱状图");
        文字背景 = transform.Find("Canvas/文字背景").GetComponent<Image>();
        文字 = transform.Find("Canvas/文字背景/Text").GetComponent<Text>();
        装饰圈 = transform.Find("Canvas/文字背景/旋转").GetComponent<Image>();
        地名背景 = transform.Find("Canvas/文字背景/地名背景").GetComponent<Image>();
        地名 = transform.Find("Canvas/文字背景/地名背景/地名字").GetComponent<Text>();
        底座特效 = transform.Find("柱状图底座特效").gameObject;

        Info = GameObject.Instantiate(InfoPrefab,文字背景.transform.parent);

        Init();
    }

    void Init()
    {
        男性柱状图.DOScaleY(0, 0);
        男性柱状图.gameObject.SetActive(false);
        女性柱状图.DOScaleY(0, 0);
        女性柱状图.gameObject.SetActive(false);
        文字背景.DOFade(0, 0);
        文字.DOFade(0, 0);
        装饰圈.DOFade(0, 0);
        地名背景.DOFade(0, 0);
        地名.DOFade(0, 0);
        底座特效.SetActive(false);
        Info.transform.DOScale(0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode  .T))
        {
            this.Control(true,Random.Range(1,5), Random.Range(1, 5), Random.Range(100, 200), Random.Range(100, 200));
        }   
        
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="b"></param>
    /// <param name="MaleRate">柱状图系数</param>
    /// <param name="FeMaleRate">柱状图系数</param>
    /// <param name="MaleCount">女性销售额</param>
    /// <param name="FeMaleCount">男性销售额</param>

    public void Control(bool b,float MaleRate,float FeMaleRate,float MaleCount,float FeMaleCount)
    {
        Init();
        foreach(Tween tween in tweens)
        {
            tween.Kill();
            tweens.Remove(tween);
        }
        Debug.Log("tweens.Count : " + tweens.Count);
        if (b)
        {
            文字.text = (MaleCount + FeMaleCount).ToString("f2");
            tweens.Add(男性柱状图.DOScaleY(0.01f, 0));
            tweens.Add(女性柱状图.DOScaleY(0.01f, 0));
            男性柱状图.gameObject.SetActive(true);
            女性柱状图.gameObject.SetActive(true);
            底座特效.SetActive(true);
            tweens.Add(地名背景.DOFade(1, 0.5f));
            tweens.Add(地名.DOFade(1, 0.5f).SetDelay(0.2f));
            tweens.Add(文字背景.DOFade(1, 0.5f).SetDelay(0.4f));
            tweens.Add(文字.DOFade(1,0.5f).SetDelay(0.6f));
            tweens.Add(装饰圈.DOFade(1, 0.5f).SetDelay(0.4f));


            tweens.Add(男性柱状图.DOScaleY(MaleCount, 1).SetDelay(1));
            tweens.Add(女性柱状图.DOScaleY(FeMaleRate, 1).SetDelay(1));
            tweens.Add(Info.transform.DOScale(new Vector3(0.6f,0.6f,0.6f), 0.5f).SetDelay(1.5f));   
        }
    }
    public void ControlInfo(bool b,float MaleCount,float feMaleCount,List<float> _柱状图,List<string> _类别值)
    {
        Info.transform.DOScale(Vector3.zero, 0);
        if (b)
        {
            Info.transform.DOScale(new Vector3(0.6f, 0.6f, 0.6f), 0.5f).SetDelay(1.5f);
        }
        Info.GetComponent<ScaleCounter>().SetInfo(b,MaleCount.ToString(),feMaleCount.ToString(), _柱状图, _类别值);
    }
}
