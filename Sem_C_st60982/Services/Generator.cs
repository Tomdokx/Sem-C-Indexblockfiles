using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sem_C_st60982.Services
{
	public static class Generator
	{
		private static Random rnd = new Random();

		private static int CurrInt { get; set; } = 0;
		public static string GenerateValue()
		{
			const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxy";
			return new string(Enumerable.Repeat(chars, SettingsPart.SIZE_OF_CHARS_PER_ITEM)
				.Select(s => s[rnd.Next(s.Length)]).ToArray());
		}

		public static int GenerateItemID ()
		{
			CurrInt++;
			return CurrInt;
		}
	}
}
