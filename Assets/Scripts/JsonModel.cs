using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 区域划分
[System.Serializable] // 序列化    
public class AreaSplitJsonInfo
{
    public List<AreaSplitJsonInfoItem> 区域情况列表;
}
// 区域划分数据解析单项   
[System.Serializable]
public class AreaSplitJsonInfoItem
{
    public string 区域名称;
    public string 区域面积;
    public string 区域人数;
    public string 区域消费;
}