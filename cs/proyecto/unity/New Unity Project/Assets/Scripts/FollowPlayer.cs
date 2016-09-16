using UnityEngine;
using System.Collections;

public class FollowPlayer : MonoBehaviour
{
	public Animator anim;

	public float Timer;
	public float TimerAux;
	public float ElseTimer;
	public float TimeraMovement;
	public float TimeraMovementAux;

	public float TimerForAttack;

	public Transform target;
	public float speed = 8f;
	public float minDistance = 2f;
	private float range;
	public bool canfollow = true;

	public bool canmovetowards;
	public bool hadattacked;

	private bool cangetdamage;
	public bool isred;
	private GameObject mob;

	public bool canAttack;
	private Transform savePosition;

	public Transform uno;
	public Transform dos;
	private bool hadturn;
    public bool isTurned;
	public bool isTurnedAux;
	public bool isTurnedAuxAux;
	public bool canAUX = false;
	public bool ischargin;
	public bool cannotAttack = false;
	public bool desesperateIdleRestingBool = false;
	void Start()
	{
		ischargin = false;
        isTurned = false;
		isred = false;
		canAttack = true;
		cangetdamage = this.gameObject.GetComponent<SlimeController> ().gettingAttacked;
		TimeraMovement = 0.5f;
		ElseTimer = 1f;
		Timer = 0.5f;
		TimerAux = 1f;
		TimerForAttack = 1f;
		TimeraMovementAux = 2f;
		target = GameObject.Find ("Player").GetComponent<Transform> ();
		anim = GetComponent<Animator>();
		mob = this.gameObject;
		canmovetowards = true;
		hadattacked = false;
		isTurnedAuxAux = false;
	}

