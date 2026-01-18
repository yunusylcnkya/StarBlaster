using UnityEngine;

/*
Bu script oyundaki sesleri yönetir.
Örneğin:
- Ateş etme sesi
- Hasar alma sesi

Bu scriptten SADECE BİR TANE olur (singleton).
Böylece sesler karışmaz ve sahne değişse bile çalışmaya devam eder.
*/

public class AudioManager : MonoBehaviour
{
    /*
    SHOOTING SFX:
    Silah ateş edildiğinde çalacak ses ve ses seviyesi
    */
    [Header("Shooting SFX")]
    [SerializeField] AudioClip shootingClip;
    [SerializeField][Range(0, 1)] float shootingVolume = 1f;

    /*
    DAMAGE SFX:
    Oyuncu hasar aldığında çalacak ses ve ses seviyesi
    */
    [Header("Damage SFX")]
    [SerializeField] AudioClip damageClip;
    [SerializeField][Range(0, 1)] float damageVolume = 1f;

    /*
    instance:
    Bu scriptten sahnede sadece 1 tane olmasını sağlar
    */
    static AudioManager instance;

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
    Eğer sahnede bu scriptten zaten varsa
    yenisini yok eder.
    Yoksa bu objeyi saklar ve sahne değişse bile silmez.
    */
    void ManageSingleton()
    {
        if (instance != null)
        {
            // Zaten varsa bu objeyi sil
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            // İlk defa oluşuyorsa sakla
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    /*
    PlayShootingSFX:
    Silah sesi çalmak için kullanılır
    */
    public void PlayShootingSFX()
    {
        PlayAudioClip(shootingClip, shootingVolume);
    }

    /*
    PlayDamageSFX:
    Hasar sesi çalmak için kullanılır
    */
    public void PlayDamageSFX()
    {
        PlayAudioClip(damageClip, damageVolume);
    }

    /*
    PlayAudioClip:
    Verilen sesi, verilen ses seviyesinde çalar.
    Ses kameranın olduğu yerden gelir.
    */
    void PlayAudioClip(AudioClip clip, float volume)
    {
        if (clip != null)
        {
            AudioSource.PlayClipAtPoint(
                clip,
                Camera.main.transform.position,
                volume
            );
        }
    }
}
