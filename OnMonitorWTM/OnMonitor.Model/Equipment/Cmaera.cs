using System;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;

namespace OnMonitor.Model.Equipment
{
    public class Camera : BasePoco
    {

        [Display(Name = "主机号")]
        public Guid DVRId { get; set; }
        public DVR DVR { get; set; }
        [Display(Name = "通道号")]
        public int channel_ID { get; set; }

        [Display(Name = "镜头号")]
        [Required(ErrorMessage = "此项不能为空")]
        [StringLength(50, ErrorMessage = "输入超出限定长度")]
        public string Camera_ID { get; set; }
        
        [Display(Name = "楼栋")]
        [StringLength(50, ErrorMessage = "输入超出限定长度")]
        public string Build { get; set; }
        
        [Display(Name = "楼层")]
        [StringLength(50, ErrorMessage = "输入超出限定长度")]
        public string floor { get; set; }
       
        [Display(Name = "方向")]
        [StringLength(50, ErrorMessage = "输入超出限定长度")]
        public string Direction { get; set; }

        [Display(Name = "位置")]
        [StringLength(50, ErrorMessage = "输入超出限定长度")]
        public string Location { get; set; }
        
        [Display(Name = "监控类别")]
        public  MonitorClassification MonitorClassification { get; set; }
       
        [Display(Name = "设备类型")]
        public CameraTpye CameraTpye { get; set; }
        
        [Display(Name = "部门")]
        public Guid DepartmentId { get; set; }
        public Department Department { get; set; }

        [Display(Name = "安装时间")]
        [StringLength(50, ErrorMessage = "输入超出限定长度")]
        public string install_time { get; set; }

        [Display(Name = "设备厂商")]
        [StringLength(50, ErrorMessage = "输入超出限定长度")]
        public string manufacturer { get; set; }
       
        [Display(Name = "安装厂商")]
        [StringLength(50, ErrorMessage = "输入超出限定长度")]
        public string category { get; set; }
        [Display(Name = "IP")]
        [StringLength(50, ErrorMessage = "输入超出限定长度")]
        public string Camera_IP { get; set; }
        [Display(Name = "端口")]
        [StringLength(50, ErrorMessage = "输入超出限定长度")]
        public string Camera_port { get; set; }
        [Display(Name = "账号")]
        [StringLength(50, ErrorMessage = "输入超出限定长度")]
        public string Camera_usre { get; set; }

        [Display(Name = "密码")]
        [StringLength(50, ErrorMessage = "输入超出限定长度")]
        public string DVR_possword { get; set; }

        [Display(Name = "备注")]
        [StringLength(50, ErrorMessage = "输入超出限定长度")]
        public string Remark { get; set; }


       // public virtual List<CameraRepair> CameraRepairs { get; set; }
    }

    public enum MonitorClassification
    {
        [Display(Name = "門崗")]
        GateSentry,
        [Display(Name = "外圍通道")]
        PeripheralChannel,
        [Display(Name = "主通道")]
        MainChannel,
        [Display(Name = "產线")]
        ProductionLine,
        [Display(Name = "物料附房")]
        MaterialsRoom,
        [Display(Name = "工程附房")]
        ProjectRoom,
        [Display(Name = "普通附房")]
        CommonRoom,
        [Display(Name = "辦公室")]
        Office,
        [Display(Name = "鞋櫃")]
        ShoeCabinet,
        [Display(Name = "碼頭")]
        Wharf,
        [Display(Name = "電梯")]
        Elevator,

    }

    public enum CameraTpye 
    {
        [Display(Name = "模擬")]
        Analog,
        [Display(Name = "同軸")]
        HDCVI720P,
        [Display(Name = "智能同軸")]
        HDCVI1080P,
        [Display(Name = "網絡")]
        IPC,
        [Display(Name = "智能網絡")]
        SmartIPC,
        [Display(Name = "球機")]
        PTZ,

    }
}
