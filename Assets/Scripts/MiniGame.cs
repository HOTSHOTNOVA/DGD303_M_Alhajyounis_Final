using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;
using Signals;


public class MiniGame : MonoBehaviour
{
    #region Self Variables

    #region Serialized Variables

    [SerializeField] private PlayerManager manager;
    [SerializeField] private new Collider collider;
    [SerializeField] private new Rigidbody rigidbody;
    #endregion

    #region Public Variables
    public float speed = 30f;
    public float duration = 5f;
    #endregion

    #region Private Variables
    private float MiniGameCollected;
    private bool triggered = false;
    private float timer = 0f;
    #endregion

    #endregion

    private void Update()
    {
        if (triggered)
        {
            timer += Time.deltaTime;
            transform.position += transform.forward * speed * Time.deltaTime;
            if (timer >= duration)
            {
                triggered = false;
                timer = 0f;
                manager.ForceCommand.Execute();
                CoreGameSignals.Instance.onMiniGameStageAreaEntered?.Invoke();
                InputSignals.Instance.onDisableInput?.Invoke();
                CoreGameSignals.Instance.onMiniGameStageAreaSuccessful?.Invoke(manager.StageID);
                InputSignals.Instance.onEnableInput?.Invoke();
                CoreGameSignals.Instance.onLevelSuccessful?.Invoke();
                manager.StageID++;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SpeedChanger"))
        {
            triggered = true;
        }
        else if (other.CompareTag("MGC"))
        {
            MiniGameCollected += 10;
        }
    }
}

    
