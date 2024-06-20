using Lightness.Graphic;

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
			X = sX;
			Y = sY;
			BaseX = sX;
			BaseY = sY;
			Enemy = IsEnemy;
			Boss = false;
			ID = DID;
			if(!IsEnemy) {
				if(DID == DNGST.DangoID.Pink) {
					T = DNGST.DangoR[(int)DID];
				} else {
					T = DNGST.DangoR[(int)DID];
				}
			} else if(DID == DNGST.DangoID.Pink) {
				T = DNGST.DangoL[(int)DID];
			} else {
				T = DNGST.DangoL[(int)DID];
			}
			MaxSpeed = DNGST.DangoMaxSpeeds[(int)DID];
			CoolingDown = DNGST.DangoCoolingDowns[(int)DID];
			HP = DNGST.DangoEnemyLifes[(int)DID];
		}
	}
}
