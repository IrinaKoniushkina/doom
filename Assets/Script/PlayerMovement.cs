using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 100f; // Скорость поворота по стрелкам
    private CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void Update()
    {
        // Движение вперед/назад
        float moveInput = Input.GetAxis("Vertical");
        Vector3 move = transform.forward * moveInput * moveSpeed * Time.deltaTime;
        controller.Move(move);

        // Поворот по стрелкам влево/вправо
        float rotationInput = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.up * rotationInput * rotationSpeed * Time.deltaTime);
    }
}