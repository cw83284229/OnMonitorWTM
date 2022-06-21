namespace EFmodel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class AlarmManage
    {
      
        public Guid ID { get; set; }

        public Alarm Alarm { get; set; }
        public Guid AlarmID { get; set; }

       
        /// <summary>
        /// ����ʱ��
        /// </summary>

        public DateTime? AlarmTime { get; set; }
        /// <summary>
        /// ����ʱ��
        /// </summary>
        
        public DateTime? WithdrawTime { get; set; }
        /// <summary>
        /// ������Ա
        /// </summary>
        [StringLength(55)]
        public string WithdrawMan { get; set; }
        /// <summary>
        /// ������ע
        /// </summary>
        [StringLength(55)]
        public string WithdrawRemark { get; set; }
        /// <summary>
        /// ��������
        /// </summary>
        public WithdrawType? WithdrawType { get; set; }
        /// <summary>
        /// ����ʱ��
        /// </summary>
       
        public DateTime? DefenceTime { get; set; }
        /// <summary>
        /// ����ʱ��
        /// </summary>
      
        public DateTime? TreatmentTime { get; set; }
        /// <summary>
        /// ����״̬
        /// </summary>
        [StringLength(55)]
        public string TreatmentTimeState { get; set; }
        /// <summary>
        /// �ֳ�������
        /// </summary>
        [StringLength(55)]
        public string TreatmentMan { get; set; }
        /// <summary>
        /// �ֳ�����ظ�
        /// </summary>
        [StringLength(55)]
        public string TreatmentReply { get; set; }
        /// <summary>
        /// �쳣����
        /// </summary>
       
        public AlarmMessageTypeEnum? AlarmType { get; set; }

        [StringLength(55)]
        public string Remark { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? CreateTime { get; set; }

        [StringLength(50)]
        public string CreateBy { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? UpdateTime { get; set; }

        [StringLength(50)]
        public string UpdateBy { get; set; }

     
    }

    public enum AlarmMessageTypeEnum
    {
        [Display(Name = "���쳣")]
        NAD,
        [Display(Name = "�ֳ�ʹ��")]
        FieldUse,
        [Display(Name = "�豸�쳣")]
        EquipmentFault,
        [Display(Name = "�쳣����")]
        AnomalyAlarm
    }
    public enum WithdrawType
    {
        [Display(Name = "�ֳ�ʹ��")]
        FieldUse,
        [Display(Name = "����")]
        OpenDoor,
        [Display(Name = "��������")]
        AlarmWithdraw,
        [Display(Name = "�豸�쳣")]
        EquipmentFault,
    }
}
