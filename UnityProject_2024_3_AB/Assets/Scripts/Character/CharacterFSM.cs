using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum CharacterState
{
    Idle,
    WalkingToShelf,
    PickingItem,
    WalkingToCounter,
    PlacingItem
}
public class Timer
{
    private float timeRemining;
    public void Set(float time)
    {
        timeRemining = time;
    }

    public void Update(float deltaTime)
    {
        if(timeRemining>0)
        {
            timeRemining -= deltaTime;
        }
    }

    public bool IsFinished()
    {
        return timeRemining <= 0;
    }
}

public class CharacterFSM : MonoBehaviour
{
    public CharacterState currentState;
    private Timer timer;

    public NavMeshAgent agent;
    public Transform target;

    public Transform Counter;
    public List<GameObject> targetPos = new List<GameObject>();

    private static int NextPriority = 0;
    private static readonly object priorityLock = new object();

    public bool isMoveDone = false;

    public List<GameObject> myBox = new List<GameObject>();

    public int boxesToPick = 5;
    private int boxesPicked = 0;

    void AssignPriority()
    {
        lock(priorityLock)
        {
            agent.avoidancePriority = NextPriority;
            NextPriority = (NextPriority + 1) % 100;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        timer = new Timer();
        currentState = CharacterState.Idle;
        agent = GetComponent<NavMeshAgent>();
        AssignPriority();
    }

    // Update is called once per frame
    void Update()
    {
        timer.Update(Time.deltaTime);

        if(!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            if(!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
            {
                isMoveDone = true;
            }
        }

        switch(currentState)
        {
            case CharacterState.Idle:
                Idle();
                break;
            case CharacterState.WalkingToShelf:
                WalkingToShelf();
                break;
            case CharacterState.PickingItem:
                PickingItem();
                break;
            case CharacterState.WalkingToCounter:
                WalkingToCounter();
                break;
            case CharacterState.PlacingItem:
                PlacingItem();
                break;

        }
    }

    void MoveToTarget()
    {
        isMoveDone = false;

        if(target != null)
        {
            agent.SetDestination(target.position);
        }
    }

    void ChangeState(CharacterState nextState, float waitTime = 0.0f)
    {
        currentState = nextState;
        timer.Set(waitTime);
    }

    void Idle()
    {
        if(timer.IsFinished())
        {
            target = targetPos[Random.Range(0, targetPos.Count)].transform;
            MoveToTarget();
            ChangeState(CharacterState.WalkingToShelf, 2.0f);
        }
    }

    void WalkingToShelf()
    {
        if (timer.IsFinished() && isMoveDone)
        {
            ChangeState(CharacterState.PickingItem, 2.0f);
            boxesPicked = 0;
        }
    }

    void PickingItem()
    {
        if (timer.IsFinished())
        {
            if(boxesPicked < boxesToPick)
            {
                GameObject box = GameObject.CreatePrimitive(PrimitiveType.Cube);
                myBox.Add(box);
                box.transform.parent = gameObject.transform;
                box.transform.localEulerAngles = Vector3.zero;
                box.transform.localPosition = new Vector3(0, boxesPicked * 2f, 0);

                boxesPicked++;
                timer.Set(0.5f);
            }
            else
            {
                target = Counter;
                MoveToTarget();
                ChangeState(CharacterState.WalkingToCounter, 2.0f);
            }
            
        }
    }

    void WalkingToCounter()
    {
        if (timer.IsFinished() && isMoveDone)
        {
            ChangeState(CharacterState.PlacingItem, 2.0f);
        }
    }

    void PlacingItem()
    {
        if (timer.IsFinished())
        {
            if(myBox.Count != 0)
            {
                myBox[0].transform.position = Counter.transform.position;
                myBox[0].transform.parent = Counter.transform;
                myBox.RemoveAt(0);
                timer.Set(0.1f);
            }
            else
            {
                ChangeState(CharacterState.Idle, 2.0f);
            }
            
        }
    }
}
