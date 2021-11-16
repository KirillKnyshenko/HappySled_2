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

    public const float lrRange = 5.5f;
    public const float rotAngle = 45;
    void Update()
    {
        rb.isKinematic = !(GameManager.gameStage == GameStage.Game);

        if (GameManager.gameStage == GameStage.Game)
        {
            oldRotate = transform.rotation;
            rotationY = Mathf.Lerp(rotationY, 0, Time.deltaTime * 10);

            // Move forward
            transform.Translate(Vector3.forward * speedForward * Time.deltaTime);

            // Left Right rotate
            if (Input.GetKey(KeyCode.Mouse0) && !Input.GetKeyDown(KeyCode.Mouse0))
            {
                posX += Input.GetAxis("Mouse X") * speedLR * Time.deltaTime;

                if (posX < -lrRange || posX > lrRange)
                {
                    rotationY = 0;
                }
                else
                    rotationY += Input.GetAxis("Mouse X");
            }


            rotationY = Mathf.Clamp(rotationY, -rotAngle, rotAngle);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(new Vector3(0, rotationY, 0)), Time.deltaTime * 10);

            posX = Mathf.Clamp(posX, -lrRange, lrRange);
            transform.position = Vector3.Lerp(transform.position, new Vector3(posX, transform.position.y, transform.position.z), speedLR * Time.deltaTime);
        }
    }
}
