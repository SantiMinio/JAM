using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour, IPause
{
    protected bool turnOn;

    [SerializeField] protected DamageReceiver dmgReceiver;
    [SerializeField] int maxLife;

    protected bool IsDead;

    private void Start()
    {
        Initialize();
    }

    private void Update()
    {
        if (paused || !turnOn || endGame) return;

        OnUpdate();
    }

    void Initialize()
    {
        dmgReceiver.OnTakeDamageEv += TakeDamage;
        dmgReceiver.OnDeathEv += Death;
        dmgReceiver.OnRefreshLifeEv += OnRefreshLife;
        dmgReceiver.OnInvulnerableEv += OnInvulerable;
        dmgReceiver.IsInvulnerable = OnIsInvulnerable;

        dmgReceiver.Configure(new LifeSystem(maxLife));
        OnInitialize();
        TurnOn();
    }

    protected void TurnOn()
    {
        if (turnOn) return;
        turnOn = true;
        dmgReceiver.ResetComponent();
        PauseManager.instance.AddToPause(this);
        Main.instance.eventManager.SubscribeToEvent(GameEvents.CharactersSeparate, OnEndGame);
        Main.instance.eventManager.SubscribeToEvent(GameEvents.HotelArrive, OnEndGame);
        OnTurnOn();
    }

    protected void TurnOff()
    {
        if (!turnOn) return;
        turnOn = false;
        IsDead = false;
        PauseManager.instance.RemoveToPause(this);
        OnTurnOff();
    }

    protected bool endGame;

    protected virtual void OnEndGame()
    {
        endGame = true;
    }

    protected abstract void OnTurnOff();
    protected abstract void OnTurnOn();

    protected abstract void OnInitialize();

    protected abstract void OnUpdate();

    protected bool paused;

    public void Pause()
    {
        paused = true;
        OnPause();
    }

    public void Resume()
    {
        paused = false;
        OnResume();
    }

    protected virtual void OnPause()
    {
    }

    protected virtual void OnResume()
    {
    }


    #region Damage Things
    protected abstract void TakeDamage(Damager dmg);

    void Death(Damager dmg)
    {
        IsDead = true;
        OnDeath();
    }

    protected abstract void OnDeath();

    protected virtual void OnRefreshLife(int currentLife, int maxLife)
    {

    }

    protected virtual void OnInvulerable()
    {

    }

    protected virtual bool OnIsInvulnerable(Damager dmg)
    {
        return false;
    }

    #endregion
}
