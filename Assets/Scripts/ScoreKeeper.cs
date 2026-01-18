using UnityEngine;

/*
Bu script oyundaki puanı (score) tutar.
Skor sahne değişse bile kaybolmaz
ve oyunda sadece 1 tane olur.
*/

public class ScoreKeeper : MonoBehaviour
{
    /*
    score:
    Oyuncunun topladığı puan
    */
    int score = 0;

    /*
    instance:
    Bu scriptten sadece 1 tane olmasını sağlar
    */
    static ScoreKeeper instance;

    /*
    Awake:
    Oyun başlar başlamaz çalışır.
    */
    void Awake()
    {
        ManageSingleton();
    }

    /*
    ManageSingleton:
    Eğer bu script sahnede zaten varsa
    yenisini yok eder.
    Yoksa saklar ve sahne değişse bile silmez.
    */
    void ManageSingleton()
    {
        if (instance != null)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    /*
    GetScore:
    Şu anki puanı geri verir.
    */
    public int GetScore()
    {
        return score;
    }

    /*
    ModifyScore:
    Puan ekler (veya çıkarabilir).
    Puan asla 0’ın altına düşmez.
    */
    public void ModifyScore(int scoreToAdd)
    {
        score += scoreToAdd;

        // Puan 0 ile çok büyük bir sayı arasında kalır
        score = Mathf.Clamp(score, 0, int.MaxValue);

        // Konsola yazdır (kontrol için)
        print(score);
    }

    /*
    ResetScore:
    Yeni oyun başlarken puanı sıfırlar.
    */
    public void ResetScore()
    {
        score = 0;
    }
}
