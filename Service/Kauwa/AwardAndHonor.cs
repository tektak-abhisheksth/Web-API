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
  public partial class AwardAndHonor : TBase
  {
    private int _userId;
    private int _mode;
    private string _title;
    private string _issuer;
    private string _date;
    private string _description;
    private long _awardAndHonorId;
    private string _username;
    private string _pictureUrl;
    private string _userIdOrName;

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

    public int Mode
    {
      get
      {
        return _mode;
      }
      set
      {
        __isset.mode = true;
        this._mode = value;
      }
    }

    public string Title
    {
      get
      {
        return _title;
      }
      set
      {
        __isset.title = true;
        this._title = value;
      }
    }

    public string Issuer
    {
      get
      {
        return _issuer;
      }
      set
      {
        __isset.issuer = true;
        this._issuer = value;
      }
    }

    public string Date
    {
      get
      {
        return _date;
      }
      set
      {
        __isset.date = true;
        this._date = value;
      }
    }

    public string Description
    {
      get
      {
        return _description;
      }
      set
      {
        __isset.description = true;
        this._description = value;
      }
    }

    public long AwardAndHonorId
    {
      get
      {
        return _awardAndHonorId;
      }
      set
      {
        __isset.awardAndHonorId = true;
        this._awardAndHonorId = value;
      }
    }

    public string Username
    {
      get
      {
        return _username;
      }
      set
      {
        __isset.username = true;
        this._username = value;
      }
    }

    public string PictureUrl
    {
      get
      {
        return _pictureUrl;
      }
      set
      {
        __isset.pictureUrl = true;
        this._pictureUrl = value;
      }
    }

    public string UserIdOrName
    {
      get
      {
        return _userIdOrName;
      }
      set
      {
        __isset.userIdOrName = true;
        this._userIdOrName = value;
      }
    }


    public Isset __isset;
    #if !SILVERLIGHT
    [Serializable]
    #endif
    public struct Isset {
      public bool userId;
      public bool mode;
      public bool title;
      public bool issuer;
      public bool date;
      public bool description;
      public bool awardAndHonorId;
      public bool username;
      public bool pictureUrl;
      public bool userIdOrName;
    }

    public AwardAndHonor() {
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
            if (field.Type == TType.I32) {
              Mode = iprot.ReadI32();
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 3:
            if (field.Type == TType.String) {
              Title = iprot.ReadString();
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 4:
            if (field.Type == TType.String) {
              Issuer = iprot.ReadString();
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 5:
            if (field.Type == TType.String) {
              Date = iprot.ReadString();
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 6:
            if (field.Type == TType.String) {
              Description = iprot.ReadString();
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 7:
            if (field.Type == TType.I64) {
              AwardAndHonorId = iprot.ReadI64();
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 8:
            if (field.Type == TType.String) {
              Username = iprot.ReadString();
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 9:
            if (field.Type == TType.String) {
              PictureUrl = iprot.ReadString();
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 10:
            if (field.Type == TType.String) {
              UserIdOrName = iprot.ReadString();
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
      TStruct struc = new TStruct("AwardAndHonor");
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
      if (__isset.mode) {
        field.Name = "mode";
        field.Type = TType.I32;
        field.ID = 2;
        oprot.WriteFieldBegin(field);
        oprot.WriteI32(Mode);
        oprot.WriteFieldEnd();
      }
      if (Title != null && __isset.title) {
        field.Name = "title";
        field.Type = TType.String;
        field.ID = 3;
        oprot.WriteFieldBegin(field);
        oprot.WriteString(Title);
        oprot.WriteFieldEnd();
      }
      if (Issuer != null && __isset.issuer) {
        field.Name = "issuer";
        field.Type = TType.String;
        field.ID = 4;
        oprot.WriteFieldBegin(field);
        oprot.WriteString(Issuer);
        oprot.WriteFieldEnd();
      }
      if (Date != null && __isset.date) {
        field.Name = "date";
        field.Type = TType.String;
        field.ID = 5;
        oprot.WriteFieldBegin(field);
        oprot.WriteString(Date);
        oprot.WriteFieldEnd();
      }
      if (Description != null && __isset.description) {
        field.Name = "description";
        field.Type = TType.String;
        field.ID = 6;
        oprot.WriteFieldBegin(field);
        oprot.WriteString(Description);
        oprot.WriteFieldEnd();
      }
      if (__isset.awardAndHonorId) {
        field.Name = "awardAndHonorId";
        field.Type = TType.I64;
        field.ID = 7;
        oprot.WriteFieldBegin(field);
        oprot.WriteI64(AwardAndHonorId);
        oprot.WriteFieldEnd();
      }
      if (Username != null && __isset.username) {
        field.Name = "username";
        field.Type = TType.String;
        field.ID = 8;
        oprot.WriteFieldBegin(field);
        oprot.WriteString(Username);
        oprot.WriteFieldEnd();
      }
      if (PictureUrl != null && __isset.pictureUrl) {
        field.Name = "pictureUrl";
        field.Type = TType.String;
        field.ID = 9;
        oprot.WriteFieldBegin(field);
        oprot.WriteString(PictureUrl);
        oprot.WriteFieldEnd();
      }
      if (UserIdOrName != null && __isset.userIdOrName) {
        field.Name = "userIdOrName";
        field.Type = TType.String;
        field.ID = 10;
        oprot.WriteFieldBegin(field);
        oprot.WriteString(UserIdOrName);
        oprot.WriteFieldEnd();
      }
      oprot.WriteFieldStop();
      oprot.WriteStructEnd();
    }

    public override string ToString() {
      StringBuilder __sb = new StringBuilder("AwardAndHonor(");
      bool __first = true;
      if (__isset.userId) {
        if(!__first) { __sb.Append(", "); }
        __first = false;
        __sb.Append("UserId: ");
        __sb.Append(UserId);
      }
      if (__isset.mode) {
        if(!__first) { __sb.Append(", "); }
        __first = false;
        __sb.Append("Mode: ");
        __sb.Append(Mode);
      }
      if (Title != null && __isset.title) {
        if(!__first) { __sb.Append(", "); }
        __first = false;
        __sb.Append("Title: ");
        __sb.Append(Title);
      }
      if (Issuer != null && __isset.issuer) {
        if(!__first) { __sb.Append(", "); }
        __first = false;
        __sb.Append("Issuer: ");
        __sb.Append(Issuer);
      }
      if (Date != null && __isset.date) {
        if(!__first) { __sb.Append(", "); }
        __first = false;
        __sb.Append("Date: ");
        __sb.Append(Date);
      }
      if (Description != null && __isset.description) {
        if(!__first) { __sb.Append(", "); }
        __first = false;
        __sb.Append("Description: ");
        __sb.Append(Description);
      }
      if (__isset.awardAndHonorId) {
        if(!__first) { __sb.Append(", "); }
        __first = false;
        __sb.Append("AwardAndHonorId: ");
        __sb.Append(AwardAndHonorId);
      }
      if (Username != null && __isset.username) {
        if(!__first) { __sb.Append(", "); }
        __first = false;
        __sb.Append("Username: ");
        __sb.Append(Username);
      }
      if (PictureUrl != null && __isset.pictureUrl) {
        if(!__first) { __sb.Append(", "); }
        __first = false;
        __sb.Append("PictureUrl: ");
        __sb.Append(PictureUrl);
      }
      if (UserIdOrName != null && __isset.userIdOrName) {
        if(!__first) { __sb.Append(", "); }
        __first = false;
        __sb.Append("UserIdOrName: ");
        __sb.Append(UserIdOrName);
      }
      __sb.Append(")");
      return __sb.ToString();
    }

  }

}