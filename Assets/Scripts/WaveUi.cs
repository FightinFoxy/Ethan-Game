using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WaveUi : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Slider waveProgressBar;
    [SerializeField] private TextMeshProUGUI waveLabel;
    
    private WaveManager waveManager;
    void Start()
    {
        waveManager = FindFirstObjectByType<WaveManager>();

        waveProgressBar.value = 0f;
        waveLabel.text = "Wave 0 / " + waveManager.GetTotalWaves();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(waveManager == null) return;

        int current = waveManager.GetCurrentWave();
        int total = waveManager.GetTotalWaves();

        // Calculate how far through the waves we are
        float progress = total > 0 ? (float)current / total :0f;

        waveProgressBar.value = progress;
        waveLabel.text = "Wave " + current + " / " + total;
        
    }
}
