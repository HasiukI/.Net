using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server_LocalMessenger
{
	public class History
	{
		public string User1 { get; set; }
		public string User2 { get; set; }
		public List<Data_LocalMessenger.HistoryMessages> HistoryMessage { get; set; }
	}
}
