using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRecoil : MonoBehaviour
{
    [HideInInspector] public Cinemachine.CinemachineFreeLook playerCamera;
    [HideInInspector] public Cinemachine.CinemachineImpulseSource cameraShake;
    [HideInInspector] public Animator rigController;

    public Vector2[] recoilPatern;
    public float duration;//0.1

    float time;
    float VerticeRecoil;//10
    float HorizontalRecoil;
    int index;

    private void Awake()
    {
        cameraShake = GetComponent<Cinemachine.CinemachineImpulseSource>();
    }

    public void Reset()
    {
        index = 0;
    }

    int NextIndex(int index)
    {
        return (index + 1) % recoilPatern.Length;
    }
    public void GenerateRecoid(string weaponName)
    {
        time = duration;
        cameraShake.GenerateImpulse(Camera.main.transform.forward);

        HorizontalRecoil = recoilPatern[index].x;
        VerticeRecoil = recoilPatern[index].y;

        index = NextIndex(index);

        rigController.Play("recoil_anim_" + weaponName, 1, 0.0f);
    }
    // Update is called once per frame
    void Update()
    {
        if (time > 0)
        {
            playerCamera.m_YAxis.Value -= ((VerticeRecoil/1000) * Time.deltaTime) / duration;
            playerCamera.m_XAxis.Value -= ((HorizontalRecoil / 10) * Time.deltaTime) / duration;
            time -= Time.deltaTime;
        }
        
    }
}
