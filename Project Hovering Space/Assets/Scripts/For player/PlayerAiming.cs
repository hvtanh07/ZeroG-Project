using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAiming : MonoBehaviour
{
    [Header("Properties")]
    public float turnSpeed = 15f;
    public float aimDuration = 0.3f;
    public float MinXTurn = -40f;
    public float MaxXTurn = 40f;

    public float MinXTurnNoGun = -80f;
    public float MaxXTurnNoGun = 90f;
    public float AimingRecoilReduction = 0.2f;
    public Transform cameraLookat;
    [Space(10)]
    [Header("Camera Components")]
    public Animator rigController;
    public Cinemachine.AxisState xAxis;
    public Cinemachine.AxisState yAxis;

    Camera maincamera;
    Animator animator;
    ActiveWeapon activeWeapon;
    int isAimingParam = Animator.StringToHash("isAiming");

    // Start is called before the first frame update
    void Start()
    {
        Camera maincamera = Camera.main;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        animator = GetComponent<Animator>();
        activeWeapon = GetComponent<ActiveWeapon>();
    }

    private void Update()
    {
        bool isAiming = Input.GetMouseButton(1);
        animator.SetBool(isAimingParam, isAiming);

        var Weapon = activeWeapon.GetActiveGun();
        if (Weapon)
        {
            Weapon.recoil.recoilModifier = isAiming ? AimingRecoilReduction : 1f;
        }
    }

    private void FixedUpdate()
    {
        xAxis.Update(Time.fixedDeltaTime);
        yAxis.Update(Time.fixedDeltaTime);

        if (rigController.GetBool("holster_weapon")){
            xAxis.m_MaxValue = 180f;
            xAxis.m_MinValue = -180f;
            xAxis.m_Wrap = true;
        }
        else
        {
            //xAxis.m_Wrap = false;
            //while (xAxis.m_MaxValue > MaxXTurn)
            //{
            //    xAxis.m_MaxValue -= Time.deltaTime;
            //}
            //while (xAxis.m_MinValue < MinXTurn)
            //{
            //    xAxis.m_MaxValue += Time.deltaTime;
            //}
            xAxis.m_MaxValue = MaxXTurn;
            xAxis.m_MinValue = MinXTurn;
            xAxis.m_Wrap = false;
        }
        if ((xAxis.Value > MaxXTurnNoGun || xAxis.Value < MinXTurnNoGun) && rigController.GetBool("holster_weapon"))
        {
            rigController.SetBool("LookAtCam", false);
        }
        else
        {
            rigController.SetBool("LookAtCam", true);
        }
        //PlayerHead.weight
        cameraLookat.eulerAngles = new Vector3(yAxis.Value, xAxis.Value, 0);

        //float yawnCamera = maincamera.transform.position.y;
        //Quaternion turn = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, yawnCamera, 0), turnSpeed * Time.deltaTime);
    }
}
