using Api.BLL.ServiceModel;
using Api.Core.Redis;
using Api.DAL;
using Api.DAL.DataContext;
using Api.DAL.Entity_Server0905;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Api.BLL.Impl
{
    public class BaseService : IBaseService
    {
        private readonly DbServer0905Context _dbServer0905 = DbContextFactory.GetDbServer0905Context();

        public List<CourseKVModel> GetCourseTypes(string specialtyId)
        {
            var redis = new RedisHelper();
            if (redis.KeyExists(RedisKeys.Courses))
            {
                if (redis.HashExists(RedisKeys.Courses, specialtyId))
                {
                    return redis.HashGet<List<CourseKVModel>>(RedisKeys.Courses, specialtyId);
                }
                else
                {
                    List<CourseKVModel> models = GetCourse();
                    redis.HashSet(RedisKeys.Courses, specialtyId, models);
                    return models;
                }
            }
            else
            {
                List<CourseKVModel> models = GetCourse();
                redis.HashSet(RedisKeys.Courses, specialtyId, models);
                return models;
            }

            List<CourseKVModel> GetCourse()
            {
                using (DbMockTestPaperContext db = new DbMockTestPaperContext())
                {
                    List<CourseKVModel> models = new List<CourseKVModel>();

                    if (specialtyId == "0")
                    {
                        models = db.Base_courseType_Computer
                            .Where(c => c.FK_SpecialtyType == specialtyId)
                            .Select(c => new CourseKVModel
                            {
                                CourseNo = c.No.ToString(),
                                CourseName = c.Name
                            }).ToList();
                    }
                    else
                    {
                        models = db.Base_courseType
                            .Where(c => c.FK_SpecialtyType == specialtyId)
                            .Select(c => new CourseKVModel
                            {
                                CourseNo = c.No,
                                CourseName = c.Name
                            }).ToList();
                    }


                    return models;
                }
            }

        }

        public List<KnowKVModel> GetKnowTypes(string specialtyId, string courseNo)
        {
            var redis = new RedisHelper();

            string key = string.Format(RedisKeys.Know, specialtyId);
            if (redis.KeyExists(key))
            {
                if (redis.HashExists(key, courseNo))
                {
                    return redis.HashGet<List<KnowKVModel>>(key, courseNo);
                }
                else
                {
                    List<KnowKVModel> models = GetKnow();
                    redis.HashSet(key, courseNo, models);
                    return models;
                }
            }
            else
            {
                List<KnowKVModel> models = GetKnow();
                redis.HashSet(key, courseNo, models);
                return models;
            }


            List<KnowKVModel> GetKnow()
            {
                using (DbMockTestPaperContext db = new DbMockTestPaperContext())
                {
                    List<KnowKVModel> models = new List<KnowKVModel>();

                    if (specialtyId == "0")
                    {
                        models = db.Base_knowledgepoint_Computer
                            .Where(c => c.FK_CourseType == courseNo)
                            .Select(c => new KnowKVModel
                            {
                                KnowNo = c.No,
                                KnowName = c.Name
                            }).ToList();
                    }
                    else
                    {
                        models = db.Base_knowledgepoint
                            .Where(c => c.FK_CourseType == courseNo)
                            .Select(c => new KnowKVModel
                            {
                                KnowNo = c.No,
                                KnowName = c.Name
                            }).ToList();
                    }


                    return models;
                }
            }

        }

        public List<Base_School> GetSchools()
        {
            var redis = new RedisHelper();

            if (redis.KeyExists(RedisKeys.Schools))
            {
                return redis.StringGet<List<Base_School>>(RedisKeys.Schools);
            }
            else
            {
                var schools = _dbServer0905.Base_School.ToList();

                redis.StringSet(RedisKeys.Schools, schools);

                return schools;
            }
        }

        public Base_School GetSchoolSingle(Expression<Func<Base_School, bool>> where)
        {
            return _dbServer0905.Base_School.FirstOrDefault(where);
        }

        public List<Base_specialtyType> GetSpecialty()
        {
            var redis = new RedisHelper();

            if (redis.KeyExists(RedisKeys.Specialty))
            {
                return redis.StringGet<List<Base_specialtyType>>(RedisKeys.Specialty);
            }
            else
            {
                var specialty = _dbServer0905.Base_specialtyType.ToList();

                redis.StringSet(RedisKeys.Specialty, specialty);

                return specialty;
            }
        }

        public Base_specialtyType GetSpecialtySingle(Expression<Func<Base_specialtyType, bool>> where)
        {
            return _dbServer0905.Base_specialtyType.FirstOrDefault(where);
        }


    }
}
