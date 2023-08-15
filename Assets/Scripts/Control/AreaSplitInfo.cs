using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AreaSplitInfo : MonoBehaviour
{
    public Text 人口;
    public Text 面积;
    public Text 消费;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetInfo(string _人口,string _面积,string _消费)
    {
        人口.text = "人口:" + _人口 + " 万人";
        面积.text = "面积:" + _面积 + " 平方公里";
        消费.text = "消费:" + _消费 + " 万人";  
    }
}
