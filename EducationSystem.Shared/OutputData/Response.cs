
namespace EducationSystem.Shared.OutputData

{
	public class Response<T>
	{
		public T? Value { get; set; }
		public bool Error { get; set; }
		public string? ErrorMessage { get; set; }
		public string? ErrorInfo { get; set; }

		public Response() { }
		public Response(T? value)
		{
			this.Value = value;
			this.Error = false;
		}
		public Response(string errormessage, string errorinfo)
		{
			this.Error = true;
			this.ErrorMessage = errormessage;
			this.ErrorInfo = errorinfo;
		}
	}
}
