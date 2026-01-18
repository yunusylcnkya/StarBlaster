using UnityEngine;

/*
Bu script düşmanların harita üzerinde
belirlenmiş bir yolu takip etmesini sağlar.
Düşmanlar noktadan noktaya gider,
yol bitince yok olurlar.
*/

public class Pathfinding : MonoBehaviour
{
    /*
    enemySpawner:
    Düşmanları oluşturan script.

    waveConfig:
    Şu anki düşman dalgasının ayarları.

    waypoints:
    Düşmanın takip edeceği noktalar.
    */
    EnemySpawner enemySpawner;
    WaveConfigSO waveConfig;
    Transform[] waypoints;

    /*
    waypointIndex:
    Şu anda gidilen noktanın numarası.
    */
    int waypointIndex = 0;

    /*
    Start:
    Düşman oluştuğunda çalışır.
    Gerekli bilgileri alır ve
    düşmanı başlangıç noktasına koyar.
    */
    void Start()
    {
        enemySpawner = FindFirstObjectByType<EnemySpawner>();
        waveConfig = enemySpawner.GetCurrentWave();

        waypoints = waveConfig.GetWaypoints();

        // Düşmanı yolun başına koy
        transform.position = waveConfig.GetStartingWaypoint().position;
    }

    /*
    Update:
    Her karede düşmanı hareket ettirir.
    */
    void Update()
    {
        FollowPath();
    }

    /*
    FollowPath:
    Düşman sıradaki noktaya doğru gider.
    O noktaya varınca bir sonrakine geçer.
    Tüm noktalar bitince düşman yok edilir.
    */
    void FollowPath()
    {
        if (waypointIndex < waypoints.Length)
        {
            // Gidilecek hedef nokta
            Vector3 targetPosition = waypoints[waypointIndex].position;

            // Bu karede ne kadar ilerleyeceği
            float moveDelta =
                waveConfig.GetEnemyMoveSpeed() * Time.deltaTime;

            // Hedefe doğru ilerle
            transform.position = Vector2.MoveTowards(
                transform.position,
                targetPosition,
                moveDelta
            );

            // Hedefe ulaştıysa sıradaki noktaya geç
            if (transform.position == targetPosition)
            {
                waypointIndex++;
            }
        }
        else
        {
            // Yol bitti, düşmanı sil
            Destroy(gameObject);
        }
    }
}
