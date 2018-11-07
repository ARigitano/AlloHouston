using CRI.HelloHouston.Experience;
using CRI.HelloHouston.Translation;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UIExperimentPanel : MonoBehaviour {
    [SerializeField]
    private Text _experimentText;
    [SerializeField]
    private Text _wallTopText;
    [SerializeField]
    private Text _wallBottomText;
    [SerializeField]
    private Text _cornerText;
    [SerializeField]
    private Text _doorText;
    [SerializeField]
    private Text _hologramText;
    [SerializeField]
    private Text _durationText;
    [SerializeField]
    private Dropdown _contextDropdown;
    [SerializeField]
    private Toggle _startToggle;
    [SerializeField]
    public Button removeButton;

    private XPContext[] _contexts;
    public XPContext currentContext { get; private set; }
    public bool start { get; private set; }
    public int id { get; private set; }
    private bool isStart = true;

    public void Init(string name, int id,
        UIExperienceTotalPanel totalPanel,
        UIListingExperiences listingExperiences,
        string experiencePath)
    {
        _experimentText.text = name;
        this.id = id;
        totalPanel.AddContext(id);
        LoadAllContexts(name, experiencePath);
        removeButton.onClick.AddListener(() => { listingExperiences.RemoveExperiment(this); });
        _startToggle.onValueChanged.AddListener((bool val) =>
        {
            start = val;
            listingExperiences.CheckNext();
        });
        start = _startToggle.isOn;
        _contextDropdown.options.Add(new Dropdown.OptionData() { text = TextManager.instance.GetText("CHOOSE") });
        foreach (var option in _contexts)
        {
            _contextDropdown.options.Add(new Dropdown.OptionData() { text = option.context });
            _contextDropdown.onValueChanged.AddListener((int value) => {
                ChooseContext(_contextDropdown.options[value].text);
                totalPanel.SetContext(id, currentContext);
                listingExperiences.CheckNext();
            });
        }
    }

    private void LoadAllContexts(string name, string experiencePath)
    {
        try
        {
            var allContextTemp = Resources.LoadAll<XPContext>(experiencePath).Where(x => x.xpGroup.experimentName == name);
            _contexts = allContextTemp.ToArray();
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message);
        }
    }

    private void ChooseContext(string option)
    {
        currentContext = _contexts.FirstOrDefault(x => x.context == option);
        SetPlaceholderText(currentContext);
    }

    private void SetPlaceholderText(XPContext context)
    {
        if (context != null)
        {
            _wallTopText.text = context.totalWallTop.ToString();
            _wallBottomText.text = context.totalWallBottom.ToString();
            _cornerText.text = context.totalCorners.ToString();
            _doorText.text = context.totalDoors.ToString();
            _hologramText.text = context.totalHolograms.ToString();
        }
        else
            ResetAllText();
    }

    private void ResetAllText()
    {
        _wallTopText.text = _wallBottomText.text = _cornerText.text = _doorText.text = _hologramText.text = "0";
    }
}
