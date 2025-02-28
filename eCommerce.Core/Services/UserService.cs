using AutoMapper;
using eCommerce.Core.DTO;
using eCommerce.Core.RepositoryContract;
using eCommerce.Core.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Core.Services;
internal class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UserService(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }
    public async Task<AuthenticationResponse?> Login(LoginRequest loginRequest)
    {
        ApplicationUser? user = await _userRepository.GetUserByEmailAndPassword(loginRequest.Email, loginRequest.Password);
        if (user == null)
        {
            return null;
        }

        return _mapper.Map<AuthenticationResponse>(user) with { success = true , token = "token"};
    }

    public async Task<AuthenticationResponse?> Register(RegisterRequest registerRequest)
    {
        ApplicationUser user = _mapper.Map<ApplicationUser>(registerRequest);
        ApplicationUser? registeredUser = await _userRepository.AddUser(user);

        if (user == null)
        {
            return null;
        }

        //return new AuthenticationResponse(
        //    user.UserId,
        //    user.Email,
        //    user.PersonName,
        //    user.Gender,
        //    "token",
        //    true
        //);

        return _mapper.Map<AuthenticationResponse>(user) with { success = true, token = "token" };
    }
}
