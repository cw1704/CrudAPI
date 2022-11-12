using GpProject206.Domain;
using GpProject206.Settings;

namespace GpProject206.Services
{
    public class MemberService : AMongoCollectionProvider<Member>
    {
        public MemberService(IDatabaseSettings settings) : base(settings, "members")
        {

        }
    }
}
