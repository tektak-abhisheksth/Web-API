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
  public partial class SkillResponse : TBase
  {
    private bool _hasNextPage;
    private List<Skill> _skills;
    private int _thumbsUp;
    private int _thumbsDown;
    private int _yourThumb;
    private DbStatus _dbStatus;

    public bool HasNextPage
    {
      get
      {
        return _hasNextPage;
      }
      set
      {
        __isset.hasNextPage = true;
        this._hasNextPage = value;
      }
    }

    public List<Skill> Skills
    {
      get
      {
        return _skills;
      }
      set
      {
        __isset.skills = true;
        this._skills = value;
      }
    }

    public int ThumbsUp
    {
      get
      {
        return _thumbsUp;
      }
      set
      {
        __isset.thumbsUp = true;
        this._thumbsUp = value;
      }
    }

    public int ThumbsDown
    {
      get
      {
        return _thumbsDown;
      }
      set
      {
        __isset.thumbsDown = true;
        this._thumbsDown = value;
      }
    }

    public int YourThumb
    {
      get
      {
        return _yourThumb;
      }
      set
      {
        __isset.yourThumb = true;
        this._yourThumb = value;
      }
    }

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


    public Isset __isset;
    #if !SILVERLIGHT
    [Serializable]
    #endif
    public struct Isset {
      public bool hasNextPage;
      public bool skills;
      public bool thumbsUp;
      public bool thumbsDown;
      public bool yourThumb;
      public bool dbStatus;
    }

    public SkillResponse() {
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
            if (field.Type == TType.Bool) {
              HasNextPage = iprot.ReadBool();
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 2:
            if (field.Type == TType.List) {
              {
                Skills = new List<Skill>();
                TList _list140 = iprot.ReadListBegin();
                for( int _i141 = 0; _i141 < _list140.Count; ++_i141)
                {
                  Skill _elem142;
                  _elem142 = new Skill();
                  _elem142.Read(iprot);
                  Skills.Add(_elem142);
                }
                iprot.ReadListEnd();
              }
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 3:
            if (field.Type == TType.I32) {
              ThumbsUp = iprot.ReadI32();
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 4:
            if (field.Type == TType.I32) {
              ThumbsDown = iprot.ReadI32();
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 5:
            if (field.Type == TType.I32) {
              YourThumb = iprot.ReadI32();
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 6:
            if (field.Type == TType.Struct) {
              DbStatus = new DbStatus();
              DbStatus.Read(iprot);
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
      TStruct struc = new TStruct("SkillResponse");
      oprot.WriteStructBegin(struc);
      TField field = new TField();
      if (__isset.hasNextPage) {
        field.Name = "hasNextPage";
        field.Type = TType.Bool;
        field.ID = 1;
        oprot.WriteFieldBegin(field);
        oprot.WriteBool(HasNextPage);
        oprot.WriteFieldEnd();
      }
      if (Skills != null && __isset.skills) {
        field.Name = "skills";
        field.Type = TType.List;
        field.ID = 2;
        oprot.WriteFieldBegin(field);
        {
          oprot.WriteListBegin(new TList(TType.Struct, Skills.Count));
          foreach (Skill _iter143 in Skills)
          {
            _iter143.Write(oprot);
          }
          oprot.WriteListEnd();
        }
        oprot.WriteFieldEnd();
      }
      if (__isset.thumbsUp) {
        field.Name = "thumbsUp";
        field.Type = TType.I32;
        field.ID = 3;
        oprot.WriteFieldBegin(field);
        oprot.WriteI32(ThumbsUp);
        oprot.WriteFieldEnd();
      }
      if (__isset.thumbsDown) {
        field.Name = "thumbsDown";
        field.Type = TType.I32;
        field.ID = 4;
        oprot.WriteFieldBegin(field);
        oprot.WriteI32(ThumbsDown);
        oprot.WriteFieldEnd();
      }
      if (__isset.yourThumb) {
        field.Name = "yourThumb";
        field.Type = TType.I32;
        field.ID = 5;
        oprot.WriteFieldBegin(field);
        oprot.WriteI32(YourThumb);
        oprot.WriteFieldEnd();
      }
      if (DbStatus != null && __isset.dbStatus) {
        field.Name = "dbStatus";
        field.Type = TType.Struct;
        field.ID = 6;
        oprot.WriteFieldBegin(field);
        DbStatus.Write(oprot);
        oprot.WriteFieldEnd();
      }
      oprot.WriteFieldStop();
      oprot.WriteStructEnd();
    }

    public override string ToString() {
      StringBuilder __sb = new StringBuilder("SkillResponse(");
      bool __first = true;
      if (__isset.hasNextPage) {
        if(!__first) { __sb.Append(", "); }
        __first = false;
        __sb.Append("HasNextPage: ");
        __sb.Append(HasNextPage);
      }
      if (Skills != null && __isset.skills) {
        if(!__first) { __sb.Append(", "); }
        __first = false;
        __sb.Append("Skills: ");
        __sb.Append(Skills);
      }
      if (__isset.thumbsUp) {
        if(!__first) { __sb.Append(", "); }
        __first = false;
        __sb.Append("ThumbsUp: ");
        __sb.Append(ThumbsUp);
      }
      if (__isset.thumbsDown) {
        if(!__first) { __sb.Append(", "); }
        __first = false;
        __sb.Append("ThumbsDown: ");
        __sb.Append(ThumbsDown);
      }
      if (__isset.yourThumb) {
        if(!__first) { __sb.Append(", "); }
        __first = false;
        __sb.Append("YourThumb: ");
        __sb.Append(YourThumb);
      }
      if (DbStatus != null && __isset.dbStatus) {
        if(!__first) { __sb.Append(", "); }
        __first = false;
        __sb.Append("DbStatus: ");
        __sb.Append(DbStatus== null ? "<null>" : DbStatus.ToString());
      }
      __sb.Append(")");
      return __sb.ToString();
    }

  }

}