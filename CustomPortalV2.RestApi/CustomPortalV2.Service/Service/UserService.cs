using CustomPortalV2.Business.Concrete;
using CustomPortalV2.Core.Model.App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomPortalV2.DataAccessLayer.Repository;
using CustomPortalV2.DataAccessLayer.Concrete;
using static CustomPortalV2.Business.Helper.Enums;
using CustomPortalV2.Core.Model.App;
using CustomPortalV2.Model.DTO;

namespace CustomPortalV2.Business.Service
{

    public class UserService : IUserService
    {
        IUserRepository _userRepository;
        ICompanyRepository _companyRepository;
        IParamRepository _paramrepository;
        ILoginrequestLogRepository _loginRequestLogRepository;
        Encryption encryption;
        public UserService(IUserRepository userRepository,
            ICompanyRepository companyRepository,
            IParamRepository paramRepository,
            ILoginrequestLogRepository loginrequestLogRepository)
        {
            _userRepository = userRepository;
            _companyRepository = companyRepository;
            _paramrepository = paramRepository;
            _loginRequestLogRepository = loginrequestLogRepository;

            encryption = new Encryption("userpwd859");
        }
        public User AddUser(User user)
        {
            throw new NotImplementedException();
        }

        public bool DeleteUser(User user)
        {
            throw new NotImplementedException();
        }

        public User GetById(int id)
        {
            throw new NotImplementedException();
        }

        public User? GetUserByUserName(string companyCode, string userName)
        {
            var company = _companyRepository.GetCompanyCode(companyCode);
            if (company == null)
            {
                return null;
            }
            return _userRepository.GetUserName(userName, company.Id);
        }

        public List<User> GetUsers(int companyId)
        {
            throw new NotImplementedException();
        }

        public enumLoginReturn Login(string clientIp, string companyCode, string username, string password, ref User? user)
        {

            var loginLog = new LoginRequestLog()
            {
                UserName = username,
                ClientIp = clientIp,
                LogDate = DateTime.Now,
            };
            try
            {
                var company = _companyRepository.GetCompanyCode(companyCode);

                if (company == null)
                    return enumLoginReturn.CompanyCodeIsNotExist;

                if (!company.Enable)
                {
                    return enumLoginReturn.CompanyDisabled;
                }

                var minTimeCount = _paramrepository.GetParam("FailTimeOutMinute");
                var failTimeCount = _paramrepository.GetParam("LoginFailMaxCount");

                var minTimeValue = int.Parse(minTimeCount.Value) * -1;
                var minDate = DateTime.Now.AddMinutes(minTimeValue);


                var countIsSuccessCount = _loginRequestLogRepository.CheckFailedLoginCount(clientIp, minDate);
                if (countIsSuccessCount >= int.Parse(failTimeCount.Value))
                {
                    return enumLoginReturn.LoginFailMaxCount;
                }
                user = _userRepository.GetUserName(username, company.Id);

                if (user == null || user.Deleted)
                {
                    return enumLoginReturn.UserNameOrPasswordWrong;
                }

                var encPassword = encryption.Encrypt(password.Trim());
                if (encPassword == user.Password)
                {
                    loginLog.Success = true;
                    return enumLoginReturn.Success;
                }
                else
                {
                    return enumLoginReturn.UserNameOrPasswordWrong;
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                _loginRequestLogRepository.AddLoginLog(loginLog);
            }


        }

        public bool UpdateUser(User user)
        {
            throw new NotImplementedException();
        }


    }


}
