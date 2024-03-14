﻿namespace Files.DataTable
{
	public class InventoryItem
	{
		public int Id { get; set; }

		public string Name { get; set; } = string.Empty;

		public string Description { get; set; } = string.Empty;

		public int Quantity { get; set; }

		public List<InventoryItem> SubItems { get; set; } = new();
	}
}
