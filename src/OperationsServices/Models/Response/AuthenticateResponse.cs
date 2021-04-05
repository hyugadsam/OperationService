using Models.Common;

namespace Models.Response
{
    public class AuthenticateResponse : BasicResponse
    {
        public User UserLogged { get; set; }
        public string Token { get; set; }

    }
}
