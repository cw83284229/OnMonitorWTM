using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WalkingTec.Mvvm.Core;
using OnMonitor.Controllers;
using OnMonitor.ViewModel.AlarmManages.AlarmManageVMs;
using OnMonitor.Model.AlarmManages;
using OnMonitor.DataAccess;
using OnMonitor.Model.Equipment;


namespace OnMonitor.Test
{
    [TestClass]
    public class AlarmManageApiTest
    {
        private AlarmManageController _controller;
        private string _seed;

        public AlarmManageApiTest()
        {
            _seed = Guid.NewGuid().ToString();
            _controller = MockController.CreateApi<AlarmManageController>(new DataContext(_seed, DBTypeEnum.Memory), "user");
        }

        [TestMethod]
        public void SearchTest()
        {
            ContentResult rv = _controller.Search(new AlarmManageSearcher()) as ContentResult;
            Assert.IsTrue(string.IsNullOrEmpty(rv.Content)==false);
        }

        [TestMethod]
        public void CreateTest()
        {
            AlarmManageVM vm = _controller.Wtm.CreateVM<AlarmManageVM>();
            AlarmManage v = new AlarmManage();
            
          //  v.AlarmId = AddAlarm();
            v.AlarmTime = DateTime.Parse("2021-09-16 10:35:46");
            v.WithdrawTime = DateTime.Parse("2021-07-07 10:35:46");
            v.WithdrawMan = "dgF3C714XbaOOu4b69G6dbtB8EFOGL3q8sEvo9t6QUF";
            v.WithdrawRemark = "yyRegAIB53a0pfTANMnK5fC5D6O2SrUM8U2wX";
           // v.WithdrawType = "QfgaZ1yV";
            v.DefenceTime = DateTime.Parse("2023-05-03 10:35:46");
            v.TreatmentTime = DateTime.Parse("2023-05-03 10:35:46");
            v.TreatmentTimeState = "hbuwh";
            v.TreatmentMan = "SEHrcCV8hb";
            v.TreatmentReply = "OFU3KnXnfi9ZSOv17KHc";
          //  v.AlarmMessage = OnMonitor.Model.AlarmManages.AlarmMessageTypeEnum.NAD;
            v.Remark = "9xQ";
            vm.Entity = v;
            var rv = _controller.Add(vm);
            Assert.IsInstanceOfType(rv, typeof(OkObjectResult));

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<AlarmManage>().Find(v.ID);
                
                Assert.AreEqual(data.AlarmTime, DateTime.Parse("2021-09-16 10:35:46"));
                Assert.AreEqual(data.WithdrawTime, DateTime.Parse("2021-07-07 10:35:46"));
                Assert.AreEqual(data.WithdrawMan, "dgF3C714XbaOOu4b69G6dbtB8EFOGL3q8sEvo9t6QUF");
                Assert.AreEqual(data.WithdrawRemark, "yyRegAIB53a0pfTANMnK5fC5D6O2SrUM8U2wX");
                Assert.AreEqual(data.WithdrawType, "QfgaZ1yV");
                Assert.AreEqual(data.DefenceTime, DateTime.Parse("2023-05-03 10:35:46"));
                Assert.AreEqual(data.TreatmentTime, DateTime.Parse("2023-05-03 10:35:46"));
                Assert.AreEqual(data.TreatmentTimeState, "hbuwh");
                Assert.AreEqual(data.TreatmentMan, "SEHrcCV8hb");
                Assert.AreEqual(data.TreatmentReply, "OFU3KnXnfi9ZSOv17KHc");
             //   Assert.AreEqual(data.AlarmMessage, OnMonitor.Model.AlarmManages.AlarmMessageTypeEnum.NAD);
                Assert.AreEqual(data.Remark, "9xQ");
                Assert.AreEqual(data.CreateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.CreateTime.Value).Seconds < 10);
            }
        }

  //      [TestMethod]
  //      public void EditTest()
  //      {
  //          AlarmManage v = new AlarmManage();
  //          using (var context = new DataContext(_seed, DBTypeEnum.Memory))
  //          {
       			
  //              v.AlarmId = AddAlarm();
  //              v.AlarmTime = DateTime.Parse("2021-09-16 10:35:46");
  //              v.WithdrawTime = DateTime.Parse("2021-07-07 10:35:46");
  //              v.WithdrawMan = "dgF3C714XbaOOu4b69G6dbtB8EFOGL3q8sEvo9t6QUF";
  //              v.WithdrawRemark = "yyRegAIB53a0pfTANMnK5fC5D6O2SrUM8U2wX";
  //              v.WithdrawType = "QfgaZ1yV";
  //              v.DefenceTime = DateTime.Parse("2023-05-03 10:35:46");
  //              v.TreatmentTime = DateTime.Parse("2023-05-03 10:35:46");
  //              v.TreatmentTimeState = "hbuwh";
  //              v.TreatmentMan = "SEHrcCV8hb";
  //              v.TreatmentReply = "OFU3KnXnfi9ZSOv17KHc";
  //              v.AlarmMessage = OnMonitor.Model.AlarmManages.AlarmMessageTypeEnum.NAD;
  //              v.Remark = "9xQ";
  //              context.Set<AlarmManage>().Add(v);
  //              context.SaveChanges();
  //          }

  //          AlarmManageVM vm = _controller.Wtm.CreateVM<AlarmManageVM>();
  //          var oldID = v.ID;
  //          v = new AlarmManage();
  //          v.ID = oldID;
       		
  //          v.AlarmTime = DateTime.Parse("2023-05-21 10:35:46");
  //          v.WithdrawTime = DateTime.Parse("2021-09-17 10:35:46");
  //          v.WithdrawMan = "fk";
  //          v.WithdrawRemark = "oap3Z1ssD0thDe3OfuxrZleCrf5wz3hkBJQ2CPoA";
  //          v.WithdrawType = "pyEW0IX1e62craJu";
  //          v.DefenceTime = DateTime.Parse("2023-07-10 10:35:46");
  //          v.TreatmentTime = DateTime.Parse("2023-05-29 10:35:46");
  //          v.TreatmentTimeState = "QH4iBrO54kQLj6DIVEk6hiPIeOPyForDb";
  //          v.TreatmentMan = "NcvuczZ5BuS8WEB40s";
  //          v.TreatmentReply = "JLRMIsjq9XwK1oZl80svUr2XG2Xb91HlUaLEViO4OOF";
  //          v.AlarmMessage = OnMonitor.Model.AlarmManages.AlarmMessageTypeEnum.FieldUse;
  //          v.Remark = "q";
  //          vm.Entity = v;
  //          vm.FC = new Dictionary<string, object>();
			
  //          vm.FC.Add("Entity.AlarmId", "");
  //          vm.FC.Add("Entity.AlarmTime", "");
  //          vm.FC.Add("Entity.WithdrawTime", "");
  //          vm.FC.Add("Entity.WithdrawMan", "");
  //          vm.FC.Add("Entity.WithdrawRemark", "");
  //          vm.FC.Add("Entity.WithdrawType", "");
  //          vm.FC.Add("Entity.DefenceTime", "");
  //          vm.FC.Add("Entity.TreatmentTime", "");
  //          vm.FC.Add("Entity.TreatmentTimeState", "");
  //          vm.FC.Add("Entity.TreatmentMan", "");
  //          vm.FC.Add("Entity.TreatmentReply", "");
  //          vm.FC.Add("Entity.AlarmMessage", "");
  //          vm.FC.Add("Entity.Remark", "");
  //          var rv = _controller.Edit(vm);
  //          Assert.IsInstanceOfType(rv, typeof(OkObjectResult));

  //          using (var context = new DataContext(_seed, DBTypeEnum.Memory))
  //          {
  //              var data = context.Set<AlarmManage>().Find(v.ID);
 				
  //              Assert.AreEqual(data.AlarmTime, DateTime.Parse("2023-05-21 10:35:46"));
  //              Assert.AreEqual(data.WithdrawTime, DateTime.Parse("2021-09-17 10:35:46"));
  //              Assert.AreEqual(data.WithdrawMan, "fk");
  //              Assert.AreEqual(data.WithdrawRemark, "oap3Z1ssD0thDe3OfuxrZleCrf5wz3hkBJQ2CPoA");
  //              Assert.AreEqual(data.WithdrawType, "pyEW0IX1e62craJu");
  //              Assert.AreEqual(data.DefenceTime, DateTime.Parse("2023-07-10 10:35:46"));
  //              Assert.AreEqual(data.TreatmentTime, DateTime.Parse("2023-05-29 10:35:46"));
  //              Assert.AreEqual(data.TreatmentTimeState, "QH4iBrO54kQLj6DIVEk6hiPIeOPyForDb");
  //              Assert.AreEqual(data.TreatmentMan, "NcvuczZ5BuS8WEB40s");
  //              Assert.AreEqual(data.TreatmentReply, "JLRMIsjq9XwK1oZl80svUr2XG2Xb91HlUaLEViO4OOF");
  //              Assert.AreEqual(data.AlarmMessage, OnMonitor.Model.AlarmManages.AlarmMessageTypeEnum.FieldUse);
  //              Assert.AreEqual(data.Remark, "q");
  //              Assert.AreEqual(data.UpdateBy, "user");
  //              Assert.IsTrue(DateTime.Now.Subtract(data.UpdateTime.Value).Seconds < 10);
  //          }

  //      }

		//[TestMethod]
  //      public void GetTest()
  //      {
  //          AlarmManage v = new AlarmManage();
  //          using (var context = new DataContext(_seed, DBTypeEnum.Memory))
  //          {
        		
  //              v.AlarmId = AddAlarm();
  //              v.AlarmTime = DateTime.Parse("2021-09-16 10:35:46");
  //              v.WithdrawTime = DateTime.Parse("2021-07-07 10:35:46");
  //              v.WithdrawMan = "dgF3C714XbaOOu4b69G6dbtB8EFOGL3q8sEvo9t6QUF";
  //              v.WithdrawRemark = "yyRegAIB53a0pfTANMnK5fC5D6O2SrUM8U2wX";
  //              v.WithdrawType = "QfgaZ1yV";
  //              v.DefenceTime = DateTime.Parse("2023-05-03 10:35:46");
  //              v.TreatmentTime = DateTime.Parse("2023-05-03 10:35:46");
  //              v.TreatmentTimeState = "hbuwh";
  //              v.TreatmentMan = "SEHrcCV8hb";
  //              v.TreatmentReply = "OFU3KnXnfi9ZSOv17KHc";
  //              v.AlarmMessage = OnMonitor.Model.AlarmManages.AlarmMessageTypeEnum.NAD;
  //              v.Remark = "9xQ";
  //              context.Set<AlarmManage>().Add(v);
  //              context.SaveChanges();
  //          }
  //          var rv = _controller.Get(v.ID.ToString());
  //          Assert.IsNotNull(rv);
  //      }

  //      [TestMethod]
  //      public void BatchDeleteTest()
  //      {
  //          AlarmManage v1 = new AlarmManage();
  //          AlarmManage v2 = new AlarmManage();
  //          using (var context = new DataContext(_seed, DBTypeEnum.Memory))
  //          {
				
  //              v1.AlarmId = AddAlarm();
  //              v1.AlarmTime = DateTime.Parse("2021-09-16 10:35:46");
  //              v1.WithdrawTime = DateTime.Parse("2021-07-07 10:35:46");
  //              v1.WithdrawMan = "dgF3C714XbaOOu4b69G6dbtB8EFOGL3q8sEvo9t6QUF";
  //              v1.WithdrawRemark = "yyRegAIB53a0pfTANMnK5fC5D6O2SrUM8U2wX";
  //              v1.WithdrawType = "QfgaZ1yV";
  //              v1.DefenceTime = DateTime.Parse("2023-05-03 10:35:46");
  //              v1.TreatmentTime = DateTime.Parse("2023-05-03 10:35:46");
  //              v1.TreatmentTimeState = "hbuwh";
  //              v1.TreatmentMan = "SEHrcCV8hb";
  //              v1.TreatmentReply = "OFU3KnXnfi9ZSOv17KHc";
  //              v1.AlarmMessage = OnMonitor.Model.AlarmManages.AlarmMessageTypeEnum.NAD;
  //              v1.Remark = "9xQ";
  //              v2.AlarmId = v1.AlarmId; 
  //              v2.AlarmTime = DateTime.Parse("2023-05-21 10:35:46");
  //              v2.WithdrawTime = DateTime.Parse("2021-09-17 10:35:46");
  //              v2.WithdrawMan = "fk";
  //              v2.WithdrawRemark = "oap3Z1ssD0thDe3OfuxrZleCrf5wz3hkBJQ2CPoA";
  //              v2.WithdrawType = "pyEW0IX1e62craJu";
  //              v2.DefenceTime = DateTime.Parse("2023-07-10 10:35:46");
  //              v2.TreatmentTime = DateTime.Parse("2023-05-29 10:35:46");
  //              v2.TreatmentTimeState = "QH4iBrO54kQLj6DIVEk6hiPIeOPyForDb";
  //              v2.TreatmentMan = "NcvuczZ5BuS8WEB40s";
  //              v2.TreatmentReply = "JLRMIsjq9XwK1oZl80svUr2XG2Xb91HlUaLEViO4OOF";
  //              v2.AlarmMessage = OnMonitor.Model.AlarmManages.AlarmMessageTypeEnum.FieldUse;
  //              v2.Remark = "q";
  //              context.Set<AlarmManage>().Add(v1);
  //              context.Set<AlarmManage>().Add(v2);
  //              context.SaveChanges();
  //          }

  //          var rv = _controller.BatchDelete(new string[] { v1.ID.ToString(), v2.ID.ToString() });
  //          Assert.IsInstanceOfType(rv, typeof(OkObjectResult));

  //          using (var context = new DataContext(_seed, DBTypeEnum.Memory))
  //          {
  //              var data1 = context.Set<AlarmManage>().Find(v1.ID);
  //              var data2 = context.Set<AlarmManage>().Find(v2.ID);
  //              Assert.AreEqual(data1, null);
  //          Assert.AreEqual(data2, null);
  //          }

  //          rv = _controller.BatchDelete(new string[] {});
  //          Assert.IsInstanceOfType(rv, typeof(OkResult));

  //      }

  //      private Guid AddMonitorRoom()
  //      {
  //          MonitorRoom v = new MonitorRoom();
  //          using (var context = new DataContext(_seed, DBTypeEnum.Memory))
  //          {
  //              try{

  //              v.Factory = "nstBC4Qiz2vpyp9SUsCo0lIsXfHrIC9";
  //              v.RoomLocation = "CUwtVQuLT6vBswhJOsdgfel3DtZ";
  //              v.RoomType = "X2Ybr4";
  //              context.Set<MonitorRoom>().Add(v);
  //              context.SaveChanges();
  //              }
  //              catch{}
  //          }
  //          return v.ID;
  //      }

  //      private Guid AddAlarmHost()
  //      {
  //          AlarmHost v = new AlarmHost();
  //          using (var context = new DataContext(_seed, DBTypeEnum.Memory))
  //          {
  //              try{

  //              v.MonitorRoomId = AddMonitorRoom();
  //              v.AlarmHost_ID = "0SDUiqZvcslo";
  //              v.User = "uUep9g0RAdQcVcLbLmgHuhzZHZvPxBWm";
  //              v.Password = "5xZGrxQCRHKsosPHu3kklHM921HfFXs";
  //              v.AlarmHostType = "birsFSUM4BCd27y";
  //              v.AlarmHostIP = "VPZP0IKM3qtxf1V";
  //              v.AlarmChannelCount = 35;
  //              v.install_time = "BUcM20wXfhBfmK7vVgSZuOS75elCyB0ssE20PW";
  //              v.category = "4l3fTgxO3wxiw";
  //              v.Remark = "fvI8dz8JwFHgQTyZuGfV1afmUWHKKQ7RWdq";
  //              context.Set<AlarmHost>().Add(v);
  //              context.SaveChanges();
  //              }
  //              catch{}
  //          }
  //          return v.ID;
  //      }

  //      private Guid AddAlarm()
  //      {
  //          Alarm v = new Alarm();
  //          using (var context = new DataContext(_seed, DBTypeEnum.Memory))
  //          {
  //              try{

  //              v.AlarmHostID = AddAlarmHost();
  //              v.Alarm_ID = "DZ7nzVNJEiqBWqFAGotgWPJ8o1TIdpzjhnKBd7";
  //              v.Channel_ID = 23;
  //              v.Build = "uvR1oHlQaH8QxJRiTWHwyImhvIgJUFA";
  //              v.floor = "55H9LggSRcQ2fGFfBcUN4xSDbUd";
  //              v.Location = "kyEWzjhE6LwXYNii75V3uXpQ8BGZenDxnMAClohEiXLuAfqbIaZGIt";
  //              v.GeteType = "NOWTT1ldwIsF0eC6S";
  //              v.SensorType = "EIAcZ3FcNwMUTgHDeabHp";
  //              v.department = "d1zXW9E384EiyJnZdCT0iuakEJ";
  //              v.Cost_code = "D4CCiT31kgVNtJIDMr";
  //              v.install_time = "nIXxEVMU6s3YJRgz35xlW0IBvjWw2BuX";
  //              v.category = "vFQHheOAWp35ErnQ3Kiq7C7xaku";
  //              v.InsideCamera_ID = "p0L0GZ8MhJYUMLUQ5OB0NtuQVH1jrIz44tADmJSdX";
  //              v.OutsideCamera_ID = "1vclZXZfvWD9OwXnUWbxvhCiWCWoIGlLdV00GMp21JsaVXZp";
  //              v.IsAlertor = true;
  //              v.IsOpenOrClosed = true;
  //              v.Remark = "DZMj";
  //              context.Set<Alarm>().Add(v);
  //              context.SaveChanges();
  //              }
  //              catch{}
  //          }
  //          return v.ID;
  //      }


    }
}
