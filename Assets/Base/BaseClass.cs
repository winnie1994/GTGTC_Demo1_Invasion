﻿using UnityEngine;
using System.Collections;

public class BaseClass : MonoBehaviour {

	public void SetColor(Color color){
		SpriteRenderer renderer = gameObject.GetComponent<SpriteRenderer> ();
		if(renderer != null)renderer.color = color;	
	}

    public void SetTemporaryColor(Color color, float time) {
        SpriteRenderer renderer = gameObject.GetComponent<SpriteRenderer>();
        if (renderer != null)
        {
            Color old_color = renderer.color;
            renderer.color = color;
            StartCoroutine(DelaySetColor(old_color, time));
        }
    }

	public Color GetColor(){
		SpriteRenderer renderer = gameObject.GetComponent<SpriteRenderer> ();
		if(renderer!= null)return renderer.color;
		return Color.white;
	}

	public Color MixColor(Color color1, Color color2, float weight){
		Color new_color = Color.Lerp (color1, color2, weight);
		new_color.a = 1f;
		return new_color;
	}

	public void Rotate(float x){
		transform.Rotate (0f, 0f, x);
	}

	public void RotateTo(float x){
		transform.rotation = Quaternion.identity;
		transform.Rotate (0f, 0f, x);
	}

	public void MoveTo(float x){
		Vector2 new_position = transform.position;
		new_position.x = x;
		transform.position = new_position;
	}

	public void ShootBullet(Rigidbody2D bullet, float velocity)
	{
		Rigidbody2D instantiated_bullet = Instantiate(bullet,transform.position, transform.rotation) as Rigidbody2D;
		instantiated_bullet.GetComponent<Rigidbody2D>().velocity = transform.TransformDirection(new Vector2(0,velocity));
        Collider2D collider1 = instantiated_bullet.GetComponent<Collider2D>();
        Collider2D collider2 = gameObject.GetComponent<Collider2D>();
        Physics2D.IgnoreCollision(collider1,collider2,true);
        StartCoroutine(ReactivateCollision(0.8f,collider1,collider2));
    }

    public string GetTag(Collision2D collision) {
        return collision.gameObject.tag;
    }

	public Scoreboard_Controller GetScoreboard(){
		Scoreboard_Controller scoreboard = GameObject.Find("GameController_Object").GetComponent<Scoreboard_Controller>();
		return scoreboard;
	}

    IEnumerator ReactivateCollision(float waitTime,Collider2D collider1, Collider2D collider2)
    {
        yield return new WaitForSeconds(waitTime);
        Physics2D.IgnoreCollision(collider1,collider2,false);

    }

    IEnumerator DelaySetColor(Color color, float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        SetColor(color);
    }

}
