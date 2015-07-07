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
  public partial class Elif : TBase
  {
    private string _fileId;
    private string _username;
    private bool _askWebp;
    private SizedCodes _sizeCodes;
    private bool _isProfilePic;

    public string FileId
    {
      get
      {
        return _fileId;
      }
      set
      {
        __isset.fileId = true;
        this._fileId = value;
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

    public bool AskWebp
    {
      get
      {
        return _askWebp;
      }
      set
      {
        __isset.askWebp = true;
        this._askWebp = value;
      }
    }

    /// <summary>
    /// 
    /// <seealso cref="SizedCodes"/>
    /// </summary>
    public SizedCodes SizeCodes
    {
      get
      {
        return _sizeCodes;
      }
      set
      {
        __isset.sizeCodes = true;
        this._sizeCodes = value;
      }
    }

    public bool IsProfilePic
    {
      get
      {
        return _isProfilePic;
      }
      set
      {
        __isset.isProfilePic = true;
        this._isProfilePic = value;
      }
    }


    public Isset __isset;
    #if !SILVERLIGHT
    [Serializable]
    #endif
    public struct Isset {
      public bool fileId;
      public bool username;
      public bool askWebp;
      public bool sizeCodes;
      public bool isProfilePic;
    }

    public Elif() {
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
            if (field.Type == TType.String) {
              FileId = iprot.ReadString();
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 2:
            if (field.Type == TType.String) {
              Username = iprot.ReadString();
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 3:
            if (field.Type == TType.Bool) {
              AskWebp = iprot.ReadBool();
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 4:
            if (field.Type == TType.I32) {
              SizeCodes = (SizedCodes)iprot.ReadI32();
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 5:
            if (field.Type == TType.Bool) {
              IsProfilePic = iprot.ReadBool();
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
      TStruct struc = new TStruct("Elif");
      oprot.WriteStructBegin(struc);
      TField field = new TField();
      if (FileId != null && __isset.fileId) {
        field.Name = "fileId";
        field.Type = TType.String;
        field.ID = 1;
        oprot.WriteFieldBegin(field);
        oprot.WriteString(FileId);
        oprot.WriteFieldEnd();
      }
      if (Username != null && __isset.username) {
        field.Name = "username";
        field.Type = TType.String;
        field.ID = 2;
        oprot.WriteFieldBegin(field);
        oprot.WriteString(Username);
        oprot.WriteFieldEnd();
      }
      if (__isset.askWebp) {
        field.Name = "askWebp";
        field.Type = TType.Bool;
        field.ID = 3;
        oprot.WriteFieldBegin(field);
        oprot.WriteBool(AskWebp);
        oprot.WriteFieldEnd();
      }
      if (__isset.sizeCodes) {
        field.Name = "sizeCodes";
        field.Type = TType.I32;
        field.ID = 4;
        oprot.WriteFieldBegin(field);
        oprot.WriteI32((int)SizeCodes);
        oprot.WriteFieldEnd();
      }
      if (__isset.isProfilePic) {
        field.Name = "isProfilePic";
        field.Type = TType.Bool;
        field.ID = 5;
        oprot.WriteFieldBegin(field);
        oprot.WriteBool(IsProfilePic);
        oprot.WriteFieldEnd();
      }
      oprot.WriteFieldStop();
      oprot.WriteStructEnd();
    }

    public override string ToString() {
      StringBuilder __sb = new StringBuilder("Elif(");
      bool __first = true;
      if (FileId != null && __isset.fileId) {
        if(!__first) { __sb.Append(", "); }
        __first = false;
        __sb.Append("FileId: ");
        __sb.Append(FileId);
      }
      if (Username != null && __isset.username) {
        if(!__first) { __sb.Append(", "); }
        __first = false;
        __sb.Append("Username: ");
        __sb.Append(Username);
      }
      if (__isset.askWebp) {
        if(!__first) { __sb.Append(", "); }
        __first = false;
        __sb.Append("AskWebp: ");
        __sb.Append(AskWebp);
      }
      if (__isset.sizeCodes) {
        if(!__first) { __sb.Append(", "); }
        __first = false;
        __sb.Append("SizeCodes: ");
        __sb.Append(SizeCodes);
      }
      if (__isset.isProfilePic) {
        if(!__first) { __sb.Append(", "); }
        __first = false;
        __sb.Append("IsProfilePic: ");
        __sb.Append(IsProfilePic);
      }
      __sb.Append(")");
      return __sb.ToString();
    }

  }

}