using UnityEngine;

/// <summary>
/// Стандартный скрипт игрока
/// </summary>
public class Player : MonoBehaviour
{

    Rigidbody rb;
    [SerializeField] float speedLR, posX;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        rb.isKinematic = !(GameManager.gameStage == GameStage.Game);

        if (GameManager.gameStage == GameStage.Game)
        {
            if (Input.GetKey(KeyCode.Mouse0) && !Input.GetKeyDown(KeyCode.Mouse0))
            {
                posX += Input.GetAxis("Mouse X") * speedLR * Time.deltaTime;
                posX = Mathf.Clamp(posX, -14.5f, 14.5f);
                transform.position = Vector3.Lerp(transform.position, new Vector3(posX, transform.position.y, transform.position.z), speedLR * Time.deltaTime);
            }
            
        }
    }
}
