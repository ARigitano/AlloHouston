using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Window;

public class Screen : MonoBehaviour
{
    [Header("Parameters")]
    public int nbLayersMax = 6; //if = 0 => no limit
    public int depthStep = 100;
    private float widthMax;
    private float heightMax;

    [Header("Elements")]
    public Vector3 initWinPos = new Vector3(0.0f, 0.0f, 0.0f);
    public WinAlert prefabWinAlert;
    public WinBlurb prefabWinBlurb;
    public WinImage prefabWinImage;
    public WinMessage prefabWinMsg;
    public WinSuccess prefabWinSuccess;

    [Header("Elements")]
    public Sprite background;
    //public Window win;
    private List<Window> listWindows = new List<Window>();

    // Start is called before the first frame update
    void Start()
    {
        // CreateWindow(win, new Vector2(0.0f, 0.0f));
        CreateWinBlurb("kjbkb");
        CreateWinSuccess("kjbkb", "lhilih");
        CreateWinAlert("kjbkb", "lhilih");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateWindow(Window tmpWin, Vector2 firstPos)
    {
        Vector3 firstWinPos = new Vector3(firstPos.x, firstPos.y, 0.0f);
        listWindows.Add(Instantiate(tmpWin, transform.position, transform.rotation, transform));
        listWindows[listWindows.Count - 1].transform.localPosition = firstPos;
        listWindows[listWindows.Count - 1].Init(firstWinPos);
        RearrangeDepth();
    }

    public void RearrangeDepth()
    {
        for (int i = 0; i < listWindows.Count; i++)
        {
            if (((listWindows.Count - i) > nbLayersMax) && (nbLayersMax!=0))
            {
                listWindows[i].ComeOut();
            }
            else
            {
                float currentDepth = (listWindows.Count - i) * depthStep;
                listWindows[i].SetDestXYZ(new Vector3(listWindows[i].GetDestXYZ().x, listWindows[i].GetDestXYZ().y, currentDepth));
                listWindows[i].Move2XYZ();
            }
        }
    }

    public void LoadScreen()
    {

    }

    public void UnloadScreen()
    {
        for (int i = 0; i < listWindows.Count; i++)
        {
            if (listWindows[i]) listWindows[i].ComeOut();
        }
    }

    // WINDOWS

    public void CreateWinAlert(string title, string text)
    {
        WinAlert tmpWinAlert = prefabWinAlert;
        CreateWindow(tmpWinAlert, new Vector2(0.0f, 0.0f));
        tmpWinAlert.SetTitle(title);
        tmpWinAlert.SetMessage(text);
    }

    public void CreateWinSuccess(string title, string text)
    {
        WinSuccess tmpWinSuccess = prefabWinSuccess;
        CreateWindow(tmpWinSuccess, new Vector2(0.0f, 0.0f));
        tmpWinSuccess.SetTitle(title);
        tmpWinSuccess.SetMessage(text);
    }

    public void CreateWinMsg(string title, string text)
    {
        WinMessage tmpWinMsg = prefabWinMsg;
        CreateWindow(tmpWinMsg, new Vector2(0.0f, 0.0f));
        tmpWinMsg.SetTitle(title);
        tmpWinMsg.SetMessage(text);
    }

    public void CreateWinBlurb(string text)
    {
        WinBlurb tmpWinBlurb = prefabWinBlurb;
        CreateWindow(tmpWinBlurb, new Vector2(0.0f, 0.0f));
        tmpWinBlurb.SetMessage(text);
    }

    public void CreateWinImg(Sprite img)
    {
        WinImage tmpWinImg = prefabWinImage;
        CreateWindow(tmpWinImg, new Vector2(0.0f, 0.0f));
        tmpWinImg.SetImage(img);
    }



    // GETSET
    public void SetDepthStep(int tmpDepthStep)
    {
        depthStep = tmpDepthStep;
    }

    public int GetDepthStep()
    {
        return depthStep;
    }

    public void SetNbLayers(int tmpNbLayers)
    {
        nbLayersMax = tmpNbLayers;
    }

    public int GetNbLayersMax()
    {
        return nbLayersMax;
    }

    public int GetCurrentNbWin()
    {
        return listWindows.Count;
    }
}
