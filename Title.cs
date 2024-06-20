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
			NowFadeOut = false;
			Mode = 0;
			DNGn = 0;
			DNG = new Texture[7];
			LoadingState = 0;
			Loading = true;
			for(int i = 0; i < LoadingBG.Length; i++) {
				LoadingBG[i] = Texture.CreateFromFile("Loading" + i + ".png");
			}
			Texture.SetFont("Consolas");
			Texture.SetTextSize(20);
			Texture.SetTextColor(255, 255, 255);
			VersionText = Texture.CreateFromText(GameCommon.Version.Get() + "*");
			Texture.SetFont("Meiryo");
			Texture.SetTextSize(20);
			Texture.SetTextColor(255, 255, 255);
			HelpTextS = Texture.CreateFromText("Select stage");
			HelpTextC = Texture.CreateFromText("Select \"Dango\"");
			MainMenu = new Menu("Meiryo", 26, 255, 255, 255, 255);
			MainMenu.Add("The STG");
			MainMenu.Add("How to play");
			MainMenu.Add("Online Ranking");
			MainMenu.Add("Settings");
			MainMenu.Add("Manually update check");
			MainMenu.Add("End Game");
			GameMenu = new Menu("Meiryo", 26, 255, 255, 255, 255);
			GameMenu.Add("Dango Mix  R");
			GameMenu.Add("Only 1 Dango R");
			GameMenu.Add("Dango Burnt R");
			GameMenu.Add("Dango Cannon R");
			GameMenu.Add("Back");
			DiffMenu = new Menu("Meiryo", 26, 255, 255, 255, 255);
			DiffMenu.Add("EASY");
			DiffMenu.Add("NORMAL");
			DiffMenu.Add("HARD");
			DiffMenu.Add("SPECIAL");
			DiffMenu.Add("Back");
			CharMenu = new Menu("Meiryo", 26, 255, 255, 255, 255);
			CharMenu.Add("  Sakura (Normal)");
			CharMenu.Add("  Mizuki (Water)");
			CharMenu.Add("  Sasako (Bamboo leaf)");
			CharMenu.Add("  Mochimi (Mochi)");
			CharMenu.Add("  Gomao (Sesame)");
			CharMenu.Add("  Kogeta (Burnt)");
			CharMenu.Add("Back");
			FramesCount = 0;
			Effect.Reset();
			GameCommon.CheckNetworkStatus();
			if(GameCommon.NetworkStatus) {
				NetStatText = Texture.CreateFromFile("DN_OK.png");
			} else {
				NetStatText = Texture.CreateFromFile("DN_ERR.png");
			}
			ICStatIcon = Texture.CreateFromFile("DNIC_ERR.png");
			return ContentReturn.OK;
		}

		public static ContentReturn Main() {
			if(Loading) {
				GameCommon.DrawNetworkError();
				Core.Draw(LoadingBG[LoadingState % 4], 920, 560);
				switch(LoadingState) {
					case 0:
						DNG[0] = Texture.CreateFromFile("DngL/DangoPinkLarge.png");
						break;
					case 1:
						DNG[1] = Texture.CreateFromFile("DngL/DangoWaterLarge.png");
						break;
					case 2:
						DNG[2] = Texture.CreateFromFile("DngL/DangoGreenLarge.png");
						break;
					case 3:
						DNG[3] = Texture.CreateFromFile("DngL/DangoYellowLarge.png");
						break;
					case 4:
						DNG[4] = Texture.CreateFromFile("DngL/DangoBlackLarge.png");
						break;
					case 5:
						DNG[5] = Texture.CreateFromFile("DngL/DangoBurnLarge.png");
						break;
					case 6:
						DNG[6] = Texture.CreateFromText("\u3000");
						break;
					case 7:
						CancelSE.LoadFile("DNGErr.wav");
						break;
					case 8:
						TitleBGM.LoadFile("BGM_TitleA.wav");
						break;
					case 9:
						BG = Texture.CreateFromFile("TitleBG.png");
						break;
					case 10:
						MainMenu.SetPointer("DangoMenu.png");
						break;
					case 11:
						MainMenu.SetSE("Menu.wav", "DNGOut.wav");
						break;
					case 12:
						CharMenu.SetPointer("DangoMenu.png");
						break;
					case 13:
						CharMenu.SetSE("Menu.wav", "DNGOut.wav");
						break;
					case 14:
						GameMenu.SetPointer("DangoMenu.png");
						break;
					case 15:
						GameMenu.SetSE("Menu.wav", "DNGOut.wav");
						break;
					case 16:
						DiffMenu.SetPointer("DangoMenu.png");
						break;
					case 17:
						DiffMenu.SetSE("Menu.wav", "DNGOut.wav");
						break;
					case 18:
						TitleBGM.Play();
						break;
					case 19:
						Loading = false;
						break;
				}
				LoadingState++;
				return ContentReturn.OK;
			}
			Core.Draw(BG, 0, 0);
			Core.Draw(VersionText, 10, 10);
			Core.Draw(NetStatText, 10, 35);
			Core.Draw(ICStatIcon, 79, 35);
			ContentReturn contentReturn = ContentReturn.OK;
			if(Mode == 0) {
				contentReturn = MainMenu.Exec(760, 370);
				if(contentReturn == ContentReturn.CHANGE) {
					FramesCount = 0;
				}
				if(contentReturn == ContentReturn.END) {
					Effect.Reset();
					NowFadeOut = true;
					switch(MainMenu.Selected) {
						case 0:
							Mode = 1;
							NowFadeOut = false;
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
			if(Mode == 1) {
				Core.Draw(HelpTextS, 700, 650);
				contentReturn = GameMenu.Exec(760, 370);
				int num = 4;
				if(contentReturn == ContentReturn.END) {
					Effect.Reset();
					NowFadeOut = true;
					Mode = 2;
					switch(GameMenu.Selected) {
						case 0:
							GameCommon.DNGST_GameMode = DNGST.GameMode.RANDOM_ALL;
							NowFadeOut = false;
							Effect.Level = 255;
							Menu.Disabled = false;
							return ContentReturn.OK;
						case 1:
							GameCommon.DNGST_GameMode = DNGST.GameMode.RANDOM_SAME;
							NowFadeOut = false;
							Effect.Level = 255;
							Menu.Disabled = false;
							return ContentReturn.OK;
						case 2:
							GameCommon.DNGST_GameMode = DNGST.GameMode.RANDOM_BURN;
							NowFadeOut = false;
							Effect.Level = 255;
							Menu.Disabled = false;
							return ContentReturn.OK;
						case 3:
							GameCommon.DNGST_GameMode = DNGST.GameMode.RANDOM_FAST;
							NowFadeOut = false;
							Effect.Level = 255;
							Menu.Disabled = false;
							return ContentReturn.OK;
						case 4:
							Mode = 0;
							NowFadeOut = false;
							Effect.Level = 255;
							Menu.Disabled = false;
							return ContentReturn.OK;
					}
				}
				if(VIOEx.GetButtonOnce(0, VirtualIO.ButtonID.CANCEL) != 0) {
					CancelSE.Play();
					Mode = 0;
				}
			}
			if(Mode == 2) {
				Core.Draw(HelpTextS, 700, 650);
				contentReturn = DiffMenu.Exec(760, 370);
				int num2 = 4;
				if(contentReturn == ContentReturn.END) {
					Effect.Reset();
					NowFadeOut = true;
					Mode = 3;
					switch(DiffMenu.Selected) {
						case 0:
							GameCommon.DNGST_DiffLv = DNGST.DiffLv.EASY;
							NowFadeOut = false;
							Effect.Level = 255;
							Menu.Disabled = false;
							return ContentReturn.OK;
						case 1:
							GameCommon.DNGST_DiffLv = DNGST.DiffLv.STANDARD;
							NowFadeOut = false;
							Effect.Level = 255;
							Menu.Disabled = false;
							return ContentReturn.OK;
						case 2:
							GameCommon.DNGST_DiffLv = DNGST.DiffLv.HARD;
							NowFadeOut = false;
							Effect.Level = 255;
							Menu.Disabled = false;
							return ContentReturn.OK;
						case 3:
							GameCommon.DNGST_DiffLv = DNGST.DiffLv.SPECIAL;
							NowFadeOut = false;
							Effect.Level = 255;
							Menu.Disabled = false;
							return ContentReturn.OK;
						case 4:
							Mode = 1;
							NowFadeOut = false;
							Effect.Level = 255;
							Menu.Disabled = false;
							return ContentReturn.OK;
					}
				}
				if(VIOEx.GetButtonOnce(0, VirtualIO.ButtonID.CANCEL) != 0) {
					CancelSE.Play();
					Mode = 1;
				}
			}
			if(Mode == 3) {
				Core.Draw(DNG[DNGn], 950, 400);
				Core.Draw(HelpTextC, 700, 650);
				contentReturn = CharMenu.Exec(760, 370);
				if(contentReturn == ContentReturn.CHANGE) {
					switch(CharMenu.Selected) {
						case 1:
							DNGn = 1;
							break;
						case 2:
							DNGn = 2;
							break;
						case 3:
							DNGn = 3;
							break;
						case 4:
							DNGn = 4;
							break;
						case 5:
							DNGn = 5;
							break;
						case 6:
							DNGn = 6;
							break;
						default:
							DNGn = 0;
							break;
					}
				}
				if(contentReturn == ContentReturn.END) {
					Effect.Reset();
					NowFadeOut = true;
					switch(CharMenu.Selected) {
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
							Mode = 2;
							NowFadeOut = false;
							Effect.Level = 255;
							Menu.Disabled = false;
							return ContentReturn.OK;
					}
				}
				if(VIOEx.GetButtonOnce(0, VirtualIO.ButtonID.CANCEL) != 0) {
					CancelSE.Play();
					Mode = 2;
				}
			}
			if(!NowFadeOut) {
				Effect.Fadein();
				FramesCount++;
				if(FramesCount > 1800 && Mode == 0) {
					MediaCommon.CloseAll();
					Scene.Set("PDAdvertise");
					return ContentReturn.CHANGE;
				}
			} else if(Effect.Fadeout() == ContentReturn.END) {
				MediaCommon.CloseAll();
				return ContentReturn.CHANGE;
			}
			GameCommon.DrawNetworkError();
			GameCommon.DrawCInfo();
			return ContentReturn.OK;
		}
	}
}
