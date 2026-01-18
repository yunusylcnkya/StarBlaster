using TMPro;
using UnityEngine;

/*
Bu script Game Over ekranında çalışır.
Oyuncunun oyunu bitirdiğinde
kaç puan topladığını ekranda gösterir.
*/

public class UIGameOver : MonoBehaviour
{
    /*
    scoreText:
    Ekranda yazı göstermek için kullanılır
    (TextMeshPro yazısı)
    */
    [SerializeField] TextMeshProUGUI scoreText;

    /*
    scoreKeeper:
    Oyuncunun skorunu tutan script
    */
    ScoreKeeper scoreKeeper;

    /*
    Awake:
    Sahne yüklenir yüklenmez çalışır.
    ScoreKeeper'ı burada buluruz.
    */
    void Awake()
    {
        scoreKeeper = FindFirstObjectByType<ScoreKeeper>();
    }

    /*
    Start:
    Oyun başladığında çalışır.
    Oyuncunun son skorunu ekrana yazar.
    */
    void Start()
    {
        scoreText.text = "FINAL SCORE:\n" + scoreKeeper.GetScore();
    }
}
