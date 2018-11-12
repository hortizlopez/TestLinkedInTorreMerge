using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BunnyMerge.Models
{
	public class TorreProfile
	{
		public TorrePerson person { get; set; }
		public List<TorreStrength> strengths { get; set; }
		public List<TorreAspiration> aspirations { get; set; }
		public List<TorreJob> jobs { get; set; }
		public List<TorreProject> projects { get; set; }
		public List<TorreEducation> education { get; set; }
		public List<TorrePublication> publications { get; set; }
		public List<TorreAchievement> achievements { get; set; }
	}
}