using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
Bu script oyundaki sahneleri (bölümleri) yönetir.
Mesela:
- Ana menü
- Oyun sahnesi
- Game Over ekranı

Ne zaman hangi sahneye gidileceğini bu script kontrol eder.
*/

public class LevelManager : MonoBehaviour
{
    /*
    sceneLoadDelay:
    Sahne değişmeden önce beklenecek süre.
    Örneğin ölünce 2 saniye bekleyip Game Over'a gider.
    */
    [SerializeField] float sceneLoadDelay = 2f;

    /*
    scoreKeeper:
    Skoru yöneten script.
    Yeni oyuna başlarken sıfırlanır.
    */
    ScoreKeeper scoreKeeper;

    /*
    Awake:
    Oyun daha başlarken çalışır.
    ScoreKeeper scriptini bulur.
    */
    void Awake()
    {
        scoreKeeper = FindFirstObjectByType<ScoreKeeper>();
    }

    /*
    LoadGame:
    Oyunu başlatır.
    Oyun sahnesini yükler
    ve skoru sıfırlar.
    */
    public void LoadGame()
    {
        SceneManager.LoadScene("GameScene");
        scoreKeeper.ResetScore();
    }

    /*
    LoadGameOver:
    Oyuncu ölünce çağrılır.
    Biraz bekler, sonra Game Over sahnesini açar.
    */
    public void LoadGameOver()
    {
        StartCoroutine(WaitAndLoad("GameOver", sceneLoadDelay));
    }

    /*
    LoadMainMenu:
    Ana menüye geri döner.
    */
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    /*
    QuitGame:
    Oyunu kapatır.
    (Unity editördeyken kapatmaz, sadece mesaj yazar)
    */
    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }

    /*
    WaitAndLoad:
    Belirtilen süre kadar bekler
    sonra istenen sahneyi yükler.
    */
    IEnumerator WaitAndLoad(string sceneName, float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneName);
    }
}
