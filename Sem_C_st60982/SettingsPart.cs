using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sem_C_st60982
{
	public static class SettingsPart
	{
		public static readonly int BLOCK_SIZE_DATA = 100;

		public static readonly int BLOCK_SIZE_INDEX = 10;

		public static readonly int GENERATE_ITEMS = 10000;

		public static readonly int SIZE_OF_CHARS_PER_ITEM = 10;

		public static readonly int SIZE_OF_ITEM = 24;

		public static readonly int IND_BL_SIZE_P = 1080;

		public static readonly int DATA_BL_SIZE_P = 19754;

		public static readonly string INDEX_FILE = "../../../Files/IndexFile";

		public static readonly string DATA_FILE = "../../../Files/DataFile";
	}
}
