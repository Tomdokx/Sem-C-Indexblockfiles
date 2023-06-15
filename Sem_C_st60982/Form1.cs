using Sem_C_st60982.Blocks;

namespace Sem_C_st60982
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		private void BuildButton_Click(object sender, EventArgs e)
		{
			indexSeqFile.BuildTheFiles();
		}

		private void SearchAllButton_Click(object sender, EventArgs e)
		{
			items = indexSeqFile.GetAllItems();
			ItemsList.DataSource = items;
			ItemsList.DisplayMember = "Display";

			DisplayLogs();
		}

		private void DisplayLogs()
		{
			IndexList.Items.Clear();
			string logs = indexSeqFile.Logger;
			string[] logarray = logs.Split('\n');
			foreach (string log in logarray)
			{
				if(!string.IsNullOrEmpty(log))
					IndexList.Items.Add(log);
			}
		}

		private void FindButton_Click(object sender, EventArgs e)
		{

			if (!string.IsNullOrEmpty(FindIDTextBox.Text))
			{
				if(int.TryParse(FindIDTextBox.Text, out int findID)){
					Item item = indexSeqFile.SearchForItem(findID);
					MessageBox.Show(item == null ? "Item with this ID was not found." : item.Display);
				}
			}
			else if(ItemsList.SelectedItem != null) {
				Item i = ItemsList.SelectedItem as Item;
				if (i != null)
				{
					_ = indexSeqFile.SearchForItem(i.ID);
					DisplayLogs();
				}
			}
			else
			{
				MessageBox.Show("Yeah, I would like to find anything, but I am not a magician to read your mind and know what you want to find.");
			}
		}
	}
}