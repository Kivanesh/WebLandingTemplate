using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebLandingTemplateRepository.Infrastructure;
using WebLandingTemplateRepository.Infrastructure.Contract;

namespace WebLandingTemplateRepository.Repository
{
    public class MessageInboxRepository : BaseRepository<ContactMessageInbox>
    {
        public MessageInboxRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }
}
