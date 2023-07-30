using System.ComponentModel;

namespace DigitalProduction.IO
{
	/// <summary>
	/// Drive types for computer.
    /// 
	/// The "Description" attribute can be accessed using Reflection to get a string representing the enumeration type.
	/// 
	/// The "Length" enumeration can be used in loops as a convenient way of terminating a loop that does not have to be changed if
	/// the number of items in the enumeration changes.  The "Length" enumeration must be the last item.
	/// for (int i = 0; i &lt; (int)EnumType.Length; i++) {...}
	/// </summary>
	public enum DriveType
	{
		/// <summary>Unknown.</summary>
		[Description("Unknown")]
		NotFound		= 0,

		/// <summary>Removable drive.</summary>
		[Description("Removable Drive")]
		Removable		= 2,

		/// <summary>Fixed drive.</summary>
		[Description("Fixed Drive")]
        Fixed			= 3,

		/// <summary>Remote drive.</summary>
		[Description("Remote Drive")]
		RemoteDisk		= 4,

		/// <summary>CD or DVD drive.</summary>
		[Description("CD or DVD Drive")]
		CD				= 5,

		/// <summary>Ramdisk.</summary>
		[Description("Ramdisk")]
		RamDisk			= 6

	} // End enum.
} // End namespace.