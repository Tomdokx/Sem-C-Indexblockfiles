using Sem_C_st60982.Blocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Sem_C_st60982.Services
{
	public class IndexSeqFile
	{
		public string Logger { get; private set; } = string.Empty;

		public void BuildTheFiles()
		{
			List<Item> items = new List<Item>();
			for (int i = 0;i < SettingsPart.GENERATE_ITEMS; i++)
			{
				items.Add(new Item { ID = Generator.GenerateItemID(), Value = Generator.GenerateValue() });
			}
			items = items.OrderBy(p => p.ID).ToList();
			List<DataBlock> dataBlocksForSave = new List<DataBlock>();
			DataBlock dataBlock = new DataBlock();

			
			while(items.Count != 0) { 
					dataBlock.Items = items.Take(SettingsPart.BLOCK_SIZE_DATA).ToArray();
					dataBlock.ID = items.Min(p => p.ID);
					dataBlocksForSave.Add(new DataBlock(dataBlock));
					dataBlock = new DataBlock();
					items.RemoveRange(0, SettingsPart.BLOCK_SIZE_DATA);
			}
			
			List<IndexBlock> indBlocksForSave = new List<IndexBlock>();
			IndexBlock indBlock = new IndexBlock();
			List<int> minVals = new List<int>();
			List<int> offsets = new List<int>();
			for (int i = 0; i < dataBlocksForSave.Count; i++)
			{
				if(i% SettingsPart.BLOCK_SIZE_INDEX == 0)
				{

					indBlocksForSave.Add(new IndexBlock { 
						DataBlockLeastVal = minVals.ToArray(),
						DataBlockOffsets = offsets.ToArray() 
					});
					minVals.Clear();
					offsets.Clear();
				}
				minVals.Add(dataBlocksForSave[i].ID);
				offsets.Add(i+1);
			}
			WriteToIndexFile(indBlocksForSave);
			WriteToDataFile(dataBlocksForSave);
		}

		public List<Item> GetAllItems()
		{
			Logger = string.Empty;
			List<DataBlock> dataBlocks = new List<DataBlock>();
			
			for (int i = 0; i < 100; i++)
				dataBlocks.Add(ReadDataBlock(i));

			List<Item> items = new List<Item>();
			dataBlocks.ForEach(p => items.AddRange(p.Items));
			return items;
		}

		private void WriteToDataFile(List<DataBlock> dataBlocksForSave)
		{
			using(FileStream fs = new FileStream(SettingsPart.DATA_FILE,FileMode.Create))
			{
				dataBlocksForSave.ForEach(p =>
				{
					p.SerializeSelf(fs);
				});
			}
		}

		private void WriteToIndexFile(List<IndexBlock> indBlocksForSave)
		{
			using (FileStream fs = new FileStream(SettingsPart.INDEX_FILE, FileMode.Create))
			{
				indBlocksForSave.ForEach(p =>
				{
					p.SerializeSelf(fs);
				});
			}
		}

		public Item? SearchForItem(int id)
		{
			Logger = string.Empty;
			int offset = SearchForBlockOffsetContainingItem(id);
			if (offset > SettingsPart.BLOCK_SIZE_DATA)
				return null;
			DataBlock dataBlock = ReadDataBlock(offset - 1);
			if(dataBlock != null) {
				return IntHalvMethForItem(dataBlock,id);
			}
			return null;
		}

		private Item? IntHalvMethForItem(DataBlock dataBlock, int id)
		{
			int lowerBound = 0;
			int upperBound = dataBlock.Items.Length -1;

			while (lowerBound <= upperBound)
			{
				int middle = (lowerBound + upperBound) / 2;
				if (id <= dataBlock.Items[middle].ID)
					upperBound = middle - 1;
				else
					lowerBound = middle + 1;

				if (id == dataBlock.Items[middle].ID)
					return dataBlock.Items[middle];
			}
			if (upperBound >= 0 && upperBound < dataBlock.Items.Length)
				return dataBlock.Items[upperBound];
			return null;
		}

		private int SearchForBlockOffsetContainingItem(int id)
		{
			IndexBlock? indexBlockWithItem = null;
			for (int i = 0; i < SettingsPart.BLOCK_SIZE_INDEX; i++)
			{
				indexBlockWithItem = ReadIndexBlock(i);
				if(i == SettingsPart.BLOCK_SIZE_INDEX)
				{
					break;
				}
				IndexBlock ib2 = ReadIndexBlock(i+1);

				if (indexBlockWithItem.DataBlockLeastVal[0] < id && id < ib2.DataBlockLeastVal[0])
				{
					break;
				}
			}
			return IntHalvMethForOffset(indexBlockWithItem, id);
		}

		private int IntHalvMethForOffset(IndexBlock indexBlock, int id)
		{
			if (indexBlock == null)
				return -1;
			int lowerBound = 0;
			int upperBound = indexBlock.DataBlockLeastVal.Length - 1;

			while (lowerBound <= upperBound)
			{
				int middle = (lowerBound + upperBound) / 2;
				if (id <= indexBlock.DataBlockLeastVal[middle])
					upperBound = middle - 1;
				else
					lowerBound = middle + 1;
				if (id == indexBlock.DataBlockLeastVal[middle])
					return indexBlock.DataBlockOffsets[middle];
			}
			if (upperBound >= 0 && upperBound < indexBlock.DataBlockLeastVal.Length)
				return indexBlock.DataBlockOffsets[upperBound];
			return -1;
		}

		private IndexBlock? ReadIndexBlock(int offset)
		{
			FileStream fs = new FileStream(SettingsPart.INDEX_FILE, FileMode.Open);
			IndexBlock? indBlock = null;
			try
			{
				fs.Position = offset * SettingsPart.IND_BL_SIZE_P;
				indBlock = IndexBlock.DeserializeSelf(fs);
			}
			catch (Exception e)
			{
				return null;
			}
			finally
			{
				Logger += $"\n Readed index block {indBlock.LowestVal}";
				fs.Close();
			}
			return indBlock;
		}

		private DataBlock? ReadDataBlock(int offset)
		{
			FileStream fs = new FileStream(SettingsPart.DATA_FILE, FileMode.Open);
			DataBlock? dataBlock = null;
			try
			{
				fs.Position = offset * SettingsPart.DATA_BL_SIZE_P;
				dataBlock = DataBlock.DeserializeSelf(fs);
			}
			catch(Exception e )
			{
				return null;
			}
			finally
			{
				Logger += $"\n Readed data block {dataBlock.ID}";
				fs.Close();
			}
			return dataBlock;
		}
	}
}
