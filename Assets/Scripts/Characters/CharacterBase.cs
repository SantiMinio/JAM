using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBase : MonoBehaviour
{
    [SerializeField] Rigidbody rb = null;
    [SerializeField] float speed = 5;
    [SerializeField] CharacterAction action = null;
    [SerializeField] Animator anim = null;
    [SerializeField] WorldHittable hiteable = null;
    [SerializeField] AudioClip fallingSound = null;
    [SerializeField] AudioClip lavaSound = null;
    [SerializeField] AudioClip hitSound = null;
    [SerializeField] AudioClip[] footStep = null;

    float stepTimer;

    float xAxis;
    float yAxis;

    public Vector3 currentDir = new Vector3(0, 0, -1);

    private void Start()
    {
        hiteable.onDead += Dead;
        AudioManager.instance.GetSoundPool(fallingSound.name, AudioManager.SoundDimesion.TwoD, fallingSound);
        AudioManager.instance.GetSoundPool(lavaSound.name, AudioManager.SoundDimesion.TwoD, lavaSound);
        AudioManager.instance.GetSoundPool(hitSound.name, AudioManager.SoundDimesion.TwoD, hitSound);
        for (int i = 0; i < footStep.Length; i++)
        {
            AudioManager.instance.GetSoundPool(footStep[i].name, AudioManager.SoundDimesion.TwoD, footStep[i]);
        }
    }

    private void Update()
    {
        if (dead) return;

        Vector3 movement = new Vector3(Mathf.Abs(yAxis) == 1 ? xAxis / 1.5f * speed : xAxis * speed,
                                       rb.velocity.y,
                                       Mathf.Abs(xAxis) == 1 ? yAxis / 1.5f * speed : yAxis * speed);
        rb.velocity = movement;
        stepTimer += Time.deltaTime;
        movement.y = 0;
        if (movement != Vector3.zero)
        {
            currentDir = new Vector3(xAxis, 0, yAxis);
            anim.SetFloat("SetXDir", xAxis);
            anim.SetFloat("SetZDir", yAxis);
            if (stepTimer > 0.2f) { stepTimer = 0; AudioManager.instance.PlaySound(footStep[Random.Range(0, footStep.Length)].name); }
        }

        anim.SetFloat("x", xAxis);
        anim.SetFloat("z", yAxis);
    }

    public void MoveY(float y)
    {
        if (y < -0.3f) y = -1;
        else if (y > 0.3f) y = 1;
        else y = 0;
        yAxis = y;
    }

    public void MoveX(float x)
    {
        if (x < -0.3f) x = -1;
        else if (x > 0.3f) x = 1;
        else x = 0;
        xAxis = x;
    }

    public void ActionAbility(KeyEventButon eventKey)
    {
        switch (eventKey)
        {
            case KeyEventButon.KeyDown:
                action.StartAction();
                break;
            case KeyEventButon.Key:
                action.KeepAction();
                break;
            case KeyEventButon.KeyUp:
                action.EndAction();
                break;
            default:
                break;
        }
    }

    bool dead;

    void Dead()
    {
        var characters = Main.instance.GetCharacters();
        for (int i = 0; i < characters.Length; i++)
        {
            if (characters[i] != this) characters[i].Cry();
        }
        dead = true;
        if (Physics.Raycast(transform.position, -transform.up, 1, 1 << 9))
        {
            anim.SetBool("Fall", true);
            AudioManager.instance.PlaySound(fallingSound.name);
            AudioManager.instance.PlaySound(lavaSound.name);
        }
        else
        {
            anim.SetBool("Dead", true);
            AudioManager.instance.PlaySound(hitSound.name);
        }
        Main.instance.eventManager.TriggerEvent(GameEvents.CharactersSeparate);
    }

    public void DeadBySeparate()
    {
        anim.SetBool("Dead", true);
        dead = true;
    }

    public void Cry()
    {
        //inserte llanto
        anim.SetBool("Cry", true);
    }
}
