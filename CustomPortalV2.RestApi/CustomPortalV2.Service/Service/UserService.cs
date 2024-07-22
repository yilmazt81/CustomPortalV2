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
using CustomPortalV2.Model;
using CustomPortalV2.Core.Model.DTO;

namespace CustomPortalV2.Business.Service
{

    public class UserService : IUserService
    {
        IUserRepository _userRepository;
        ICompanyRepository _companyRepository;
        IParamRepository _paramrepository;
        ILoginrequestLogRepository _loginRequestLogRepository;
        IAppLangRepository _appLangRepository;
        IBranchRepository _branchRepository;
        Encryption encryption;
        public UserService(IUserRepository userRepository,
            ICompanyRepository companyRepository,
            IParamRepository paramRepository,
            ILoginrequestLogRepository loginrequestLogRepository,
            IAppLangRepository appLangRepository, IBranchRepository branchRepository)
        {
            _userRepository = userRepository;
            _companyRepository = companyRepository;
            _paramrepository = paramRepository;
            _loginRequestLogRepository = loginrequestLogRepository;
            _appLangRepository = appLangRepository;
            _branchRepository = branchRepository;

            encryption = new Encryption("usr_9189f3f");
        }
        public User AddUser(User user)
        {
            return _userRepository.AddUser(user);

        }

        public bool DeleteUser(User user)
        {
            user.Deleted = true;
            return _userRepository.Update(user);
        }

        public DefaultReturn<List<BranchPackage>> GetBranchPackage(int userId)
        {
            var user = GetById(userId);
            DefaultReturn<List<BranchPackage>> defaultReturn = new DefaultReturn<List<BranchPackage>>();
            var branchPackages = _userRepository.GetBranchPackages();
            List<BranchPackage> rPackages = new List<BranchPackage>();

            foreach (var brancPackage in branchPackages)
            {
                var packageName = _appLangRepository.Get("BranchPackage." + brancPackage.Name, user.AppLangId, brancPackage.Name);

                rPackages.Add(new BranchPackage()
                {
                    Id = brancPackage.Id,
                    Name = packageName,
                    MonthlyRecordCount= brancPackage.MonthlyRecordCount 
                });

            }
            defaultReturn.Data = rPackages;

            return defaultReturn;
        }

        public User GetById(int id)
        {
            return _userRepository.Get(id);

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

        public DefaultReturn<List<UserRuleMenuDTO>> GetUserManu(int userId, int branchId)
        {
            DefaultReturn<List<UserRuleMenuDTO>> returnType = new DefaultReturn<List<UserRuleMenuDTO>>();
            var branch = _branchRepository.Get(s => s.Id == branchId);
            if (branch == null)
            {
                returnType.ReturnCode = 5;
                returnType.ReturnMessage = "BranchNotExist";
                return returnType;
            }
            returnType.Data = new List<UserRuleMenuDTO>();
            var menuList = _userRepository.GetUserRuleMenus(branch.UserRuleId);
            var user = GetById(userId);
            foreach (var oneMenu in menuList)
            {
                var menuName = _appLangRepository.Get("MainMenu." + oneMenu.MenuAdress, user.AppLangId, oneMenu.MenuName);

                UserRuleMenuDTO userRuleMenuDTO = new UserRuleMenuDTO()
                {
                    name = menuName,
                    to = oneMenu.MenuAdress,
                    icon = oneMenu.IconClass,
                };

                returnType.Data.Add(userRuleMenuDTO);
            }

            return returnType;

        }

        public DefaultReturn<List<UserRule>> GetUserRoles(int userId)
        {
            var user = GetById(userId);
            DefaultReturn<List<UserRule>> defaultReturn = new DefaultReturn<List<UserRule>>();
            var userRoles = _userRepository.GetUserRules();
            List<UserRule> ruserRules = new List<UserRule>();

            foreach (var userRule in userRoles)
            {
                var roleName = _appLangRepository.Get("UserRole." + userRule.Name, user.AppLangId, userRule.Name);

                ruserRules.Add(new UserRule()
                {
                    Id = userRule.Id,
                    Name = roleName,
                });

            }
            defaultReturn.Data = ruserRules;

            return defaultReturn;

        }

        public List<User> GetUsers(int companyId)
        {
            return _userRepository.GetAll(companyId).ToList();

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
                {

                    return enumLoginReturn.CompanyCodeIsNotExist;
                }

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
                    return enumLoginReturn.FailTimeOutMinute;
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
            return _userRepository.Update(user);
        }

        
    }


}
