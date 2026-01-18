using UnityEngine;

/*
Bu script bir şeyin ne kadar hasar verdiğini anlatır.
Mesela:
- Düşman
- Mermi
- Tuzak

Başka scriptler bu objenin ne kadar can azalttığını
bu scriptten öğrenir.
*/

public class DamageDealer : MonoBehaviour
{
    /*
    damage:
    Bu obje çarptığında kaç can götürecek
    */
    [SerializeField] int damage = 10;

    /*
    GetDamage:
    Hasar bilgisini dışarıya verir.
    Diğer scriptler:
    "Bu bana kaç hasar verecek?"
    diye buradan öğrenir.
    */
    public int GetDamage()
    {
        return damage;
    }

    /*
    Hit:
    Bu obje birine çarptığında çağrılır.
    Genelde mermi gibi tek kullanımlık şeyler
    çarptıktan sonra yok olur.
    */
    public void Hit()
    {
        Destroy(gameObject);
    }
}
