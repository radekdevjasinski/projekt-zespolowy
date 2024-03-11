using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using static UnityEngine.RuleTile.TilingRuleOutput;
using Transform = UnityEngine.Transform;

public class PlayerControler : MonoBehaviour
{
    private Rigidbody2D rigidbody;
    private CustomPlayerActions playerActions;
    private InputAction moveAction;
    private InputAction lookAction;
    private ActiveItemBase activeItem;
    [SerializeField] private float speed = 10;
    void Awake()
    {
        this.playerActions=new CustomPlayerActions();
        rigidbody=GetComponent<Rigidbody2D>();
        activeItem = new FistITem();
    }

    void OnEnable()
    {
        playerActions.Player.Move.Enable();
        playerActions.Player.Look.Enable();
        playerActions.Player.Fire.Enable();
        moveAction = playerActions.Player.Move;
        lookAction = playerActions.Player.Look;

        playerActions.Player.Fire.performed += onAttack;

    }
    void OnDisable()
    {
        playerActions.Player.Move.Disable();
        playerActions.Player.Fire.Disable();
        playerActions.Player.Look.Disable();
    }

    void Update()
    {
        onLook();
    }



    void onMove()
    {
        Vector2 movement = moveAction.ReadValue<Vector2>();
        movement *= speed*100 * Time.deltaTime;
        rigidbody.AddForce(movement);
    }
    void onLook()
    {

      
        Vector3 mouseWorldPostiion =Camera.main.ScreenToWorldPoint(lookAction.ReadValue<Vector2>());

            Vector2 direction = transform.position - mouseWorldPostiion;
            float angle = Mathf.Atan2(direction.y, direction.x);
            float degrees = angle * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, degrees+90);
      }

    void onAttack(InputAction.CallbackContext context)
    {


        activeItem.use(this.gameObject);
        //Debug.Log("attack");
    
    }


    void OnTriggerEnter2D(Collider2D col)
    {
        //Debug.Log(col.gameObject.name + " : " + gameObject.name + " : " + Time.time);
        //spriteMove = -0.1f;
        Debug.Log("trigger");
        ItemToPick active = col.GetComponent<ItemToPick>();
        if (active != null)
            itemPick(active);
    }

    void itemPick(ItemToPick active)
    {
        if (activeItem == null)
        {
            ActiveItemBase _active = active.getActiveItemScript();

            if (_active == null)
                Debug.LogError("Empty Item");
            else
            {
                Debug.Log("Imem pice Item" + _active.name);
            }
            this.activeItem = _active;

            Destroy(active.gameObject);
        }
    }
    void FixedUpdate()
    {
        onMove();
       
    }

}
