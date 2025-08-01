using NUnit.Framework.Constraints;
using System.Collections;
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

    public GameObject interactableObject;
    public bool isHoldingInteractable = false;
    public GameObject holdingLocation;
    public float rightThrowSpeed = 5.0f;
    public float upThrowSpeed = 5.0f;


    void Start()
    {
        attachment = GetComponentInChildren<RobotAttachment>();

        rb = GetComponent<Rigidbody2D>();
        attachment.rb = rb;
        rb.centerOfMass = new Vector2(-0.3f, -1.45f);
        maxFuel = Fuel;
        holdingLocation = GameObject.Find("holdingLocation");
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

        if (Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(PickUpOrThrow());
        }
    }

    private IEnumerator PickUpOrThrow()
    {
        if (interactableObject != null)
        {
            GameObject pickUpObject = interactableObject;
            Rigidbody2D interactableRB = pickUpObject.GetComponent<Rigidbody2D>();
            Collider2D col = pickUpObject.GetComponent<Collider2D>();
            ThowableObjectBehavior tob = pickUpObject.GetComponent<ThowableObjectBehavior>();
            if (!isHoldingInteractable)
            {
                //Disables collision so it can go through the player and makes it so it wont move once it is there
                col.isTrigger = true;
                interactableRB.gravityScale = 0;
                pickUpObject.transform.SetParent(holdingLocation.transform, true);
                tob.lerping = true;

                //Lerp Variables
                float t = 0.0f;
                float lerpSpeed = 0.005f;
                float minLerpDistance = 0.1f;
                Vector3 startLoc = pickUpObject.transform.position;

                //The Lerp
                while (Vector3.Magnitude(pickUpObject.transform.position - holdingLocation.transform.position) > minLerpDistance)
                {
                    t += lerpSpeed;
                    pickUpObject.transform.position = Vector3.Lerp(startLoc, holdingLocation.transform.position, t);
                    yield return null;
                }

                //couple touch-ups
                pickUpObject.transform.position = holdingLocation.transform.position;
                isHoldingInteractable = true;
                tob.lerping = false;
                interactableObject = pickUpObject;
            } else
            {
                Vector3 throwDirectionVector = transform.right * rightThrowSpeed + transform.up * upThrowSpeed;
                interactableRB.gravityScale = 1;
                col.isTrigger = false;
                interactableObject.transform.parent = null;
                interactableRB.AddForce(throwDirectionVector);
                isHoldingInteractable = false;
            }
        }
    }
}
