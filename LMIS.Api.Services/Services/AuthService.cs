using AutoMapper;
using LMIS.Api.Core.DTOs.User;
using LMIS.Api.Core.Repository.IRepository;
using LMIS.Api.Services.Services.IServices;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMIS.Api.Services.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public IConfiguration _configuration { get; set; }

        public AuthService(IUnitOfWork unitOfWork, IMapper Mapper, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _mapper = Mapper;
            _configuration = configuration;
        }

        public async Task Login(LoginDTO loginDTO)
        {
            try
            {
                // get user from the database
                var user = await _unitOfWork.User.GetFirstOrDefaultAsync(user => user.Email == loginDTO.Email);
                if (user != null)
                {
                    // checking is the account is confirmed already
                    if (user.IsConfirmed == false)
                    {
                        return;
                    }
                    // hash the password

                    // loginModel.Password = _unitOfWork.User.HashPassword(loginModel.Password);

                    // check if the password match
                    var isMatched = _unitOfWork.User.VerifyPassword(user.Password, loginDTO.Password);

                    if (isMatched)
                    {
                        // generate token
                        var tokenHandler = new JwtSecurityToken();
                        LoginTokenDTO userData = await _unitOfWork.User.GenerateToken(user, _configuration, _unitOfWork);
                        _unitOfWork.Save();
                        ReturnTokenDetail(userData);
                    }

                    else
                        return;
                }
            }
            catch (Exception)
            {

                return;
            }
        }
        public LoginTokenDTO ReturnTokenDetail(LoginTokenDTO loginDTO)
        {
            LoginTokenDTO tokenDTO = loginDTO;
            return tokenDTO;

        }

    }
}
