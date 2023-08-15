using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [Header("<<<区域划分")]
    public List<AreaSplitController> areaSplitControllers;
    public Text 区域划分中文;
    public Text 区域划分英文;
    public Image 区域划分背景;    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // 初始化场景
    public void InitScene()
    {
        // 区域划分
        区域划分中文.DOFade(0, 0);
        区域划分中文.transform.DOLocalMoveX(-8, 0);
        区域划分英文.DOFade(0, 0);
        区域划分英文.transform.DOLocalMoveX(-8, 0);
        区域划分背景.DOFade(0, 0);
        区域划分背景.transform.DOLocalMoveX(-27, 0);

        areaSplitControllers.ForEach(areaSplitController =>
        {
            areaSplitController.Control(false);
        });
    }

    // 进入场景后
    public void ChangeScene()
    {
            
    }
}
