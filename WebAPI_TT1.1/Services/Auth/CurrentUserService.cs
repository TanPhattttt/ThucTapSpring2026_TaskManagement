using System.Security.Claims;

namespace WebAPI_TT1._1.Services.Auth
{
    public class CurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService()
        {
        }


        public Guid? FakeUserId { get; set; }
        public string? FakeRole { get; set; }

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Guid UserId
        {
            get
            {
                if (FakeUserId.HasValue)
                    return FakeUserId.Value;

                var id = _httpContextAccessor.HttpContext?.User
                                .FindFirst(ClaimTypes.NameIdentifier)?.Value;

                return Guid.Parse(id!);
            }
        }

        public string Role
        {
            get
            {
                if (!string.IsNullOrEmpty(FakeRole))
                    return FakeRole;

                return _httpContextAccessor.HttpContext?.User
                                .FindFirst(ClaimTypes.Role)?.Value!;
            }
        }
    }
}
