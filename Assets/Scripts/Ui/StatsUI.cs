using UnityEngine;
using TMPro;

public class StatsUI : MonoBehaviour
{
    public GameObject statsPanel;
    public TextMeshProUGUI speedText;
    public TextMeshProUGUI fireRateText;
    public TextMeshProUGUI damageText;
    public TextMeshProUGUI rangeText;
    public TextMeshProUGUI projectileSpeedText;
    public TextMeshProUGUI luckText;

    private bool isPanelVisible = true;
    private PlayerAttributesController playerAttributes;
    private Controls controls;
    void Awake()
    {
        controls = new Controls();
    }

    void OnEnable()
    {
        controls.Player.Enable();
        controls.Player.StatisticToggle.performed +=(ctx)=> onToggle();
    }

    void OnDisable()
    {
        controls.Player.Disable();
    }

    void Start()
    {
        playerAttributes = FindObjectOfType<PlayerAttributesController>();
        UpdateStatsUI();
    }



    void onToggle()
    {
        ToggleStatsPanel();
    }

    void Update()
    {
        UpdateStatsUI();
    }

    private void UpdateStatsUI()
    {
        speedText.text = playerAttributes.Speed.ToString("F2");
        fireRateText.text = playerAttributes.FireRate.ToString("F2");
        damageText.text = playerAttributes.Damage.ToString("F2");
        rangeText.text = playerAttributes.Range.ToString("F2");
        projectileSpeedText.text = playerAttributes.ProjectileSpeed.ToString("F2");
        luckText.text = playerAttributes.Luck.ToString("F2");
    }

    private void ToggleStatsPanel()
    {
        isPanelVisible = !isPanelVisible;
        statsPanel.SetActive(isPanelVisible);
    }
}
