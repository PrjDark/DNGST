using Lightness.Core;
using Lightness.Framework;
using Lightness.Graphic;
using Lightness.IO;
using Lightness.Media;
using Lightness.Resources;
using System;
using System.Diagnostics;

namespace LEContents {
	public static class Title {
		public static Texture[] LoadingBG = new Texture[4];

		public static int LoadingState = 0;

		public static bool Loading = true;

		public static Texture VersionText = null;

		public static Texture NetStatText = null;

		public static Texture ICStatIcon = null;

		public static Texture BG = null;

		public static Texture HelpTextS = null;

		public static Texture HelpTextC = null;

		public static Texture[] DNG = null;

		public static int DNGn = 0;

		public static int FramesCount = 0;

		public static BGM TitleBGM = new BGM();

		public static SE CancelSE = new SE();

		public static VirtualIOEx VIOEx = new VirtualIOEx();

		public static bool NowFadeOut = false;

		public static int Mode = 0;

		public static Menu MainMenu = null;

		public static Menu GameMenu = null;

		public static Menu CharMenu = null;

		public static Menu DiffMenu = null;

		public static ContentReturn Initialize() {
			MediaCommon.CloseAll();
			Title.NowFadeOut = false;
			Title.Mode = 0;
			Title.DNGn = 0;
			Title.DNG = new Texture[7];
			Title.LoadingState = 0;
			Title.Loading = true;
			for (int i = 0; i < Title.LoadingBG.Length; i++) {
				Title.LoadingBG[i] = Texture.CreateFromFile("Loading" + i + ".png");
			}
			Texture.SetFont("Consolas");
			Texture.SetTextSize(20);
			Texture.SetTextColor(255, 255, 255);
			Title.VersionText = Texture.CreateFromText(GameCommon.Version.Get() + "*");
			Texture.SetFont("Meiryo");
			Texture.SetTextSize(20);
			Texture.SetTextColor(255, 255, 255);
			Title.HelpTextS = Texture.CreateFromText("ステージを選択してください");
			Title.HelpTextC = Texture.CreateFromText("370");
			Title.MainMenu = new Menu("Meiryo", 26, 255, 255, 255, 255);
			Title.MainMenu.Add("だんごうちほうだい");
			Title.MainMenu.Add("さくらのうちかた講座");
			Title.MainMenu.Add("オンラインランキング");
			Title.MainMenu.Add("設定");
			Title.MainMenu.Add("手動アップデート確認");
			Title.MainMenu.Add("終了");
			Title.GameMenu = new Menu("Meiryo", 26, 255, 255, 255, 255);
			Title.GameMenu.Add("一期一会のだんごたち R");
			Title.GameMenu.Add("だんごの内乱 R");
			Title.GameMenu.Add("焼きすぎだんご R");
			Title.GameMenu.Add("だんご速射砲 R");
			Title.GameMenu.Add("もどる");
			Title.DiffMenu = new Menu("Meiryo", 26, 255, 255, 255, 255);
			Title.DiffMenu.Add("EASY");
			Title.DiffMenu.Add("NORMAL");
			Title.DiffMenu.Add("HARD");
			Title.DiffMenu.Add("SPECIAL");
			Title.DiffMenu.Add("もどる");
			Title.CharMenu = new Menu("Meiryo", 26, 255, 255, 255, 255);
			Title.CharMenu.Add("\u3000さくら");
			Title.CharMenu.Add("\u3000みずき");
			Title.CharMenu.Add("\u3000ささこ");
			Title.CharMenu.Add("\u3000もちみ");
			Title.CharMenu.Add("\u3000ごまお");
			Title.CharMenu.Add("\u3000こげた");
			Title.CharMenu.Add("もどる");
			Title.FramesCount = 0;
			Effect.Reset();
			GameCommon.CheckNetworkStatus();
			if (GameCommon.NetworkStatus) {
				Title.NetStatText = Texture.CreateFromFile("DN_OK.png");
			} else {
				Title.NetStatText = Texture.CreateFromFile("DN_ERR.png");
			}
			Title.ICStatIcon = Texture.CreateFromFile("DNIC_ERR.png");
			return ContentReturn.OK;
		}

