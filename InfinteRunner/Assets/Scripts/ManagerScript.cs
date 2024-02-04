using UnityEngine;
using UnityEngine.UI;

public class ManagerScript : MonoBehaviour
{
    public static ManagerScript Instance;

    public bool playerDead = false;
    public Slider slider;
    public Slider fogSliderStart;
    public Slider fogSliderEnd;
    public Camera mainCamera;
    public bool spawnTransition = false;
    public int zonePublic = 0;
    public int zoneLenghtPublic = 0;
    public int goDown = 0;
    public int lastZone = 0;

    public int playerScore = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        slider.value = 0.5f;
        RenderSettings.fogDensity = 0.05f;
    }

    private void FixedUpdate()
    {
        if (!ManagerScript.Instance.playerDead)
        {
            slider.value -= 0.1f * Time.fixedDeltaTime;
        }
        
        if(slider.value <= 0.25f && RenderSettings.fogDensity <= 0.085)
        {
            RenderSettings.fogDensity += 0.01f * Time.fixedDeltaTime;
        }
        else if (slider.value > 0.25f && RenderSettings.fogDensity >= 0.05)
        {
            RenderSettings.fogDensity -= 0.01f * Time.fixedDeltaTime;
        }
    }

    public void SetOil(float oil)
    {
        slider.value = oil;
    }
}
