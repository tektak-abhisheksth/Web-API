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
  public partial class UserAcademicResponse : TBase
  {
    private DbStatus _dbStatus;
    private long _academicId;

    public DbStatus DbStatus
    {
      get
      {
        return _dbStatus;
      }
      set
      {
        __isset.dbStatus = true;
        this._dbStatus = value;
      }
    }

    public long AcademicId
    {
      get
      {
        return _academicId;
      }
      set
      {
        __isset.academicId = true;
        this._academicId = value;
      }
    }


    public Isset __isset;
    #if !SILVERLIGHT
    [Serializable]
    #endif
    public struct Isset {
      public bool dbStatus;
      public bool academicId;
    }

    public UserAcademicResponse() {
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
            if (field.Type == TType.Struct) {
              DbStatus = new DbStatus();
              DbStatus.Read(iprot);
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 2:
            if (field.Type == TType.I64) {
              AcademicId = iprot.ReadI64();
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
      TStruct struc = new TStruct("UserAcademicResponse");
      oprot.WriteStructBegin(struc);
      TField field = new TField();
      if (DbStatus != null && __isset.dbStatus) {
        field.Name = "dbStatus";
        field.Type = TType.Struct;
        field.ID = 1;
        oprot.WriteFieldBegin(field);
        DbStatus.Write(oprot);
        oprot.WriteFieldEnd();
      }
      if (__isset.academicId) {
        field.Name = "academicId";
        field.Type = TType.I64;
        field.ID = 2;
        oprot.WriteFieldBegin(field);
        oprot.WriteI64(AcademicId);
        oprot.WriteFieldEnd();
      }
      oprot.WriteFieldStop();
      oprot.WriteStructEnd();
    }

    public override string ToString() {
      StringBuilder __sb = new StringBuilder("UserAcademicResponse(");
      bool __first = true;
      if (DbStatus != null && __isset.dbStatus) {
        if(!__first) { __sb.Append(", "); }
        __first = false;
        __sb.Append("DbStatus: ");
        __sb.Append(DbStatus== null ? "<null>" : DbStatus.ToString());
      }
      if (__isset.academicId) {
        if(!__first) { __sb.Append(", "); }
        __first = false;
        __sb.Append("AcademicId: ");
        __sb.Append(AcademicId);
      }
      __sb.Append(")");
      return __sb.ToString();
    }

  }

}