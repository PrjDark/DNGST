using Lightness.Core;
using Lightness.DNetwork;
using Lightness.Framework;
using Lightness.Graphic;
using Lightness.IO;
using Lightness.Resources;
using System;
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
			Result.VIOEx.GetButtonOnce(0, VirtualIO.ButtonID.OK);
			Result.CoolDownFrame = 30;
			Result.BG = Texture.CreateFromFile("ResultBG.png");
			Result.DNRegOK = Texture.CreateFromFile("DN_RegOK.png");
			Result.DNRegErr = Texture.CreateFromFile("DN_RegErr.png");
			Effect.Reset();
			Texture.SetFont("Consolas");
			Texture.SetTextColor(255, 255, 255);
			Texture.SetTextSize(50);
			if (DNGST.StageCleared) {
				Result.TStatus = Texture.CreateFromText("-Stage Cleared-");
				Result.ClearBonus = 10000;
			} else {
				Result.TStatus = Texture.CreateFromText("-Stage Failed-");
				Result.ClearBonus = 0;
			}
			Result.TotalScore = DNGST.Score + DNGST.Life * 25000 + DNGST.SP * 5000 + Result.ClearBonus;
			Result.TTotalScore = Texture.CreateFromText(string.Format("{0:D10}", Result.TotalScore));
			Texture.SetTextSize(32);
			Result.TType = Texture.CreateFromText(DNGST.DiffS);
			Result.TScore = Texture.CreateFromText(string.Format("{0:D10}", DNGST.Score));
			Result.TDango = Texture.CreateFromText(string.Format("{0} × 25000 = {1}", DNGST.Life, DNGST.Life * 25000));
			Result.TSP = Texture.CreateFromText(string.Format("{0} × 5000 = {1}", DNGST.SP, DNGST.SP * 5000));
			Result.TClear = Texture.CreateFromText(string.Format("{0}", Result.ClearBonus));
			Texture.SetTextSize(28);
			Result.TStage = Texture.CreateFromText(DNGST.Stage);
			ContentStream contentStream = new ContentStream("UserData.lec", true);
			StreamReader streamReader = new StreamReader(contentStream, Encoding.UTF8, true);
			string text = streamReader.ReadLine();
			contentStream.Close();
			if (text == null) {
				text = "";
			}
			string text2 = "*";
			try {
				ContentStream contentStream2 = new ContentStream("dNetworkKey.bin", false);
				StreamReader streamReader2 = new StreamReader(contentStream2, Encoding.ASCII, true);
				text2 = streamReader2.ReadLine();
				contentStream2.Close();
				if (text2 == null) {
					text2 = "";
				}
			} catch {
			}
			string uRL = string.Format("http://DNGST.network.xprj.net/Ranking/Register?Version={0}&dNetworkKey={10}&Stage={1}&Type={2}&Score={3}&Clear={4}&Life={5}&SP={6}&TotalScore={7}&Dango={8}&UserName={9}", new object[]
			{
				GameCommon.Version.Get(),
				DNGST.StageId,
				DNGST.Diff,
				DNGST.Score,
				DNGST.StageCleared,
				DNGST.Life,
				DNGST.SP,
				Result.TotalScore,
				GameCommon.DNGST_SelectedDangoID,
				text,
				text2
			});
			if (text == "") {
				Config_PlayerName.ReturnToResult = true;
			}
			DNet dNet = new DNet(uRL);
			if (dNet.Status <= 350) {
				Result.SuccessRegister = true;
			} else {
				Result.SuccessRegister = false;
			}
			return ContentReturn.OK;
		}

		public static ContentReturn Main() {
			if (Config_PlayerName.ReturnToResult) {
				Scene.Set("Config_PlayerName");
				return ContentReturn.CHANGE;
			}
			Core.Draw(Result.BG, 0, 0, 255);
			if (Result.SuccessRegister) {
				Core.Draw(Result.DNRegOK, 10, 670, 255);
			} else {
				Core.Draw(Result.DNRegErr, 10, 670, 255);
			}
			Core.Draw(Result.TStatus, 670, 30);
			Core.Draw(Result.TStage, 670, 120);
			Core.Draw(Result.TType, 670, 180);
			Core.Draw(Result.TScore, 670, 250);
			Core.Draw(Result.TClear, 670, 360);
			Core.Draw(Result.TDango, 670, 420);
			Core.Draw(Result.TSP, 670, 490);
			Core.Draw(Result.TTotalScore, 670, 580);
			if (Result.CoolDownFrame != 0) {
				Result.CoolDownFrame--;
			}
			if (Result.NowFadeOut) {
				if (Effect.Fadeout() == ContentReturn.END) {
					Scene.Set("Title");
					return ContentReturn.CHANGE;
				}
			} else if (Effect.Fadein() == ContentReturn.END) {
				if ((Result.VIOEx.GetButtonOnce(0, VirtualIO.ButtonID.CANCEL) != 0 || Result.VIOEx.GetPointOnce(VirtualIO.PointID.L) != 0 || Result.VIOEx.GetButtonOnce(0, VirtualIO.ButtonID.START) != 0) && Result.CoolDownFrame == 0) {
					Result.NowFadeOut = true;
					Effect.Reset();
				}
			} else {
				Result.VIOEx.GetButtonOnce(0, VirtualIO.ButtonID.CANCEL);
				Result.VIOEx.GetPointOnce(VirtualIO.PointID.L);
			}
			return ContentReturn.OK;
		}
	}
}
