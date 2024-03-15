namespace Files.DataTable
{
	/// <summary>
	/// SortDirection is used instead of the CommunityToolkit equivalent because it is tied to the model
	/// </summary>
	public enum SortDirection : byte
	{
		/// <summary>
		/// Sort in ascending order.
		/// </summary>
		Ascending = 0,

		/// <summary>
		/// Sort in descending order.
		/// </summary>
		Descending = 1
	}
}
