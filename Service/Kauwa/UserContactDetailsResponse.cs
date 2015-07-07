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
  public partial class UserContactDetailsResponse : TBase
  {
    private string _contactChatNetworkId;
    private string _contactCustomIds;
    private long _contactId;
    private string _customContactsCustomeIds;
    private DbStatus _dbstatus;

    public string ContactChatNetworkId
    {
      get
      {
        return _contactChatNetworkId;
      }
      set
      {
        __isset.contactChatNetworkId = true;
        this._contactChatNetworkId = value;
      }
    }

    public string ContactCustomIds
    {
      get
      {
        return _contactCustomIds;
      }
      set
      {
        __isset.contactCustomIds = true;
        this._contactCustomIds = value;
      }
    }

    public long ContactId
    {
      get
      {
        return _contactId;
      }
      set
      {
        __isset.contactId = true;
        this._contactId = value;
      }
    }

    public string CustomContactsCustomeIds
    {
      get
      {
        return _customContactsCustomeIds;
      }
      set
      {
        __isset.customContactsCustomeIds = true;
        this._customContactsCustomeIds = value;
      }
    }

    public DbStatus Dbstatus
    {
      get
      {
        return _dbstatus;
      }
      set
      {
        __isset.dbstatus = true;
        this._dbstatus = value;
      }
    }


    public Isset __isset;
    #if !SILVERLIGHT
    [Serializable]
    #endif
    public struct Isset {
      public bool contactChatNetworkId;
      public bool contactCustomIds;
      public bool contactId;
      public bool customContactsCustomeIds;
      public bool dbstatus;
    }

    public UserContactDetailsResponse() {
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
              ContactChatNetworkId = iprot.ReadString();
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 2:
            if (field.Type == TType.String) {
              ContactCustomIds = iprot.ReadString();
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 3:
            if (field.Type == TType.I64) {
              ContactId = iprot.ReadI64();
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 4:
            if (field.Type == TType.String) {
              CustomContactsCustomeIds = iprot.ReadString();
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 5:
            if (field.Type == TType.Struct) {
              Dbstatus = new DbStatus();
              Dbstatus.Read(iprot);
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
      TStruct struc = new TStruct("UserContactDetailsResponse");
      oprot.WriteStructBegin(struc);
      TField field = new TField();
      if (ContactChatNetworkId != null && __isset.contactChatNetworkId) {
        field.Name = "contactChatNetworkId";
        field.Type = TType.String;
        field.ID = 1;
        oprot.WriteFieldBegin(field);
        oprot.WriteString(ContactChatNetworkId);
        oprot.WriteFieldEnd();
      }
      if (ContactCustomIds != null && __isset.contactCustomIds) {
        field.Name = "contactCustomIds";
        field.Type = TType.String;
        field.ID = 2;
        oprot.WriteFieldBegin(field);
        oprot.WriteString(ContactCustomIds);
        oprot.WriteFieldEnd();
      }
      if (__isset.contactId) {
        field.Name = "contactId";
        field.Type = TType.I64;
        field.ID = 3;
        oprot.WriteFieldBegin(field);
        oprot.WriteI64(ContactId);
        oprot.WriteFieldEnd();
      }
      if (CustomContactsCustomeIds != null && __isset.customContactsCustomeIds) {
        field.Name = "customContactsCustomeIds";
        field.Type = TType.String;
        field.ID = 4;
        oprot.WriteFieldBegin(field);
        oprot.WriteString(CustomContactsCustomeIds);
        oprot.WriteFieldEnd();
      }
      if (Dbstatus != null && __isset.dbstatus) {
        field.Name = "dbstatus";
        field.Type = TType.Struct;
        field.ID = 5;
        oprot.WriteFieldBegin(field);
        Dbstatus.Write(oprot);
        oprot.WriteFieldEnd();
      }
      oprot.WriteFieldStop();
      oprot.WriteStructEnd();
    }

    public override string ToString() {
      StringBuilder __sb = new StringBuilder("UserContactDetailsResponse(");
      bool __first = true;
      if (ContactChatNetworkId != null && __isset.contactChatNetworkId) {
        if(!__first) { __sb.Append(", "); }
        __first = false;
        __sb.Append("ContactChatNetworkId: ");
        __sb.Append(ContactChatNetworkId);
      }
      if (ContactCustomIds != null && __isset.contactCustomIds) {
        if(!__first) { __sb.Append(", "); }
        __first = false;
        __sb.Append("ContactCustomIds: ");
        __sb.Append(ContactCustomIds);
      }
      if (__isset.contactId) {
        if(!__first) { __sb.Append(", "); }
        __first = false;
        __sb.Append("ContactId: ");
        __sb.Append(ContactId);
      }
      if (CustomContactsCustomeIds != null && __isset.customContactsCustomeIds) {
        if(!__first) { __sb.Append(", "); }
        __first = false;
        __sb.Append("CustomContactsCustomeIds: ");
        __sb.Append(CustomContactsCustomeIds);
      }
      if (Dbstatus != null && __isset.dbstatus) {
        if(!__first) { __sb.Append(", "); }
        __first = false;
        __sb.Append("Dbstatus: ");
        __sb.Append(Dbstatus== null ? "<null>" : Dbstatus.ToString());
      }
      __sb.Append(")");
      return __sb.ToString();
    }

  }

}
