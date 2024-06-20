using Lightness.Graphic;

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
			X = sX;
			Y = sY;
			BaseX = sX;
			BaseY = sY;
			Enemy = IsEnemy;
			ID = DID;
			if(!IsEnemy) {
				if(DID == DNGST.DangoID.Pink) {
					T = DNGST.DangoBulletR[(int)DID];
				} else {
					T = DNGST.DangoBulletR[(int)DID];
				}
			} else if(DID == DNGST.DangoID.Pink) {
				T = DNGST.DangoBulletL[(int)DID];
			} else {
				T = DNGST.DangoBulletL[(int)DID];
			}
		}
	}
}