	void Update()
	{
		if (this.gameObject.name == "Pig") {
			anim.SetBool ("ispig", true);
			if (cannotAttack && !isred && !canAttack || desesperateIdleRestingBool) {
				anim.SetBool ("pigLeft", false);
				anim.SetBool ("pigRight", false);
				anim.SetBool ("pigUp", false);
				anim.SetBool ("pigDown", false);
				anim.SetBool ("IdleDown", false);
				anim.SetBool ("IdleUp", false);
				anim.SetBool ("IdleLeftRight", false);
			}
		}
		this.gameObject.GetComponent<SlimeController> ().canmove = false;
		range = Vector2.Distance (transform.position, target.position);
		if (this.gameObject.transform.position.x < target.position.x + 3 && this.gameObject.transform.position.x > target.position.x - 3) {
			isTurnedAuxAux = true;
		
			if (this.gameObject.transform.position.y < target.position.y - 3) {
				isTurnedAux = true;
				if (cannotAttack && !isred && !canAttack || desesperateIdleRestingBool) {
					anim.SetBool ("pigLeft", false);
					anim.SetBool ("pigRight", false);
					anim.SetBool ("pigUp", true);
					anim.SetBool ("pigDown", false);
					anim.SetBool ("IdleDown", false);
					anim.SetBool ("IdleUp", true);
					anim.SetBool ("IdleLeftRight", false);
				} else {
					anim.SetBool ("pigLeft", false);
					anim.SetBool ("pigRight", false);
					anim.SetBool ("pigUp", true);
					anim.SetBool ("pigDown", false);
					anim.SetBool ("IdleDown", false);
					anim.SetBool ("IdleUp", false);
					anim.SetBool ("IdleLeftRight", false);
				}
			}
			if (this.gameObject.transform.position.y > target.position.y + 3) {

				if (cannotAttack && !isred && !canAttack || desesperateIdleRestingBool) {
					anim.SetBool ("pigLeft", false);
					anim.SetBool ("pigRight", false);
					anim.SetBool ("pigUp", false);
					anim.SetBool ("pigDown", true);
					anim.SetBool ("IdleDown", true);
					anim.SetBool ("IdleUp", false);
					anim.SetBool ("IdleLeftRight", false);
				} else {	
					anim.SetBool ("pigLeft", false);
					anim.SetBool ("pigRight", false);
					anim.SetBool ("pigUp", false);
					anim.SetBool ("pigDown", true);
					anim.SetBool ("IdleDown", false);
					anim.SetBool ("IdleUp", false);
					anim.SetBool ("IdleLeftRight", false);
				}
				isTurnedAux = false;
			}
		} else {
			if (transform.position.x < target.position.x) {
				uno.position = transform.position;
				dos.position = target.position;
				isTurned = true;
				if (cannotAttack && !isred && !canAttack || desesperateIdleRestingBool) {
					anim.SetBool ("pigLeft", false);
					anim.SetBool ("pigRight", true);
					anim.SetBool ("pigUp", false);
					anim.SetBool ("pigDown", false);
					anim.SetBool ("IdleDown", false);
					anim.SetBool ("IdleUp", false);
					anim.SetBool ("IdleLeftRight", true);
				} else {
					anim.SetBool ("pigLeft", false);
					anim.SetBool ("pigRight", true);
					anim.SetBool ("pigUp", false);
					anim.SetBool ("pigDown", false);
					anim.SetBool ("IdleDown", false);
					anim.SetBool ("IdleUp", false);
					anim.SetBool ("IdleLeftRight", false);
				}
				var lookPos = target.position - transform.position;
				lookPos.y = 0;
				var rotation = Quaternion.LookRotation (lookPos);
				transform.rotation = Quaternion.Slerp (transform.rotation, rotation, Time.deltaTime * 80);
			} else {
				uno = transform;
				dos = target;
				isTurned = false;
				if (cannotAttack && !isred && !canAttack || desesperateIdleRestingBool) {
					anim.SetBool ("pigLeft", true);
					anim.SetBool ("pigRight", false);
					anim.SetBool ("pigUp", false);
					anim.SetBool ("pigDown", false);
					anim.SetBool ("IdleDown", false);
					anim.SetBool ("IdleUp", false);
					anim.SetBool ("IdleLeftRight", true);
				} else {
					anim.SetBool ("pigLeft", true);
					anim.SetBool ("pigRight", false);
					anim.SetBool ("pigUp", false);
					anim.SetBool ("pigDown", false);
					anim.SetBool ("IdleDown", false);
					anim.SetBool ("IdleUp", false);
					anim.SetBool ("IdleLeftRight", false);
				}
				var lookPos = transform.position - target.position;
				lookPos.y = 0;
				var rotation = Quaternion.LookRotation (lookPos);
				transform.rotation = Quaternion.Slerp (transform.rotation, rotation, Time.deltaTime * 80);
			}
		}
		if (canmovetowards) {
			if (canfollow) { 	
				if (range > minDistance) {
					canAUX = false;
					canAttack = false;
					TimeraMovement -= Time.deltaTime;
					if (TimeraMovement < 0) {
						TimeraMovementAux -= Time.deltaTime;
						cannotAttack = true;
						if (TimeraMovementAux > 0) {
							cannotAttack = false;
							desesperateIdleRestingBool = false;
							anim.SetBool ("isattacking", false);		
							isred = false;
							transform.position = Vector2.MoveTowards (transform.position, target.position, (speed) * Time.deltaTime);
							savePosition = this.transform;
					
						} else {
							TimeraMovementAux = 2f;
							TimeraMovement = 0.5f;
						}
					}
				}
				{
		
				}
				if (range < minDistance) {
					if (!canAttack) {
						cannotAttack = true;
					} else {
						cannotAttack = false;
						desesperateIdleRestingBool = false;
					}
					TimeraMovement = 0.5f;
					isred = false;
					if (!canAUX) {
						minDistance = 20;
					}
					//
					Timer -= Time.deltaTime;
					if (Timer < 0f) {
						canAttack = true;
						anim.SetBool ("isattacking", true);
						isred = true;
						transform.position = Vector2.MoveTowards (transform.position, target.position, (speed + (speed % 20)) * Time.deltaTime);
						TimerAux -= Time.deltaTime;
						if (TimerAux < 0f) {
							ischargin = false;
							canAttack = false;
							isred = false;
							anim.SetBool ("isattacking", false);
							transform.position = Vector2.MoveTowards (transform.position, target.position, speed * Time.deltaTime);
							TimerAux = 1f;
							Timer = 0.5f;
							canAUX = true;
							minDistance = 8; //

						}
					}



					//transform.position = Vector2.MoveTowards(savePosition, target.position, (speed*2) * Time.deltaTime);
				} else {
					TimerAux = 1f;
					Timer = 0.5f;
					anim.SetBool ("isattacking", false);
					if (!canfollow) {

						ElseTimer -= Time.deltaTime;
						if (ElseTimer < 0) {
						
							ElseTimer = 1f;
							canfollow = true;
						}

					}

					//else
					//{
					//	this.gameObject.GetComponent<SlimeController>().canmove = true;
					//}
		

				}
			} else {
				//	transform.position = Vector2.MoveTowards(transform.position, savePosition.position, 1);
				TimerForAttack -= Time.deltaTime;
				Debug.Log ("lolazo");

				if (TimerForAttack < 0f) {
					canmovetowards = true;
					TimerForAttack = 1f;
				}
			}
		} else
			Debug.Log ("no lolazo");
		if (cannotAttack) {
		TimerForAttack -= Time.deltaTime;
			desesperateIdleRestingBool = true;
		if (TimerForAttack < 0f) {
			desesperateIdleRestingBool = false;
			canmovetowards = true;
			TimerForAttack = 1f;
		}
	}
		}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.name == "Right" || other.name == "Left" || other.name == "Up" || other.name == "Down") {//el daño se rec	ibe cuando es en el pollyngon
			canmovetowards = false;
			TimerAux = 1f;
			anim.SetBool("isattacking", false);
			TimeraMovement = 0.5f;
			ElseTimer = 1f;
			Timer = 0.5f;
			TimerAux = 1f;
			TimerForAttack = 1f;
			TimeraMovementAux = 2f;
			cannotAttack = true;
		}
		if (other.name == "sword") {

		
					//anim.GetBool("isattacking")) 
					//ACA TENGO QUE AGREGAR LOGICA DE QUE SE PUEDE ATACAR SI ESTÁ REPELIENDO, O TIENE QUE SER UN STAT. 
			canmovetowards = false;
			cannotAttack = true;
			TimerAux = 1f;
			anim.SetBool("isattacking", false);
			TimeraMovement = 0.5f;
			ElseTimer = 1f;
			Timer = 0.5f;
			TimerAux = 1f;
			TimerForAttack = 1f;
			TimeraMovementAux = 2f;
			
		}
	}
}

//TimeraMovement = Es el timer de cuanto tiempo está el mob en reposo cuando el rango es mayor a la distancia minima para atacar.
//TimeraMovementAux = Es el timer del movimiento hacia el personaje cuando el reango es mayor a la distancia minima para atacar.

//Timer = El tiempo en el que Slime está en reposo antes o después de atacar.
//TimerAux = El tiempo que está el Slime en estampida. 

//TimerForAttack = Después de atacar se espera 1f para volver a evaluar si se debe atacar o desplazarse hacia el jugador normalmente.