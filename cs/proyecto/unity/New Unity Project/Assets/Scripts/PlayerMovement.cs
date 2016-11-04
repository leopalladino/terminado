using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
    public float moveSpeed;
    private float runMoveSpeed;
    public float diagonalMoveModifier;
    private float hadRun;
    private float currentMoveSpeed;

    private Rigidbody2D rbody;
    public Animator anim;
    public Vector2 lastMove;
    public bool ismoving;
    public bool isrunning;
    private static bool playerExists;
    private bool attacking;
    public float attackTime;
    private float attackTimeCounter;
    public GameObject weapon;

    private float waitToWaste;
    private float waitToWasteRun;

    public bool canMove;
    private bool mustRun;

	public bool canattack;
	public Animation anima;
    public bool hadAttacked;
    public float auxiliarfloat;

	private bool isshift;


	private PotionManager PM;
	private ShopManager Shop;
	private ShopHolder Shopi;

	public bool up = false;
	public bool down = true;
	public bool right = false;
	public bool left = false;

	public AudioSource ASstep;
	public AudioSource ASswing;
	public AudioSource ASbackground;

	public bool ayudamoises = false;
	void Awake() {
		Application.targetFrameRate = 200;
	}
    // Use this for initialization
    void Start() {
		ASstep = GameObject.Find ("StepSound").GetComponent<AudioSource>();
		ASswing = GameObject.Find ("SwingSound").GetComponent<AudioSource>();
		ASbackground = GameObject.Find ("BackgroundSound").GetComponent<AudioSource>();

		ASbackground.Play ();

		Shop = FindObjectOfType<ShopManager>();
		Shopi = FindObjectOfType<ShopHolder>();
		PM = FindObjectOfType<PotionManager>();
		canattack = true;
        rbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        weapon.GetComponent<SpriteRenderer>().sortingOrder = 3;
        
        hadRun = 2;
        canMove = true;
        hadAttacked = false;
        waitToWaste = 0.5f; //HAY QUE HACERLO SMOOT.
        auxiliarfloat = 0.7f; //este float es el tiempo entre cada golpe
        waitToWasteRun = 1.5f;
        anim.SetFloat("lastmove_x", 0f);
        anim.SetFloat("lastmove_y", -1f);



		runMoveSpeed = moveSpeed;
    }

    // Update is called once per frame
    void Update() {
		if (anim.GetBool("iswalking")) {
			if (!ayudamoises) {
				ASstep.Play ();
			//AS.PlayOneShot (AC);
				Debug.Log ("OST");
				ayudamoises = true;
			}
		} else {
			ASstep.Stop ();
			ayudamoises = false;
		}
		if (Application.loadedLevelName != "escena2") {
			OnRightClick ();
		}
			
        ismoving = false;
        isrunning = false;

        if (hadAttacked) {//CUANDOS SE ACABA LA STAMINA (3 GOLPES APROX) SE TIENE QUE ACTIVAR ESTO
            if (this.gameObject.GetComponent<Stamina>().playerCurrentStamina == 0)
            {

            }
            else
            {
				//if (weapon ==standar) {
					auxiliarfloat -= Time.deltaTime;
					if (auxiliarfloat < 0) {
						auxiliarfloat = 0.7f;
						hadAttacked = false;
					}
				//}
             
            }

        }

        if (!canMove)
        {
            ismoving = false;
            isrunning = false;
            rbody.velocity = Vector2.zero;
            return;
        }

        if (!attacking)
        {

            anim.SetFloat("lastmove_y", -1f);
            weapon.GetComponent<PolygonCollider2D>().enabled = false;

            if (Input.GetAxisRaw("Horizontal") > 0.5f || Input.GetAxisRaw("Horizontal") < -0.5f)
            {
                rbody.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * currentMoveSpeed, rbody.velocity.y);
                ismoving = true;
                lastMove = new Vector2(Input.GetAxisRaw("Horizontal"), 0f);
                weapon.GetComponent<SpriteRenderer>().sortingOrder = 3;
				if (lastMove.x == 1f) {
					right = true;
					up = false;
					down = false;
					left = false;
				} if (lastMove.x == -1f)  {
					right = false;
					up = false;
					down = false;
					left = true;
				}
            }
            if (Input.GetAxisRaw("Vertical") > 0.5f || Input.GetAxisRaw("Vertical") < -0.5f)
            {
                rbody.velocity = new Vector2(rbody.velocity.x, Input.GetAxisRaw("Vertical") * currentMoveSpeed);
                ismoving = true;
                lastMove = new Vector2(0f, Input.GetAxisRaw("Vertical"));
				if (lastMove.y == 1f) {
					right = false;
					up = true;
					down = false;
					left = false;
				} 
				if (lastMove.y == -1f) {
					right = false;
					up = false;
					down = true;
					left = false;
				}
				if (Input.GetAxisRaw("Vertical") == 1f && Input.GetAxisRaw("Horizontal") == -1f ||Input.GetAxisRaw("Vertical") == 1f && Input.GetAxisRaw("Horizontal") == 1f   )
                {
                    weapon.GetComponent<SpriteRenderer>().sortingOrder = 4;
                }

				if (Input.GetAxisRaw("Vertical") == -1f && Input.GetAxisRaw("Horizontal") == 1f || Input.GetAxisRaw("Vertical") == -1f && Input.GetAxisRaw("Horizontal") == -1f  )
                {
                    weapon.GetComponent<SpriteRenderer>().sortingOrder = 3;

                }
				if (Input.GetAxisRaw("Vertical") == 1f && Input.GetAxisRaw("Horizontal") == 0f  )
				{
					weapon.GetComponent<SpriteRenderer>().sortingOrder = 4;

				}

            }

            if (Input.GetAxisRaw("Horizontal") < 0.5f && Input.GetAxisRaw("Horizontal") > -0.5f)
            {
                rbody.velocity = new Vector2(0f, rbody.velocity.y);
            }
            if (Input.GetAxisRaw("Vertical") < 0.5f && Input.GetAxisRaw("Vertical") > -0.5f)
            {
                rbody.velocity = new Vector2(rbody.velocity.x, 0f);
            }

        }
    
        if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) > 0.5f && Mathf.Abs(Input.GetAxisRaw("Vertical")) > 0.5f)
        {
            currentMoveSpeed = moveSpeed * diagonalMoveModifier;
        }
        else
        {
            currentMoveSpeed = moveSpeed;
        }

        if (attackTimeCounter > 0)
        {
            attackTimeCounter -= Time.deltaTime;
        }
        if (attackTimeCounter <= 0)
        {
            attacking = false;
			anim.SetBool("attack", false);
        }
		if (!attacking && lastMove.y == -1f || !attacking && lastMove.y == 0f ) {
			weapon.GetComponent<SpriteRenderer>().sortingOrder = 3;
		}
	
		if (Application.loadedLevelName != "escena2") {
			
		}
		if (Shopi.isinzone == false) {
			if (Input.GetKeyDown (KeyCode.F)) {
				PM.PotionEffect ();
			}
		}

		if (Input.GetKeyDown(KeyCode.LeftAlt)) {
			if (left) {
				this.transform.position = new Vector3 (transform.position.x - 5,transform.position.y,transform.position.z);

			}
			if (right) {
				this.transform.position = new Vector3 (transform.position.x + 5,transform.position.y,transform.position.z);
			}
			if (up) {
				this.transform.position = new Vector3 (transform.position.x,transform.position.y + 5,transform.position.z);
			}
			if (down) {
				this.transform.position = new Vector3 (transform.position.x,transform.position.y - 5,transform.position.z);
			}
		}
        if (Input.GetMouseButtonDown(0))
        {
			
			if (!isrunning) {
				
			

            if (this.gameObject.GetComponent<Stamina>().playerCurrentStamina != 0)
            {
					if (!hadAttacked) {
						if (canattack) {
							ASswing.Play ();
							this.gameObject.GetComponent<Stamina> ().must = false;
							hadAttacked = true;
							this.gameObject.GetComponent<Stamina> ().WastingStamina (10);
							attackTimeCounter = attackTime;
							attacking = true;
							rbody.velocity = Vector2.zero;
							if (!weapon.GetComponent<Animator>().GetBool("StandardBow")) {
							anim.SetBool ("attack", true);
							weapon.GetComponent<PolygonCollider2D> ().enabled = true;
							if (lastMove.y == -1f || Input.GetAxisRaw ("Vertical") == 0f && Input.GetAxisRaw ("Horizontal") == 0f) {
								weapon.GetComponent<SpriteRenderer> ().sortingOrder = 4;
							}
						} else {
								FireArrow ();
							}
						}
					}
           }
		}
		}

        if (this.gameObject.GetComponent<Stamina>().playerCurrentStamina != 0)
        {
            if (Input.GetKey(KeyCode.LeftShift) && rbody.velocity != Vector2.zero )
        {
				if (!anim.GetBool("isArmorRed")) {
					this.gameObject.GetComponent<Stamina> ().must = false;
					if (hadRun != 1) {
                    
						isrunning = true;
						anim.SetBool ("isrunning", true);
						moveSpeed = moveSpeed + moveSpeed / 2;
						hadRun = 1;
						return;
					}

					if (hadRun == 1) {
					
						waitToWaste -= Time.deltaTime;
						if (waitToWaste < 0) {	
							this.gameObject.GetComponent<Stamina> ().WastingStamina (5);
							waitToWaste = 0.5f;

						}

					}
				}
            }
        }
        
        if (!Input.GetKey(KeyCode.LeftShift) || rbody.velocity == Vector2.zero || this.gameObject.GetComponent<Stamina>().playerCurrentStamina == 0)
        {
			anim.SetBool("isrunning", false);
            hadRun = 2;
            moveSpeed = runMoveSpeed;
            if (this.gameObject.GetComponent<Stamina>().playerCurrentStamina >= 0)
            {
                waitToWasteRun -= Time.deltaTime;
                if (waitToWasteRun < 0)
                {
                    this.gameObject.GetComponent<Stamina>().must = true;
                    waitToWasteRun = 1.5f;
                }
               
            }

         
        
        }
        



        anim.SetFloat("input_x", Input.GetAxisRaw("Horizontal"));
        anim.SetFloat("input_y", Input.GetAxisRaw("Vertical"));
        anim.SetBool("iswalking", ismoving);
        anim.SetFloat("lastmove_x", lastMove.x);
        anim.SetFloat("lastmove_y", lastMove.y);
        
	}

	public void OnRightClick()
	{
		if (Input.GetMouseButtonDown(1) && !hadAttacked && !isrunning && !attacking)
	{
		if (Shop.LevelOfSword == 2) {
			Shop.waterIsOn = true;
		}
			if (Shop.LevelOfSword == 3) {
				Shop.thunderIsON = true;
			}


	}
		if (Shop.LevelOfSword == 2 && Shop.waterIsOn && hadAttacked) {
			Shop.waterIsOn = false;
		}
		if (Shop.LevelOfSword == 3 && Shop.thunderIsON && hadAttacked) {
			Shop.thunderIsON = false;
		}
		if(anim.GetCurrentAnimatorStateInfo(0).IsName("WaterSword_2")){
			canattack = false;
		}
	}
	public void FireArrow(){
		Debug.Log ("Lol");
		GameObject obj = (GameObject)Instantiate(Resources.Load("Arrow"), transform.position, transform.rotation);
		if (up) {
			obj.GetComponent<Rigidbody2D>().velocity = new Vector2 (0f, 8f);
		}
		if (down) {
			obj.GetComponent<Rigidbody2D>().velocity = new Vector2 (0f, -8f);
		}
		if (left) {
			obj.GetComponent<Rigidbody2D>().velocity = new Vector2 (-8f, 0f);
		}
		if (right) {
			obj.GetComponent<Rigidbody2D>().velocity = new Vector2 (8f, 0f);
		}

	}

    }

