using Lightness.Core;
using Lightness.DNetwork;
using Lightness.Framework;
using Lightness.Graphic;
using Lightness.IO;
using Lightness.Resources;
using System.IO;
using System.Text;

namespace LEContents {
	public static class Result {
		public static Texture BG = null;

		public static Texture DNRegOK = null;

		public static Texture DNRegErr = null;

		public static Texture TStatus = null;

		public static Texture TStage = null;

		public static Texture TType = null;

		public static Texture TScore = null;

		public static Texture TClear = null;

		public static Texture TDango = null;

		public static Texture TSP = null;

		public static Texture TTotalScore = null;

		public static int TotalScore = 0;

		public static int ClearBonus = 0;

		public static int CoolDownFrame = 0;

		public static bool NowFadeOut = false;

		public static bool SuccessRegister = false;

		public static VirtualIOEx VIOEx = new VirtualIOEx();

		public static ContentReturn Initialize() {
			VIOEx.GetButtonOnce(0, VirtualIO.ButtonID.OK);
			CoolDownFrame = 30;
			BG = Texture.CreateFromFile("ResultBG.png");
			DNRegOK = Texture.CreateFromFile("DN_RegOK.png");
			DNRegErr = Texture.CreateFromFile("DN_RegErr.png");
			Effect.Reset();
			Texture.SetFont("Consolas");
			Texture.SetTextColor(255, 255, 255);
			Texture.SetTextSize(50);
			if(DNGST.StageCleared) {
				TStatus = Texture.CreateFromText("-Stage Cleared-");
				ClearBonus = 10000;
			} else {
				TStatus = Texture.CreateFromText("-Stage Failed-");
				ClearBonus = 0;
			}
			TotalScore = DNGST.Score + DNGST.Life * 25000 + DNGST.SP * 5000 + ClearBonus;
			TTotalScore = Texture.CreateFromText(string.Format("{0:D10}", TotalScore));
			Texture.SetTextSize(32);
			TType = Texture.CreateFromText(DNGST.DiffS);
			TScore = Texture.CreateFromText(string.Format("{0:D10}", DNGST.Score));
			TDango = Texture.CreateFromText(string.Format("{0} × 25000 = {1}", DNGST.Life, DNGST.Life * 25000));
			TSP = Texture.CreateFromText(string.Format("{0} × 5000 = {1}", DNGST.SP, DNGST.SP * 5000));
			TClear = Texture.CreateFromText(string.Format("{0}", ClearBonus));
			Texture.SetTextSize(28);
			TStage = Texture.CreateFromText(DNGST.Stage);
			ContentStream contentStream = new ContentStream("UserData.lec", true);
			StreamReader streamReader = new StreamReader(contentStream, Encoding.UTF8, true);
			string text = streamReader.ReadLine();
			contentStream.Close();
			if(text == null) {
				text = "";
			}
			string text2 = "*";
			try {
				ContentStream contentStream2 = new ContentStream("dNetworkKey.bin", false);
				StreamReader streamReader2 = new StreamReader(contentStream2, Encoding.ASCII, true);
				text2 = streamReader2.ReadLine();
				contentStream2.Close();
				if(text2 == null) {
					text2 = "";
				}
			} catch {
			}
			string uRL = string.Format("http://DNGST.network.dark-x.net/Ranking/Register?Version={0}&dNetworkKey={10}&Stage={1}&Type={2}&Score={3}&Clear={4}&Life={5}&SP={6}&TotalScore={7}&Dango={8}&UserName={9}", GameCommon.Version.Get(), DNGST.StageId, DNGST.Diff, DNGST.Score, DNGST.StageCleared, DNGST.Life, DNGST.SP, TotalScore, GameCommon.DNGST_SelectedDangoID, text, text2);
			if(text == "") {
				Config_PlayerName.ReturnToResult = true;
			}
			DNet dNet = new DNet(uRL);
			if(dNet.Status <= 350) {
				SuccessRegister = true;
			} else {
				SuccessRegister = false;
			}
			return ContentReturn.OK;
		}

		public static ContentReturn Main() {
			if(Config_PlayerName.ReturnToResult) {
				Scene.Set("Config_PlayerName");
				return ContentReturn.CHANGE;
			}
			Core.Draw(BG, 0, 0, 255);
			if(SuccessRegister) {
				Core.Draw(DNRegOK, 10, 670, 255);
			} else {
				Core.Draw(DNRegErr, 10, 670, 255);
			}
			Core.Draw(TStatus, 670, 30);
			Core.Draw(TStage, 670, 120);
			Core.Draw(TType, 670, 180);
			Core.Draw(TScore, 670, 250);
			Core.Draw(TClear, 670, 360);
			Core.Draw(TDango, 670, 420);
			Core.Draw(TSP, 670, 490);
			Core.Draw(TTotalScore, 670, 580);
			if(CoolDownFrame != 0) {
				CoolDownFrame--;
			}
			if(NowFadeOut) {
				if(Effect.Fadeout() == ContentReturn.END) {
					Scene.Set("Title");
					return ContentReturn.CHANGE;
				}
			} else if(Effect.Fadein() == ContentReturn.END) {
				if((VIOEx.GetButtonOnce(0, VirtualIO.ButtonID.CANCEL) != 0 || VIOEx.GetPointOnce(VirtualIO.PointID.L) != 0 || VIOEx.GetButtonOnce(0, VirtualIO.ButtonID.START) != 0) && CoolDownFrame == 0) {
					NowFadeOut = true;
					Effect.Reset();
				}
			} else {
				VIOEx.GetButtonOnce(0, VirtualIO.ButtonID.CANCEL);
				VIOEx.GetPointOnce(VirtualIO.PointID.L);
			}
			return ContentReturn.OK;
		}
	}
}
