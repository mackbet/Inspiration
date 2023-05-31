using UnityEngine;

public class CameraRotator : MonoBehaviour
{
    public float sensitivity = 2f;
    public float maxYAngle = 80f;
    [SerializeField] private Transform _camera;

    [field: SerializeField] public Camera camera { get; private set; }

    private float rotationY = 0f;
    private float rotationX = 0f;
    private void Start()
    {
        SettingsPanel.onSensitivityChanged += SetSensitivity;
        sensitivity = SettingsPanel.GetSensitivity();
    }
    void Update()
    {
        if (Time.timeScale != 1) return;

        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        // Поворачиваем камеру по горизонтали
        rotationX += mouseX;
        rotationX = Mathf.Repeat(rotationX, 360f);

        // Поворачиваем камеру по вертикали
        rotationY -= mouseY;
        rotationY = Mathf.Clamp(rotationY, -maxYAngle, maxYAngle);

        // Применяем поворот камеры
        transform.rotation = Quaternion.Euler(rotationY, rotationX, 0f);
    }

    private void SetSensitivity(float newSensitivity)
    {
        sensitivity = newSensitivity;
    }
}
