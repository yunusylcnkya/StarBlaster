using TMPro;
using UnityEngine;
using UnityEngine.UI;

/*
Bu script oyun sırasında ekrandaki
CAN BARINI ve SKOR yazısını günceller.
*/

public class UIUpdater : MonoBehaviour
{
    /* ================= CAN ================= */

    /*
    healthSlider:
    Oyuncunun canını gösteren bar
    */
    [SerializeField] Slider healthSlider;

    /*
    playerHealth:
    Oyuncunun can bilgisini tutan script
    */
    [SerializeField] Health playerHealth;

    /* ================= SKOR ================= */

    /*
    scoreText:
    Ekranda skoru yazdırır
    */
    [SerializeField] TextMeshProUGUI scoreText;

    /*
    scoreKeeper:
    Skor bilgisini tutar
    */
    ScoreKeeper scoreKeeper;

    /*
    Start:
    Oyun başlarken çalışır
    - ScoreKeeper bulunur
    - Can barının maksimum değeri ayarlanır
    */
    void Start()
    {
        scoreKeeper = FindFirstObjectByType<ScoreKeeper>();
        healthSlider.maxValue = playerHealth.GetHealth();
    }

    /*
    Update:
    Her karede çalışır
    - Skoru günceller
    - Can barını günceller
    */
    void Update()
    {
        // Skoru 9 haneli olarak gösterir (000000120 gibi)
        scoreText.text = scoreKeeper.GetScore().ToString("000000000");

        // Can barını günceller
        healthSlider.value = playerHealth.GetHealth();
    }
}
