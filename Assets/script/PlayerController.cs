using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f; // Kecepatan gerak pemain

    void Update()
    {
        // Panggil fungsi untuk mengendalikan pemain
        MovePlayer();
    }

    void MovePlayer()
    {
        // Mendapatkan input dari sumbu horizontal (W, A, S, D atau panah kiri dan kanan)
        float horizontalInput = Input.GetAxis("Horizontal");
        // Mendapatkan input dari sumbu vertikal (W, A, S, D atau panah atas dan bawah)
        float verticalInput = Input.GetAxis("Vertical");

        // Menghitung vektor gerak berdasarkan input keyboard
        Vector3 moveDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;

        // Menghitung pergerakan berdasarkan arah input dan kecepatan
        Vector3 moveAmount = moveDirection * moveSpeed * Time.deltaTime;

        // Menggerakkan pemain
        transform.Translate(moveAmount);
    }
}
