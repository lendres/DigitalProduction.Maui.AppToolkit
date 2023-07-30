namespace DigitalProduction.ComponentModel
{
	/// <summary>
	/// Attribute for applying alternate names to a class.
	/// </summary>
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = true)]
	public class AliasAttribute : Attribute
	{
		#region Fields

		private string			_alias;

		#endregion

		#region Construction

		/// <summary>
		/// Default constructor.
		/// </summary>
		public AliasAttribute() {}

		
		/// <summary>
		/// Default constructor.
		/// </summary>
		public AliasAttribute(string alias)
		{
			_alias = alias;
		}

		#endregion

		#region Properties

		/// <summary>
		/// An alternate name for the class/structure.
		/// </summary>
		public string Alias
		{
			get
			{
				return _alias;
			}

			set
			{
				_alias = value;
			}
		}

		#endregion

	} // End class.
} // End namespace.