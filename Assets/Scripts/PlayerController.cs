using System;
using UnityEngine;
using UnityEngine.InputSystem;

/*
Bu script oyuncunun hareket etmesini
ve ateş etmesini kontrol eder.
Oyuncu ekrandan dışarı çıkamaz
ve tuşlara basınca mermi atar.
*/

public class PlayerController : MonoBehaviour
{
    /*
    moveSpeed:
    Oyuncunun ne kadar hızlı hareket edeceği

    Bound Padding:
    Oyuncunun ekran kenarlarına ne kadar yaklaşabileceği
    */
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float leftBoundPadding;
    [SerializeField] float rightBoundPadding;
    [SerializeField] float upBoundPadding;
    [SerializeField] float downBoundPadding;

    /*
    playerShooter:
    Oyuncunun ateş etmesini sağlayan script

    moveAction & fireAction:
    Klavye veya gamepad girişleri
    */
    Shooter playerShooter;
    InputAction moveAction;
    InputAction fireAction;

    /*
    moveVector:
    Oyuncunun hangi yöne gittiği

    minBounds & maxBounds:
    Kameranın sol-alt ve sağ-üst sınırları
    */
    Vector3 moveVector;
    Vector2 minBounds;
    Vector2 maxBounds;

    /*
    Start:
    Oyun başlarken çalışır.
    Gerekli scriptleri ve tuşları alır.
    */
    void Start()
    {
        playerShooter = GetComponent<Shooter>();

        moveAction = InputSystem.actions.FindAction("Move");
        fireAction = InputSystem.actions.FindAction("Fire");

        InitBounds();
    }

    /*
    Update:
    Her karede çalışır.
    Oyuncuyu hareket ettirir ve ateş eder.
    */
    void Update()
    {
        MovePlayer();
        FireShooter();
    }

    /*
    InitBounds:
    Kameranın sınırlarını hesaplar.
    Böylece oyuncu ekrandan çıkamaz.
    */
    void InitBounds()
    {
        Camera mainCamera = Camera.main;

        // Ekranın sol alt ve sağ üst köşeleri
        minBounds = mainCamera.ViewportToWorldPoint(new Vector2(0, 0));
        maxBounds = mainCamera.ViewportToWorldPoint(new Vector2(1, 1));
    }

    /*
    MovePlayer:
    Tuşlara basılmasına göre oyuncuyu hareket ettirir.
    Oyuncu ekran sınırları dışına çıkamaz.
    */
    void MovePlayer()
    {
        moveVector = moveAction.ReadValue<Vector2>();

        Vector3 newPos =
            transform.position +
            moveVector * moveSpeed * Time.deltaTime;

        // Ekran dışına çıkmasını engelle
        newPos.x = Math.Clamp(
            newPos.x,
            minBounds.x + leftBoundPadding,
            maxBounds.x - rightBoundPadding
        );

        newPos.y = Math.Clamp(
            newPos.y,
            minBounds.y + downBoundPadding,
            maxBounds.y - upBoundPadding
        );

        transform.position = newPos;
    }

    /*
    FireShooter:
    Ateş tuşuna basılıysa mermi atar,
    basılı değilse durur.
    */
    void FireShooter()
    {
        playerShooter.isFiring = fireAction.IsPressed();
    }
}
