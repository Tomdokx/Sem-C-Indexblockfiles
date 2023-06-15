using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Sem_C_st60982.Blocks
{
	public class IndexBlock
	{
		public IndexBlock()
		{

		}

		public IndexBlock(IndexBlock indexBlock)
		{
			DataBlockLeastVal = new int[indexBlock.DataBlockLeastVal.Length];
			indexBlock.DataBlockLeastVal.CopyTo(DataBlockLeastVal,0);
			DataBlockOffsets = new int[indexBlock.DataBlockOffsets.Length];
			indexBlock.DataBlockOffsets.CopyTo(DataBlockOffsets, 0);
		}

		public int[] DataBlockLeastVal { get; set; }

		public int[] DataBlockOffsets {get;set;}

		public int LowestVal { get {  return DataBlockLeastVal[0]; } }

		public void SerializeSelf(Stream stream)
		{
			BinaryFormatter formatter = new BinaryFormatter();
			for(int i = 0; i < DataBlockLeastVal.Length; i++)
			{
				formatter.Serialize(stream, DataBlockLeastVal[i]);
				formatter.Serialize(stream, DataBlockOffsets[i]);
			}
		}

		public static IndexBlock DeserializeSelf(Stream stream)
		{
			BinaryFormatter formatter = new BinaryFormatter();
			
			int[] dataBlocksOff = new int[SettingsPart.BLOCK_SIZE_INDEX];
			int[] dataBlocksVal = new int[SettingsPart.BLOCK_SIZE_INDEX];
			for (int i = 0; i < dataBlocksOff.Length; i++)
			{
				dataBlocksVal[i] = (int)formatter.Deserialize(stream);
				dataBlocksOff[i] = (int)formatter.Deserialize(stream);
			}
			return new IndexBlock { DataBlockLeastVal = dataBlocksVal, DataBlockOffsets = dataBlocksOff };
		}
	}
}
