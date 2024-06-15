using Lightness.Graphic;
using System;

namespace LEContents {
	public class CharData {
		public int Count;

		public int EffectCount;

		public int X;

		public int Y;

		public int BaseX;

		public int BaseY;

		public int A = 255;

		public int MaxSpeed = 4;

		public int CoolingDown = 30;

		public int HP = 1;

		public int MaxHP = 1;

		public Texture T;

		public DNGST.DangoID ID = DNGST.DangoID.Pink;

		public bool Enemy;

		public bool Boss;

		public CharData(DNGST.DangoID DID, bool IsEnemy, int sX, int sY) {
			this.X = sX;
			this.Y = sY;
			this.BaseX = sX;
			this.BaseY = sY;
			this.Enemy = IsEnemy;
			this.Boss = false;
			this.ID = DID;
			if (!IsEnemy) {
				if (DID == DNGST.DangoID.Pink) {
					this.T = DNGST.DangoR[(int)DID];
				} else {
					this.T = DNGST.DangoR[(int)DID];
				}
			} else if (DID == DNGST.DangoID.Pink) {
				this.T = DNGST.DangoL[(int)DID];
			} else {
				this.T = DNGST.DangoL[(int)DID];
			}
			this.MaxSpeed = DNGST.DangoMaxSpeeds[(int)DID];
			this.CoolingDown = DNGST.DangoCoolingDowns[(int)DID];
			this.HP = DNGST.DangoEnemyLifes[(int)DID];
		}
	}
}
