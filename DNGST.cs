using Lightness.Core;
using Lightness.Framework;
using Lightness.Graphic;
using Lightness.IO;
using Lightness.Media;
using Lightness.Resources;
using System;

namespace LEContents {
	public static class DNGST {
		public enum GameMode {
			RANDOM_ALL,
			RANDOM_SAME,
			RANDOM_BURN,
			RANDOM_FAST
		}

		public enum DiffLv {
			EASY,
			STANDARD,
			HARD,
			SPECIAL
		}

		public enum DangoID {
			None,
			Pink,
			Blue,
			Green,
			Black,
			Yellow,
			Burn
		}

		public enum EffectID {
			None,
			Hit,
			HitEx
		}

		public static Random rnd = new Random();

		public static Texture TStage = null;

		public static Texture TDiff = null;

		public static Texture TScore = null;

		public static Texture TLife = null;

		public static Texture TSP = null;

		public static string Stage = "";

		public static string DiffS = "";

		public static string StageId = "";

		public static int Diff = 0;

		public static int Score = 0;

		public static int OldScore = 0;

		public static int Life = 0;

		public static int SP = 0;

		public static Texture[] LoadingBG = new Texture[4];

		public static int LoadingState = 0;

		public static bool Loading = true;

		public static bool First = true;

		public static bool GameEnd = false;

		public static bool BossMode = false;

		public static bool FadeOut = false;

		public static SE BulletSES = new SE();

		public static SE BulletSEE = new SE();

		public static BGM GameBGM = new BGM();

		public static BGM BossBGM = new BGM();

		public static SE HitSE = new SE();

		public static SE BurnSE = new SE();

		public static SE KillMeSE = new SE();

		public static SE SpecialSE = new SE();

		public static SE ErrorSE = new SE();

		public static SE ClearSE = new SE();

		public static SE GameOverSE = new SE();

		public static SE AddSE = new SE();

		public static SE ChangeSE = new SE();

		public static CharData[] Char = null;

		public static BulletData[] Bullet = null;

		public static int AddWaiter = 0;

		public static int DangoTypes = 7;

		public static int[] DangoMaxSpeeds = new int[]
		{
			0,
			4,
			10,
			6,
			6,
			4,
			1
		};

		public static int[] DangoBulletSpeeds = new int[]
		{
			0,
			10,
			16,
			12,
			14,
			8,
			2
		};

		public static int[] DangoCoolingDowns = new int[]
		{
			0,
			10,
			10,
			10,
			10,
			10,
			10
		};

		public static int[] DangoEnemyMaxSpeeds = new int[]
		{
			0,
			2,
			4,
			2,
			2,
			2,
			1
		};

		public static int[] DangoEnemyBulletSpeeds = new int[]
		{
			0,
			4,
			8,
			6,
			6,
			4,
			2
		};

		public static int[] DangoEnemyCoolingDowns = new int[]
		{
			0,
			80,
			60,
			80,
			80,
			60,
			80
		};

		public static int[] DangoEnemyLifes = new int[]
		{
			0,
			1,
			1,
			1,
			2,
			1,
			3
		};

		public static int[] DangoEnemyScores = new int[]
		{
			0,
			100,
			500,
			200,
			1000,
			250,
			2000
		};

		public static Texture[] DangoL = new Texture[DNGST.DangoTypes];

		public static Texture[] DangoR = new Texture[DNGST.DangoTypes];

		public static Texture[] DangoBulletL = new Texture[DNGST.DangoTypes];

		public static Texture[] DangoBulletR = new Texture[DNGST.DangoTypes];

		public static Texture DangoLargeL = null;

		public static Texture DangoLargeLBurn = null;

		public static Texture DangoLargeR = null;

		public static Texture BG = null;

		public static Texture DynBG = null;

		public static Texture NoTexture = Texture.CreateFromText(" ");

		public static Texture[] Effects = new Texture[16];

		public static Paint PWhite = new Paint(255, 255, 255, 255);

		public static Texture White = DNGST.PWhite.ToTexture();

		public static int DynBGSpeed = 0;

		public static int DynBGPos = 0;

		public static int DynBGA = 0;

		public static int CharHalfSize = 32;

		public static int BulletHalfSize = 12;

		public static int AddTimeRndMax = 0;

		public static int AddTimeStatic = 0;

		public static int BossAddTimeRndMax = 0;

		public static int BossAddTimeStatic = 0;

		public static int BossMaxWTypes = 0;

		public static int FramesCount = 0;

		public static int AddWaitCount = 0;

		public static int Timer = 0;

		public static int TempW = 0;

		public static int TempM = 0;

		public static int Temp = 0;

		public static bool Reloading = false;

		public static bool NoDamage = false;

		public static bool Specialing = false;

		public static bool StageCleared = false;

		public static VirtualIOEx VIOEx = new VirtualIOEx();

		public static string DangoLargeRFile = "";

		public static string DangoLargeLFile = "";

