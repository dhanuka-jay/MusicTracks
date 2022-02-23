using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicTracks.Models
{
    public class InsClass
    {
        public string UserID { get; set; }
		public int subscription { get; set; }

		public int userType { get; set; }
		public int active { get; set; }

		public string userName { get; set; }
		public string userPassword { get; set; }
		public string userDisplayName { get; set; }
		public string profileImage { get; set; }
		public string insertedBy { get; set; }
		public string songTitle { get; set; }
		public string songArtist { get; set; }
		public string songDescription { get; set; }
		public string songId { get; set; }
		public string TrackID { get; set; }
		public string ratings { get; set; }
		public string instrumentId { get; set; }
		public string trackLocation { get; set; }
		public string instrumentName { get; set; }
		public DateTime insertedDate { get; set; }
		public DateTime trackDate { get; set; }

	}
}
