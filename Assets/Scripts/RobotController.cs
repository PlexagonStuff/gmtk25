using UnityEditor.U2D.Aseprite;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RobotController : MonoBehaviour
{

    private RobotAttachment attachment;
    private Rigidbody2D rb;

    private float maxFuel;
    [SerializeField]
    private Corpse corpse;

    [SerializeField]
    private float _fuel;
    public float Fuel
    {
        get
        {
            return _fuel;
        }
        set
        {
            if (_fuel != value)
            {
                _fuel = value;
                fuelMat.SetFloat("_FuelLevel", _fuel/maxFuel);
            } 
        }    
    }

    [SerializeField]
    private Material fuelMat;

    
    void Start()
    {
        attachment = GetComponentInChildren<RobotAttachment>();

        rb = GetComponent<Rigidbody2D>();
        attachment.rb = rb;
        rb.centerOfMass = new Vector2(-0.6f, -4f); 
        maxFuel = Fuel;
    }

    void FixedUpdate()
    {
        bool keyPressed = false;
        bool leftRightPressed = false;
        if (Fuel > 0)
        {
            if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            {
                attachment.RightArrow();
                keyPressed = true;
                leftRightPressed = true;
            }
            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            {
                attachment.LeftArrow();
                keyPressed = true;
                leftRightPressed = true;
            }
            if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
            {
                attachment.UpArrow();
                keyPressed = true;
            }
            if (Input.GetKey(KeyCode.R))
            {
                LayerMask mask = LayerMask.GetMask("GroundLayer");

                BoundsInt boombox = new BoundsInt(Vector3Int.FloorToInt(transform.position), new Vector3Int(2, 2, 0));
                Debug.Log(boombox);
                Tilemap tilemap = FindFirstObjectByType<Tilemap>();
                tilemap.SetTilesBlock(boombox, null);

            }
        }
        else
        {
            Instantiate(corpse, transform.position, transform.rotation);

            Destroy(gameObject);
        }

        if (!leftRightPressed)
        {
            attachment.NoLeftRight();
        }

        if (!keyPressed)
        {
            attachment.Idle();
        }
    }
}
