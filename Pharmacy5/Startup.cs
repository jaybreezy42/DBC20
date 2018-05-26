using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using Pharmacy5.Models;
using System.Linq;
using SendGrid.Helpers.Mail;
using System;
using System.Threading.Tasks;
using SendGrid;

[assembly: OwinStartupAttribute(typeof(Pharmacy5.Startup))]
namespace Pharmacy5
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateRoles();
            //Execute().Wait();
        }
        //creating a super user-hard coding(admin, defining password,names, emails) to exit in order to alway have access to the system
        private void CreateRoles()
        {
            ApplicationDbContext context = new ApplicationDbContext();
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            if (!roleManager.RoleExists("admin"))
            {
                var role = new IdentityRole();
                role.Name = "admin";
                roleManager.Create(role);

                var user = new ApplicationUser();
                user.UserName = "martinayeboah@gmail.com";
                user.Email = "martinayeboah@gmail.com";

                string userPWD = "Admin@1234";

                var chkUser = UserManager.Create(user, userPWD);

                //Add default User to Role Admin   
                if (chkUser.Succeeded)
                {
                    var result = UserManager.AddToRole(user.Id, "admin");

                }
            }
            if (context.Users.SingleOrDefault(u => u.UserName == "martinayeboah@gmail.com") == null)
            {
                var user = new ApplicationUser();
                user.UserName = "martinayeboah@gmail.com";
                user.Email = "martinayeboah@gmail.com";

                string userPWD = "Admin@1234";

                var chkUser = UserManager.Create(user, userPWD);

                //Add default User to Role Admin   
                if (chkUser.Succeeded)
                {
                    var result = UserManager.AddToRole(user.Id, "admin");

                }
            }

            if (!roleManager.RoleExists("Employee"))
            {
                var role = new IdentityRole();
                role.Name = "Employee";
                roleManager.Create(role);
            }



        }
        
        
            private static void Main()
            {
                Execute().Wait();
            }

            static async Task Execute()
            {
                var apiKey = Environment.GetEnvironmentVariable("SendEmail");
                var client = new SendGridClient(apiKey);
                var from = new EmailAddress("info@Pharmacy5.com", "LizMat Pharmacy");
                var subject = "Sending with SendGrid is Fun";
                var to = new EmailAddress("adoboahseesi@hotmail.com", "Test1");
                var plainTextContent = "and easy to do anywhere, even with C#";
                var htmlContent = "<strong>and easy to do anywhere, even with C#</strong>";
                var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
                var response = await client.SendEmailAsync(msg);
            }
        
    }

}
