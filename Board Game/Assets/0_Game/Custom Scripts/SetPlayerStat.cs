using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.TopDownEngine;
using MoreMountains.Tools;

public class SetPlayerStat : MonoBehaviour
{
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
        m_character = LevelManager.Instance.Players[0];
        m_player = m_character.gameObject;
        m_movement = m_player.GetComponent<CharacterMovement>();
        m_health = m_player.GetComponent<Health>();
        m_healthAutoRefill = m_player.GetComponent<HealthAutoRefill>();
        m_healthBar = m_player.GetComponent<MMHealthBar>();
        m_damageDash = m_player.GetComponent<CharacterDamageDash3D>();
        m_handleWeapon = m_player.GetComponent<CharacterHandleWeapon>();
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
        float Bar_x_size;
        if (difference > 0)
        {
            Bar_x_size = Mathf.Lerp(1.5f, 2.5f, difference / 700.0f);
        }
        else
        {
            Bar_x_size = Mathf.Lerp(0.5f, 1.5f, health / 300.0f);
        }
        m_healthBar.Size.x = Bar_x_size;
        m_health.MaximumHealth = health;
        m_health.CurrentHealth = health;
    }

    public void SetHealthRefillAmountFromPlayerPrefs()
    {
        m_healthAutoRefill.HealthPerBurst = PlayerPrefs.GetFloat("HealthRefillAmount");
    }

    public void SetMovementSpeedFromPlayerPrefs()
    {
        m_movement.MovementSpeed = PlayerPrefs.GetFloat("MovementSpeed");
    }

    public void SetDamageDashCooldownFromPlayerPrefs()
    {
        m_damageDash.Cooldown.RefillDuration = PlayerPrefs.GetFloat("DashCooldown");
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