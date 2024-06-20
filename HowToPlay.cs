using Lightness.Core;
using Lightness.Framework;
using Lightness.Graphic;
using Lightness.IO;
using Lightness.Media;
using Lightness.Resources;

namespace LEContents {
	public static class HowToPlay {
		public static Texture MenuTitleText = null;

		public static Texture HelpImageTex = null;

		public static Texture HelpHelpTex = null;

		public static Texture BG = null;

		public static bool ShowDetail = false;

		public static bool HelpInit = false;

		public static string HelpImage = "";

		public static VirtualIOEx VIOEx = new VirtualIOEx();

		public static SE CancelSE = new SE();

		public static Menu MainMenu = null;

		public static bool NowFadeOut = false;

		public static int Counter = 0;

		public static ContentReturn Initialize() {
			Effect.Reset();
			return InitializeEx();
		}

		public static ContentReturn InitializeEx() {
			NowFadeOut = false;
			BG = Texture.CreateFromFile("MainBG.png");
			CancelSE.LoadFile("DNGErr.wav");
			Texture.SetFont("Consolas");
			Texture.SetTextSize(32);
			Texture.SetTextColor(255, 255, 255);
			MenuTitleText = Texture.CreateFromText("How to play");
			MainMenu = new Menu("Meiryo", 26, 255, 255, 255, 255);
			MainMenu.SetPointer("DangoMenu.png");
			MainMenu.SetSE("Menu.wav", "DNGOut.wav");
			MainMenu.Add("Keyboard");
			MainMenu.Add("Arcade");
			MainMenu.Add("Controller");
			MainMenu.Add("Back");
			ShowDetail = false;
			HelpInit = false;
			HelpImage = "";
			return ContentReturn.OK;
		}

		public static ContentReturn Main() {
			Core.Draw(BG, 0, 0);
			if(VIOEx.GetButtonOnce(0, VirtualIO.ButtonID.CANCEL) != 0) {
				Scene.Set("Title");
				Effect.Reset();
				ShowDetail = false;
				NowFadeOut = true;
				CancelSE.Play();
			}
			if(!ShowDetail) {
				Core.Draw(MenuTitleText, 20, 20);
				ContentReturn contentReturn = MainMenu.Exec(320, 120);
				if(contentReturn == ContentReturn.END) {
					ShowDetail = true;
					switch(MainMenu.Selected) {
						case 0:
							HelpImage = "Help_Control.png";
							break;
						case 1:
							HelpImage = "Help_Control2.png";
							break;
						case 2:
							HelpImage = "Help_Control3.png";
							break;
						case 3:
							Scene.Set("Title");
							Effect.Reset();
							ShowDetail = false;
							NowFadeOut = true;
							break;
					}
				}
			} else if(!HelpInit) {
				HelpImageTex = Texture.CreateFromFile(HelpImage);
				Texture.SetFont("Meiryo");
				Texture.SetTextSize(24);
				Texture.SetTextColor(255, 255, 255);
				HelpHelpTex = Texture.CreateFromText("L Click / DECIDE button: Back");
				HelpInit = true;
				Counter = 0;
			} else {
				Core.Draw(HelpImageTex, 0, 0);
				int counter = Counter;
				int num = 30;
				Counter++;
				if(Counter > 60) {
					Counter = 0;
				}
				if(VIOEx.GetButtonOnce(0, VirtualIO.ButtonID.OK) != 0 || VIOEx.GetButtonOnce(0, VirtualIO.ButtonID.CANCEL) != 0 || VIOEx.GetButtonOnce(0, VirtualIO.ButtonID.START) != 0 || VIOEx.GetPointOnce(VirtualIO.PointID.L) != 0) {
					int selected = MainMenu.Selected;
					InitializeEx();
					MainMenu.Selected = selected;
				}
			}
			if(!NowFadeOut) {
				Effect.Fadein();
			} else if(Effect.Fadeout() == ContentReturn.END) {
				return ContentReturn.CHANGE;
			}
			GameCommon.DrawCInfo();
			return ContentReturn.OK;
		}
	}
}
