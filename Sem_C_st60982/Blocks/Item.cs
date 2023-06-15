using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Sem_C_st60982.Blocks
{
	[Serializable]
	public class Item 
	{
		public int ID { get; set; }
		public string Value { get; set; }

		public string Display { get { return $"{ID}: {Value}"; } }

		public void SerializeSelf(Stream stream)
		{
			BinaryFormatter formatter = new BinaryFormatter();
			formatter.Serialize(stream, this);
		}

		public static Item DeserializeSelf(Stream stream)
		{
			BinaryFormatter formatter = new BinaryFormatter();
			return (Item) formatter.Deserialize(stream);
		}
	}
}