		public static ContentReturn Initialize(DNGST.DangoID DID) {
			DNGST.Char = new CharData[128];
			DNGST.Bullet = new BulletData[512];
			switch (DID) {
				case DNGST.DangoID.Pink:
					DNGST.DangoLargeRFile = "DngR/DangoPinkEx.png";
					break;
				case DNGST.DangoID.Blue:
					DNGST.DangoLargeRFile = "DngR/DangoWaterEx.png";
					break;
				case DNGST.DangoID.Green:
					DNGST.DangoLargeRFile = "DngR/DangoGreenEx.png";
					break;
				case DNGST.DangoID.Black:
					DNGST.DangoLargeRFile = "DngR/DangoBlackEx.png";
					break;
				case DNGST.DangoID.Yellow:
					DNGST.DangoLargeRFile = "DngR/DangoYellowEx.png";
					break;
				case DNGST.DangoID.Burn:
					DNGST.DangoLargeRFile = "DngR/DangoBurnEx.png";
					break;
			}
			switch (DID) {
				case DNGST.DangoID.Pink:
					DNGST.DangoLargeLFile = "DngL/DangoPinkLarge.png";
					break;
				case DNGST.DangoID.Blue:
					DNGST.DangoLargeLFile = "DngL/DangoWaterLarge.png";
					break;
				case DNGST.DangoID.Green:
					DNGST.DangoLargeLFile = "DngL/DangoGreenLarge.png";
					break;
				case DNGST.DangoID.Black:
					DNGST.DangoLargeLFile = "DngL/DangoBlackLarge.png";
					break;
				case DNGST.DangoID.Yellow:
					DNGST.DangoLargeLFile = "DngL/DangoYellowLarge.png";
					break;
				case DNGST.DangoID.Burn:
					DNGST.DangoLargeLFile = "DngL/DangoBurnLarge.png";
					break;
			}
			DNGST.StageId = "";
			DNGST.Stage = "";
			DNGST.DiffS = "";
			DNGST.Diff = 0;
			DNGST.Score = 0;
			DNGST.Life = 2;
			DNGST.SP = 2;
			DNGST.AddTimeRndMax = 0;
			DNGST.AddTimeStatic = 0;
			DNGST.Timer = 0;
			DNGST.Temp = (DNGST.TempW = (DNGST.TempM = 0));
			DNGST.BossMode = (DNGST.StageCleared = (DNGST.Specialing = (DNGST.Reloading = (DNGST.GameEnd = (DNGST.FadeOut = (DNGST.NoDamage = false))))));
			if (GameCommon.DNGST_GameMode == DNGST.GameMode.RANDOM_ALL) {
				DNGST.Stage = "一期一会のだんごたち ～R～";
				DNGST.StageId = "RANDOM_ALL";
			}
			if (GameCommon.DNGST_GameMode == DNGST.GameMode.RANDOM_SAME) {
				DNGST.Stage = "だんごの内乱 ～R～";
				DNGST.StageId = "RANDOM_SAME";
			}
			if (GameCommon.DNGST_GameMode == DNGST.GameMode.RANDOM_BURN) {
				DNGST.Stage = "焼きすぎだんご ～R～";
				DNGST.StageId = "RANDOM_BURN";
			}
			if (GameCommon.DNGST_GameMode == DNGST.GameMode.RANDOM_FAST) {
				DNGST.Stage = "だんご速射砲 ～R～";
				DNGST.StageId = "RANDOM_FAST";
				for (int i = 0; i < Enum.GetNames(typeof(DNGST.DangoID)).Length; i++) {
					int num = 2;
					int num2 = 4;
					DNGST.DangoMaxSpeeds[i] = DNGST.DangoMaxSpeeds[i] * num;
					DNGST.DangoBulletSpeeds[i] = DNGST.DangoBulletSpeeds[i] * num;
					DNGST.DangoEnemyMaxSpeeds[i] = DNGST.DangoEnemyMaxSpeeds[i] * num2;
					DNGST.DangoEnemyBulletSpeeds[i] = DNGST.DangoEnemyBulletSpeeds[i] * num2;
					DNGST.DangoEnemyCoolingDowns[i] = DNGST.DangoEnemyCoolingDowns[i] / num2;
				}
			}
			switch (GameCommon.DNGST_DiffLv) {
				case DNGST.DiffLv.EASY:
					DNGST.AddTimeRndMax = 60;
					DNGST.AddTimeStatic = 100;
					DNGST.DiffS = "EASY";
					break;
				case DNGST.DiffLv.STANDARD:
					DNGST.AddTimeRndMax = 50;
					DNGST.AddTimeStatic = 50;
					DNGST.DiffS = "NORMAL";
					break;
				case DNGST.DiffLv.HARD:
					DNGST.AddTimeRndMax = 30;
					DNGST.AddTimeStatic = 25;
					DNGST.DiffS = "HARD";
					break;
				case DNGST.DiffLv.SPECIAL:
					DNGST.AddTimeRndMax = 15;
					DNGST.AddTimeStatic = 0;
					DNGST.DiffS = "SPECIAL";
					break;
			}
			DNGST.Diff = (int)GameCommon.DNGST_DiffLv;
			Texture.SetFont("Consolas");
			Texture.SetTextSize(18);
			Texture.SetTextColor(255, 255, 255);
			DNGST.TStage = Texture.CreateFromText(string.Format("STAGE: {0}\u3000\u3000\u3000", DNGST.Stage));
			DNGST.TDiff = Texture.CreateFromText(string.Format(" TYPE: {0}\u3000\u3000\u3000", DNGST.DiffS));
			DNGST.TScore = Texture.CreateFromText(string.Format("SCORE: {0:D10}\u3000\u3000\u3000", DNGST.Score));
			DNGST.TLife = Texture.CreateFromText(string.Format("DANGO: {0}\u3000\u3000\u3000", DNGST.Life));
			DNGST.TSP = Texture.CreateFromText(string.Format("   SP: {0}\u3000\u3000\u3000", DNGST.SP));
			DNGST.AddWaitCount = (DNGST.FramesCount = (DNGST.LoadingState = 0));
			DNGST.Loading = (DNGST.First = true);
			for (int j = 0; j < DNGST.LoadingBG.Length; j++) {
				DNGST.LoadingBG[j] = Texture.CreateFromFile("Loading" + j + ".png");
			}
			DNGST.DynBGPos = 0;
			DNGST.DynBGA = 25;
			DNGST.DynBGSpeed = 5;
			DNGST.AddWaiter = 0;
			return ContentReturn.OK;
		}

