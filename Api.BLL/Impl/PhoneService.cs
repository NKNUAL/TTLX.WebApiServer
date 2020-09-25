using Api.BLL.ServiceModel;
using Api.DAL;
using Api.DAL.DataContext;
using Api.Queue;
using Api.Queue.QueueModel;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.BLL.Impl
{
    public class PhoneService : IPhoneService
    {

        private readonly DbUserAdminContext _dbUserAdmin = DbContextFactory.GetDbUserAdmin();

        public IBaseService _baseService { get; set; }
        public PhoneService(IBaseService baseService)
        {
            _baseService = baseService;
        }


        public List<PhoneUserModel> GetPhoneUser(string schoolNo, string specialtyCode, string studentName)
        {
            var query = _dbUserAdmin.UserTable.Where(s => s.FK_SchoolID == schoolNo);

            if (!string.IsNullOrEmpty(specialtyCode))
            {
                var sId = _dbUserAdmin.Base_specialtyType
                    .FirstOrDefault(s => s.SpecialtyCode == specialtyCode).No;

                int specId = int.Parse(sId);
                query = query.Where(s => s.FK_Specialty == specId);
            }
            if (!string.IsNullOrEmpty(studentName))
            {
                query = query.Where(s => s.UserName.Contains(studentName));
            }

            return query
                .Where(s => s.IsDelete == 0 && s.UserType == 1 && s.Roid == 5)
                .Select(s => new PhoneUserModel
                {
                    ClassCode = s.UserClassCode,
                    ClassName = s.UserClass,
                    UserName = s.UserName,
                    Password = s.UserPassword,
                    Lexueid = s.lexueid,
                    Kaohao = s.KaoHao,
                    SpecialtyName = s.FK_SpecialtyName
                }).ToList();
        }

        public int GetSchoolHasUploadCount(string schoolNo, int specialtyId)
        {
            return
                _dbUserAdmin.UserTable
                .Count(s => s.FK_SchoolID == schoolNo && s.FK_Specialty == specialtyId &&
                s.IsDelete == 0 && s.UserType == 1 && s.Roid == 5);
        }

        public int GetSchoolLimit(string schoolNo, string specialtyId)
        {
            var limit = _dbUserAdmin.SchoolPhoneUserLimit
                .FirstOrDefault(s => s.SchoolNo == schoolNo && s.SpecialtyId == specialtyId);
            if (limit == null)
            {
                return 0;
            }
            return limit.LimitCount ?? 0;
        }

        public List<PhoneLimitModel> GetSchoolLimitCount(string schoolNo)
        {
            var limits = _dbUserAdmin.SchoolPhoneUserLimit
                .Where(s => s.SchoolNo == schoolNo)
                .ToList();

            var specialties = _baseService.GetSpecialty();

            List<PhoneLimitModel> models = new List<PhoneLimitModel>();

            foreach (var specialty in specialties)
            {
                var limit = limits.Find(s => s.SpecialtyId == specialty.No);
                if (limit == null)
                {
                    models.Add(new PhoneLimitModel
                    {
                        SpecialtyId = specialty.No,
                        SpecialtyName = specialty.Name,
                        LimitCount = 0
                    });
                }
                else
                {
                    models.Add(new PhoneLimitModel
                    {
                        SpecialtyId = specialty.No,
                        SpecialtyName = specialty.Name,
                        LimitCount = limit.LimitCount ?? 0
                    });
                }
            }
            return models;
        }

        public ResultModel UploadStudent(UploadUserServiceModel uploadUser)
        {
            foreach (var item in uploadUser.Specialties)
            {
                int limitCount = GetSchoolLimit(uploadUser.SchoolNo, item.SpecialtyId);
                int useCount = GetSchoolHasUploadCount(uploadUser.SchoolNo, int.Parse(item.SpecialtyId));
                if (item.Students.Count > (limitCount - useCount))
                {
                    return new ResultModel
                    {
                        code = 0,
                        message = item.SpecialtyName + "超出上传数量限制，请删除部分学生后再重试"
                    };
                }
            }

            var users = Mapper.Map<UploadUserServiceModel, UploadUserQueueModel>(uploadUser);

            GlabolDataExe.Instance.AddData(QueueDataType.Upload, users);

            return new ResultModel { code = 1 };
        }

        public ResultModel UserEditInfo(string lexueid, string password, string userName)
        {
            if (string.IsNullOrEmpty(password))
                return new ResultModel { code = 0, message = "新密码不能为空" };

            if (string.IsNullOrEmpty(userName))
                return new ResultModel { code = 0, message = "姓名不能为空" };

            var user = _dbUserAdmin.UserTable.FirstOrDefault(s => s.lexueid == lexueid && s.IsDelete == 0);

            if (user == null)
                return new ResultModel { code = 0, message = "用户不存在或已删除" };

            user.UserPassword = password;
            user.UserName = userName;

            _dbUserAdmin.SaveChanges();

            return new ResultModel { code = 1 };
        }
    }
}
