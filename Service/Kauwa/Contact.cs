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
  public partial class Contact : TBase
  {
    private long _contactId;
    private string _userId;
    private string _contactTypeId;
    private string _address;
    private string _phone;
    private string _mobile;
    private string _email;
    private string _fax;
    private string _website;

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

    public string UserId
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

    public string ContactTypeId
    {
      get
      {
        return _contactTypeId;
      }
      set
      {
        __isset.contactTypeId = true;
        this._contactTypeId = value;
      }
    }

    public string Address
    {
      get
      {
        return _address;
      }
      set
      {
        __isset.address = true;
        this._address = value;
      }
    }

    public string Phone
    {
      get
      {
        return _phone;
      }
      set
      {
        __isset.phone = true;
        this._phone = value;
      }
    }

    public string Mobile
    {
      get
      {
        return _mobile;
      }
      set
      {
        __isset.mobile = true;
        this._mobile = value;
      }
    }

    public string Email
    {
      get
      {
        return _email;
      }
      set
      {
        __isset.email = true;
        this._email = value;
      }
    }

    public string Fax
    {
      get
      {
        return _fax;
      }
      set
      {
        __isset.fax = true;
        this._fax = value;
      }
    }

    public string Website
    {
      get
      {
        return _website;
      }
      set
      {
        __isset.website = true;
        this._website = value;
      }
    }


    public Isset __isset;
    #if !SILVERLIGHT
    [Serializable]
    #endif
    public struct Isset {
      public bool contactId;
      public bool userId;
      public bool contactTypeId;
      public bool address;
      public bool phone;
      public bool mobile;
      public bool email;
      public bool fax;
      public bool website;
    }

    public Contact() {
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
              ContactId = iprot.ReadI64();
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 2:
            if (field.Type == TType.String) {
              UserId = iprot.ReadString();
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 3:
            if (field.Type == TType.String) {
              ContactTypeId = iprot.ReadString();
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 4:
            if (field.Type == TType.String) {
              Address = iprot.ReadString();
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 5:
            if (field.Type == TType.String) {
              Phone = iprot.ReadString();
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 6:
            if (field.Type == TType.String) {
              Mobile = iprot.ReadString();
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 7:
            if (field.Type == TType.String) {
              Email = iprot.ReadString();
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 8:
            if (field.Type == TType.String) {
              Fax = iprot.ReadString();
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 9:
            if (field.Type == TType.String) {
              Website = iprot.ReadString();
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
      TStruct struc = new TStruct("Contact");
      oprot.WriteStructBegin(struc);
      TField field = new TField();
      if (__isset.contactId) {
        field.Name = "contactId";
        field.Type = TType.I64;
        field.ID = 1;
        oprot.WriteFieldBegin(field);
        oprot.WriteI64(ContactId);
        oprot.WriteFieldEnd();
      }
      if (UserId != null && __isset.userId) {
        field.Name = "userId";
        field.Type = TType.String;
        field.ID = 2;
        oprot.WriteFieldBegin(field);
        oprot.WriteString(UserId);
        oprot.WriteFieldEnd();
      }
      if (ContactTypeId != null && __isset.contactTypeId) {
        field.Name = "contactTypeId";
        field.Type = TType.String;
        field.ID = 3;
        oprot.WriteFieldBegin(field);
        oprot.WriteString(ContactTypeId);
        oprot.WriteFieldEnd();
      }
      if (Address != null && __isset.address) {
        field.Name = "address";
        field.Type = TType.String;
        field.ID = 4;
        oprot.WriteFieldBegin(field);
        oprot.WriteString(Address);
        oprot.WriteFieldEnd();
      }
      if (Phone != null && __isset.phone) {
        field.Name = "phone";
        field.Type = TType.String;
        field.ID = 5;
        oprot.WriteFieldBegin(field);
        oprot.WriteString(Phone);
        oprot.WriteFieldEnd();
      }
      if (Mobile != null && __isset.mobile) {
        field.Name = "mobile";
        field.Type = TType.String;
        field.ID = 6;
        oprot.WriteFieldBegin(field);
        oprot.WriteString(Mobile);
        oprot.WriteFieldEnd();
      }
      if (Email != null && __isset.email) {
        field.Name = "email";
        field.Type = TType.String;
        field.ID = 7;
        oprot.WriteFieldBegin(field);
        oprot.WriteString(Email);
        oprot.WriteFieldEnd();
      }
      if (Fax != null && __isset.fax) {
        field.Name = "fax";
        field.Type = TType.String;
        field.ID = 8;
        oprot.WriteFieldBegin(field);
        oprot.WriteString(Fax);
        oprot.WriteFieldEnd();
      }
      if (Website != null && __isset.website) {
        field.Name = "website";
        field.Type = TType.String;
        field.ID = 9;
        oprot.WriteFieldBegin(field);
        oprot.WriteString(Website);
        oprot.WriteFieldEnd();
      }
      oprot.WriteFieldStop();
      oprot.WriteStructEnd();
    }

    public override string ToString() {
      StringBuilder __sb = new StringBuilder("Contact(");
      bool __first = true;
      if (__isset.contactId) {
        if(!__first) { __sb.Append(", "); }
        __first = false;
        __sb.Append("ContactId: ");
        __sb.Append(ContactId);
      }
      if (UserId != null && __isset.userId) {
        if(!__first) { __sb.Append(", "); }
        __first = false;
        __sb.Append("UserId: ");
        __sb.Append(UserId);
      }
      if (ContactTypeId != null && __isset.contactTypeId) {
        if(!__first) { __sb.Append(", "); }
        __first = false;
        __sb.Append("ContactTypeId: ");
        __sb.Append(ContactTypeId);
      }
      if (Address != null && __isset.address) {
        if(!__first) { __sb.Append(", "); }
        __first = false;
        __sb.Append("Address: ");
        __sb.Append(Address);
      }
      if (Phone != null && __isset.phone) {
        if(!__first) { __sb.Append(", "); }
        __first = false;
        __sb.Append("Phone: ");
        __sb.Append(Phone);
      }
      if (Mobile != null && __isset.mobile) {
        if(!__first) { __sb.Append(", "); }
        __first = false;
        __sb.Append("Mobile: ");
        __sb.Append(Mobile);
      }
      if (Email != null && __isset.email) {
        if(!__first) { __sb.Append(", "); }
        __first = false;
        __sb.Append("Email: ");
        __sb.Append(Email);
      }
      if (Fax != null && __isset.fax) {
        if(!__first) { __sb.Append(", "); }
        __first = false;
        __sb.Append("Fax: ");
        __sb.Append(Fax);
      }
      if (Website != null && __isset.website) {
        if(!__first) { __sb.Append(", "); }
        __first = false;
        __sb.Append("Website: ");
        __sb.Append(Website);
      }
      __sb.Append(")");
      return __sb.ToString();
    }

  }

}