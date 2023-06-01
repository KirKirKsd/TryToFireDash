using System;
using Unity.VisualScripting;
using UnityEditor.UIElements;
using UnityEngine;

public class Light : MonoBehaviour {

	public float damage = 3f;
	public bool canChangeSize;
	public bool isBonfire;
	public UnityEngine.Light pointLight;

	private int waveEntry;
	[HideInInspector] public int waveAlive;
	private Waves wavesScript;

	public GameObject light1;
	public GameObject light2;
	public GameObject light3;

	public Color color1;
	public Color color2;
	public Color color3;

	private void Start() {
		wavesScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Waves>();
		waveEntry = wavesScript.currentWave;
	}

	private void Update() {
		if (wavesScript.currentWave == waveEntry + waveAlive + 1 && !isBonfire) {
			Destroy(transform.parent.gameObject);
		}
	}

	private void OnTriggerStay(Collider other) {
		if (other.CompareTag("Enemy")) {
			other.gameObject.GetComponent<Enemy>().TakeDamageOfLight(damage);
		}
	}

	public void SetSize(float size) {
		if (canChangeSize) {
			pointLight.range = size;
			switch (size) {
				case 7:
					waveAlive = 20;
					light1.SetActive(true);
					pointLight.color = color1;
					break;
				case 13:
					waveAlive = 10;
					light2.SetActive(true);
					pointLight.color = color2;
					break;
				case 19:
					waveAlive = 5;
					light3.SetActive(true);
					pointLight.color = color3;
					break;
			}
		}
	}
	
	public void AddDamage() {
		damage += Mathf.Round(damage * 0.5f);
	}

}
