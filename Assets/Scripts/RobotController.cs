using NUnit.Framework.Constraints;
using UnityEditor.U2D.Aseprite;
using UnityEngine;
using UnityEngine.SceneManagement;
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

    private float resetTimer = 0;
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
                fuelMat.SetFloat("_FuelLevel", _fuel / maxFuel);
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

    void Respawner()
    {
        SceneManager.LoadSceneAsync("RoundStart", LoadSceneMode.Additive);
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
                resetTimer += Time.fixedDeltaTime;
                if (resetTimer < 1.5)
                {
                    return;
                }
                int ExplosionRadius = 1;
                Vector2 pos = transform.position;
                LayerMask mask = LayerMask.GetMask("GroundLayer");
                Tilemap ground = FindAnyObjectByType<Tilemap>();//Get the tilemap

                for (int x = -ExplosionRadius; x < ExplosionRadius; x++)
                {
                    for (int y = -ExplosionRadius; y < ExplosionRadius; y++) //find the box
                    {
                        Vector3Int Tilepos = ground.WorldToCell(new Vector2(pos.x + x, pos.y + y));
                        ground.SetTile(Tilepos, null);
                    }
                }
                Respawner();
                Destroy(gameObject);

            }
        }
        else
        {
            Instantiate(corpse, transform.position, transform.rotation);
            Respawner();
            Destroy(gameObject);
        }

        if (!leftRightPressed)
        {
            attachment.NoLeftRight();
        }

        if (!keyPressed)
        {
            attachment.Idle();
            resetTimer = 0;
        }
    }
}
