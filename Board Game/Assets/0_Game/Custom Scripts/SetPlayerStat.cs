using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.TopDownEngine;

public class SetPlayerStat : MonoBehaviour
{
    [Header("Reference Only: ")]
    [SerializeField] private GameObject m_player;
    [SerializeField] private Character m_character;
    [SerializeField] private Health m_health;
    [SerializeField] private HealthAutoRefill m_healthAutoRefill;
    [SerializeField] private CharacterMovement m_movement;
    [SerializeField] private CharacterDamageDash3D m_damageDash;

    private void Start()
    {
        m_character = LevelManager.Instance.Players[0];
        m_player = m_character.gameObject;
        m_movement = m_player.GetComponent<CharacterMovement>();
        m_health = m_player.GetComponent<Health>();
        m_healthAutoRefill = m_player.GetComponent<HealthAutoRefill>();
        m_damageDash = m_player.GetComponent<CharacterDamageDash3D>();
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
}