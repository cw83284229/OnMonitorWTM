using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using OnMonitor.Model.AlarmManages;
using OnMonitor.Model.Equipment;
using OnMonitor.Model.Project;
using OnMonitor.Model.Repair;
using WalkingTec.Mvvm.Core;

namespace OnMonitor.DataAccess
{
    public class DataContext : FrameworkContext
    {
        public DbSet<FrameworkUser> FrameworkUsers { get; set; }
        public DbSet<DVR> DVRs { get; set; }
        public DbSet<Camera> Cameras { get; set; }
        public DbSet<MonitorRoom> MonitorRooms { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Alarm> Alarms { get; set; }
        public DbSet<AlarmHost> AlarmHosts { get; set; }
        //工程信息
        public DbSet<ProjectManages> ProjectManages { get; set; }
        public DbSet<ProjectChangeCamera> projectChangeCameras { get; set; }
        public DbSet<CameraLayout> CameraLayouts { get; set; }  
        //维修信息

        public DbSet<CameraRepair> CameraRepairs { get; set; }
        public DbSet<AlarmRepair> AlarmRepairs { get; set; }
        public DbSet<DVRInfoCheck> DVRInfoChecks { get; set; }

        //报警信息
        public DbSet<AlarmManage> AlarmManages { get; set; }    
        public DataContext(CS cs)
             : base(cs)
        {
        }

        public DataContext(string cs, DBTypeEnum dbtype)
            : base(cs, dbtype)
        {
        }

        public DataContext(string cs, DBTypeEnum dbtype, string version = null)
            : base(cs, dbtype, version)
        {
        }

        
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public override async Task<bool> DataInit(object allModules, bool IsSpa)
        {
            var state = await base.DataInit(allModules, IsSpa);
            bool emptydb = false;
            try
            {
                emptydb = Set<FrameworkUser>().Count() == 0 && Set<FrameworkUserRole>().Count() == 0;
            }
            catch { }
            if (state == true || emptydb == true)
            {
                //when state is true, means it's the first time EF create database, do data init here
                //当state是true的时候，表示这是第一次创建数据库，可以在这里进行数据初始化
                var user = new FrameworkUser
                {
                    ITCode = "admin",
                    Password = Utils.GetMD5String("000000"),
                    IsValid = true,
                    Name = "Admin"
                };

                var userrole = new FrameworkUserRole
                {
                    UserCode = user.ITCode,
                    RoleCode = "001"
                };
                
                var adminmenus = Set<FrameworkMenu>().Where(x => x.Url != null && x.Url.StartsWith("/api") == false).ToList();
                foreach (var item in adminmenus)
                {
                    item.Url = "/_admin" + item.Url;
                }

                Set<FrameworkUser>().Add(user);
                Set<FrameworkUserRole>().Add(userrole);
                await SaveChangesAsync();
            }
            return state;
        }

    }

    /// <summary>
    /// DesignTimeFactory for EF Migration, use your full connection string,
    /// EF will find this class and use the connection defined here to run Add-Migration and Update-Database
    /// </summary>
    public class DataContextFactory : IDesignTimeDbContextFactory<DataContext>
    {
        public DataContext CreateDbContext(string[] args)
        {
            IConfiguration configuration = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json", optional: true)
               .Build();


            string contextstring = configuration.GetConnectionString("Default");

            return new DataContext(contextstring, DBTypeEnum.SqlServer);
        }
    }

}