		public static void MoveBullet(BulletData BD) {
			BD.Count++;
			if (!BD.Enemy) {
				if (!BD.Special) {
					BD.X += DNGST.DangoBulletSpeeds[(int)BD.ID];
					return;
				}
				BD.X += 8;
				return;
			} else {
				if (BD.ID == DNGST.DangoID.Pink) {
					BD.X -= DNGST.DangoEnemyBulletSpeeds[(int)BD.ID];
					BD.Y = (int)(Math.Sin((double)BD.Count / 30.0) * 60.0) + BD.BaseY;
					return;
				}
				if (BD.ID == DNGST.DangoID.Blue) {
					BD.X -= DNGST.DangoEnemyBulletSpeeds[(int)BD.ID];
					return;
				}
				if (BD.ID == DNGST.DangoID.Green) {
					BD.BaseX -= DNGST.DangoEnemyBulletSpeeds[(int)BD.ID];
					int num = 1;
					int num2 = 2;
					BD.X = (int)(Math.Sin((double)num * (double)BD.Count / 50.0) * 120.0) + BD.BaseX;
					BD.Y = (int)(Math.Sin((double)num2 * (double)BD.Count / 50.0) * 120.0) + BD.BaseY;
					return;
				}
				if (BD.ID == DNGST.DangoID.Yellow) {
					BD.BaseX -= DNGST.DangoEnemyBulletSpeeds[(int)BD.ID];
					int num3 = (int)(Math.Sin((double)BD.Count / 100.0 * 3.0) * 100.0);
					BD.X = (int)(Math.Cos((double)BD.Count / 20.0) * (double)num3) + BD.BaseX;
					BD.Y = (int)(Math.Sin((double)BD.Count / 20.0) * (double)num3) + BD.BaseY;
					return;
				}
				if (BD.ID == DNGST.DangoID.Black) {
					BD.BaseX -= DNGST.DangoEnemyBulletSpeeds[(int)BD.ID];
					int num4 = 64;
					BD.X = (int)(Math.Cos((double)BD.Count / 10.0) * (double)num4) + BD.BaseX;
					BD.Y = (int)(Math.Sin((double)BD.Count / 10.0) * (double)num4) + BD.BaseY;
					return;
				}
				if (BD.ID == DNGST.DangoID.Burn) {
					BD.X -= DNGST.DangoEnemyBulletSpeeds[(int)BD.ID];
					BD.Y += DNGST.rnd.Next(5) - 2;
				}
				return;
			}
		}

		public static void MoveChar(CharData CD) {
			CD.Count++;
			if (CD.ID == DNGST.DangoID.Pink) {
				CD.X -= DNGST.DangoEnemyMaxSpeeds[(int)CD.ID];
				CD.Y = (int)(Math.Sin((double)CD.Count / 30.0) * 60.0) + CD.BaseY;
				return;
			}
			if (CD.ID == DNGST.DangoID.Blue) {
				CD.X -= DNGST.DangoEnemyMaxSpeeds[(int)CD.ID];
				return;
			}
			if (CD.ID == DNGST.DangoID.Green) {
				CD.BaseX -= DNGST.DangoEnemyMaxSpeeds[(int)CD.ID];
				int num = 1;
				int num2 = 2;
				CD.X = (int)(Math.Sin((double)num * (double)CD.Count / 50.0) * 120.0) + CD.BaseX;
				CD.Y = (int)(Math.Sin((double)num2 * (double)CD.Count / 50.0) * 120.0) + CD.BaseY;
				return;
			}
			if (CD.ID == DNGST.DangoID.Yellow) {
				CD.BaseX -= DNGST.DangoEnemyMaxSpeeds[(int)CD.ID];
				int num3 = (int)(Math.Sin((double)CD.Count / 100.0 * 3.0) * 100.0);
				CD.X = (int)(Math.Cos((double)CD.Count / 20.0) * (double)num3) + CD.BaseX;
				CD.Y = (int)(Math.Sin((double)CD.Count / 20.0) * (double)num3) + CD.BaseY;
				return;
			}
			if (CD.ID == DNGST.DangoID.Black) {
				CD.BaseX -= DNGST.DangoEnemyMaxSpeeds[(int)CD.ID];
				int num4 = 64;
				CD.X = (int)(Math.Cos((double)CD.Count / 10.0) * (double)num4) + CD.BaseX;
				CD.Y = (int)(Math.Sin((double)CD.Count / 10.0) * (double)num4) + CD.BaseY;
				return;
			}
			if (CD.ID == DNGST.DangoID.Burn) {
				CD.X -= DNGST.DangoEnemyMaxSpeeds[(int)CD.ID];
				CD.Y += DNGST.rnd.Next(5) - 2;
			}
		}

		public static void AddCharRandom() {
			int num = DNGST.rnd.Next((DNGST.DangoTypes - 1) * 100);
			num = num % (DNGST.DangoTypes - 1) + 1;
			if (num == (int)GameCommon.DNGST_SelectedDangoID) {
				num = DNGST.rnd.Next(DNGST.DangoTypes - 1) + 1;
			}
			if (num == (int)GameCommon.DNGST_SelectedDangoID) {
				num = DNGST.rnd.Next(DNGST.DangoTypes - 1) + 1;
			}
			DNGST.AddChar((DNGST.DangoID)num, true, 1280, DNGST.rnd.Next(740));
		}

		public static void GameInit() {
			DNGST.Char[0] = new CharData(GameCommon.DNGST_SelectedDangoID, false, 20, 260);
		}

