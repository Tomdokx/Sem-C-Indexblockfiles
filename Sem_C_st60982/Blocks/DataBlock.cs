using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Sem_C_st60982.Blocks
{
	public class DataBlock
	{
		public DataBlock() { }

		public DataBlock(DataBlock dataBlock)
		{
			Items = new Item[dataBlock.Items.Length];
			dataBlock.Items.CopyTo(this.Items,0);
			ID = dataBlock.ID;
		}
		public Item[] Items { get; set; }
		public int ID { get; set; }

		public void SerializeSelf(Stream stream)
		{
			BinaryFormatter formatter = new BinaryFormatter();
			formatter.Serialize(stream, ID);
			foreach (Item item in Items)
				item.SerializeSelf(stream);
		}

		public static DataBlock DeserializeSelf(Stream stream)
		{
			BinaryFormatter formatter = new BinaryFormatter();
			int id = (int)formatter.Deserialize(stream);
			Item[] items = new Item[SettingsPart.BLOCK_SIZE_DATA];
			for (int i = 0;i < items.Length;i++)
				items[i] = Item.DeserializeSelf(stream);
			return new DataBlock { ID = id , Items = items};
		}
	}
}