		public static ContentReturn Main() {
			if (Title.Loading) {
				GameCommon.DrawNetworkError();
				Core.Draw(Title.LoadingBG[Title.LoadingState % 4], 920, 560);
				switch (Title.LoadingState) {
					case 0:
						Title.DNG[0] = Texture.CreateFromFile("DngL/DangoPinkLarge.png");
						break;
					case 1:
						Title.DNG[1] = Texture.CreateFromFile("DngL/DangoWaterLarge.png");
						break;
					case 2:
						Title.DNG[2] = Texture.CreateFromFile("DngL/DangoGreenLarge.png");
						break;
					case 3:
						Title.DNG[3] = Texture.CreateFromFile("DngL/DangoYellowLarge.png");
						break;
					case 4:
						Title.DNG[4] = Texture.CreateFromFile("DngL/DangoBlackLarge.png");
						break;
					case 5:
						Title.DNG[5] = Texture.CreateFromFile("DngL/DangoBurnLarge.png");
						break;
					case 6:
						Title.DNG[6] = Texture.CreateFromText("\u3000");
						break;
					case 7:
						Title.CancelSE.LoadFile("DNGErr.wav");
						break;
					case 8:
						Title.TitleBGM.LoadFile("BGM_TitleA.wav");
						break;
					case 9:
						Title.BG = Texture.CreateFromFile("TitleBG.png");
						break;
					case 10:
						Title.MainMenu.SetPointer("DangoMenu.png");
						break;
					case 11:
						Title.MainMenu.SetSE("Menu.wav", "DNGOut.wav");
						break;
					case 12:
						Title.CharMenu.SetPointer("DangoMenu.png");
						break;
					case 13:
						Title.CharMenu.SetSE("Menu.wav", "DNGOut.wav");
						break;
					case 14:
						Title.GameMenu.SetPointer("DangoMenu.png");
						break;
					case 15:
						Title.GameMenu.SetSE("Menu.wav", "DNGOut.wav");
						break;
					case 16:
						Title.DiffMenu.SetPointer("DangoMenu.png");
						break;
					case 17:
						Title.DiffMenu.SetSE("Menu.wav", "DNGOut.wav");
						break;
					case 18:
						Title.TitleBGM.Play();
						break;
					case 19:
						Title.Loading = false;
						break;
				}
				Title.LoadingState++;
				return ContentReturn.OK;
			}
			Core.Draw(Title.BG, 0, 0);
			Core.Draw(Title.VersionText, 10, 10);
			Core.Draw(Title.NetStatText, 10, 35);
			Core.Draw(Title.ICStatIcon, 79, 35);
			if (Title.Mode == 0) {
				ContentReturn contentReturn = Title.MainMenu.Exec(760, 370);
				if (contentReturn == ContentReturn.CHANGE) {
					Title.FramesCount = 0;
				}
				if (contentReturn == ContentReturn.END) {
					Effect.Reset();
					Title.NowFadeOut = true;
					switch (Title.MainMenu.Selected) {
						case 0:
							Title.Mode = 1;
							Title.NowFadeOut = false;
							Effect.Level = 255;
							Menu.Disabled = false;
							return ContentReturn.OK;
						case 1:
							Scene.Set("HowToPlay");
							break;
						case 2:
							Scene.Set("Ranking");
							break;
						case 3:
							Scene.Set("Config");
							break;
						case 4:
							Process.Start("http://project.xprj.net/game/DNGST");
							Menu.Disabled = false;
							Environment.Exit(0);
							break;
						case 5:
							Scene.Set("GameEnd");
							break;
					}
				}
			}
			if (Title.Mode == 1) {
				Core.Draw(Title.HelpTextS, 700, 650);
				ContentReturn contentReturn = Title.GameMenu.Exec(760, 370);
				if (contentReturn == ContentReturn.END) {
					Effect.Reset();
					Title.NowFadeOut = true;
					Title.Mode = 2;
					switch (Title.GameMenu.Selected) {
						case 0:
							GameCommon.DNGST_GameMode = DNGST.GameMode.RANDOM_ALL;
							Title.NowFadeOut = false;
							Effect.Level = 255;
							Menu.Disabled = false;
							return ContentReturn.OK;
						case 1:
							GameCommon.DNGST_GameMode = DNGST.GameMode.RANDOM_SAME;
							Title.NowFadeOut = false;
							Effect.Level = 255;
							Menu.Disabled = false;
							return ContentReturn.OK;
						case 2:
							GameCommon.DNGST_GameMode = DNGST.GameMode.RANDOM_BURN;
							Title.NowFadeOut = false;
							Effect.Level = 255;
							Menu.Disabled = false;
							return ContentReturn.OK;
						case 3:
							GameCommon.DNGST_GameMode = DNGST.GameMode.RANDOM_FAST;
							Title.NowFadeOut = false;
							Effect.Level = 255;
							Menu.Disabled = false;
							return ContentReturn.OK;
						case 4:
							Title.Mode = 0;
							Title.NowFadeOut = false;
							Effect.Level = 255;
							Menu.Disabled = false;
							return ContentReturn.OK;
					}
				}
				if (Title.VIOEx.GetButtonOnce(0, VirtualIO.ButtonID.CANCEL) != 0) {
					Title.CancelSE.Play();
					Title.Mode = 0;
				}
			}
			if (Title.Mode == 2) {
				Core.Draw(Title.HelpTextS, 700, 650);
				ContentReturn contentReturn = Title.DiffMenu.Exec(760, 370);
				if (contentReturn == ContentReturn.END) {
					Effect.Reset();
					Title.NowFadeOut = true;
					Title.Mode = 3;
					switch (Title.DiffMenu.Selected) {
						case 0:
							GameCommon.DNGST_DiffLv = DNGST.DiffLv.EASY;
							Title.NowFadeOut = false;
							Effect.Level = 255;
							Menu.Disabled = false;
							return ContentReturn.OK;
						case 1:
							GameCommon.DNGST_DiffLv = DNGST.DiffLv.STANDARD;
							Title.NowFadeOut = false;
							Effect.Level = 255;
							Menu.Disabled = false;
							return ContentReturn.OK;
						case 2:
							GameCommon.DNGST_DiffLv = DNGST.DiffLv.HARD;
							Title.NowFadeOut = false;
							Effect.Level = 255;
							Menu.Disabled = false;
							return ContentReturn.OK;
						case 3:
							GameCommon.DNGST_DiffLv = DNGST.DiffLv.SPECIAL;
							Title.NowFadeOut = false;
							Effect.Level = 255;
							Menu.Disabled = false;
							return ContentReturn.OK;
						case 4:
							Title.Mode = 1;
							Title.NowFadeOut = false;
							Effect.Level = 255;
							Menu.Disabled = false;
							return ContentReturn.OK;
					}
				}
				if (Title.VIOEx.GetButtonOnce(0, VirtualIO.ButtonID.CANCEL) != 0) {
					Title.CancelSE.Play();
					Title.Mode = 1;
				}
			}
			if (Title.Mode == 3) {
				Core.Draw(Title.DNG[Title.DNGn], 950, 400);
				Core.Draw(Title.HelpTextC, 700, 650);
				ContentReturn contentReturn = Title.CharMenu.Exec(760, 370);
				if (contentReturn == ContentReturn.CHANGE) {
					switch (Title.CharMenu.Selected) {
						case 1:
							Title.DNGn = 1;
							break;
						case 2:
							Title.DNGn = 2;
							break;
						case 3:
							Title.DNGn = 3;
							break;
						case 4:
							Title.DNGn = 4;
							break;
						case 5:
							Title.DNGn = 5;
							break;
						case 6:
							Title.DNGn = 6;
							break;
						default:
							Title.DNGn = 0;
							break;
					}
				}
				if (contentReturn == ContentReturn.END) {
					Effect.Reset();
					Title.NowFadeOut = true;
					switch (Title.CharMenu.Selected) {
						case 0:
							Scene.Set("DNGST_X");
							GameCommon.DNGST_SelectedDangoID = DNGST.DangoID.Pink;
							break;
						case 1:
							Scene.Set("DNGST_X");
							GameCommon.DNGST_SelectedDangoID = DNGST.DangoID.Blue;
							break;
						case 2:
							Scene.Set("DNGST_X");
							GameCommon.DNGST_SelectedDangoID = DNGST.DangoID.Green;
							break;
						case 3:
							Scene.Set("DNGST_X");
							GameCommon.DNGST_SelectedDangoID = DNGST.DangoID.Yellow;
							break;
						case 4:
							Scene.Set("DNGST_X");
							GameCommon.DNGST_SelectedDangoID = DNGST.DangoID.Black;
							break;
						case 5:
							Scene.Set("DNGST_X");
							GameCommon.DNGST_SelectedDangoID = DNGST.DangoID.Burn;
							break;
						case 6:
							Title.Mode = 2;
							Title.NowFadeOut = false;
							Effect.Level = 255;
							Menu.Disabled = false;
							return ContentReturn.OK;
					}
				}
				if (Title.VIOEx.GetButtonOnce(0, VirtualIO.ButtonID.CANCEL) != 0) {
					Title.CancelSE.Play();
					Title.Mode = 2;
				}
			}
			if (!Title.NowFadeOut) {
				Effect.Fadein();
				Title.FramesCount++;
				if (Title.FramesCount > 1800 && Title.Mode == 0) {
					MediaCommon.CloseAll();
					Scene.Set("PDAdvertise");
					return ContentReturn.CHANGE;
				}
			} else if (Effect.Fadeout() == ContentReturn.END) {
				MediaCommon.CloseAll();
				return ContentReturn.CHANGE;
			}
			GameCommon.DrawNetworkError();
			GameCommon.DrawCInfo();
			return ContentReturn.OK;
		}
	}
}
