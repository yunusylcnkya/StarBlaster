using System.Collections;
using UnityEngine;

/*
Bu script düşmanları dalga dalga (wave wave) oyuna getirir.
Mesela:
- İlk dalgada 3 düşman
- Sonraki dalgada 5 düşman
- Hepsi belirli aralıklarla çıkar

Bu sayede oyun yavaş başlar, sonra zorlaşır.
*/

public class EnemySpawner : MonoBehaviour
{
    /*
    waveConfigs:
    Her dalganın bilgilerini tutar.
    İçinde:
    - Kaç düşman var
    - Hangi düşman çıkacak
    - Ne kadar sürede çıkacak
    gibi bilgiler bulunur.
    */
    [SerializeField] WaveConfigSO[] waveConfigs;

    /*
    timeBetweenWaves:
    Bir dalga bittikten sonra,
    diğer dalganın başlaması için beklenecek süre.
    */
    [SerializeField] float timeBetweenWaves = 1f;

    /*
    isLooping:
    Eğer true ise:
    - Son dalga bitince tekrar başa döner
    - Düşmanlar sonsuz şekilde gelir
    */
    [SerializeField] bool isLooping;

    /*
    currentWave:
    Şu anda oynanan dalgayı tutar.
    */
    WaveConfigSO currentWave;

    /*
    Start:
    Oyun başlar başlamaz düşman üretmeye başlar.
    */
    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    /*
    SpawnEnemies:
    Düşmanları sırayla ve bekleyerek çıkaran coroutine.
    */
    IEnumerator SpawnEnemies()
    {
        do
        {
            // Tüm dalgaları sırayla dolaş
            foreach (WaveConfigSO wave in waveConfigs)
            {
                currentWave = wave;

                // Bu dalgadaki düşmanları tek tek çıkar
                for (int i = 0; i < currentWave.GetEnemyCount(); i++)
                {
                    Instantiate(
                        currentWave.GetEnemyPrefab(i),
                        currentWave.GetStartingWaypoint().position,
                        Quaternion.identity,
                        transform);

                    // Bir sonraki düşman için bekle
                    yield return new WaitForSeconds(
                        currentWave.GetRandomEnemySpawnTime());
                }

                // Dalga bitince biraz bekle
                yield return new WaitForSeconds(timeBetweenWaves);
            }

        } while (isLooping); // Eğer açıksa başa dön
    }

    /*
    GetCurrentWave:
    Başka scriptler "şu an hangi dalgadayız?"
    diye sorduğunda kullanılır.
    */
    public WaveConfigSO GetCurrentWave()
    {
        return currentWave;
    }
}
