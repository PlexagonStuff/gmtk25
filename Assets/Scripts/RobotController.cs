using UnityEngine;

public class RobotController : MonoBehaviour
{

    private RobotAttachment attachment;
    private Rigidbody2D rb;

    private float maxFuel;
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

        maxFuel = Fuel;
    }

    void FixedUpdate()
    {
        bool keyPressed = false;
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            attachment.RightArrow();
            keyPressed = true;
        }
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            attachment.LeftArrow();
            keyPressed = true;
        }
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            attachment.UpArrow();
            keyPressed = true;
        }
        if (!keyPressed) {
            attachment.Idle();
        }
    }
}
