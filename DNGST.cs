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

		public static int[] DangoMaxSpeeds = new int[7] { 0, 4, 10, 6, 6, 4, 1 };

		public static int[] DangoBulletSpeeds = new int[7] { 0, 10, 16, 12, 14, 8, 2 };

		public static int[] DangoCoolingDowns = new int[7] { 0, 10, 10, 10, 10, 10, 10 };

		public static int[] DangoEnemyMaxSpeeds = new int[7] { 0, 2, 4, 2, 2, 2, 1 };

		public static int[] DangoEnemyBulletSpeeds = new int[7] { 0, 4, 8, 6, 6, 4, 2 };

		public static int[] DangoEnemyCoolingDowns = new int[7] { 0, 80, 60, 80, 80, 60, 80 };

		public static int[] DangoEnemyLifes = new int[7] { 0, 1, 1, 1, 2, 1, 3 };

		public static int[] DangoEnemyScores = new int[7] { 0, 100, 500, 200, 1000, 250, 2000 };

		public static Texture[] DangoL = new Texture[DangoTypes];

		public static Texture[] DangoR = new Texture[DangoTypes];

		public static Texture[] DangoBulletL = new Texture[DangoTypes];

		public static Texture[] DangoBulletR = new Texture[DangoTypes];

		public static Texture DangoLargeL = null;

		public static Texture DangoLargeLBurn = null;

		public static Texture DangoLargeR = null;

		public static Texture BG = null;

		public static Texture DynBG = null;

		public static Texture NoTexture = Texture.CreateFromText(" ");

		public static Texture[] Effects = new Texture[16];

		public static Paint PWhite = new Paint(255, 255, 255, 255);

		public static Texture White = PWhite.ToTexture();

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

		public static ContentReturn Initialize(DangoID DID) {
			Char = new CharData[128];
			Bullet = new BulletData[512];
			switch(DID) {
				case DangoID.Pink:
					DangoLargeRFile = "DngR/DangoPinkEx.png";
					break;
				case DangoID.Blue:
					DangoLargeRFile = "DngR/DangoWaterEx.png";
					break;
				case DangoID.Green:
					DangoLargeRFile = "DngR/DangoGreenEx.png";
					break;
				case DangoID.Black:
					DangoLargeRFile = "DngR/DangoBlackEx.png";
					break;
				case DangoID.Yellow:
					DangoLargeRFile = "DngR/DangoYellowEx.png";
					break;
				case DangoID.Burn:
					DangoLargeRFile = "DngR/DangoBurnEx.png";
					break;
			}
			switch(DID) {
				case DangoID.Pink:
					DangoLargeLFile = "DngL/DangoPinkLarge.png";
					break;
				case DangoID.Blue:
					DangoLargeLFile = "DngL/DangoWaterLarge.png";
					break;
				case DangoID.Green:
					DangoLargeLFile = "DngL/DangoGreenLarge.png";
					break;
				case DangoID.Black:
					DangoLargeLFile = "DngL/DangoBlackLarge.png";
					break;
				case DangoID.Yellow:
					DangoLargeLFile = "DngL/DangoYellowLarge.png";
					break;
				case DangoID.Burn:
					DangoLargeLFile = "DngL/DangoBurnLarge.png";
					break;
			}
			StageId = "";
			Stage = "";
			DiffS = "";
			Diff = 0;
			Score = 0;
			Life = 2;
			SP = 2;
			AddTimeRndMax = 0;
			AddTimeStatic = 0;
			Timer = 0;
			Temp = (TempW = (TempM = 0));
			BossMode = (StageCleared = (Specialing = (Reloading = (GameEnd = (FadeOut = (NoDamage = false))))));
			if(GameCommon.DNGST_GameMode == GameMode.RANDOM_ALL) {
				Stage = "一期一会のだんごたち ～R～";
				StageId = "RANDOM_ALL";
			}
			if(GameCommon.DNGST_GameMode == GameMode.RANDOM_SAME) {
				Stage = "だんごの内乱 ～R～";
				StageId = "RANDOM_SAME";
			}
			if(GameCommon.DNGST_GameMode == GameMode.RANDOM_BURN) {
				Stage = "焼きすぎだんご ～R～";
				StageId = "RANDOM_BURN";
			}
			if(GameCommon.DNGST_GameMode == GameMode.RANDOM_FAST) {
				Stage = "だんご速射砲 ～R～";
				StageId = "RANDOM_FAST";
				for(int i = 0; i < Enum.GetNames(typeof(DangoID)).Length; i++) {
					int num = 2;
					int num2 = 4;
					DangoMaxSpeeds[i] *= num;
					DangoBulletSpeeds[i] *= num;
					DangoEnemyMaxSpeeds[i] *= num2;
					DangoEnemyBulletSpeeds[i] *= num2;
					DangoEnemyCoolingDowns[i] /= num2;
				}
			}
			switch(GameCommon.DNGST_DiffLv) {
				case DiffLv.EASY:
					AddTimeRndMax = 60;
					AddTimeStatic = 100;
					DiffS = "EASY";
					break;
				case DiffLv.STANDARD:
					AddTimeRndMax = 50;
					AddTimeStatic = 50;
					DiffS = "NORMAL";
					break;
				case DiffLv.HARD:
					AddTimeRndMax = 30;
					AddTimeStatic = 25;
					DiffS = "HARD";
					break;
				case DiffLv.SPECIAL:
					AddTimeRndMax = 15;
					AddTimeStatic = 0;
					DiffS = "SPECIAL";
					break;
			}
			Diff = (int)GameCommon.DNGST_DiffLv;
			Texture.SetFont("Consolas");
			Texture.SetTextSize(18);
			Texture.SetTextColor(255, 255, 255);
			TStage = Texture.CreateFromText(string.Format("STAGE: {0}\u3000\u3000\u3000", Stage));
			TDiff = Texture.CreateFromText(string.Format(" TYPE: {0}\u3000\u3000\u3000", DiffS));
			TScore = Texture.CreateFromText(string.Format("SCORE: {0:D10}\u3000\u3000\u3000", Score));
			TLife = Texture.CreateFromText(string.Format("DANGO: {0}\u3000\u3000\u3000", Life));
			TSP = Texture.CreateFromText(string.Format("   SP: {0}\u3000\u3000\u3000", SP));
			AddWaitCount = (FramesCount = (LoadingState = 0));
			Loading = (First = true);
			for(int j = 0; j < LoadingBG.Length; j++) {
				LoadingBG[j] = Texture.CreateFromFile("Loading" + j + ".png");
			}
			DynBGPos = 0;
			DynBGA = 25;
			DynBGSpeed = 5;
			AddWaiter = 0;
			return ContentReturn.OK;
		}

		public static void MoveBullet(BulletData BD) {
			BD.Count++;
			if(!BD.Enemy) {
				if(!BD.Special) {
					BD.X += DangoBulletSpeeds[(int)BD.ID];
				} else {
					BD.X += 8;
				}
			} else if(BD.ID == DangoID.Pink) {
				BD.X -= DangoEnemyBulletSpeeds[(int)BD.ID];
				BD.Y = (int)(Math.Sin((double)BD.Count / 30.0) * 60.0) + BD.BaseY;
			} else if(BD.ID == DangoID.Blue) {
				BD.X -= DangoEnemyBulletSpeeds[(int)BD.ID];
			} else if(BD.ID == DangoID.Green) {
				BD.BaseX -= DangoEnemyBulletSpeeds[(int)BD.ID];
				int num = 1;
				int num2 = 2;
				BD.X = (int)(Math.Sin((double)num * (double)BD.Count / 50.0) * 120.0) + BD.BaseX;
				BD.Y = (int)(Math.Sin((double)num2 * (double)BD.Count / 50.0) * 120.0) + BD.BaseY;
			} else if(BD.ID == DangoID.Yellow) {
				BD.BaseX -= DangoEnemyBulletSpeeds[(int)BD.ID];
				int num3 = (int)(Math.Sin((double)BD.Count / 100.0 * 3.0) * 100.0);
				BD.X = (int)(Math.Cos((double)BD.Count / 20.0) * (double)num3) + BD.BaseX;
				BD.Y = (int)(Math.Sin((double)BD.Count / 20.0) * (double)num3) + BD.BaseY;
			} else if(BD.ID == DangoID.Black) {
				BD.BaseX -= DangoEnemyBulletSpeeds[(int)BD.ID];
				int num4 = 64;
				BD.X = (int)(Math.Cos((double)BD.Count / 10.0) * (double)num4) + BD.BaseX;
				BD.Y = (int)(Math.Sin((double)BD.Count / 10.0) * (double)num4) + BD.BaseY;
			} else if(BD.ID == DangoID.Burn) {
				BD.X -= DangoEnemyBulletSpeeds[(int)BD.ID];
				BD.Y += rnd.Next(5) - 2;
			}
		}

		public static void MoveChar(CharData CD) {
			CD.Count++;
			if(CD.ID == DangoID.Pink) {
				CD.X -= DangoEnemyMaxSpeeds[(int)CD.ID];
				CD.Y = (int)(Math.Sin((double)CD.Count / 30.0) * 60.0) + CD.BaseY;
			} else if(CD.ID == DangoID.Blue) {
				CD.X -= DangoEnemyMaxSpeeds[(int)CD.ID];
			} else if(CD.ID == DangoID.Green) {
				CD.BaseX -= DangoEnemyMaxSpeeds[(int)CD.ID];
				int num = 1;
				int num2 = 2;
				CD.X = (int)(Math.Sin((double)num * (double)CD.Count / 50.0) * 120.0) + CD.BaseX;
				CD.Y = (int)(Math.Sin((double)num2 * (double)CD.Count / 50.0) * 120.0) + CD.BaseY;
			} else if(CD.ID == DangoID.Yellow) {
				CD.BaseX -= DangoEnemyMaxSpeeds[(int)CD.ID];
				int num3 = (int)(Math.Sin((double)CD.Count / 100.0 * 3.0) * 100.0);
				CD.X = (int)(Math.Cos((double)CD.Count / 20.0) * (double)num3) + CD.BaseX;
				CD.Y = (int)(Math.Sin((double)CD.Count / 20.0) * (double)num3) + CD.BaseY;
			} else if(CD.ID == DangoID.Black) {
				CD.BaseX -= DangoEnemyMaxSpeeds[(int)CD.ID];
				int num4 = 64;
				CD.X = (int)(Math.Cos((double)CD.Count / 10.0) * (double)num4) + CD.BaseX;
				CD.Y = (int)(Math.Sin((double)CD.Count / 10.0) * (double)num4) + CD.BaseY;
			} else if(CD.ID == DangoID.Burn) {
				CD.X -= DangoEnemyMaxSpeeds[(int)CD.ID];
				CD.Y += rnd.Next(5) - 2;
			}
		}

		public static void AddCharRandom() {
			int num = rnd.Next((DangoTypes - 1) * 100);
			num = num % (DangoTypes - 1) + 1;
			if(num == (int)GameCommon.DNGST_SelectedDangoID) {
				num = rnd.Next(DangoTypes - 1) + 1;
			}
			if(num == (int)GameCommon.DNGST_SelectedDangoID) {
				num = rnd.Next(DangoTypes - 1) + 1;
			}
			AddChar((DangoID)num, true, 1280, rnd.Next(740));
		}

		public static void GameInit() {
			Char[0] = new CharData(GameCommon.DNGST_SelectedDangoID, false, 20, 260);
		}

		public static ContentReturn Main(DangoID DID) {
			First = true;
			if(Loading) {
				Core.Draw(LoadingBG[LoadingState / 2 % 4], 920, 560);
				switch(LoadingState) {
					case 0:
						BG = Texture.CreateFromFile("GameBG.png");
						break;
					case 1:
						DynBG = Texture.CreateFromFile("DynamicBG.png");
						break;
					case 2:
						BulletSES.LoadFile("BulletS.wav");
						BulletSEE.LoadFile("BulletE.wav");
						break;
					case 3:
						BurnSE.LoadFile("DNGBurn.wav");
						break;
					case 4:
						GameBGM.LoadFile("BGM_Game.wav");
						break;
					case 5:
						BossBGM.LoadFile("BGM_Boss.wav");
						break;
					case 6:
						GameOverSE.LoadFile("DNGGO.wav");
						break;
					case 7:
						AddSE.LoadFile("DNGAdd.wav");
						break;
					case 8:
						Effects[1] = Texture.CreateFromFile("HitEffect.png");
						break;
					case 9:
						Effects[2] = Texture.CreateFromFile("HitEffectEx.png");
						break;
					case 10:
						DangoL[1] = Texture.CreateFromFile("DngL/DangoPink.png");
						break;
					case 11:
						DangoL[2] = Texture.CreateFromFile("DngL/DangoWater.png");
						break;
					case 12:
						DangoL[3] = Texture.CreateFromFile("DngL/DangoGreen.png");
						break;
					case 13:
						DangoL[4] = Texture.CreateFromFile("DngL/DangoBlack.png");
						break;
					case 14:
						DangoL[5] = Texture.CreateFromFile("DngL/DangoYellow.png");
						break;
					case 15:
						DangoL[6] = Texture.CreateFromFile("DngL/DangoBurn.png");
						break;
					case 16:
						DangoR[1] = Texture.CreateFromFile("DngR/DangoPink.png");
						break;
					case 17:
						DangoR[2] = Texture.CreateFromFile("DngR/DangoWater.png");
						break;
					case 18:
						DangoR[3] = Texture.CreateFromFile("DngR/DangoGreen.png");
						break;
					case 19:
						DangoR[4] = Texture.CreateFromFile("DngR/DangoBlack.png");
						break;
					case 20:
						DangoR[5] = Texture.CreateFromFile("DngR/DangoYellow.png");
						break;
					case 21:
						DangoR[6] = Texture.CreateFromFile("DngR/DangoBurn.png");
						break;
					case 22:
						DangoBulletL[1] = Texture.CreateFromFile("DngLB/DangoPink.png");
						break;
					case 23:
						DangoBulletL[2] = Texture.CreateFromFile("DngLB/DangoWater.png");
						break;
					case 24:
						DangoBulletL[3] = Texture.CreateFromFile("DngLB/DangoGreen.png");
						break;
					case 25:
						DangoBulletL[4] = Texture.CreateFromFile("DngLB/DangoBlack.png");
						break;
					case 26:
						DangoBulletL[5] = Texture.CreateFromFile("DngLB/DangoYellow.png");
						break;
					case 27:
						DangoBulletL[6] = Texture.CreateFromFile("DngLB/DangoBurn.png");
						break;
					case 28:
						DangoBulletR[1] = Texture.CreateFromFile("DngRB/DangoPink.png");
						break;
					case 29:
						DangoBulletR[2] = Texture.CreateFromFile("DngRB/DangoWater.png");
						break;
					case 30:
						DangoBulletR[3] = Texture.CreateFromFile("DngRB/DangoGreen.png");
						break;
					case 31:
						DangoBulletR[4] = Texture.CreateFromFile("DngRB/DangoBlack.png");
						break;
					case 32:
						DangoBulletR[5] = Texture.CreateFromFile("DngRB/DangoYellow.png");
						break;
					case 33:
						DangoBulletR[6] = Texture.CreateFromFile("DngRB/DangoBurn.png");
						break;
					case 34:
						DangoLargeL = Texture.CreateFromFile(DangoLargeLFile);
						DangoLargeLBurn = Texture.CreateFromFile("DngL/DangoBurnLarge.png");
						break;
					case 35:
						DangoLargeR = Texture.CreateFromFile(DangoLargeRFile);
						break;
					case 36:
						SpecialSE.LoadFile("DNGSP.wav");
						break;
					case 37:
						ErrorSE.LoadFile("DNGErr.wav");
						break;
					case 38:
						ChangeSE.LoadFile("DNGOut.wav");
						break;
					case 39:
						ClearSE.LoadFile("BGM_Result.wav");
						break;
					case 40:
						GameInit();
						break;
					case 41:
						GameBGM.Play();
						break;
					case 42:
						Loading = false;
						break;
				}
				LoadingState++;
				return ContentReturn.OK;
			}
			if(Score != OldScore) {
				TScore = Texture.CreateFromText(string.Format("SCORE: {0:D10}\u3000\u3000\u3000", Score));
				OldScore = Score;
			}
			Core.Draw(BG, 0, 0, 255);
			Common.GSprite.DrawEx(DynBG, Common.WindowX - DynBGPos, 0, DynBGA, 0, 0, 1280, 720);
			Common.GSprite.DrawEx(DynBG, Common.WindowX - DynBGPos - 1280, 0, DynBGA, 0, 0, 1280, 720);
			DynBGPos += DynBGSpeed;
			if(DynBGPos > 1280) {
				DynBGPos = 0;
			}
			if(AddWaiter <= 0 && !BossMode) {
				if(GameCommon.DNGST_GameMode == GameMode.RANDOM_ALL) {
					AddCharRandom();
				}
				if(GameCommon.DNGST_GameMode == GameMode.RANDOM_SAME) {
					AddChar(GameCommon.DNGST_SelectedDangoID, true, 1280, rnd.Next(740));
				}
				if(GameCommon.DNGST_GameMode == GameMode.RANDOM_BURN) {
					AddChar(DangoID.Burn, true, 1280, rnd.Next(740));
				}
				if(GameCommon.DNGST_GameMode == GameMode.RANDOM_FAST) {
					AddCharRandom();
				}
				AddWaiter = (rnd.Next(AddTimeRndMax * 5) + 1) % AddTimeRndMax + AddTimeStatic;
				Debug.Log('I', "AddDango", "AddedDango: Next: {0}", AddWaiter);
			}
			if(VIOEx.GetButtonOnce(0, VirtualIO.ButtonID.MENU) != 0) {
				GameEnd = true;
				Debug.Log('I', "GameEnd", "Abort");
			}
			if(Char[0] != null) {
				if(VirtualIO.GetButton(0, VirtualIO.ButtonID.DOWN) != 0) {
					if(VirtualIO.GetButton(0, VirtualIO.ButtonID.L) != 0) {
						Char[0].Y += Char[0].MaxSpeed / 2;
					} else {
						Char[0].Y += Char[0].MaxSpeed;
					}
				}
				if(VirtualIO.GetButton(0, VirtualIO.ButtonID.UP) != 0) {
					if(VirtualIO.GetButton(0, VirtualIO.ButtonID.L) != 0) {
						Char[0].Y -= Char[0].MaxSpeed / 2;
					} else {
						Char[0].Y -= Char[0].MaxSpeed;
					}
				}
				if(VirtualIO.GetButton(0, VirtualIO.ButtonID.RIGHT) != 0) {
					if(VirtualIO.GetButton(0, VirtualIO.ButtonID.L) != 0) {
						Char[0].X += Char[0].MaxSpeed / 2;
					} else {
						Char[0].X += Char[0].MaxSpeed;
					}
				}
				if(VirtualIO.GetButton(0, VirtualIO.ButtonID.LEFT) != 0) {
					if(VirtualIO.GetButton(0, VirtualIO.ButtonID.L) != 0) {
						Char[0].X -= Char[0].MaxSpeed / 2;
					} else {
						Char[0].X -= Char[0].MaxSpeed;
					}
				}
				if(VirtualIO.GetAnalog(0, VirtualIO.AnalogID.LY) < 0) {
					Char[0].Y += (VirtualIO.GetAnalog(0, VirtualIO.AnalogID.LY) + 128) / (127 / Char[0].MaxSpeed);
				}
				if(VirtualIO.GetAnalog(0, VirtualIO.AnalogID.LY) > 0) {
					Char[0].Y += (VirtualIO.GetAnalog(0, VirtualIO.AnalogID.LY) - 128) / (127 / Char[0].MaxSpeed);
				}
				if(VirtualIO.GetAnalog(0, VirtualIO.AnalogID.LX) < 0) {
					Char[0].X += (VirtualIO.GetAnalog(0, VirtualIO.AnalogID.LX) + 128) / (127 / Char[0].MaxSpeed);
				}
				if(VirtualIO.GetAnalog(0, VirtualIO.AnalogID.LX) > 0) {
					Char[0].X += (VirtualIO.GetAnalog(0, VirtualIO.AnalogID.LX) - 128) / (127 / Char[0].MaxSpeed);
				}
				if(Char[0].X <= 0) {
					Char[0].X = 0;
				}
				if(Char[0].X >= Common.WindowX - 64) {
					Char[0].X = Common.WindowX - 64;
				}
				if(Char[0].Y <= 0) {
					Char[0].Y = 0;
				}
				if(Char[0].Y >= Common.WindowY - 64) {
					Char[0].Y = Common.WindowY - 64;
				}
				if(VIOEx.GetButtonOnce(0, VirtualIO.ButtonID.OK) != 0) {
					AddBullet(Char[0].ID, false, Char[0].X + 52, Char[0].Y + 32);
					Char[0].CoolingDown = DangoCoolingDowns[(int)Char[0].ID];
				}
				if(VirtualIO.GetButton(0, VirtualIO.ButtonID.OK) != 0 && Char[0].CoolingDown == 0) {
					AddBullet(Char[0].ID, false, Char[0].X + 52, Char[0].Y + 32);
					Char[0].CoolingDown = DangoCoolingDowns[(int)Char[0].ID];
				}
				if(VIOEx.GetButtonOnce(0, VirtualIO.ButtonID.CANCEL) != 0 && !Specialing) {
					if(SP > 0) {
						int num = AddBullet(Char[0].ID, false, -512, 0);
						Bullet[num].T = DangoLargeR;
						Bullet[num].Special = true;
						SpecialSE.Play();
						Specialing = true;
						Score += 10000;
						SP--;
						TSP = Texture.CreateFromText(string.Format("   SP: {0}\u3000\u3000\u3000", SP));
					} else {
						ErrorSE.Play();
					}
				}
			} else if(Life > 0) {
				if(Reloading) {
					if(Timer <= 0) {
						Life--;
						Char[0] = new CharData(GameCommon.DNGST_SelectedDangoID, false, 20, 260);
						TLife = Texture.CreateFromText(string.Format("DANGO: {0}\u3000\u3000\u3000", Life));
						AddSE.Play();
						Reloading = false;
						NoDamage = true;
						Char[0].A = 128;
						Timer = 300;
						SP = 2;
						TSP = Texture.CreateFromText(string.Format("   SP: {0}\u3000\u3000\u3000", SP));
						BurnSE.Play();
						for(int i = 0; i < Bullet.Length; i++) {
							if(Bullet[i] != null) {
								AddEffect(EffectID.Hit, Bullet[i].X - 8, Bullet[i].Y - 8);
								Bullet[i] = null;
								Score += 100;
							}
						}
						for(int j = 2; j < Char.Length; j++) {
							if(Char[j] != null) {
								AddEffect(EffectID.HitEx, Char[j].X - 16, Char[j].Y - 16);
								Char[j] = null;
								Score += 1000;
							}
						}
					}
				} else {
					Timer = 60;
					Reloading = true;
				}
			} else {
				GameEnd = true;
				Debug.Log('I', "GameEnd", "No Life");
			}
			for(int k = 0; k < Char.Length; k++) {
				if(Char[k] != null && Char[k].Enemy && !Char[k].Boss) {
					MoveChar(Char[k]);
					if(Char[k].CoolingDown == 0) {
						AddBullet(Char[k].ID, true, Char[k].X - 16, Char[k].Y + 32);
						Char[k].CoolingDown = DangoEnemyCoolingDowns[(int)Char[k].ID];
					}
				}
			}
			for(int l = 0; l < Bullet.Length; l++) {
				if(Bullet[l] == null) {
					continue;
				}
				MoveBullet(Bullet[l]);
				if(!Bullet[l].Special) {
					continue;
				}
				for(int m = 0; m < Bullet.Length; m++) {
					if(Bullet[m] != null && !Bullet[m].Effect && l != m && Bullet[m].X <= Bullet[l].X + 960 && Bullet[m].Enemy) {
						AddEffect(EffectID.Hit, Bullet[m].X - 8, Bullet[m].Y - 8);
						Bullet[m] = null;
						Score += 100;
					}
				}
				for(int n = 2; n < Char.Length; n++) {
					if(Char[n] != null && Char[n].X <= Bullet[l].X + 960) {
						AddEffect(EffectID.HitEx, Char[n].X - 16, Char[n].Y - 16);
						BurnSE.Play();
						Char[n] = null;
						Score += 1000;
					}
				}
			}
			for(int num2 = 0; num2 < Char.Length; num2++) {
				if(Char[num2] == null || Char[num2].Boss) {
					continue;
				}
				for(int num3 = 0; num3 < Bullet.Length; num3++) {
					if(Bullet[num3] == null || Char[num2].Enemy == Bullet[num3].Enemy || Bullet[num3].Effect) {
						continue;
					}
					int num4;
					if(Char[num2].Enemy) {
						num4 = 1296;
					} else {
						num4 = 576;
						if(NoDamage) {
							continue;
						}
					}
					int num5 = Char[num2].X + 32 - (Bullet[num3].X + 12);
					int num6 = Char[num2].Y + 32 - (Bullet[num3].Y + 12);
					int num7 = num5 * num5 + num6 * num6;
					if(num7 < num4) {
						Char[num2].HP--;
						Debug.Log('I', "Hit Checker", "Hit: {0}", num3);
						AddEffect(EffectID.Hit, Bullet[num3].X - 8, Bullet[num3].Y - 8);
						if(Char[num2].Enemy) {
							Score += 100;
						}
						Bullet[num3] = null;
					}
				}
			}
			for(int num8 = 0; num8 < Char.Length; num8++) {
				if(Char[num8] != null) {
					Core.Draw(Char[num8].T, Char[num8].X, Char[num8].Y, Char[num8].A);
					if(Char[num8].CoolingDown > 0) {
						Char[num8].CoolingDown--;
					}
				}
			}
			for(int num9 = 0; num9 < Bullet.Length; num9++) {
				if(Bullet[num9] == null) {
					continue;
				}
				Core.Draw(Bullet[num9].T, Bullet[num9].X, Bullet[num9].Y, Bullet[num9].A);
				if(Bullet[num9].Effect) {
					Bullet[num9].A = Bullet[num9].A - 24;
					if(Bullet[num9].A <= 0) {
						Bullet[num9] = null;
						Debug.Log('I', "Object Deleter", "Deleted Effect: {0}", num9);
					}
				}
			}
			for(int num10 = 0; num10 < Char.Length; num10++) {
				if(Char[num10] != null) {
					if(-256 > Char[num10].X || Char[num10].X > Common.WindowX + 256 || -256 > Char[num10].Y || Char[num10].Y > Common.WindowY + 256) {
						Char[num10] = null;
						Debug.Log('I', "Object Deleter", "Deleted Char: {0} by gone", num10);
					} else if(Char[num10].HP <= 0) {
						BurnSE.Play();
						Score += 1000;
						Score += DangoEnemyScores[(int)Char[num10].ID];
						AddEffect(EffectID.HitEx, Char[num10].X - 16, Char[num10].Y - 16);
						Char[num10] = null;
						Debug.Log('I', "Object Deleter", "Deleted Char: {0} by Life", num10);
					}
				}
			}
			for(int num11 = 0; num11 < Bullet.Length; num11++) {
				if(Bullet[num11] != null && (-64 > Bullet[num11].X || Bullet[num11].X > Common.WindowX || 0 > Bullet[num11].Y || Bullet[num11].Y > Common.WindowY + 64)) {
					if(!Bullet[num11].Special) {
						Bullet[num11] = null;
						Debug.Log('I', "Object Deleter", "Deleted Bullet: {0}", num11);
					} else if(Bullet[num11].X > Common.WindowX) {
						Bullet[num11] = null;
						Specialing = false;
						Debug.Log('I', "Object Deleter", "Deleted Special Bullet: {0}", num11);
					}
				}
			}
			if(NoDamage && Timer <= 0) {
				NoDamage = false;
				Char[0].A = 255;
				ChangeSE.Play();
			}
			if(FramesCount >= 3200) {
				if(!BossMode) {
					BossMode = true;
					GameBGM.Close();
					BossBGM.Play();
					if(GameCommon.DNGST_GameMode == GameMode.RANDOM_ALL) {
						Char[1] = new CharData(GameCommon.DNGST_SelectedDangoID, true, 900, 250);
						Char[1].T = DangoLargeL;
					}
					if(GameCommon.DNGST_GameMode == GameMode.RANDOM_SAME) {
						Char[1] = new CharData(GameCommon.DNGST_SelectedDangoID, true, 900, 250);
						Char[1].T = DangoLargeL;
					}
					if(GameCommon.DNGST_GameMode == GameMode.RANDOM_BURN) {
						Char[1] = new CharData(DangoID.Burn, true, 900, 250);
						Char[1].T = DangoLargeLBurn;
					}
					if(GameCommon.DNGST_GameMode == GameMode.RANDOM_FAST) {
						Char[1] = new CharData(GameCommon.DNGST_SelectedDangoID, true, 900, 250);
						Char[1].T = DangoLargeL;
					}
					Char[1].MaxHP = (Char[1].HP = 100);
					Char[1].Boss = true;
					Char[1].Count = -1;
					switch(GameCommon.DNGST_DiffLv) {
						case DiffLv.EASY:
							BossAddTimeRndMax = 60;
							BossAddTimeStatic = 100;
							BossMaxWTypes = 4;
							Char[1].MaxHP = (Char[1].HP = 100);
							break;
						case DiffLv.STANDARD:
							BossAddTimeRndMax = 50;
							BossAddTimeStatic = 50;
							BossMaxWTypes = 7;
							Char[1].MaxHP = (Char[1].HP = 150);
							break;
						case DiffLv.HARD:
							BossAddTimeRndMax = 30;
							BossAddTimeStatic = 25;
							BossMaxWTypes = 9;
							Char[1].MaxHP = (Char[1].HP = 200);
							break;
						case DiffLv.SPECIAL:
							BossAddTimeRndMax = 15;
							BossAddTimeStatic = 0;
							BossMaxWTypes = 9;
							Char[1].MaxHP = (Char[1].HP = 255);
							break;
					}
					Timer = rnd.Next(BossAddTimeRndMax) + BossAddTimeStatic;
				} else if(Char[1] != null) {
					Common.GSprite.DrawEx(White, 20, 20, 255, 0, 0, Char[1].HP * 1000 / Char[1].MaxHP * 600 / 1000, 4);
					if(Char[1].X >= 650 && Char[1].Count == -1) {
						Char[1].X -= 2;
						Char[1].BaseX = Char[1].X;
						Char[1].BaseY = Char[1].Y;
					} else {
						if(Char[1].X == 650 && Char[1].Y == 150) {
							TempM = rnd.Next(1);
							Debug.Log('I', "Boss", "Home Pos");
							Char[1].Count = 1;
						}
						if(TempM == 0) {
							int num12 = 2;
							int num13 = 3;
							Char[1].BaseX = (int)(Math.Sin((double)num12 * (double)Char[1].Count / 60.0) * 80.0) + 650;
							Char[1].X = (int)(Math.Sin((double)num12 * (double)Char[1].Count / 60.0) * 160.0) + Char[1].BaseX;
							Char[1].Y = (int)(Math.Sin((double)num13 * (double)Char[1].Count / 60.0) * 160.0) + Char[1].BaseY;
						}
						if(TempM == 1) {
							int num14 = (int)(Math.Sin((double)Char[1].Count / 100.0 * 3.0) * 100.0);
							Char[1].X = (int)(Math.Cos((double)Char[1].Count / 20.0) * (double)num14) * 120 + Char[1].BaseX;
							Char[1].Y = (int)(Math.Sin((double)Char[1].Count / 20.0) * (double)num14) * 120 + Char[1].BaseY;
						}
						Char[1].Count++;
						if(Timer <= 0) {
							int num15 = rnd.Next(BossMaxWTypes);
							TempW = 0;
							TempM = 0;
							switch(num15) {
								case 0:
									AddChar(Char[1].ID, true, Char[1].X, Char[1].Y + 128);
									break;
								case 1:
									AddChar(Char[1].ID, true, Char[1].X, Char[1].Y + 128);
									break;
								case 2:
									AddChar(Char[1].ID, true, Char[1].X, Char[1].Y + 128);
									break;
								case 3:
									TempW = 1;
									Temp = 10;
									break;
								case 4:
									TempW = 1;
									Temp = 20;
									break;
								case 5:
									TempW = 1;
									Temp = 40;
									break;
								case 6:
									TempW = 1;
									Temp = 40;
									break;
								case 7:
									TempW = 1;
									Temp = 60;
									break;
								case 8:
									TempW = 2;
									Temp = 20;
									break;
								default:
									AddBullet(Char[1].ID, true, Char[1].X, Char[1].Y + 128);
									break;
							}
							Timer = rnd.Next(BossAddTimeRndMax) + BossAddTimeStatic;
						}
						if(TempW == 1 && Temp >= 0 && Temp % 2 == 0) {
							AddBullet(Char[1].ID, true, Char[1].X, Char[1].Y + 128);
						}
						if(TempW == 2 && Temp >= 0 && Temp % 2 == 0) {
							AddChar(Char[1].ID, true, Char[1].X, Char[1].Y + 128);
						}
						Temp--;
						if(AddWaiter <= 0) {
							AddChar(Char[1].ID, true, 1280, rnd.Next(720));
							AddWaiter = (rnd.Next(AddTimeRndMax * 5) + 1) % AddTimeRndMax + AddTimeStatic;
							Debug.Log('I', "AddDango", "AddedDango: Next: {0}", AddWaiter);
						}
						int num16 = 7569;
						for(int num17 = 0; num17 < Bullet.Length; num17++) {
							if(Bullet[num17] != null && !Bullet[num17].Enemy && !Bullet[num17].Effect) {
								int num18 = Char[1].X + 128 - (Bullet[num17].X + 12);
								int num19 = Char[1].Y + 128 - (Bullet[num17].Y + 12);
								int num20 = num18 * num18 + num19 * num19;
								if(num20 < num16) {
									Char[1].HP--;
									Debug.Log('I', "Hit Checker", "Boss Hit: {0}", num17);
									AddEffect(EffectID.HitEx, Bullet[num17].X - 8, Bullet[num17].Y - 8);
									Score += 100;
									Bullet[num17] = null;
								}
							}
						}
					}
				} else {
					Debug.Log('I', "GameEnd", "Boss Died");
					StageCleared = true;
					GameEnd = true;
				}
			}
			Core.Draw(TStage, 970, 20, 255);
			Core.Draw(TDiff, 970, 50, 255);
			Core.Draw(TScore, 970, 80, 255);
			Core.Draw(TLife, 970, 110, 255);
			Core.Draw(TSP, 970, 140, 255);
			FramesCount++;
			AddWaiter--;
			Timer--;
			if(!GameEnd) {
				Effect.Fadein();
				return ContentReturn.OK;
			}
			if(!FadeOut) {
				GameBGM.Close();
				BossBGM.Close();
				if(StageCleared) {
					ClearSE.Play();
				} else {
					GameOverSE.Play();
				}
				Effect.Reset();
				FadeOut = true;
			}
			if(Effect.Fadeout() == ContentReturn.END) {
				Scene.Set("Result");
				return ContentReturn.CHANGE;
			}
			return ContentReturn.OK;
		}

		public static int AddBullet(DangoID DID, bool IsEnemy, int X, int Y) {
			for(int i = 0; i < Bullet.Length; i++) {
				if(Bullet[i] == null) {
					Bullet[i] = new BulletData(DID, IsEnemy, X, Y);
					if(!IsEnemy) {
						BulletSES.Play();
					} else if(First) {
						BulletSEE.Play();
						First = false;
					}
					return i;
				}
			}
			return -1;
		}

		public static void AddEffect(EffectID EID, int X, int Y) {
			for(int i = 0; i < Bullet.Length; i++) {
				if(Bullet[i] == null) {
					Bullet[i] = new BulletData(DangoID.None, false, X, Y);
					Bullet[i].Effect = true;
					Bullet[i].T = Effects[(int)EID];
					break;
				}
			}
		}

		public static void AddChar(DangoID DID, bool IsEnemy, int X, int Y) {
			for(int i = 2; i < Char.Length; i++) {
				if(Char[i] == null) {
					Char[i] = new CharData(DID, IsEnemy, X, Y);
					break;
				}
			}
		}
	}
}
