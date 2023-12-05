using System.Net;

namespace ApiUfoCasesNet8.Modelos
{
    public class RespuestAPI
    {
        public RespuestAPI()
        {
            ErrorMessages = new List<string>();
        }

        public HttpStatusCode StatusCode { get; set; }

        public bool IsSucces { get; set; } = true;

        public List<string> ErrorMessages { get; set; }

        public object Resoult { get; set; }


    }
}
