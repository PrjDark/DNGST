using Lightness.Core;
using System;

namespace LEContents {
	public static class DNGST_X {
		public static ContentReturn Initialize() {
			return DNGST.Initialize(GameCommon.DNGST_SelectedDangoID);
		}

		public static ContentReturn Main() {
			return DNGST.Main(GameCommon.DNGST_SelectedDangoID);
		}
	}
}
