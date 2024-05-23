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

    void Start()
    {
        playerAttributes = FindObjectOfType<PlayerAttributesController>();
        UpdateStatsUI();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            ToggleStatsPanel();
        }
        UpdateStatsUI();
    }

    private void UpdateStatsUI()
    {
        speedText.text = "Speed: " + playerAttributes.Speed.ToString("F2");
        fireRateText.text = "Fire Rate: " + playerAttributes.FireRate.ToString("F2");
        damageText.text = "Damage: " + playerAttributes.Damage.ToString("F2");
        rangeText.text = "Range: " + playerAttributes.Range.ToString("F2");
        projectileSpeedText.text = "Projectile Speed: " + playerAttributes.ProjectileSpeed.ToString("F2");
        luckText.text = "Luck: " + playerAttributes.Luck.ToString("F2");
    }

    private void ToggleStatsPanel()
    {
        isPanelVisible = !isPanelVisible;
        statsPanel.SetActive(isPanelVisible);
    }
}
