using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HearstMappingsEditor.Data.Models.References
{
	[Table("vPerimeters")] // database view
	public class PerimeterEntity
	{
		[Key]
		public long? PerimeterID { get; set; }

        public string PerimeterCode { get; set; }

        public string PerimeterCurrency { get; set; }

        public string PerimeterDesc { get; set; }
	}
}
