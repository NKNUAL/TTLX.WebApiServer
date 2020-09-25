using Api.BLL.ServiceModel;
using Api.DAL.Entity_Server0905;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Api.BLL
{
    public interface IBaseService : IDependency
    {

        List<Base_specialtyType> GetSpecialty();

        Base_specialtyType GetSpecialtySingle(Expression<Func<Base_specialtyType, bool>> where);

        List<Base_School> GetSchools();

        Base_School GetSchoolSingle(Expression<Func<Base_School, bool>> where);

        List<CourseKVModel> GetCourseTypes(string specialtyId);

        List<KnowKVModel> GetKnowTypes(string specialtyId, string courseNo);

    }
}
