using System.Collections;
using UnityEngine;

/*
Bu script mermi atan her şey için kullanılır.
Bu şey:
- Oyuncu olabilir
- Düşman olabilir

İster tuşa basınca,
ister otomatik (AI) olarak ateş edebilir.
*/

public class Shooter : MonoBehaviour
{
    /*
    BASE VARIABLES:
    Merminin ne olduğu, ne kadar hızlı gittiği
    ve ne sıklıkla atıldığı gibi temel ayarlar
    */
    [Header("Base Variables")]
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileLifetime = 5f;
    [SerializeField] float baseFireRate = 0.2f;

    /*
    AI VARIABLES:
    Eğer düşman otomatik ateş edecekse
    bu ayarlar kullanılır.
    */
    [Header("AI Variables")]
    [SerializeField] bool useAI;
    [SerializeField] float minimumFireRate = 0.2f;
    [SerializeField] float fireRateVariance = 0f;

    /*
    isFiring:
    Şu anda ateş ediliyor mu?

    fireCoroutine:
    Sürekli ateş etme işlemini tutar
    */
    [HideInInspector] public bool isFiring;
    Coroutine fireCoroutine;

    /*
    audioManager:
    Ateş sesi çalmak için kullanılır
    */
    AudioManager audioManager;

    /*
    Start:
    Oyun başlarken çalışır.
    Eğer AI açıksa otomatik ateş etmeye başlar.
    */
    void Start()
    {
        audioManager = FindFirstObjectByType<AudioManager>();

        if (useAI)
        {
            isFiring = true;
        }
    }

    /*
    Update:
    Her karede ateş edip etmeyeceğini kontrol eder.
    */
    void Update()
    {
        Fire();
    }

    /*
    Fire:
    Ateş etme durumuna göre
    coroutine başlatır veya durdurur.
    */
    void Fire()
    {
        if (isFiring && fireCoroutine == null)
        {
            // Ateş etmeye başla
            fireCoroutine = StartCoroutine(FireContinuously());
        }
        else if (!isFiring && fireCoroutine != null)
        {
            // Ateş etmeyi durdur
            StopCoroutine(fireCoroutine);
            fireCoroutine = null;
        }
    }

    /*
    FireContinuously:
    Belirli aralıklarla sürekli mermi üretir.
    Bu işlem durdurulana kadar devam eder.
    */
    IEnumerator FireContinuously()
    {
        while (true)
        {
            // Mermiyi oluştur
            GameObject projectile =
                Instantiate(projectilePrefab, transform.position, Quaternion.identity);

            // Merminin yönünü ayarla
            projectile.transform.rotation = transform.rotation;

            // Mermiyi ileri doğru fırlat
            Rigidbody2D projectileRB = projectile.GetComponent<Rigidbody2D>();
            projectileRB.linearVelocity = transform.up * projectileSpeed;

            // Mermiyi belli bir süre sonra yok et
            Destroy(projectile, projectileLifetime);

            // Ateş etme aralığını hesapla
            float waitTime = Random.Range(
                baseFireRate - fireRateVariance,
                baseFireRate + fireRateVariance
            );

            // Çok hızlı ateş etmesini engelle
            waitTime = Mathf.Clamp(waitTime, minimumFireRate, float.MaxValue);

            // Ateş sesi çal
            audioManager.PlayShootingSFX();

            // Bir sonraki mermi için bekle
            yield return new WaitForSeconds(waitTime);
        }
    }
}
