using DTOs.Group;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebAPI.Data;
using WebAPI.Data.Entities;

namespace WebAPI.Hubs
{
    public class VideoChatHub : Hub
    {
        private UserManager<ApplicationUser> userManager;
        private ApplicationDbContext applicationDbContext;
        public VideoChatHub(UserManager<ApplicationUser> userManager, ApplicationDbContext applicationDbContext)
        {
            this.userManager = userManager;
            this.applicationDbContext = applicationDbContext;
        }
        public override async Task OnConnectedAsync()
        {
            if (!Context.User.Identity.IsAuthenticated)
            {
                return;
            }
            ApplicationUser appUser = await userManager.FindByIdAsync(Context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            if (appUser.IsOnline is false)
            {
                appUser.IsOnline = true;
                appUser.TabsOpen = 1;
                await userManager.UpdateAsync(appUser);
                await Clients.All.SendAsync("UpdateGroups");
                return;
            }
            if (appUser.IsOnline)
            {
                appUser.TabsOpen++;
                await userManager.UpdateAsync(appUser);
            }
        }
        public override async Task OnDisconnectedAsync(Exception ex)
        {
            if (!Context.User.Identity.IsAuthenticated)
            {
                return;
            }
            ApplicationUser appUser = await userManager.FindByIdAsync(Context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            if (appUser.TabsOpen > 0)
            {
                appUser.TabsOpen--;
                await userManager.UpdateAsync(appUser);
            }
            if (appUser.TabsOpen == 0)
            {
                appUser.IsOnline = false;
                await userManager.UpdateAsync(appUser);
                await Clients.AllExcept(appUser.Id).SendAsync("UpdateGroups");
            }
        }
        public async Task SendMessage(MessagesDTO messageDTO)
        {
            ApplicationUser appUser = await userManager.FindByIdAsync(Context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            Group group = applicationDbContext.Groups.Include(s => s.Messages).ThenInclude(t => t.SenderApplicationUser).Where(group => group.Id == messageDTO.GroupId).First();
            group.Messages.Add(new GroupMessage
            {
                SenderApplicationUser = appUser,
                Text = messageDTO.Content,
                CreatedAt = DateTime.Now
            });
            await applicationDbContext.SaveChangesAsync();
            await Clients.All.SendAsync("UpdateGroupMessages", group.Id);
        }
        public async Task RemoveMessage(MessagesDTO messageDTO)
        {
            ApplicationUser appUser = applicationDbContext.Users.Include(s => s.SentMessages).Where(uer => uer.Id == Context.User.FindFirst(ClaimTypes.NameIdentifier).Value).First();
            GroupMessage message = appUser.SentMessages.Where(s => s.Id == messageDTO.Id).FirstOrDefault();
            appUser.SentMessages.Remove(message);
            await applicationDbContext.SaveChangesAsync();
            await Clients.All.SendAsync("UpdateGroupMessages", messageDTO.GroupId);
        }

        public async Task CreateLearningNote(LearningNoteDTO learningNoteDTO)
        {
            ApplicationUser appUser = await userManager.FindByIdAsync(Context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            Group group = applicationDbContext.Groups.Include(s => s.Messages).ThenInclude(t => t.SenderApplicationUser).Where(group => group.Id == learningNoteDTO.GroupId).First();

            await Clients.All.SendAsync("UpdateGroupMessages", learningNoteDTO.GroupId);
        }
    }
}
