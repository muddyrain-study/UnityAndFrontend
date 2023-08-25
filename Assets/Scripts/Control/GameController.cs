using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using SimpleJSON;

public enum sceneType
{
    None,
    区域划分,
    销售情况,
    销售热力
}
public class GameController : MonoBehaviour
{
    public SmoothCameraOrbit smoothCameraOrbit;
    [Header("<<<区域划分>>>")]
    public List<AreaSplitController> areaSplitControllers;
    public Text 区域划分中文;
    public Text 区域划分英文;
    public Image 区域划分背景;
    public Vector3 OldTarget;
    public AreaSplitJsonInfo areaSplitJsonInfo = new();
    [Header("<<<销售情况>>>")]
    public Gradient Gradient;
    public MeshRenderer[] meshRenders;
    public List<ScaleControl> scaleControls;
    public Text 销售情况中文;
    public Text 销售情况英文;
    public Image 销售情况背景;
    public Transform 销售情况图例;
    public MeshRenderer Gis;
    [Header("销售热力")]
    public Text 销售热力中文;
    public Text 销售热力英文;
    public Image 销售热力背景;
    public List<GameObject> 销售热力分布s;
    public GameObject 销售热力整体;

    sceneType sceneType = sceneType.None;

    List<Tween> tweens = new List<Tween>();
    Ray ray;
    RaycastHit hit;
    Tweener TweenerPos;



