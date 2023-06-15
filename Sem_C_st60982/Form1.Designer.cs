using Sem_C_st60982.Blocks;
using Sem_C_st60982.Services;

namespace Sem_C_st60982
{
	partial class Form1
	{
		/// <summary>
		///  Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		///  Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		///  Required method for Designer support - do not modify
		///  the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			BuildButton = new Button();
			FindButton = new Button();
			SearchAllButton = new Button();
			ItemsList = new ListBox();
			IndexList = new ListBox();
			FindIDTextBox = new TextBox();
			SuspendLayout();
			// 
			// BuildButton
			// 
			BuildButton.Location = new Point(437, 70);
			BuildButton.Name = "BuildButton";
			BuildButton.Size = new Size(94, 29);
			BuildButton.TabIndex = 0;
			BuildButton.Text = "Build";
			BuildButton.UseVisualStyleBackColor = true;
			BuildButton.Click += BuildButton_Click;
			// 
			// FindButton
			// 
			FindButton.Location = new Point(437, 196);
			FindButton.Name = "FindButton";
			FindButton.Size = new Size(94, 29);
			FindButton.TabIndex = 1;
			FindButton.Text = "Find";
			FindButton.UseVisualStyleBackColor = true;
			FindButton.Click += FindButton_Click;
			// 
			// SearchAllButton
			// 
			SearchAllButton.Location = new Point(437, 292);
			SearchAllButton.Name = "SearchAllButton";
			SearchAllButton.Size = new Size(94, 29);
			SearchAllButton.TabIndex = 2;
			SearchAllButton.Text = "WriteAll";
			SearchAllButton.UseVisualStyleBackColor = true;
			SearchAllButton.Click += SearchAllButton_Click;
			// 
			// ItemsList
			// 
			ItemsList.FormattingEnabled = true;
			ItemsList.ItemHeight = 20;
			ItemsList.Location = new Point(49, 53);
			ItemsList.Name = "ItemsList";
			ItemsList.Size = new Size(382, 444);
			ItemsList.TabIndex = 3;
			// 
			// IndexList
			// 
			IndexList.FormattingEnabled = true;
			IndexList.ItemHeight = 20;
			IndexList.Location = new Point(537, 53);
			IndexList.Name = "IndexList";
			IndexList.Size = new Size(382, 444);
			IndexList.TabIndex = 4;
			// 
			// FindIDTextBox
			// 
			FindIDTextBox.Location = new Point(437, 163);
			FindIDTextBox.Name = "FindIDTextBox";
			FindIDTextBox.Size = new Size(94, 27);
			FindIDTextBox.TabIndex = 5;
			// 
			// Form1
			// 
			AutoScaleDimensions = new SizeF(8F, 20F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(958, 521);
			Controls.Add(FindIDTextBox);
			Controls.Add(IndexList);
			Controls.Add(ItemsList);
			Controls.Add(SearchAllButton);
			Controls.Add(FindButton);
			Controls.Add(BuildButton);
			Name = "Form1";
			Text = "Form1";
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private Button BuildButton;
		private Button FindButton;
		private Button SearchAllButton;
		private ListBox ItemsList;
		private ListBox IndexList;
		private List<Item> items = new List<Item>();
		private IndexSeqFile indexSeqFile = new IndexSeqFile();
		private TextBox FindIDTextBox;
	}
}