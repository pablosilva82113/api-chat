namespace WebChat.Tools

{
    public class Response
    {
        public string message { get; set; }

        public int code { get; private set; }

        public bool isOk { get; set; }

        public string date { get; private set; }

        public int dataCount { get; private set; }

        public object data { get; set; }

        public Response()
        {
            date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            isOk = true;
            message = "Ok";
            code = 0;
        }

        public void Unauthorized()
        {
            isOk = false;
            message = "No autorizado";
            code = 400;
        }

        public void DataInvalid(object data = null)
        {
            isOk = false;
            message = "Los datos son invalidos";
            this.data = data;
        }

        public void SetMinimus(string message, bool isOk = false)
        {
            this.message = message;
            this.isOk = isOk;
        }

        public void SetDataList<T>(List<T> data)
        {
            dataCount = data.Count();
            this.data = data;
        }

        public void SetData<T>(T data) where T : class
        {
            this.data = data;
            dataCount = 1;
        }

        public void SetCode(int code) => this.code = code;
    }
}
