/**
 * Autogenerated by Thrift Compiler (0.9.2)
 *
 * DO NOT EDIT UNLESS YOU ARE SURE THAT YOU KNOW WHAT YOU ARE DOING
 *  @generated
 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Thrift;
using Thrift.Collections;
using System.Runtime.Serialization;
using Thrift.Protocol;
using Thrift.Transport;

namespace TekTak.iLoop.Kauwa
{

  #if !SILVERLIGHT
  [Serializable]
  #endif
  public partial class EmployeeWorkSchedule : TBase
  {
    private long _personEmpId;
    private bool _scheduleType;
    private string _text;
    private int _employeeTypeId;
    private string _employeeType;
    private int _day;
    private string _startTime;
    private string _endTime;

    public long PersonEmpId
    {
      get
      {
        return _personEmpId;
      }
      set
      {
        __isset.personEmpId = true;
        this._personEmpId = value;
      }
    }

    public bool ScheduleType
    {
      get
      {
        return _scheduleType;
      }
      set
      {
        __isset.scheduleType = true;
        this._scheduleType = value;
      }
    }

    public string Text
    {
      get
      {
        return _text;
      }
      set
      {
        __isset.text = true;
        this._text = value;
      }
    }

    public int EmployeeTypeId
    {
      get
      {
        return _employeeTypeId;
      }
      set
      {
        __isset.employeeTypeId = true;
        this._employeeTypeId = value;
      }
    }

    public string EmployeeType
    {
      get
      {
        return _employeeType;
      }
      set
      {
        __isset.employeeType = true;
        this._employeeType = value;
      }
    }

    public int Day
    {
      get
      {
        return _day;
      }
      set
      {
        __isset.day = true;
        this._day = value;
      }
    }

    public string StartTime
    {
      get
      {
        return _startTime;
      }
      set
      {
        __isset.startTime = true;
        this._startTime = value;
      }
    }

    public string EndTime
    {
      get
      {
        return _endTime;
      }
      set
      {
        __isset.endTime = true;
        this._endTime = value;
      }
    }


    public Isset __isset;
    #if !SILVERLIGHT
    [Serializable]
    #endif
    public struct Isset {
      public bool personEmpId;
      public bool scheduleType;
      public bool text;
      public bool employeeTypeId;
      public bool employeeType;
      public bool day;
      public bool startTime;
      public bool endTime;
    }

    public EmployeeWorkSchedule() {
    }

    public void Read (TProtocol iprot)
    {
      TField field;
      iprot.ReadStructBegin();
      while (true)
      {
        field = iprot.ReadFieldBegin();
        if (field.Type == TType.Stop) { 
          break;
        }
        switch (field.ID)
        {
          case 1:
            if (field.Type == TType.I64) {
              PersonEmpId = iprot.ReadI64();
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 2:
            if (field.Type == TType.Bool) {
              ScheduleType = iprot.ReadBool();
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 3:
            if (field.Type == TType.String) {
              Text = iprot.ReadString();
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 4:
            if (field.Type == TType.I32) {
              EmployeeTypeId = iprot.ReadI32();
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 5:
            if (field.Type == TType.String) {
              EmployeeType = iprot.ReadString();
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 6:
            if (field.Type == TType.I32) {
              Day = iprot.ReadI32();
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 7:
            if (field.Type == TType.String) {
              StartTime = iprot.ReadString();
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 8:
            if (field.Type == TType.String) {
              EndTime = iprot.ReadString();
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          default: 
            TProtocolUtil.Skip(iprot, field.Type);
            break;
        }
        iprot.ReadFieldEnd();
      }
      iprot.ReadStructEnd();
    }

    public void Write(TProtocol oprot) {
      TStruct struc = new TStruct("EmployeeWorkSchedule");
      oprot.WriteStructBegin(struc);
      TField field = new TField();
      if (__isset.personEmpId) {
        field.Name = "personEmpId";
        field.Type = TType.I64;
        field.ID = 1;
        oprot.WriteFieldBegin(field);
        oprot.WriteI64(PersonEmpId);
        oprot.WriteFieldEnd();
      }
      if (__isset.scheduleType) {
        field.Name = "scheduleType";
        field.Type = TType.Bool;
        field.ID = 2;
        oprot.WriteFieldBegin(field);
        oprot.WriteBool(ScheduleType);
        oprot.WriteFieldEnd();
      }
      if (Text != null && __isset.text) {
        field.Name = "text";
        field.Type = TType.String;
        field.ID = 3;
        oprot.WriteFieldBegin(field);
        oprot.WriteString(Text);
        oprot.WriteFieldEnd();
      }
      if (__isset.employeeTypeId) {
        field.Name = "employeeTypeId";
        field.Type = TType.I32;
        field.ID = 4;
        oprot.WriteFieldBegin(field);
        oprot.WriteI32(EmployeeTypeId);
        oprot.WriteFieldEnd();
      }
      if (EmployeeType != null && __isset.employeeType) {
        field.Name = "employeeType";
        field.Type = TType.String;
        field.ID = 5;
        oprot.WriteFieldBegin(field);
        oprot.WriteString(EmployeeType);
        oprot.WriteFieldEnd();
      }
      if (__isset.day) {
        field.Name = "day";
        field.Type = TType.I32;
        field.ID = 6;
        oprot.WriteFieldBegin(field);
        oprot.WriteI32(Day);
        oprot.WriteFieldEnd();
      }
      if (StartTime != null && __isset.startTime) {
        field.Name = "startTime";
        field.Type = TType.String;
        field.ID = 7;
        oprot.WriteFieldBegin(field);
        oprot.WriteString(StartTime);
        oprot.WriteFieldEnd();
      }
      if (EndTime != null && __isset.endTime) {
        field.Name = "endTime";
        field.Type = TType.String;
        field.ID = 8;
        oprot.WriteFieldBegin(field);
        oprot.WriteString(EndTime);
        oprot.WriteFieldEnd();
      }
      oprot.WriteFieldStop();
      oprot.WriteStructEnd();
    }

    public override string ToString() {
      StringBuilder __sb = new StringBuilder("EmployeeWorkSchedule(");
      bool __first = true;
      if (__isset.personEmpId) {
        if(!__first) { __sb.Append(", "); }
        __first = false;
        __sb.Append("PersonEmpId: ");
        __sb.Append(PersonEmpId);
      }
      if (__isset.scheduleType) {
        if(!__first) { __sb.Append(", "); }
        __first = false;
        __sb.Append("ScheduleType: ");
        __sb.Append(ScheduleType);
      }
      if (Text != null && __isset.text) {
        if(!__first) { __sb.Append(", "); }
        __first = false;
        __sb.Append("Text: ");
        __sb.Append(Text);
      }
      if (__isset.employeeTypeId) {
        if(!__first) { __sb.Append(", "); }
        __first = false;
        __sb.Append("EmployeeTypeId: ");
        __sb.Append(EmployeeTypeId);
      }
      if (EmployeeType != null && __isset.employeeType) {
        if(!__first) { __sb.Append(", "); }
        __first = false;
        __sb.Append("EmployeeType: ");
        __sb.Append(EmployeeType);
      }
      if (__isset.day) {
        if(!__first) { __sb.Append(", "); }
        __first = false;
        __sb.Append("Day: ");
        __sb.Append(Day);
      }
      if (StartTime != null && __isset.startTime) {
        if(!__first) { __sb.Append(", "); }
        __first = false;
        __sb.Append("StartTime: ");
        __sb.Append(StartTime);
      }
      if (EndTime != null && __isset.endTime) {
        if(!__first) { __sb.Append(", "); }
        __first = false;
        __sb.Append("EndTime: ");
        __sb.Append(EndTime);
      }
      __sb.Append(")");
      return __sb.ToString();
    }

  }

}
