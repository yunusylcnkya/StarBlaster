using System.Collections;
using UnityEngine;

/*
Bu script kamerayı kısa süreli sallamak için kullanılır.
Mesela:
- Patlama olunca
- Oyuncu hasar alınca
- Büyük bir şey yere düşünce

Kamera sallanınca oyun daha heyecanlı görünür.
*/

public class CameraShake : MonoBehaviour
{
    /*
    shakeDuration:
    Kameranın ne kadar süre sallanacağını belirler.

    shakeMagnitude:
    Kameranın ne kadar güçlü sallanacağını belirler.
    */
    [SerializeField] float shakeDuration = 0.5f;
    [SerializeField] float shakeMagnitude = 0.5f;

    /*
    initialPosition:
    Kameranın normalde durduğu yer.
    Sallama bittikten sonra buraya geri döner.
    */
    Vector3 initialPosition;

    /*
    Start:
    Oyun başlarken kameranın ilk yerini kaydeder.
    */
    void Start()
    {
        initialPosition = transform.position;
    }

    /*
    Play:
    Kamera sallansın diye dışarıdan çağrılan fonksiyon.
    Örneğin başka bir script:
    CameraShake.Play();
    diyebilir.
    */
    public void Play()
    {
        StartCoroutine(ShakeCamera());
    }

    /*
    ShakeCamera:
    Kamerayı küçük küçük rastgele hareket ettirir.
    Bu sayede sallanıyormuş gibi görünür.
    Süre bitince kamera eski yerine döner.
    */
    IEnumerator ShakeCamera()
    {
        float timeElapsed = 0;

        while (timeElapsed < shakeDuration)
        {
            // Kamerayı rastgele küçük bir yere kaydır
            transform.position =
                initialPosition +
                (Vector3)Random.insideUnitCircle * shakeMagnitude;

            // Geçen zamanı artır
            timeElapsed += Time.deltaTime;

            // Bir sonraki kareyi bekle
            yield return new WaitForEndOfFrame();
        }

        // Sallanma bitince kamerayı eski yerine koy
        transform.position = initialPosition;
    }
}
