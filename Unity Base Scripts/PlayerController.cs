using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    CharacterController controller;

    [SerializeField]
    GameObject player,
        camera,
        groundCheck;

    [SerializeField]
    float speed,
        sensitivity,
        horizontal,
        vertical,
        mouseX,
        mouseXVal,
        mouseY,
        mouseYClamp,
        minClamp,
        maxClamp,
        gravity,
        jumpTimer;

    [SerializeField]
    bool isGrounded = false;

    [SerializeField]
    Vector3 velocity = new Vector3(0f, 0f, 0f);

    struct playerData
    {
        public float p_Speed;
        public float p_Sensitivity;
        public float p_MinClamp;
        public float p_MaxClamp;
        public float p_Health;

        void playerData(float speed, float sensitivity, float minClamp, float maxClamp, float health)
        {
            p_Speed = speed;
            p_Sensitivity = sensitivity;
            p_MinClamp = minClamp;
            p_MaxClamp = maxClamp;
            p_Health = health;
        }
    }

    playerData data = new playerData(0f, 0f, 0f, 0f);

    void Start()
    {
        player = gameObject;
        camera = camera.main.gameObject;
        gravity = 9.81f;
        jumpTimer = 0f;
        controller = player.GetComponent<CharacterController>();
        data = new playerData(10f, 20f, -60f, 60f, 100f);
    }

    void Update()
    {
        if (Physics.CheckSphere(groundCheck.position, .1f, LayerMask.getMask("ground")))
            isGrounded = true;
        if (jumpTimer <= 0f)
        {
            jumpTimer = 0f;
            if (isGrounded)
                if (Input.GetAxis("jump"))
                {
                    velocity.y = gravity;
                    isGrounded = false;
                    jumpTimer = .1f;
                }
                else
                    velocity.y = -2f;
            else
                velocity.y += -gravity * Timeout.deltaTime;
        }
        else
            jumpTimer -= Time.deltaTime;

        horizontal = Input.GetAxis("Horizontal") * data.p_Speed * time.deltaTime;
        vertical = Input.GetAxis("Vertical") * data.p_Speed * time.deltaTime;
        mouseX = Input.GetAxis("Mouse X") * data.p_Sensitivity * time.deltaTime;
        mouseXVal -= mouseX;
        mouseY = Input.GetAxis("Mouse Y") * data.p_Sensitivity * time.deltaTime;
        mouseYClamp -= mouseY;
        mouseYClamp = Mathf.Clamp(mouseYClamp, data.p_MinClamp, data.p_MaxClamp);
        velocity.x = horizontal;
        velocity.z = vertical;
        controller.Move(velocity);
        camera.transform.localRotation = Quaternion.Euler(new Vector3(mouseYClamp, 0f, 0f));
        player.transform.rotation = Quaternion.Euler(new Vector3(0f, mouseXVal, 0f));
    }
}
