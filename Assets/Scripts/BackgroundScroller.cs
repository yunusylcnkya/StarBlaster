using UnityEngine;

/*
Bu script arka planın hareket ediyormuş gibi görünmesini sağlar.
Aslında arka plan yer değiştirmez,
sadece resmi kaydırılır.
Bu sayede oyun daha canlı görünür.
*/

public class BackgroundScroller : MonoBehaviour
{
    /*
    moveSpeed:
    Arka planın ne kadar hızlı ve hangi yönde kayacağını belirler.
    X sağ-sol, Y yukarı-aşağı hareket demektir.
    */
    [SerializeField] Vector2 moveSpeed;

    /*
    offset:
    Resmin ne kadar kaydırıldığını tutar.
    
    material:
    Arka planın kaplandığı resim (texture) bilgisidir.
    */
    Vector2 offset;
    Material material;

    /*
    Start:
    Oyun başlarken çalışır.
    Bu objenin Sprite'ının kullandığı materyali alırız.
    */
    void Start()
    {
        material = GetComponent<SpriteRenderer>().material;
    }

    /*
    Update:
    Her karede çalışır.
    Arka planı yavaş yavaş kaydırır.
    */
    void Update()
    {
        // Zaman geçtikçe offset artar (arka plan kayar)
        offset += moveSpeed * Time.deltaTime;

        // Resmin kaydırma ayarını değiştirir
        material.mainTextureOffset = offset;
    }
}
