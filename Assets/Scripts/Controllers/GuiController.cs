using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PlanetsCore;

public class GuiController : MonoBehaviour
{
    public Text lbTime;
    public GameObject pnBodyInfo;
    public Text lbSurfaceType;
    public Text lbBodyName;
    public Text lbBodyInfo;
    public GameObject pnMessage;
    public Text lbMessage;
    public Text lbMessageTitle;
    public GameObject pnSettings;
    public Toggle tgSound;
    public Text lbScaleSize;
    public Slider slScaleSize;
    public Toggle tgLogSize;
    public Toggle tgAmbientLight;
    public Toggle tgSkyBox;
    public Toggle tgShowMap;
    public Light BackLight;
    public GameObject pnMap;
    public Slider slScaleMap;

    public static GuiController Instance { get; private set; }

    void Awake()
    {
        Instance = this;
        ReadPlayerPrefs();
    }

    int updating;

    private void ReadPlayerPrefs()
    {
        updating++;

        var sound = PlayerPrefs.GetInt("Sound", 1) > 0;
        var scaleSize = PlayerPrefs.GetFloat("ScaleSize", 40);
        var logSize = PlayerPrefs.GetInt("LogSize", 1) > 0;
        var ambientLight = PlayerPrefs.GetInt("AmbientLight", 0) > 0;
        var skyBox = PlayerPrefs.GetInt("SkyBox", 0) > 0;
        var map = PlayerPrefs.GetInt("Map", 0) > 0;
        var scaleMap = PlayerPrefs.GetFloat("ScaleMap", 1.5f);

        tgSound.isOn = sound;
        lbScaleSize.text = "Scale Size of Objects: " + (int) scaleSize;
        slScaleSize.value = scaleSize;
        tgLogSize.isOn = logSize;
        tgAmbientLight.isOn = ambientLight;
        tgSkyBox.isOn = skyBox;
        tgShowMap.isOn = map;
        slScaleMap.value = scaleMap;

        StateBus.DistanceScaleRelativeToSize = 1f / scaleSize;
        StateBus.LogarithmicSize = logSize;
        Helper.Camera.GetComponent<AudioSource>().mute = !sound;
        BackLight.enabled = ambientLight;
        Camera.main.clearFlags = skyBox ? CameraClearFlags.Skybox : CameraClearFlags.Color;
        pnMap.SetActive(map);
        StateBus.MapScale = scaleMap;

        updating--;
    }

    void Update ()
	{
        //draw time
	    lbTime.text = GetTimeString();

        //if camera target is changed - draw info for new target
	    if (StateBus.CameraTargetChanged)
	    {
	        var body = StateBus.CameraTargetBody;
            var desc = StateBus.CameraTargetDesc;

            if (body != null)
	        {
	            pnBodyInfo.SetActive(true);
	            lbBodyName.text = desc.Name;
                lbSurfaceType.text = desc.SurfaceType.ToString();
	            lbBodyInfo.text = string.Format(
@"Mass:      {0,6:0.00} e
Radius:     {1,6:0.00} e
Orb.Speed:  {2,6:0.0} km/s
Orb.Radius: {3,6:0.000} au", 
               desc.Mass / Constants.EARTH_MASS, desc.Radius / Constants.EARTH_RADIUS, desc.OrbitalSpeed / 1000, desc.OrbitRadius / Constants.AU);
	        }
	    }

        //set back light direction
	    BackLight.gameObject.transform.LookAt(-Camera.main.transform.position);
	}

    string GetTimeString()
    {
        var time = StateBus.ModelTime;
        var hours = (int)(time / (60 * 60));
        var days = hours / 24;
        var years = (int)(days / (365.25));

        var h = hours % 24;
        var d = (int)(days - years * 365.25);

        if (years == 0 && d == 0)
            return string.Format("{0,2}h", h);

        if (years == 0)
            return string.Format("{1,3}d {0,2}h", h, d);

        return string.Format("{2}y {1,3}d {0,2}h", h, d, years);
    }

    public void PlayTimeClick()
    {
        StateBus.TimeMultiplier = 1;
    }

    public void BackTimeClick()
    {
        if (StateBus.TimeMultiplier > 1)
            StateBus.TimeMultiplier /= 4;
        else
        if (StateBus.TimeMultiplier > -2)
            StateBus.TimeMultiplier = -2;
        else
            StateBus.TimeMultiplier *= 4;
    }

    public void PauseTimeClick()
    {
        StateBus.TimeMultiplier = 0;
    }

    public void FastTimeClick()
    {
        if (StateBus.TimeMultiplier <= 0)
            StateBus.TimeMultiplier = 4;
        else
            StateBus.TimeMultiplier *= 4;
    }

    public void LoadModelClick()
    {
        var res = ModelLoader.Instance.LoadModel();
        if (!res)
        {
            ShowMessage("Model file is not found", "Model file is not found.\r\n\r\nYou need to create model and save file *.planets to folder:\r\n" + Application.dataPath);
        }
    }

    public void CloseMessageClick()
    {
        pnMessage.SetActive(false);
    }

    public void ShowMessage(string title, string message)
    {
        lbMessageTitle.text = title;
        lbMessage.text = message;
        pnMessage.SetActive(true);
    }

    public void SettingsClick()
    {
        pnSettings.SetActive(true);
    }

    public void CloseSettingsClick()
    {
        pnSettings.SetActive(false);
    }

    public void CloseAppClick()
    {
        Application.Quit();
    }

    public void BuildTrajectoryClick()
    {
        var b1 = StateBus.Model.Bodies[7];
        var b2 = StateBus.Model.Bodies[8];
        TrajectoryBuilder.Instance.Build(b1, b2, StateBus.ModelTime, new Vector3(0000, 0, -60000));
    }

    public void OnSettingsChanged()
    {
        if (updating > 0) return;

        PlayerPrefs.SetInt("Sound", tgSound.isOn ? 1 : 0);
        PlayerPrefs.SetFloat("ScaleSize", slScaleSize.value);
        PlayerPrefs.SetInt("LogSize", tgLogSize.isOn ? 1 : 0);
        PlayerPrefs.SetInt("AmbientLight", tgAmbientLight.isOn ? 1 : 0);
        PlayerPrefs.SetInt("SkyBox", tgSkyBox.isOn ? 1 : 0);
        PlayerPrefs.SetInt("Map", tgShowMap.isOn ? 1 : 0);
        PlayerPrefs.SetFloat("ScaleMap", slScaleMap.value);

        ReadPlayerPrefs();

        //fire event of changing of player prefs
        StateBus.PlayerPrefsChanged += true;
    }
}
