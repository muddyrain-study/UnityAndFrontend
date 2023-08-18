using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ScaleCounter : MonoBehaviour
{
    public Text 男性销售额;
    public Text 女性销售额;
    public Image[] 柱状图;
    public Text[] 类别值;
    public Text[] 数值;
    public List<Tween> tweens;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void SetInfo(bool b, string _男性消费额, string _女性销售额, List<float> _柱状图, List<string> _类别值)
    {
        for (int i = 0; i < 柱状图.Length; i++)
        {
            柱状图[i].DOFillAmount(0, 0);
            类别值[i].DOFade(0, 0);
            数值[i].DOFade(0, 0);     
        }
        foreach(Tween tween in tweens)
        {
            tween.Kill();
            tweens.Remove(tween);   
        }
        if (b)
        {
            男性销售额.text = "男性消费额：" + _男性消费额 + "万元";
            女性销售额.text = "女性消费额：" + _女性销售额 + "万元";  
            for (int i = 0; i < 柱状图.Length; i++)
            {
                类别值[i].text = _类别值[i];
                数值[i].text = _柱状图[i].ToString();
                tweens.Add(类别值[i].DOFade(1, 0.5f).SetDelay(1));
                tweens.Add(数值[i].DOFade(1, 0.5f).SetDelay(1));
                tweens.Add(柱状图[i].DOFillAmount(_柱状图[i], 1f).SetDelay(2));

            }
        }
    }
}
