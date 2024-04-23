using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Guard : MonoBehaviour
{
    public Transform[] patrolPoints; 
    public float moveSpeed = 3f; 
    public float rotationSpeed = 2f; 
    public LayerMask playerLayer; // Layer mask untuk pemain
    public GameObject playerObject; // GameObject pemain

    public float viewAngle = 45f; // Sudut pandang guard
    public float viewDistance = 10f; // Jarak pandang guard

    private int currentPatrolIndex = 0; 
    private bool gameOver = false; // Menandakan apakah permainan sudah berakhir
    private bool playerDetected = false; // Menandakan apakah pemain terdeteksi oleh guard

    void Update()
    {
        if (!gameOver && !playerDetected) // Pastikan permainan belum berakhir dan pemain belum terdeteksi sebelum mengizinkan patroli
        {
            Patrol(); 
        }
    // Raycast dari posisi guard ke arah pemain
    RaycastHit hit;
    if (Physics.Raycast(transform.position, playerObject.transform.position - transform.position, out hit, Mathf.Infinity, playerLayer))
    {
        Debug.Log("Hit " + hit.collider.name); // Cetak nama collider yang terkena raycast ke konsol
    }
    }

    void Patrol()
    {
        if (CanSeePlayer())
        {
            // Jika pemain terdeteksi, panggil fungsi untuk menghentikan permainan dan menampilkan pesan game over
            GameOver();
            playerDetected = true; // Setel pemain terdeteksi menjadi true
        }
        else
        {
            MoveToNextPatrolPoint();
        }
    }

    void GameOver()
    {
        gameOver = true; // Setel game over menjadi true
        Debug.Log("Player detected! Game over."); // Tampilkan pesan game over di konsol
        //SceneManager.LoadScene("SampleScene"); // Anda juga bisa menambahkan kode untuk mengatur kondisi kalah, misalnya kembali ke menu utama atau memuat ulang level
    }

    bool CanSeePlayer()
    {
        if (playerObject != null) // Periksa apakah playerObject tidak null
        {
            Vector3 direction = playerObject.transform.position - transform.position;
            float angle = Vector3.Angle(direction, transform.forward);

            if (angle < viewAngle)
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, direction.normalized, out hit, viewDistance, playerLayer))
                {
                    if (hit.collider.gameObject == playerObject)
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }

    void MoveToNextPatrolPoint()
    {
        Vector3 direction = patrolPoints[currentPatrolIndex].position - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, patrolPoints[currentPatrolIndex].position) < 0.5f)
        {
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
        }
    }
}
