using UnityEngine;

/// <summary>
/// Стандартный скрипт игрока
/// </summary>

public class Player : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] float speedLR, speedForward, posX, rotationY;
    [SerializeField] Quaternion rotateLR, oldRotate;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        rb.isKinematic = !(GameManager.gameStage == GameStage.Game);

        if (GameManager.gameStage == GameStage.Game)
        {
            oldRotate = transform.rotation;
            rotationY = Mathf.Lerp(rotationY, 0, Time.deltaTime);

            // Move forward
            transform.Translate(Vector3.forward * speedForward * Time.deltaTime);

            // Left Right rotate
            if (Input.GetKey(KeyCode.Mouse0) && !Input.GetKeyDown(KeyCode.Mouse0))
            {
                rotationY += Input.GetAxis("Mouse X");
                rotationY = Mathf.Clamp(rotationY, -15, 15);
                transform.localEulerAngles = Vector3.Lerp(transform.localEulerAngles, new Vector3(0, rotationY, 0), Time.deltaTime);
                
                posX += Input.GetAxis("Mouse X") * speedLR * Time.deltaTime;
                posX = Mathf.Clamp(posX, -5.5f, 5.5f);
                transform.position = Vector3.Lerp(transform.position, new Vector3(posX, transform.position.y, transform.position.z), speedLR * Time.deltaTime);
            }
        }
    }
}
