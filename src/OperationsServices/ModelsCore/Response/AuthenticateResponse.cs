using ModelsCore.Common;

namespace ModelsCore.Response
{
    public class AuthenticateResponse : BasicResponse
    {
        public User UserLogged { get; set; }
        public string Token { get; set; }
    }
}
