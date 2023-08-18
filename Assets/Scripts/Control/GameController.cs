using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public SmoothCameraOrbit smoothCameraOrbit;
    [Header("<<<区域划分")]
    public List<AreaSplitController> areaSplitControllers;
    public Text 区域划分中文;
    public Text 区域划分英文;
    public Image 区域划分背景;
    public Vector3  OldTarget;    

    List<Tween> tweens = new List<Tween>();
    Ray ray;
    RaycastHit hit;
    Tweener TweenerPos;

       

    // Start is called before the first frame update
    void Start()
    {
        InitScene();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ChangeScene(1);
        }

        if (Input.GetMouseButtonDown(0)) {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray,out hit,100000))
            {
                if(hit.transform.tag == "区域划分")
                {
                    TweenerPos.Kill();
                    smoothCameraOrbit.xDeg = Random.Range(170f, 185f);
                    smoothCameraOrbit.yDeg = Random.Range(45f, 50f);
                    smoothCameraOrbit.desiredDistance = Random.Range(175f, 185f);
                    TweenerPos = smoothCameraOrbit.target.DOMove(hit.transform.position,2);
                    foreach(AreaSplitController areaSplitController in areaSplitControllers)
                    {

                        if (hit.transform.name == areaSplitController.name.Split("_")[1].Substring(4))
                        {
                            areaSplitController.Control(true);
                            areaSplitController.ControlInfo(true, Random.Range(500f,1200f).ToString(), Random.Range(500f, 1200f).ToString(), Random.Range(500f, 1200f).ToString());
                        }
                        else
                        {
                            areaSplitController.Control(false);
                            areaSplitController.ControlInfo(false, null, null, null);
                        }

                    }
                }
            }
        }
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
            areaSplitController.ControlInfo(false,null,null,null);
        });
    }

    public void ChangeScene(int index)
    {
        InitScene();
        foreach(Tween tween in tweens)
        {
            tween.Kill();
        }   
        switch (index)
        {
            // 区域划分
            case 1:
                tweens.Add(区域划分背景.DOFade(1, 0.5f));
                tweens.Add(区域划分背景.transform.DOLocalMoveX(-18, 0).SetDelay(0.2f));
                tweens.Add(区域划分中文.DOFade(1, 0.5f).SetDelay(0.4f));
                tweens.Add(区域划分中文.transform.DOLocalMoveX(0, 0).SetDelay(0.6f));
                tweens.Add(区域划分英文.DOFade(1, 0.5f).SetDelay(0.6f));
                tweens.Add(区域划分英文.transform.DOLocalMoveX(0, 0.5f ).SetDelay(0.8f));
              
                areaSplitControllers.ForEach(areaSplitController =>
                {
                    areaSplitController.Control(true);
                });
                smoothCameraOrbit.xDeg = Random.Range(170f, 185f);    
                smoothCameraOrbit.yDeg = Random.Range(40f, 50f);    
                smoothCameraOrbit.desiredDistance = Random.Range(170f, 185f);
                smoothCameraOrbit.target.DOMove(OldTarget,1);   
                break;
            // 人口分布
            case 2:

                break;
            // 销售热力
            case 3:

                break;
        } 
    }
}
