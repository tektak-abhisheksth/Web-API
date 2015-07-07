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
  public partial class EmployeeRatings : TBase
  {
    private int _userId;
    private string _employeeIds;
    private int _rating;

    public int UserId
    {
      get
      {
        return _userId;
      }
      set
      {
        __isset.userId = true;
        this._userId = value;
      }
    }

    public string EmployeeIds
    {
      get
      {
        return _employeeIds;
      }
      set
      {
        __isset.employeeIds = true;
        this._employeeIds = value;
      }
    }

    public int Rating
    {
      get
      {
        return _rating;
      }
      set
      {
        __isset.rating = true;
        this._rating = value;
      }
    }


    public Isset __isset;
    #if !SILVERLIGHT
    [Serializable]
    #endif
    public struct Isset {
      public bool userId;
      public bool employeeIds;
      public bool rating;
    }

    public EmployeeRatings() {
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
            if (field.Type == TType.I32) {
              UserId = iprot.ReadI32();
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 2:
            if (field.Type == TType.String) {
              EmployeeIds = iprot.ReadString();
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 3:
            if (field.Type == TType.I32) {
              Rating = iprot.ReadI32();
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
      TStruct struc = new TStruct("EmployeeRatings");
      oprot.WriteStructBegin(struc);
      TField field = new TField();
      if (__isset.userId) {
        field.Name = "userId";
        field.Type = TType.I32;
        field.ID = 1;
        oprot.WriteFieldBegin(field);
        oprot.WriteI32(UserId);
        oprot.WriteFieldEnd();
      }
      if (EmployeeIds != null && __isset.employeeIds) {
        field.Name = "employeeIds";
        field.Type = TType.String;
        field.ID = 2;
        oprot.WriteFieldBegin(field);
        oprot.WriteString(EmployeeIds);
        oprot.WriteFieldEnd();
      }
      if (__isset.rating) {
        field.Name = "rating";
        field.Type = TType.I32;
        field.ID = 3;
        oprot.WriteFieldBegin(field);
        oprot.WriteI32(Rating);
        oprot.WriteFieldEnd();
      }
      oprot.WriteFieldStop();
      oprot.WriteStructEnd();
    }

    public override string ToString() {
      StringBuilder __sb = new StringBuilder("EmployeeRatings(");
      bool __first = true;
      if (__isset.userId) {
        if(!__first) { __sb.Append(", "); }
        __first = false;
        __sb.Append("UserId: ");
        __sb.Append(UserId);
      }
      if (EmployeeIds != null && __isset.employeeIds) {
        if(!__first) { __sb.Append(", "); }
        __first = false;
        __sb.Append("EmployeeIds: ");
        __sb.Append(EmployeeIds);
      }
      if (__isset.rating) {
        if(!__first) { __sb.Append(", "); }
        __first = false;
        __sb.Append("Rating: ");
        __sb.Append(Rating);
      }
      __sb.Append(")");
      return __sb.ToString();
    }

  }

}
