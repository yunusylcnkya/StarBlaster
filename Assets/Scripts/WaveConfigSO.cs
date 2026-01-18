using UnityEngine;

/*
WaveConfigSO:
Bir DÜŞMAN DALGASININ (WAVE) tüm ayarlarını tutan ScriptableObject
Yani:
- Hangi düşmanlar gelecek
- Nereden gidecekler
- Ne hızda hareket edecekler
- Ne sıklıkla spawn olacaklar
*/

[CreateAssetMenu(fileName = "WaveConfig", menuName = "New WaveConfig")]
public class WaveConfigSO : ScriptableObject
{
    /*
    enemyPrefabs:
    Bu dalgada spawn olacak düşman prefabları
    */
    [SerializeField] GameObject[] enemyPrefabs;

    /*
    pathPrefab:
    Düşmanların takip edeceği yol (waypoint'ler)
    */
    [SerializeField] Transform pathPrefab;

    /*
    enemyMoveSpeed:
    Düşmanların hareket hızı
    */
    [SerializeField] float enemyMoveSpeed = 5f;

    /*
    timeBetweenEnemySpawns:
    Düşmanlar arasındaki normal spawn süresi
    */
    [SerializeField] float timeBetweenEnemySpawns = 1f;

    /*
    enemySpawnVariance:
    Spawn süresine rastgelelik katar
    */
    [SerializeField] float enemySpawnVariance = 0f;

    /*
    minimumSpawnTime:
    Spawn süresi bundan daha kısa olamaz
    */
    [SerializeField] float minimumSpawnTime = 0.2f;

    /* ================= GETTER METODLAR ================= */

    // Toplam düşman sayısını döndürür
    public int GetEnemyCount()
    {
        return enemyPrefabs.Length;
    }

    // İstenilen index'teki düşman prefabını döndürür
    public GameObject GetEnemyPrefab(int index)
    {
        return enemyPrefabs[index];
    }

    // Yolun başlangıç noktasını döndürür
    public Transform GetStartingWaypoint()
    {
        return pathPrefab.GetChild(0);
    }

    // Düşman hareket hızını döndürür
    public float GetEnemyMoveSpeed()
    {
        return enemyMoveSpeed;
    }

    /*
    Tüm waypoint'leri sırayla alır
    Enemy'ler bu noktaları takip eder
    */
    public Transform[] GetWaypoints()
    {
        Transform[] waypoints = new Transform[pathPrefab.childCount];

        for (int i = 0; i < pathPrefab.childCount; i++)
        {
            waypoints[i] = pathPrefab.GetChild(i);
        }

        return waypoints;
    }

    /*
    Rastgele spawn süresi üretir
    (variance + minimum kontrolü ile)
    */
    public float GetRandomEnemySpawnTime()
    {
        float spawnTime = Random.Range(
            timeBetweenEnemySpawns - enemySpawnVariance,
            timeBetweenEnemySpawns + enemySpawnVariance
        );

        spawnTime = Mathf.Clamp(spawnTime, minimumSpawnTime, float.MaxValue);

        return spawnTime;
    }
}
