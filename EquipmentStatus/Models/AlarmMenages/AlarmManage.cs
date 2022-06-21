using EFmodel;
using System;

namespace OnMonitor.Model.AlarmManages
{
    /// <summary>
    /// 报警记录
    /// </summary>
	
    public class AlarmManage 
    {
        public Guid ID { get; set; }   
        public Alarm Alarm { get; set; }
       
        public Guid? AlarmId { get; set; }
       
        public DateTime? AlarmTime { get; set; }
       
        public DateTime? WithdrawTime { get; set; }
       
        public string WithdrawMan { get; set; }
       
        public string WithdrawRemark { get; set; }
       
        public WithdrawType? WithdrawType { get; set; }
       
        public DateTime? DefenceTime { get; set; }
       
        public DateTime? TreatmentTime { get; set; }
        
        public string TreatmentTimeState { get; set; }
      
        public string TreatmentMan { get; set; }
      
        public string TreatmentReply { get; set; }
       
        public AlarmMessageTypeEnum? AlarmType { get; set; }
        
        public string Remark { get; set; }
      
     

       
        public DateTime? CreateTime { get; set; }

      
        public string CreateBy { get; set; }

       
        public DateTime? UpdateTime { get; set; }

       
        public string UpdateBy { get; set; }
    }
    public enum AlarmMessageTypeEnum
    {
        
        NAD,
       
        FieldUse,
       
        EquipmentFault,
       
        AnomalyAlarm
    }
    public enum WithdrawType
    {
         
         FieldUse,
         
          OpenDoor,
         
          AlarmWithdraw,
       
          EquipmentFault,
    }

}