		public static ContentReturn Main(DNGST.DangoID DID) {
			DNGST.First = true;
			if (DNGST.Loading) {
				Core.Draw(DNGST.LoadingBG[DNGST.LoadingState / 2 % 4], 920, 560);
				switch (DNGST.LoadingState) {
					case 0:
						DNGST.BG = Texture.CreateFromFile("GameBG.png");
						break;
					case 1:
						DNGST.DynBG = Texture.CreateFromFile("DynamicBG.png");
						break;
					case 2:
						DNGST.BulletSES.LoadFile("BulletS.wav");
						DNGST.BulletSEE.LoadFile("BulletE.wav");
						break;
					case 3:
						DNGST.BurnSE.LoadFile("DNGBurn.wav");
						break;
					case 4:
						DNGST.GameBGM.LoadFile("BGM_Game.wav");
						break;
					case 5:
						DNGST.BossBGM.LoadFile("BGM_Boss.wav");
						break;
					case 6:
						DNGST.GameOverSE.LoadFile("DNGGO.wav");
						break;
					case 7:
						DNGST.AddSE.LoadFile("DNGAdd.wav");
						break;
					case 8:
						DNGST.Effects[1] = Texture.CreateFromFile("HitEffect.png");
						break;
					case 9:
						DNGST.Effects[2] = Texture.CreateFromFile("HitEffectEx.png");
						break;
					case 10:
						DNGST.DangoL[1] = Texture.CreateFromFile("DngL/DangoPink.png");
						break;
					case 11:
						DNGST.DangoL[2] = Texture.CreateFromFile("DngL/DangoWater.png");
						break;
					case 12:
						DNGST.DangoL[3] = Texture.CreateFromFile("DngL/DangoGreen.png");
						break;
					case 13:
						DNGST.DangoL[4] = Texture.CreateFromFile("DngL/DangoBlack.png");
						break;
					case 14:
						DNGST.DangoL[5] = Texture.CreateFromFile("DngL/DangoYellow.png");
						break;
					case 15:
						DNGST.DangoL[6] = Texture.CreateFromFile("DngL/DangoBurn.png");
						break;
					case 16:
						DNGST.DangoR[1] = Texture.CreateFromFile("DngR/DangoPink.png");
						break;
					case 17:
						DNGST.DangoR[2] = Texture.CreateFromFile("DngR/DangoWater.png");
						break;
					case 18:
						DNGST.DangoR[3] = Texture.CreateFromFile("DngR/DangoGreen.png");
						break;
					case 19:
						DNGST.DangoR[4] = Texture.CreateFromFile("DngR/DangoBlack.png");
						break;
					case 20:
						DNGST.DangoR[5] = Texture.CreateFromFile("DngR/DangoYellow.png");
						break;
					case 21:
						DNGST.DangoR[6] = Texture.CreateFromFile("DngR/DangoBurn.png");
						break;
					case 22:
						DNGST.DangoBulletL[1] = Texture.CreateFromFile("DngLB/DangoPink.png");
						break;
					case 23:
						DNGST.DangoBulletL[2] = Texture.CreateFromFile("DngLB/DangoWater.png");
						break;
					case 24:
						DNGST.DangoBulletL[3] = Texture.CreateFromFile("DngLB/DangoGreen.png");
						break;
					case 25:
						DNGST.DangoBulletL[4] = Texture.CreateFromFile("DngLB/DangoBlack.png");
						break;
					case 26:
						DNGST.DangoBulletL[5] = Texture.CreateFromFile("DngLB/DangoYellow.png");
						break;
					case 27:
						DNGST.DangoBulletL[6] = Texture.CreateFromFile("DngLB/DangoBurn.png");
						break;
					case 28:
						DNGST.DangoBulletR[1] = Texture.CreateFromFile("DngRB/DangoPink.png");
						break;
					case 29:
						DNGST.DangoBulletR[2] = Texture.CreateFromFile("DngRB/DangoWater.png");
						break;
					case 30:
						DNGST.DangoBulletR[3] = Texture.CreateFromFile("DngRB/DangoGreen.png");
						break;
					case 31:
						DNGST.DangoBulletR[4] = Texture.CreateFromFile("DngRB/DangoBlack.png");
						break;
					case 32:
						DNGST.DangoBulletR[5] = Texture.CreateFromFile("DngRB/DangoYellow.png");
						break;
					case 33:
						DNGST.DangoBulletR[6] = Texture.CreateFromFile("DngRB/DangoBurn.png");
						break;
					case 34:
						DNGST.DangoLargeL = Texture.CreateFromFile(DNGST.DangoLargeLFile);
						DNGST.DangoLargeLBurn = Texture.CreateFromFile("DngL/DangoBurnLarge.png");
						break;
					case 35:
						DNGST.DangoLargeR = Texture.CreateFromFile(DNGST.DangoLargeRFile);
						break;
					case 36:
						DNGST.SpecialSE.LoadFile("DNGSP.wav");
						break;
					case 37:
						DNGST.ErrorSE.LoadFile("DNGErr.wav");
						break;
					case 38:
						DNGST.ChangeSE.LoadFile("DNGOut.wav");
						break;
					case 39:
						DNGST.ClearSE.LoadFile("BGM_Result.wav");
						break;
					case 40:
						DNGST.GameInit();
						break;
					case 41:
						DNGST.GameBGM.Play();
						break;
					case 42:
						DNGST.Loading = false;
						break;
				}
				DNGST.LoadingState++;
				return ContentReturn.OK;
			}
			if (DNGST.Score != DNGST.OldScore) {
				DNGST.TScore = Texture.CreateFromText(string.Format("SCORE: {0:D10}\u3000\u3000\u3000", DNGST.Score));
				DNGST.OldScore = DNGST.Score;
			}
			Core.Draw(DNGST.BG, 0, 0, 255);
			Common.GSprite.DrawEx(DNGST.DynBG, Common.WindowX - DNGST.DynBGPos, 0, DNGST.DynBGA, 0, 0, 1280, 720);
			Common.GSprite.DrawEx(DNGST.DynBG, Common.WindowX - DNGST.DynBGPos - 1280, 0, DNGST.DynBGA, 0, 0, 1280, 720);
			DNGST.DynBGPos += DNGST.DynBGSpeed;
			if (DNGST.DynBGPos > 1280) {
				DNGST.DynBGPos = 0;
			}
			if (DNGST.AddWaiter <= 0 && !DNGST.BossMode) {
				if (GameCommon.DNGST_GameMode == DNGST.GameMode.RANDOM_ALL) {
					DNGST.AddCharRandom();
				}
				if (GameCommon.DNGST_GameMode == DNGST.GameMode.RANDOM_SAME) {
					DNGST.AddChar(GameCommon.DNGST_SelectedDangoID, true, 1280, DNGST.rnd.Next(740));
				}
				if (GameCommon.DNGST_GameMode == DNGST.GameMode.RANDOM_BURN) {
					DNGST.AddChar(DNGST.DangoID.Burn, true, 1280, DNGST.rnd.Next(740));
				}
				if (GameCommon.DNGST_GameMode == DNGST.GameMode.RANDOM_FAST) {
					DNGST.AddCharRandom();
				}
				DNGST.AddWaiter = (DNGST.rnd.Next(DNGST.AddTimeRndMax * 5) + 1) % DNGST.AddTimeRndMax + DNGST.AddTimeStatic;
				Debug.Log('I', "AddDango", "AddedDango: Next: {0}", new object[]
				{
					DNGST.AddWaiter
				});
			}
			if (DNGST.VIOEx.GetButtonOnce(0, VirtualIO.ButtonID.MENU) != 0) {
				DNGST.GameEnd = true;
				Debug.Log('I', "GameEnd", "Abort", new object[0]);
			}
			if (DNGST.Char[0] != null) {
				if (VirtualIO.GetButton(0, VirtualIO.ButtonID.DOWN) != 0) {
					if (VirtualIO.GetButton(0, VirtualIO.ButtonID.L) != 0) {
						DNGST.Char[0].Y += DNGST.Char[0].MaxSpeed / 2;
					} else {
						DNGST.Char[0].Y += DNGST.Char[0].MaxSpeed;
					}
				}
				if (VirtualIO.GetButton(0, VirtualIO.ButtonID.UP) != 0) {
					if (VirtualIO.GetButton(0, VirtualIO.ButtonID.L) != 0) {
						DNGST.Char[0].Y -= DNGST.Char[0].MaxSpeed / 2;
					} else {
						DNGST.Char[0].Y -= DNGST.Char[0].MaxSpeed;
					}
				}
				if (VirtualIO.GetButton(0, VirtualIO.ButtonID.RIGHT) != 0) {
					if (VirtualIO.GetButton(0, VirtualIO.ButtonID.L) != 0) {
						DNGST.Char[0].X += DNGST.Char[0].MaxSpeed / 2;
					} else {
						DNGST.Char[0].X += DNGST.Char[0].MaxSpeed;
					}
				}
				if (VirtualIO.GetButton(0, VirtualIO.ButtonID.LEFT) != 0) {
					if (VirtualIO.GetButton(0, VirtualIO.ButtonID.L) != 0) {
						DNGST.Char[0].X -= DNGST.Char[0].MaxSpeed / 2;
					} else {
						DNGST.Char[0].X -= DNGST.Char[0].MaxSpeed;
					}
				}
				if (VirtualIO.GetAnalog(0, VirtualIO.AnalogID.LY) < 0) {
					DNGST.Char[0].Y += (VirtualIO.GetAnalog(0, VirtualIO.AnalogID.LY) + 128) / (127 / DNGST.Char[0].MaxSpeed);
				}
				if (VirtualIO.GetAnalog(0, VirtualIO.AnalogID.LY) > 0) {
					DNGST.Char[0].Y += (VirtualIO.GetAnalog(0, VirtualIO.AnalogID.LY) - 128) / (127 / DNGST.Char[0].MaxSpeed);
				}
				if (VirtualIO.GetAnalog(0, VirtualIO.AnalogID.LX) < 0) {
					DNGST.Char[0].X += (VirtualIO.GetAnalog(0, VirtualIO.AnalogID.LX) + 128) / (127 / DNGST.Char[0].MaxSpeed);
				}
				if (VirtualIO.GetAnalog(0, VirtualIO.AnalogID.LX) > 0) {
					DNGST.Char[0].X += (VirtualIO.GetAnalog(0, VirtualIO.AnalogID.LX) - 128) / (127 / DNGST.Char[0].MaxSpeed);
				}
				if (DNGST.Char[0].X <= 0) {
					DNGST.Char[0].X = 0;
				}
				if (DNGST.Char[0].X >= Common.WindowX - 64) {
					DNGST.Char[0].X = Common.WindowX - 64;
				}
				if (DNGST.Char[0].Y <= 0) {
					DNGST.Char[0].Y = 0;
				}
				if (DNGST.Char[0].Y >= Common.WindowY - 64) {
					DNGST.Char[0].Y = Common.WindowY - 64;
				}
				if (DNGST.VIOEx.GetButtonOnce(0, VirtualIO.ButtonID.OK) != 0) {
					DNGST.AddBullet(DNGST.Char[0].ID, false, DNGST.Char[0].X + 52, DNGST.Char[0].Y + 32);
					DNGST.Char[0].CoolingDown = DNGST.DangoCoolingDowns[(int)DNGST.Char[0].ID];
				}
				if (VirtualIO.GetButton(0, VirtualIO.ButtonID.OK) != 0 && DNGST.Char[0].CoolingDown == 0) {
					DNGST.AddBullet(DNGST.Char[0].ID, false, DNGST.Char[0].X + 52, DNGST.Char[0].Y + 32);
					DNGST.Char[0].CoolingDown = DNGST.DangoCoolingDowns[(int)DNGST.Char[0].ID];
				}
				if (DNGST.VIOEx.GetButtonOnce(0, VirtualIO.ButtonID.CANCEL) != 0 && !DNGST.Specialing) {
					if (DNGST.SP > 0) {
						int num = DNGST.AddBullet(DNGST.Char[0].ID, false, -512, 0);
						DNGST.Bullet[num].T = DNGST.DangoLargeR;
						DNGST.Bullet[num].Special = true;
						DNGST.SpecialSE.Play();
						DNGST.Specialing = true;
						DNGST.Score += 10000;
						DNGST.SP--;
						DNGST.TSP = Texture.CreateFromText(string.Format("   SP: {0}\u3000\u3000\u3000", DNGST.SP));
					} else {
						DNGST.ErrorSE.Play();
					}
				}
			} else if (DNGST.Life > 0) {
				if (DNGST.Reloading) {
					if (DNGST.Timer <= 0) {
						DNGST.Life--;
						DNGST.Char[0] = new CharData(GameCommon.DNGST_SelectedDangoID, false, 20, 260);
						DNGST.TLife = Texture.CreateFromText(string.Format("DANGO: {0}\u3000\u3000\u3000", DNGST.Life));
						DNGST.AddSE.Play();
						DNGST.Reloading = false;
						DNGST.NoDamage = true;
						DNGST.Char[0].A = 128;
						DNGST.Timer = 300;
						DNGST.SP = 2;
						DNGST.TSP = Texture.CreateFromText(string.Format("   SP: {0}\u3000\u3000\u3000", DNGST.SP));
						DNGST.BurnSE.Play();
						for (int i = 0; i < DNGST.Bullet.Length; i++) {
							if (DNGST.Bullet[i] != null) {
								DNGST.AddEffect(DNGST.EffectID.Hit, DNGST.Bullet[i].X - 8, DNGST.Bullet[i].Y - 8);
								DNGST.Bullet[i] = null;
								DNGST.Score += 100;
							}
						}
						for (int j = 2; j < DNGST.Char.Length; j++) {
							if (DNGST.Char[j] != null) {
								DNGST.AddEffect(DNGST.EffectID.HitEx, DNGST.Char[j].X - 16, DNGST.Char[j].Y - 16);
								DNGST.Char[j] = null;
								DNGST.Score += 1000;
							}
						}
					}
				} else {
					DNGST.Timer = 60;
					DNGST.Reloading = true;
				}
			} else {
				DNGST.GameEnd = true;
				Debug.Log('I', "GameEnd", "No Life", new object[0]);
			}
			for (int k = 0; k < DNGST.Char.Length; k++) {
				if (DNGST.Char[k] != null && DNGST.Char[k].Enemy && !DNGST.Char[k].Boss) {
					DNGST.MoveChar(DNGST.Char[k]);
					if (DNGST.Char[k].CoolingDown == 0) {
						DNGST.AddBullet(DNGST.Char[k].ID, true, DNGST.Char[k].X - 16, DNGST.Char[k].Y + 32);
						DNGST.Char[k].CoolingDown = DNGST.DangoEnemyCoolingDowns[(int)DNGST.Char[k].ID];
					}
				}
			}
			for (int l = 0; l < DNGST.Bullet.Length; l++) {
				if (DNGST.Bullet[l] != null) {
					DNGST.MoveBullet(DNGST.Bullet[l]);
					if (DNGST.Bullet[l].Special) {
						for (int m = 0; m < DNGST.Bullet.Length; m++) {
							if (DNGST.Bullet[m] != null && !DNGST.Bullet[m].Effect && l != m && DNGST.Bullet[m].X <= DNGST.Bullet[l].X + 960 && DNGST.Bullet[m].Enemy) {
								DNGST.AddEffect(DNGST.EffectID.Hit, DNGST.Bullet[m].X - 8, DNGST.Bullet[m].Y - 8);
								DNGST.Bullet[m] = null;
								DNGST.Score += 100;
							}
						}
						for (int n = 2; n < DNGST.Char.Length; n++) {
							if (DNGST.Char[n] != null && DNGST.Char[n].X <= DNGST.Bullet[l].X + 960) {
								DNGST.AddEffect(DNGST.EffectID.HitEx, DNGST.Char[n].X - 16, DNGST.Char[n].Y - 16);
								DNGST.BurnSE.Play();
								DNGST.Char[n] = null;
								DNGST.Score += 1000;
							}
						}
					}
				}
			}
			for (int num2 = 0; num2 < DNGST.Char.Length; num2++) {
				if (DNGST.Char[num2] != null && !DNGST.Char[num2].Boss) {
					for (int num3 = 0; num3 < DNGST.Bullet.Length; num3++) {
						if (DNGST.Bullet[num3] != null && DNGST.Char[num2].Enemy != DNGST.Bullet[num3].Enemy && !DNGST.Bullet[num3].Effect) {
							int num4;
							if (DNGST.Char[num2].Enemy) {
								num4 = 1296;
							} else {
								num4 = 576;
								if (DNGST.NoDamage) {
									goto IL_FC9;
								}
							}
							int num5 = DNGST.Char[num2].X + 32 - (DNGST.Bullet[num3].X + 12);
							int num6 = DNGST.Char[num2].Y + 32 - (DNGST.Bullet[num3].Y + 12);
							int num7 = num5 * num5 + num6 * num6;
							if (num7 < num4) {
								DNGST.Char[num2].HP--;
								Debug.Log('I', "Hit Checker", "Hit: {0}", new object[]
								{
									num3
								});
								DNGST.AddEffect(DNGST.EffectID.Hit, DNGST.Bullet[num3].X - 8, DNGST.Bullet[num3].Y - 8);
								if (DNGST.Char[num2].Enemy) {
									DNGST.Score += 100;
								}
								DNGST.Bullet[num3] = null;
							}
						}
					IL_FC9: ;
					}
				}
			}
			for (int num8 = 0; num8 < DNGST.Char.Length; num8++) {
				if (DNGST.Char[num8] != null) {
					Core.Draw(DNGST.Char[num8].T, DNGST.Char[num8].X, DNGST.Char[num8].Y, DNGST.Char[num8].A);
					if (DNGST.Char[num8].CoolingDown > 0) {
						DNGST.Char[num8].CoolingDown--;
					}
				}
			}
			for (int num9 = 0; num9 < DNGST.Bullet.Length; num9++) {
				if (DNGST.Bullet[num9] != null) {
					Core.Draw(DNGST.Bullet[num9].T, DNGST.Bullet[num9].X, DNGST.Bullet[num9].Y, DNGST.Bullet[num9].A);
					if (DNGST.Bullet[num9].Effect) {
						DNGST.Bullet[num9].A = DNGST.Bullet[num9].A - 24;
						if (DNGST.Bullet[num9].A <= 0) {
							DNGST.Bullet[num9] = null;
							Debug.Log('I', "Object Deleter", "Deleted Effect: {0}", new object[]
							{
								num9
							});
						}
					}
				}
			}
			for (int num10 = 0; num10 < DNGST.Char.Length; num10++) {
				if (DNGST.Char[num10] != null) {
					if (-256 > DNGST.Char[num10].X || DNGST.Char[num10].X > Common.WindowX + 256 || -256 > DNGST.Char[num10].Y || DNGST.Char[num10].Y > Common.WindowY + 256) {
						DNGST.Char[num10] = null;
						Debug.Log('I', "Object Deleter", "Deleted Char: {0} by gone", new object[]
						{
							num10
						});
					} else if (DNGST.Char[num10].HP <= 0) {
						DNGST.BurnSE.Play();
						DNGST.Score += 1000;
						DNGST.Score += DNGST.DangoEnemyScores[(int)DNGST.Char[num10].ID];
						DNGST.AddEffect(DNGST.EffectID.HitEx, DNGST.Char[num10].X - 16, DNGST.Char[num10].Y - 16);
						DNGST.Char[num10] = null;
						Debug.Log('I', "Object Deleter", "Deleted Char: {0} by Life", new object[]
						{
							num10
						});
					}
				}
			}
			for (int num11 = 0; num11 < DNGST.Bullet.Length; num11++) {
				if (DNGST.Bullet[num11] != null && (-64 > DNGST.Bullet[num11].X || DNGST.Bullet[num11].X > Common.WindowX || 0 > DNGST.Bullet[num11].Y || DNGST.Bullet[num11].Y > Common.WindowY + 64)) {
					if (!DNGST.Bullet[num11].Special) {
						DNGST.Bullet[num11] = null;
						Debug.Log('I', "Object Deleter", "Deleted Bullet: {0}", new object[]
						{
							num11
						});
					} else if (DNGST.Bullet[num11].X > Common.WindowX) {
						DNGST.Bullet[num11] = null;
						DNGST.Specialing = false;
						Debug.Log('I', "Object Deleter", "Deleted Special Bullet: {0}", new object[]
						{
							num11
						});
					}
				}
			}
			if (DNGST.NoDamage && DNGST.Timer <= 0) {
				DNGST.NoDamage = false;
				DNGST.Char[0].A = 255;
				DNGST.ChangeSE.Play();
			}
			if (DNGST.FramesCount >= 3200) {
				if (!DNGST.BossMode) {
					DNGST.BossMode = true;
					DNGST.GameBGM.Close();
					DNGST.BossBGM.Play();
					if (GameCommon.DNGST_GameMode == DNGST.GameMode.RANDOM_ALL) {
						DNGST.Char[1] = new CharData(GameCommon.DNGST_SelectedDangoID, true, 900, 250);
						DNGST.Char[1].T = DNGST.DangoLargeL;
					}
					if (GameCommon.DNGST_GameMode == DNGST.GameMode.RANDOM_SAME) {
						DNGST.Char[1] = new CharData(GameCommon.DNGST_SelectedDangoID, true, 900, 250);
						DNGST.Char[1].T = DNGST.DangoLargeL;
					}
					if (GameCommon.DNGST_GameMode == DNGST.GameMode.RANDOM_BURN) {
						DNGST.Char[1] = new CharData(DNGST.DangoID.Burn, true, 900, 250);
						DNGST.Char[1].T = DNGST.DangoLargeLBurn;
					}
					if (GameCommon.DNGST_GameMode == DNGST.GameMode.RANDOM_FAST) {
						DNGST.Char[1] = new CharData(GameCommon.DNGST_SelectedDangoID, true, 900, 250);
						DNGST.Char[1].T = DNGST.DangoLargeL;
					}
					DNGST.Char[1].MaxHP = (DNGST.Char[1].HP = 100);
					DNGST.Char[1].Boss = true;
					DNGST.Char[1].Count = -1;
					switch (GameCommon.DNGST_DiffLv) {
						case DNGST.DiffLv.EASY:
							DNGST.BossAddTimeRndMax = 60;
							DNGST.BossAddTimeStatic = 100;
							DNGST.BossMaxWTypes = 4;
							DNGST.Char[1].MaxHP = (DNGST.Char[1].HP = 100);
							break;
						case DNGST.DiffLv.STANDARD:
							DNGST.BossAddTimeRndMax = 50;
							DNGST.BossAddTimeStatic = 50;
							DNGST.BossMaxWTypes = 7;
							DNGST.Char[1].MaxHP = (DNGST.Char[1].HP = 150);
							break;
						case DNGST.DiffLv.HARD:
							DNGST.BossAddTimeRndMax = 30;
							DNGST.BossAddTimeStatic = 25;
							DNGST.BossMaxWTypes = 9;
							DNGST.Char[1].MaxHP = (DNGST.Char[1].HP = 200);
							break;
						case DNGST.DiffLv.SPECIAL:
							DNGST.BossAddTimeRndMax = 15;
							DNGST.BossAddTimeStatic = 0;
							DNGST.BossMaxWTypes = 9;
							DNGST.Char[1].MaxHP = (DNGST.Char[1].HP = 255);
							break;
					}
					DNGST.Timer = DNGST.rnd.Next(DNGST.BossAddTimeRndMax) + DNGST.BossAddTimeStatic;
				} else if (DNGST.Char[1] != null) {
					Common.GSprite.DrawEx(DNGST.White, 20, 20, 255, 0, 0, DNGST.Char[1].HP * 1000 / DNGST.Char[1].MaxHP * 600 / 1000, 4);
					if (DNGST.Char[1].X >= 650 && DNGST.Char[1].Count == -1) {
						DNGST.Char[1].X -= 2;
						DNGST.Char[1].BaseX = DNGST.Char[1].X;
						DNGST.Char[1].BaseY = DNGST.Char[1].Y;
					} else {
						if (DNGST.Char[1].X == 650 && DNGST.Char[1].Y == 150) {
							DNGST.TempM = DNGST.rnd.Next(1);
							Debug.Log('I', "Boss", "Home Pos", new object[0]);
							DNGST.Char[1].Count = 1;
						}
						if (DNGST.TempM == 0) {
							int num12 = 2;
							int num13 = 3;
							DNGST.Char[1].BaseX = (int)(Math.Sin((double)num12 * (double)DNGST.Char[1].Count / 60.0) * 80.0) + 650;
							DNGST.Char[1].X = (int)(Math.Sin((double)num12 * (double)DNGST.Char[1].Count / 60.0) * 160.0) + DNGST.Char[1].BaseX;
							DNGST.Char[1].Y = (int)(Math.Sin((double)num13 * (double)DNGST.Char[1].Count / 60.0) * 160.0) + DNGST.Char[1].BaseY;
						}
						if (DNGST.TempM == 1) {
							int num14 = (int)(Math.Sin((double)DNGST.Char[1].Count / 100.0 * 3.0) * 100.0);
							DNGST.Char[1].X = (int)(Math.Cos((double)DNGST.Char[1].Count / 20.0) * (double)num14) * 120 + DNGST.Char[1].BaseX;
							DNGST.Char[1].Y = (int)(Math.Sin((double)DNGST.Char[1].Count / 20.0) * (double)num14) * 120 + DNGST.Char[1].BaseY;
						}
						DNGST.Char[1].Count++;
						if (DNGST.Timer <= 0) {
							int num15 = DNGST.rnd.Next(DNGST.BossMaxWTypes);
							DNGST.TempW = 0;
							DNGST.TempM = 0;
							switch (num15) {
								case 0:
									DNGST.AddChar(DNGST.Char[1].ID, true, DNGST.Char[1].X, DNGST.Char[1].Y + 128);
									break;
								case 1:
									DNGST.AddChar(DNGST.Char[1].ID, true, DNGST.Char[1].X, DNGST.Char[1].Y + 128);
									break;
								case 2:
									DNGST.AddChar(DNGST.Char[1].ID, true, DNGST.Char[1].X, DNGST.Char[1].Y + 128);
									break;
								case 3:
									DNGST.TempW = 1;
									DNGST.Temp = 10;
									break;
								case 4:
									DNGST.TempW = 1;
									DNGST.Temp = 20;
									break;
								case 5:
									DNGST.TempW = 1;
									DNGST.Temp = 40;
									break;
								case 6:
									DNGST.TempW = 1;
									DNGST.Temp = 40;
									break;
								case 7:
									DNGST.TempW = 1;
									DNGST.Temp = 60;
									break;
								case 8:
									DNGST.TempW = 2;
									DNGST.Temp = 20;
									break;
								default:
									DNGST.AddBullet(DNGST.Char[1].ID, true, DNGST.Char[1].X, DNGST.Char[1].Y + 128);
									break;
							}
							DNGST.Timer = DNGST.rnd.Next(DNGST.BossAddTimeRndMax) + DNGST.BossAddTimeStatic;
						}
						if (DNGST.TempW == 1 && DNGST.Temp >= 0 && DNGST.Temp % 2 == 0) {
							DNGST.AddBullet(DNGST.Char[1].ID, true, DNGST.Char[1].X, DNGST.Char[1].Y + 128);
						}
						if (DNGST.TempW == 2 && DNGST.Temp >= 0 && DNGST.Temp % 2 == 0) {
							DNGST.AddChar(DNGST.Char[1].ID, true, DNGST.Char[1].X, DNGST.Char[1].Y + 128);
						}
						DNGST.Temp--;
						if (DNGST.AddWaiter <= 0) {
							DNGST.AddChar(DNGST.Char[1].ID, true, 1280, DNGST.rnd.Next(720));
							DNGST.AddWaiter = (DNGST.rnd.Next(DNGST.AddTimeRndMax * 5) + 1) % DNGST.AddTimeRndMax + DNGST.AddTimeStatic;
							Debug.Log('I', "AddDango", "AddedDango: Next: {0}", new object[]
							{
								DNGST.AddWaiter
							});
						}
						int num16 = 7569;
						for (int num17 = 0; num17 < DNGST.Bullet.Length; num17++) {
							if (DNGST.Bullet[num17] != null && !DNGST.Bullet[num17].Enemy && !DNGST.Bullet[num17].Effect) {
								int num18 = DNGST.Char[1].X + 128 - (DNGST.Bullet[num17].X + 12);
								int num19 = DNGST.Char[1].Y + 128 - (DNGST.Bullet[num17].Y + 12);
								int num20 = num18 * num18 + num19 * num19;
								if (num20 < num16) {
									DNGST.Char[1].HP--;
									Debug.Log('I', "Hit Checker", "Boss Hit: {0}", new object[]
									{
										num17
									});
									DNGST.AddEffect(DNGST.EffectID.HitEx, DNGST.Bullet[num17].X - 8, DNGST.Bullet[num17].Y - 8);
									DNGST.Score += 100;
									DNGST.Bullet[num17] = null;
								}
							}
						}
					}
				} else {
					Debug.Log('I', "GameEnd", "Boss Died", new object[0]);
					DNGST.StageCleared = true;
					DNGST.GameEnd = true;
				}
			}
			Core.Draw(DNGST.TStage, 970, 20, 255);
			Core.Draw(DNGST.TDiff, 970, 50, 255);
			Core.Draw(DNGST.TScore, 970, 80, 255);
			Core.Draw(DNGST.TLife, 970, 110, 255);
			Core.Draw(DNGST.TSP, 970, 140, 255);
			DNGST.FramesCount++;
			DNGST.AddWaiter--;
			DNGST.Timer--;
			if (!DNGST.GameEnd) {
				Effect.Fadein();
				return ContentReturn.OK;
			}
			if (!DNGST.FadeOut) {
				DNGST.GameBGM.Close();
				DNGST.BossBGM.Close();
				if (DNGST.StageCleared) {
					DNGST.ClearSE.Play();
				} else {
					DNGST.GameOverSE.Play();
				}
				Effect.Reset();
				DNGST.FadeOut = true;
			}
			if (Effect.Fadeout() == ContentReturn.END) {
				Scene.Set("Result");
				return ContentReturn.CHANGE;
			}
			return ContentReturn.OK;
		}

