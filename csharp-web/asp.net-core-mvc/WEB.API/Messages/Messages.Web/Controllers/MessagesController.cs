using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Messages.Web.Data;
using Messages.Web.Domain;
using Messages.Web.Service;
using Messages.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Messages.Web.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly MessagesDbContext context;
   

        public MessagesController(MessagesDbContext context)
        {
            this.context = context;
        }

        [HttpGet(Name = "All")]
        [Route("all")]
        public ActionResult<IEnumerable<Message>> AllOrderedByCreatedOnAscending()
        {
            return this.context.Messages
                .OrderBy(message => message.CreatedOn)
                .ToList();
        }

        [HttpPost(Name = "Create")]
        [Route("create")]
        public async Task<ActionResult> Create(MessageCreateModel messageCreateModel)
        {
            Message message = new Message
            {
                Content = messageCreateModel.Content,
                User = messageCreateModel.User,
                CreatedOn = DateTime.UtcNow
            };

            await this.context.Messages.AddAsync(message);
            await this.context.SaveChangesAsync();

            return this.Ok();
        }
    }
}
