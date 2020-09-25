using Api.Core.Logger;
using Api.Core.Redis;
using Api.DAL.DataContext;
using Api.Queue.QueueModel;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Queue.Impl
{
    public class PhoneUserQueueData : IQueueData
    {
        private ConcurrentQueue<UploadUserQueueModel> _queue { get; set; } = new ConcurrentQueue<UploadUserQueueModel>();
        private bool _isEnd { get; set; } = true;
        public void ToDb()
        {
            if (!_isEnd)
                return;
            _isEnd = false;
            if (_queue.Count > 0)
                LogWriter.Instance.AddLog("学生上传队列中当前剩余数量：" + _queue.Count);

            bool hasData = _queue.TryDequeue(out UploadUserQueueModel data);

            if (hasData)
            {
                using (DbUserAdminContext db = new DbUserAdminContext())
                {
                    Dictionary<string, string> dicSpecialty = db.Base_specialtyType.ToDictionary(k => k.No, v => v.Name);
                    Dictionary<string, string> dicSchool = db.Base_School.ToDictionary(k => k.SchoolNo, v => v.SchoolName);

                    var redis = new RedisHelper(RedisIndex.StudentManagerSystem);

                    while (hasData)
                    {
                        try
                        {

                            string maxKaohao = "";
                            int currKaohao = 0;

                            int totalStudentCount = 0;
                            data.Specialties.ForEach(s => totalStudentCount += s.Students.Count);

                            if (redis.KeyExists(RedisKeys.MaxKaohaoKey))
                            {
                                maxKaohao = redis.StringGet(RedisKeys.MaxKaohaoKey);
                                currKaohao = Convert.ToInt32(maxKaohao) + 1;
                                maxKaohao = (Convert.ToInt32(maxKaohao) + totalStudentCount).ToString();
                            }
                            else
                            {
                                int iMaxKaohao = db.Database.SqlQuery<int?>("select Max(Cast(Right(KaoHao,LEN(KaoHao)-2) as int)) from UserTable  where UserType=1 and KaoHao like '[kK][sS]%'").FirstOrDefault() ?? 0;
                                maxKaohao = (iMaxKaohao + totalStudentCount).ToString();
                                currKaohao = iMaxKaohao + 1;
                            }
                            redis.StringSet(RedisKeys.MaxKaohaoKey, maxKaohao);

                            string dateNow = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                            foreach (var item in data.Specialties)
                            {
                                foreach (var student in item.Students)
                                {
                                    db.UserTable.Add(new DAL.Entity_UserAdmin.UserTable
                                    {
                                        FK_SchoolID = data.SchoolNo,
                                        FK_School = dicSchool[data.SchoolNo],
                                        FK_Specialty = Convert.ToInt32(item.SpecialtyId),
                                        FK_SpecialtyName = dicSpecialty[item.SpecialtyId],
                                        RegisterDate = dateNow,
                                        UserClass = student.ClassName,
                                        UserClassCode = student.ClassCode,
                                        UserName = student.UserName,
                                        UserPassword = student.Password,
                                        Roid = 5,
                                        IsDelete = 0,
                                        KaoHao = "ks" + currKaohao,
                                        UserDesc = "学校导入学生",
                                        lexueid = "17" + data.SchoolNo + string.Format("{0:000}", currKaohao++),
                                        UserType = 1,
                                        UseState = 1,
                                        IsLocked = 0,
                                    });
                                }
                            }
                            db.SaveChanges();
                        }
                        catch (Exception ex)
                        {
                            LogContent.Instance.WriteLog(new AppOpLog()
                            {
                                LogMessage = "[手机用户上传出错]：" + ex.Message + $"。[学校：{data.SchoolNo}]",
                                MemberID = "学生数据上传",
                                MethodName = "[GlabolData:StudentToDb]"
                            }, Log4NetLevel.Error);
                            _queue.Enqueue(data);
                        }

                        hasData = _queue.TryDequeue(out data);
                    }
                }
            }

            _isEnd = true;
        }
        public void Enqueue(object obj)
        {
            if (obj is UploadUserQueueModel)
            {
                UploadUserQueueModel t = obj as UploadUserQueueModel;
                _queue.Enqueue(t);
            }
        }
    }
}
