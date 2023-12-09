namespace Data_LocalMessenger
{
	[Serializable]
	public class Message
	{
		public string From { get; set; }
		public string To { get; set; }
		public CommandMessage Command { get; set; }
		public string TextMessage { get; set; }
		public string? FileName { get; set; }
		public byte[]? FileData { get; set; }
		public DateTime CreatedAt { get; set; }
        public List<HistoryMessages> HistoryMessages { get; set; }
	}

	[Serializable]
	public class HistoryMessages
	{
		public string UserName { get; set; }
		public string Message { get; set; }
		public string? FileName { get; set; }
		public byte[]? bytes { get; set; }
	}

	[Serializable]
	public enum CommandMessage{
		Login,
		Send,
		ReturnHistory,

		End,
		Refresh,
		Negative,
	}
}