		public static int AddBullet(DNGST.DangoID DID, bool IsEnemy, int X, int Y) {
			for (int i = 0; i < DNGST.Bullet.Length; i++) {
				if (DNGST.Bullet[i] == null) {
					DNGST.Bullet[i] = new BulletData(DID, IsEnemy, X, Y);
					if (!IsEnemy) {
						DNGST.BulletSES.Play();
					} else if (DNGST.First) {
						DNGST.BulletSEE.Play();
						DNGST.First = false;
					}
					return i;
				}
			}
			return -1;
		}

		public static void AddEffect(DNGST.EffectID EID, int X, int Y) {
			for (int i = 0; i < DNGST.Bullet.Length; i++) {
				if (DNGST.Bullet[i] == null) {
					DNGST.Bullet[i] = new BulletData(DNGST.DangoID.None, false, X, Y);
					DNGST.Bullet[i].Effect = true;
					DNGST.Bullet[i].T = DNGST.Effects[(int)EID];
					return;
				}
			}
		}

		public static void AddChar(DNGST.DangoID DID, bool IsEnemy, int X, int Y) {
			for (int i = 2; i < DNGST.Char.Length; i++) {
				if (DNGST.Char[i] == null) {
					DNGST.Char[i] = new CharData(DID, IsEnemy, X, Y);
					return;
				}
			}
		}
	}
}