    // Start is called before the first frame update
    void Start()
    {
        InitScene();
        // 携程 区域划分数据解析
        StartCoroutine(GetHttpJson_AreaSplitJsonInfo());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ChangeScene(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ChangeScene(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ChangeScene(3);
        }
        RayCastMethod();
    }

    IEnumerator GetHttpJson_AreaSplitJsonInfo()
    {
        UnityWebRequest webRequest = UnityWebRequest.Get("https://www.fastmock.site/mock/bc3782ee2b2804b36999cb3144f15ee6/DataVoss/AreaSplitInfo");
        yield return webRequest.SendWebRequest();
        if (webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError(webRequest.error);
        }
        else
        {
            areaSplitJsonInfo = JsonUtility.FromJson<AreaSplitJsonInfo>(webRequest.downloadHandler.text);
            for (int i = 0; i < areaSplitControllers.Count; i++)
            {
                for (int j = 0; j < areaSplitJsonInfo.区域情况列表.Count; j++)
                {
                    string strName = areaSplitControllers[i].name.Split("_")[1].Substring(4);
                    if (strName == areaSplitJsonInfo.区域情况列表[j].区域名称)
                    {
                        areaSplitControllers[i].SetInfo(areaSplitJsonInfo.区域情况列表[j].区域人数, areaSplitJsonInfo.区域情况列表[j].区域面积, areaSplitJsonInfo.区域情况列表[j].区域消费);
                    }
                }
            }
        }
    }

    void RayCastMethod()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100000))
            {
                if (sceneType == sceneType.区域划分)
                {
                    if (hit.transform.tag == "区域划分")
                    {
                        TweenerPos.Kill();
                        smoothCameraOrbit.xDeg = Random.Range(170f, 185f);
                        smoothCameraOrbit.yDeg = Random.Range(45f, 50f);
                        smoothCameraOrbit.desiredDistance = Random.Range(175f, 185f);
                        TweenerPos = smoothCameraOrbit.target.DOMove(hit.transform.position, 2);
                        foreach (AreaSplitController areaSplitController in areaSplitControllers)
                        {

                            if (hit.transform.name == areaSplitController.name.Split("_")[1].Substring(4))
                            {
                                areaSplitController.Control(true);
                                areaSplitController.ControlInfo(true);
                            }
                            else
                            {
                                areaSplitController.Control(false);
                                areaSplitController.ControlInfo(false);
                            }

                        }
                    }
                }
                if (sceneType == sceneType.销售情况)
                {
                    TweenerPos.Kill();
                    smoothCameraOrbit.xDeg = Random.Range(170f, 185f);
                    smoothCameraOrbit.yDeg = Random.Range(45f, 50f);
                    smoothCameraOrbit.desiredDistance = Random.Range(175f, 185f);
                    TweenerPos = smoothCameraOrbit.target.DOMove(hit.transform.position, 2);
                    foreach (ScaleControl scaleControl in scaleControls)
                    {
                        if (hit.transform.name == scaleControl.name)
                        {
                            float male = Random.Range(100f, 500f);
                            float female = Random.Range(100f, 500f);
                            float maleRate = male / 500f;
                            float femaleRate = female / 500f;
                            scaleControl.Control(true, maleRate, femaleRate, male, female);
                            scaleControl.ControlInfo(true, maleRate, femaleRate, null, null);
                        }
                        else
                        {
                            scaleControl.Control(false, 0, 0, 0, 0);

                        }

                    }
                }
                if (sceneType == sceneType.销售热力)
                {
                    TweenerPos.Kill();
                    smoothCameraOrbit.xDeg = Random.Range(170f, 185f);
                    smoothCameraOrbit.yDeg = Random.Range(45f, 50f);
                    smoothCameraOrbit.desiredDistance = Random.Range(175f, 185f);
                    TweenerPos = smoothCameraOrbit.target.DOMove(hit.transform.position, 2);

                    销售热力整体.SetActive(false);

                    foreach (GameObject 销售热力分布 in 销售热力分布s)
                    {
                        if (hit.transform.name == 销售热力分布.name.Split("_")[1])
                        {
                            销售热力分布.SetActive(true);
                        }
                        else
                        {
                            销售热力分布.SetActive(false);
                        }
                    }

                }
            }
        }
    }
    // 初始化场景
    public void InitScene()
    {
        Gis.material.DOFade(1, 0);
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
            areaSplitController.ControlInfo(false);
        });
        // 销售情况
        销售情况中文.DOFade(0, 0);
        销售情况中文.transform.DOLocalMoveX(-8, 0);
        销售情况英文.DOFade(0, 0);
        销售情况英文.transform.DOLocalMoveX(-8, 0);
        销售情况背景.DOFade(0, 0);
        销售情况背景.transform.DOLocalMoveX(-27, 0);
        scaleControls.ForEach(item =>
        {
            item.Control(false, 0, 0, 0, 0);
        });
        销售情况图例.DOScale(Vector3.zero, 0);
        // 销售热力
        销售热力中文.DOFade(0, 0);
        销售热力中文.transform.DOLocalMoveX(-8, 0);
        销售热力英文.DOFade(0, 0);
        销售热力英文.transform.DOLocalMoveX(-8, 0);
        销售热力背景.DOFade(0, 0);
        销售热力背景.transform.DOLocalMoveX(-27, 0);
        销售热力分布s.ForEach(item =>
        {
            item.SetActive(false);
        });
        销售热力整体.SetActive(false);
    }

    public void ChangeScene(int index)
    {
        InitScene();
        foreach (Tween tween in tweens)
        {
            tween.Kill();
        }
        switch (index)
        {
            // 区域划分
            case 1:
                sceneType = sceneType.区域划分;
                TweenerPos.Kill();
                tweens.Add(区域划分背景.DOFade(1, 0.5f));
                tweens.Add(区域划分背景.transform.DOLocalMoveX(-18, 0).SetDelay(0.2f));
                tweens.Add(区域划分中文.DOFade(1, 0.5f).SetDelay(0.4f));
                tweens.Add(区域划分中文.transform.DOLocalMoveX(0, 0).SetDelay(0.6f));
                tweens.Add(区域划分英文.DOFade(1, 0.5f).SetDelay(0.6f));
                tweens.Add(区域划分英文.transform.DOLocalMoveX(0, 0.5f).SetDelay(0.8f));

                areaSplitControllers.ForEach(areaSplitController =>
                {
                    areaSplitController.Control(true);
                });
                smoothCameraOrbit.xDeg = Random.Range(170f, 185f);
                smoothCameraOrbit.yDeg = Random.Range(40f, 50f);
                smoothCameraOrbit.desiredDistance = Random.Range(170f, 185f);
                smoothCameraOrbit.target.DOMove(OldTarget, 1);
                break;
            // 销售情况
            case 2:
                sceneType = sceneType.销售情况;
                Gis.material.DOFade(0, 0);
                tweens.Add(销售情况背景.DOFade(1, 0.5f));
                tweens.Add(销售情况背景.transform.DOLocalMoveX(-18, 0).SetDelay(0.2f));
                tweens.Add(销售情况中文.DOFade(1, 0.5f).SetDelay(0.4f));
                tweens.Add(销售情况中文.transform.DOLocalMoveX(0, 0).SetDelay(0.6f));
                tweens.Add(销售情况英文.DOFade(1, 0.5f).SetDelay(0.6f));
                tweens.Add(销售情况英文.transform.DOLocalMoveX(0, 0.5f).SetDelay(0.8f));
                tweens.Add(销售情况图例.transform.DOScale(1, 0.5f).SetDelay(0.8f));

                scaleControls.ForEach(item =>
                {
                    float male = Random.Range(100f, 500f);
                    float female = Random.Range(100f, 500f);
                    float maleRate = male / 500f;
                    float femaleRate = female / 500f;
                    item.Control(true, maleRate, femaleRate, male, female);
                });
                smoothCameraOrbit.xDeg = Random.Range(170f, 185f);
                smoothCameraOrbit.yDeg = Random.Range(40f, 50f);
                smoothCameraOrbit.desiredDistance = Random.Range(170f, 185f);
                smoothCameraOrbit.target.DOMove(OldTarget, 1);
                break;
            // 销售热力
            case 3:
                sceneType = sceneType.销售热力;
                Gis.material.DOFade(0, 0);
                tweens.Add(销售热力背景.DOFade(1, 0.5f));
                tweens.Add(销售热力背景.transform.DOLocalMoveX(-18, 0).SetDelay(0.2f));
                tweens.Add(销售热力中文.DOFade(1, 0.5f).SetDelay(0.4f));
                tweens.Add(销售热力中文.transform.DOLocalMoveX(0, 0).SetDelay(0.6f));
                tweens.Add(销售热力英文.DOFade(1, 0.5f).SetDelay(0.6f));
                tweens.Add(销售热力英文.transform.DOLocalMoveX(0, 0.5f).SetDelay(0.8f));
                销售热力整体.SetActive(true);

                smoothCameraOrbit.xDeg = Random.Range(170f, 185f);
                smoothCameraOrbit.yDeg = Random.Range(40f, 50f);
                smoothCameraOrbit.desiredDistance = Random.Range(170f, 185f);
                smoothCameraOrbit.target.DOMove(OldTarget, 1);
                break;
        }
    }
}
