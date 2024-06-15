using Lightness.Graphic;
using System;

namespace LEContents {
	public class BulletData {
		public int Count;

		public int EffectCount;

		public int X;

		public int Y;

		public int BaseX;

		public int BaseY;

		public int A = 255;

		public Texture T;

		public DNGST.DangoID ID = DNGST.DangoID.Pink;

		public DNGST.EffectID EID;

		public bool Enemy;

		public bool Effect;

		public bool Special;

		public BulletData(DNGST.DangoID DID, bool IsEnemy, int sX, int sY) {
			this.X = sX;
			this.Y = sY;
			this.BaseX = sX;
			this.BaseY = sY;
			this.Enemy = IsEnemy;
			this.ID = DID;
			if (!IsEnemy) {
				if (DID == DNGST.DangoID.Pink) {
					this.T = DNGST.DangoBulletR[(int)DID];
					return;
				}
				this.T = DNGST.DangoBulletR[(int)DID];
				return;
			} else {
				if (DID == DNGST.DangoID.Pink) {
					this.T = DNGST.DangoBulletL[(int)DID];
					return;
				}
				this.T = DNGST.DangoBulletL[(int)DID];
				return;
			}
		}
	}
}
