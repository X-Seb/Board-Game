using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.TopDownEngine;
using MoreMountains.Tools;

public class SetPlayerStat : MonoBehaviour
{
    [SerializeField] private bool m_setStatsOnStart;
    [Header("Reference Only: ")]
    [SerializeField] private GameObject m_player;
    [SerializeField] private Character m_character;
    [SerializeField] private Health m_health;
    [SerializeField] private HealthAutoRefill m_healthAutoRefill;
    [SerializeField] private MMHealthBar m_healthBar;
    [SerializeField] private CharacterMovement m_movement;
    [SerializeField] private CharacterDamageDash3D m_damageDash;
    [SerializeField] private CharacterHandleWeapon m_handleWeapon;

    private void Start()
    {
        //m_character = LevelManager.Instance.Players[0];
        //m_player = m_character.gameObject;
        m_player = GameObject.Find("Player");
        m_character = m_player.GetComponent<Character>();
        m_movement = m_player.GetComponent<CharacterMovement>();
        m_health = m_player.GetComponent<Health>();
        m_healthAutoRefill = m_player.GetComponent<HealthAutoRefill>();
        m_healthBar = m_player.GetComponent<MMHealthBar>();
        m_damageDash = m_player.GetComponent<CharacterDamageDash3D>();
        m_handleWeapon = m_player.GetComponent<CharacterHandleWeapon>();

        if (m_setStatsOnStart && PlayerPrefs.GetInt("CustomRun") == 1)
        {
            SetAllStatsFromPlayerPrefs();
            Debug.Log("SetAllStatsFromPlayerPrefs at Start");
        }
    }

    public void SetAllStatsFromPlayerPrefs()
    {
        SetHealthFromPlayerPrefs();
        SetHealthRefillAmountFromPlayerPrefs();
        SetMovementSpeedFromPlayerPrefs();
        SetDamageDashCooldownFromPlayerPrefs();
    }

    public void SetHealthFromPlayerPrefs()
    {
        int health = PlayerPrefs.GetInt("MaxHealth");
        float difference = health - 300.0f;
        float bar_x_scale;
        float bar_y_scale;
        if (difference > 0)
        {
            bar_x_scale = Mathf.Lerp(1.0f, 1.7f, difference / 700.0f);
            bar_y_scale = Mathf.Lerp(1.0f, 1.5f, difference / 700.0f);
        }
        else
        {
            bar_x_scale = Mathf.Lerp(0.6f, 1.0f, health / 300.0f);
            bar_y_scale = Mathf.Lerp(0.7f, 1.0f, health / 300.0f);
        }

        //m_healthBar.TargetProgressBar.gameObject.transform.localScale = new Vector3(bar_x_scale, bar_y_scale, 1);

        m_health.InitialHealth = health;
        m_health.MaximumHealth = health;
        m_health.SetHealth(health);
    }

    public void SetHealthRefillAmountFromPlayerPrefs()
    {
        m_healthAutoRefill.HealthPerBurst = PlayerPrefs.GetFloat("HealthRefillAmount");
    }

    public void SetMovementSpeedFromPlayerPrefs()
    {
        m_movement.MovementSpeed = PlayerPrefs.GetFloat("MovementSpeed");
        m_movement.WalkSpeed = PlayerPrefs.GetFloat("MovementSpeed");
        m_movement.ResetSpeed();
        m_movement.ForceInitialization();
    }

    public void SetDamageDashCooldownFromPlayerPrefs()
    {
        float cooldown = PlayerPrefs.GetFloat("DashCooldown");

        if (cooldown == 0.0f)
        {
            m_damageDash.Cooldown.Unlimited = true;
        }
        else
        {
            m_damageDash.Cooldown.RefillDuration = cooldown;
        }
        m_damageDash.Cooldown.Initialization();
    }

    public void DisableHandleWeapon()
    {
        m_handleWeapon.AbilityPermitted = false;
    }

    public void EnableHandleWeapon()
    {
        m_handleWeapon.AbilityPermitted = true;
    }
}