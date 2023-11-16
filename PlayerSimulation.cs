using System;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IKitchenObjectParent
{
    public static List<Player> Instances { get; private set; } = new List<Player>();

    public event EventHandler OnPickedSomething;
    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;

    public class OnSelectedCounterChangedEventArgs : EventArgs
    {
        public BaseCounter selectedCounter;
    }

    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private LayerMask countersLayerMask;
    [SerializeField] private Transform kitchenObjectHoldPoint;

    private bool isWalking;
    private Vector3 lastInteractDir;
    private BaseCounter selectedCounter;
    private KitchenObject kitchenObject;

    private enum PlayerState
    {
        Idle,
        Moving,
        Interacting,
        Delivering
    }

    private PlayerState currentState;

    private void Awake()
    {
        Instances.Add(this);
    }

    private void Start()
    {
        // Iterate through each player instance and initiate the simulation
        foreach (Player player in Instances)
        {
            player.SimulateActions();
        }
    }

    private void SimulateActions()
    {
        // Simulate receiving orders
        SimulateReceiveOrders();

        // Simulate preparing dish
        SimulatePrepareDish();

        // Simulate delivering dish
        SimulateDeliverDish();
    }

    private void SimulateReceiveOrders()
    {
        // Change state to Moving
        currentState = PlayerState.Moving;

        // Simulate moving to a counter to receive orders
        // Assume there's a method to move the player to a specific position
        MovePlayerTo(new Vector3(UnityEngine.Random.Range(-5f, 5f), 0, UnityEngine.Random.Range(-5f, 5f)));

        // Change state to Idle after reaching the counter
        currentState = PlayerState.Idle;
    }

    private void SimulatePrepareDish()
    {
        // Change state to Interacting
        currentState = PlayerState.Interacting;

        // Simulate interacting with a counter to prepare a dish
        // Assume there's a method to interact with the counter
        InteractWithCounter();

        // Simulate time taken to prepare a dish
        // You can use WaitForSeconds or a timer here

        // Change state to Idle after preparing the dish
        currentState = PlayerState.Idle;
    }

    private void SimulateDeliverDish()
    {
        // Change state to Delivering
        currentState = PlayerState.Delivering;

        // Simulate moving to deliver the dish
        // Assume there's a method to move the player to a delivery point
        MovePlayerTo(new Vector3(UnityEngine.Random.Range(-5f, 5f), 0, UnityEngine.Random.Range(-5f, 5f)));

        // Change state to Idle after reaching the delivery point
        currentState = PlayerState.Idle;
    }

    private void Update()
    {
        // Handle different actions based on the player's state
        switch (currentState)
        {
            case PlayerState.Moving:
                // Handle movement logic
                // ...

                break;

            case PlayerState.Interacting:
                // Handle interaction logic
                // ...

                break;

            case PlayerState.Delivering:
                // Handle delivering logic
                // ...

                break;

            case PlayerState.Idle:
            default:
                // Default to idle state
                // ...

                break;
        }
    }

    // Add methods for moving and interacting based on the player's state
    private void MovePlayerTo(Vector3 destination)
    {
        // Implement movement logic here
        // ...
    }

    private void InteractWithCounter()
    {
        // Implement interaction logic here
        // ...
    }

    // Other methods...

    private void SetSelectedCounter(BaseCounter selectedCounter)
    {
        this.selectedCounter = selectedCounter;

        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs
        {
            selectedCounter = selectedCounter
        });
    }

    public Transform GetKitchenObjectFollowTransform()
    {
        return kitchenObjectHoldPoint;
    }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;

        if (kitchenObject != null)
        {
            OnPickedSomething?.Invoke(this, EventArgs.Empty);
        }
    }

    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }

    public void ClearKitchenObject()
    {
        kitchenObject = null;
    }

    public bool HasKitchenObject()
    {
        return kitchenObject != null;
    }
}
