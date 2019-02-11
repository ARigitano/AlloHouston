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

    public int currentNbWin
    {
        get
        {
            return listWindows.Count;
        }
    }

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

    public Window CreateWindow(Window tmpWin, Vector2 firstPos)
    {
        Vector3 firstWinPos = new Vector3(firstPos.x, firstPos.y, 0.0f);
        Window window = Instantiate(tmpWin, transform.position, transform.rotation, transform);
        window.transform.localPosition = firstPos;
        window.Init(firstWinPos);
        listWindows.Add(window);
        RearrangeDepth();
        return window;
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
                listWindows[i].dest = new Vector3(listWindows[i].dest.x, listWindows[i].dest.y, currentDepth);
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
    public WinAlert CreateWinAlert(string title, string text)
    {
        WinAlert winAlert = (WinAlert)CreateWindow(prefabWinAlert, new Vector2(0.0f, 0.0f));
        winAlert.SetTitle(title);
        winAlert.SetMessage(text);
        return winAlert;
    }

    public WinSuccess CreateWinSuccess(string title, string text)
    {
        WinSuccess winSuccess = (WinSuccess)CreateWindow(prefabWinSuccess, new Vector2(0.0f, 0.0f));
        winSuccess.SetTitle(title);
        winSuccess.SetMessage(text);
        return winSuccess;
    }

    public WinMessage CreateWinMessage(string title, string text)
    {
        WinMessage winMsg = (WinMessage)CreateWindow(prefabWinMsg, new Vector2(0.0f, 0.0f));
        winMsg.SetTitle(title);
        winMsg.SetMessage(text);
        return winMsg;
    }

    public WinBlurb CreateWinBlurb(string text)
    {
        WinBlurb winBlurb = (WinBlurb)CreateWindow(prefabWinBlurb, new Vector2(0.0f, 0.0f));
        winBlurb.SetMessage(text);
        return winBlurb;
    }

    public WinImage CreateWinImage(Sprite img)
    {
        WinImage winImage = (WinImage)CreateWindow(prefabWinImage, new Vector2(0.0f, 0.0f));
        winImage.SetImage(img);
        return winImage;
    }

    public Window CreateWinCustom(Window prefabWin)
    {
        Window win = CreateWindow(prefabWin, new Vector2(0.0f, 0.0f));
        return win;
    }
}
