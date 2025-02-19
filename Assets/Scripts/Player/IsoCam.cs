using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//-------------------------------------------------------------------------------------------------------------------------------------------------------------

// Creator: Vince & John
// Additions: Taliesin Millhouse & Nicholas Teese
// Description: Isometric camera for following the player and handling screen shaking as well as red screen flash for indicating damage

//-------------------------------------------------------------------------------------------------------------------------------------------------------------


public class IsoCam : MonoBehaviour
{
    public float m_camMoveSpeed = 10;
    public float m_camRotSpeed = 10;

    private Vector3 offset;

    private bool m_shake = false;
    private float m_shakeTimer = 0.0f;
    private float m_shakeDuration = 0.0f;
    public float m_shakeAmount = 0.0f;


    private bool m_flashingRed = false;
    private float m_flashTimer = 0.0f;
    private float m_flashDuration = 0.0f;
    private float m_intensity = 0.0f;

    private Plane[] m_frustrumPlanes = null;

    public Material m_flashRed;

    private Camera m_isometricCamera;

    public Camera m_uiCamera;

    public static IsoCam m_playerCamera;

    public Camera IsometricCamera { get { return m_isometricCamera; } }

    public Plane[] FrustrumPlanes { get { m_frustrumPlanes = GeometryUtility.CalculateFrustumPlanes(m_isometricCamera); return m_frustrumPlanes; } }

    private void Awake()
    {
        if (m_playerCamera == null)
        {
            m_playerCamera = this;
        }
        else if (m_playerCamera != null)
        {
            Destroy(gameObject);
        }

        m_isometricCamera = GetComponent<Camera>();
    }

    // Use this for initialization
    void Start()
    {
        offset = this.transform.position - Player.m_player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Player.m_player != null)
        {
           this.transform.position = Vector3.MoveTowards(this.transform.position, Player.m_player.transform.position + offset, m_camMoveSpeed);
           this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, Quaternion.LookRotation(Player.m_player.transform.position - this.transform.position), m_camRotSpeed);
        }

        if(m_shake)
        {
            m_shakeTimer += Time.deltaTime;

            Shake(m_shakeAmount, m_shakeDuration);

            if(m_shakeTimer >= m_shakeDuration)
            {
                m_shake = false;
                m_shakeTimer = 0.0f;
            }
        }

        if(m_flashingRed)
        {
            m_flashTimer += Time.deltaTime;
            FlashRed(m_flashDuration);

            if(m_flashDuration != 0.0f)
            {
                m_intensity = 1.0f - (m_flashTimer / m_flashDuration);
            }
                        
            if(m_flashTimer >= m_flashDuration)
            {
                m_intensity = 0.0f;
                m_flashTimer = 0.0f;
                m_flashingRed = false;
            }           
        }
        else
        {
            if (m_flashRed != null)
            {
                m_flashRed.SetFloat("_Intensity", 0.0f);
            }
        }
    }

    public void Shake(float a_shakeAmount, float a_shakeDuration)
    {
        m_shakeAmount = a_shakeAmount;
        m_shakeDuration = a_shakeDuration;
       
        m_shake = true;
        this.transform.position += Random.onUnitSphere * a_shakeAmount * Time.deltaTime;
    }

    public void FlashRed(float m_duration)
    {
        m_flashDuration = m_duration;

        m_flashingRed = true;

        if(m_flashRed != null)
        {
            m_flashRed.SetFloat("_Intensity", m_intensity);
        }
    }
}
