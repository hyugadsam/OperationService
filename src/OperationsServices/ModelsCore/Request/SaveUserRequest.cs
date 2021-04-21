using ModelsCore.Common;

namespace ModelsCore.Request
{
    public class SaveUserRequest
    {
        public User UserInfo { get; set; }
        public bool HasNewPassword { get; set; }
    }
}
