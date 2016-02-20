using System;

/*
 * EnemyVO Class - Data value for Enemy info
 *
 * 
 */

[Serializable]
public class EnemyVO {

	public string enemyType = "";
	public int level = 0;
	public int stage = 0;

	public EnemyVO(string enemyType, int level, int stage){
		this.enemyType = enemyType;
		this.level = level;
		this.stage = stage;
	}
}
