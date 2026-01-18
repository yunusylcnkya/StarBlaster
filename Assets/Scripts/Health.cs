using UnityEngine;

/*
Bu script bir objenin canını (health) kontrol eder.
Bu obje:
- Oyuncu olabilir
- Düşman olabilir

Can azalır, sıfıra düşerse obje yok olur.
Oyuncu ölürse oyun biter,
düşman ölürse skor kazanılır.
*/

public class Health : MonoBehaviour
{
    /*
    isPlayer:
    Bu obje oyuncu mu?
    true → oyuncu
    false → düşman
    */
    [SerializeField] bool isPlayer;

    /*
    scoreValue:
    Eğer bu obje düşmansa,
    öldüğünde kaç puan verecek
    */
    [SerializeField] int scoreValue = 50;

    /*
    health:
    Objenin kaç canı olduğu
    */
    [SerializeField] int health = 50;

    /*
    hitParticles:
    Hasar alındığında çıkacak efekt
    */
    [SerializeField] ParticleSystem hitParticles;

    /*
    applyCameraShake:
    Hasar alınca kamera sarsılsın mı?
    */
    [SerializeField] bool applyCameraShake;

    CameraShake cameraShake;
    AudioManager audioManager;
    ScoreKeeper scoreKeeper;
    LevelManager levelManager;

    /*
    Start:
    Oyun başlarken gerekli diğer scriptleri bulur.
    */
    void Start()
    {
        cameraShake = Camera.main.GetComponent<CameraShake>();
        audioManager = FindFirstObjectByType<AudioManager>();
        scoreKeeper = FindFirstObjectByType<ScoreKeeper>();
        levelManager = FindFirstObjectByType<LevelManager>();
    }

    /*
    OnTriggerEnter2D:
    Bu objeye bir şey çarptığında çalışır.
    Eğer çarpan şey hasar veriyorsa
    (DamageDealer varsa) can azaltılır.
    */
    void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.GetComponent<DamageDealer>();

        if (damageDealer != null)
        {
            // Can azalt
            TakeDamage(damageDealer.GetDamage());

            // Vurulma efekti
            PlayHitParticles();

            // Mermi gibi şeyleri yok et
            damageDealer.Hit();

            // Hasar sesi çal
            audioManager.PlayDamageSFX();

            // İsteniyorsa kamerayı salla
            if (applyCameraShake)
            {
                cameraShake.Play();
            }
        }
    }

    /*
    TakeDamage:
    Canı azaltır.
    Can 0 veya altına düşerse ölür.
    */
    void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }

    /*
    Die:
    Obje öldüğünde ne olacağını belirler.
    */
    void Die()
    {
        if (isPlayer)
        {
            // Oyuncu öldüyse oyun biter
            levelManager.LoadGameOver();
        }
        else
        {
            // Düşman öldüyse skor ekle
            scoreKeeper.ModifyScore(scoreValue);
        }

        // Objeyi tamamen sil
        Destroy(gameObject);
    }

    /*
    PlayHitParticles:
    Hasar alındığında kısa bir efekt çıkarır.
    Efekt bitince kendini siler.
    */
    void PlayHitParticles()
    {
        if (hitParticles != null)
        {
            ParticleSystem particles =
                Instantiate(hitParticles, transform.position, Quaternion.identity);

            Destroy(
                particles,
                particles.main.duration + particles.main.startLifetime.constantMax
            );
        }
    }

    /*
    GetHealth:
    Başka scriptler bu objenin kaç canı kaldığını
    öğrenmek isterse kullanılır.
    */
    public int GetHealth()
    {
        return health;
    }
}
