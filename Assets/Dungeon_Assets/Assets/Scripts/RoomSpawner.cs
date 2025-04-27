using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour {

	public int openingDirection;
	// 1 --> need bottom door
	// 2 --> need top door
	// 3 --> need left door
	// 4 --> need right door


	private RoomTemplates templates;
	private int rand;
	public bool spawned = false;

	public float waitTime = 4f;

	void Start(){
		Destroy(gameObject, waitTime);
		templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
		Invoke("Spawn", 0.1f);
	}

	private Vector3 SnapToGrid(Vector3 originalPos)
{
    float gridSize = 1f; // Change this if your tiles are 16px/32px/64px, etc.
    float snappedX = Mathf.Round(originalPos.x / gridSize) * gridSize;
    float snappedY = Mathf.Round(originalPos.y / gridSize) * gridSize;
    return new Vector3(snappedX, snappedY, originalPos.z);
}



	void Spawn(){
	if (spawned == false){
		if (openingDirection == 1){
			rand = Random.Range(0, templates.bottomRooms.Length);
			Instantiate(templates.bottomRooms[rand], SnapToGrid(transform.position), templates.bottomRooms[rand].transform.rotation);
		}
		else if (openingDirection == 2){
			rand = Random.Range(0, templates.topRooms.Length);
			Instantiate(templates.topRooms[rand], SnapToGrid(transform.position), templates.topRooms[rand].transform.rotation);
		}
		else if (openingDirection == 3){
			rand = Random.Range(0, templates.leftRooms.Length);
			Instantiate(templates.leftRooms[rand], SnapToGrid(transform.position), templates.leftRooms[rand].transform.rotation);
		}
		else if (openingDirection == 4){
			rand = Random.Range(0, templates.rightRooms.Length);
			Instantiate(templates.rightRooms[rand], SnapToGrid(transform.position), templates.rightRooms[rand].transform.rotation);
		}
		spawned = true;
	}
}


	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("SpawnPoint"))
		{
			RoomSpawner otherSpawner = other.GetComponent<RoomSpawner>();

			if (otherSpawner != null && otherSpawner.spawned == false && spawned == false)
			{
				Instantiate(templates.closedRoom, transform.position, Quaternion.identity);
				Destroy(gameObject);
			}
			spawned = true;
		}
	}

}
