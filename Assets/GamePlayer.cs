using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayer : MonoBehaviour {

	public void Die() {
		Destroy(this.gameObject);
	}
}
