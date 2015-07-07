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
  public partial class MobileContact : TBase
  {
    private int _userMobileContactsId;
    private string _countryCode;
    private string _primaryContactNumber;

    public int UserMobileContactsId
    {
      get
      {
        return _userMobileContactsId;
      }
      set
      {
        __isset.userMobileContactsId = true;
        this._userMobileContactsId = value;
      }
    }

    public string CountryCode
    {
      get
      {
        return _countryCode;
      }
      set
      {
        __isset.countryCode = true;
        this._countryCode = value;
      }
    }

    public string PrimaryContactNumber
    {
      get
      {
        return _primaryContactNumber;
      }
      set
      {
        __isset.primaryContactNumber = true;
        this._primaryContactNumber = value;
      }
    }


    public Isset __isset;
    #if !SILVERLIGHT
    [Serializable]
    #endif
    public struct Isset {
      public bool userMobileContactsId;
      public bool countryCode;
      public bool primaryContactNumber;
    }

    public MobileContact() {
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
              UserMobileContactsId = iprot.ReadI32();
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 2:
            if (field.Type == TType.String) {
              CountryCode = iprot.ReadString();
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 3:
            if (field.Type == TType.String) {
              PrimaryContactNumber = iprot.ReadString();
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
      TStruct struc = new TStruct("MobileContact");
      oprot.WriteStructBegin(struc);
      TField field = new TField();
      if (__isset.userMobileContactsId) {
        field.Name = "userMobileContactsId";
        field.Type = TType.I32;
        field.ID = 1;
        oprot.WriteFieldBegin(field);
        oprot.WriteI32(UserMobileContactsId);
        oprot.WriteFieldEnd();
      }
      if (CountryCode != null && __isset.countryCode) {
        field.Name = "countryCode";
        field.Type = TType.String;
        field.ID = 2;
        oprot.WriteFieldBegin(field);
        oprot.WriteString(CountryCode);
        oprot.WriteFieldEnd();
      }
      if (PrimaryContactNumber != null && __isset.primaryContactNumber) {
        field.Name = "primaryContactNumber";
        field.Type = TType.String;
        field.ID = 3;
        oprot.WriteFieldBegin(field);
        oprot.WriteString(PrimaryContactNumber);
        oprot.WriteFieldEnd();
      }
      oprot.WriteFieldStop();
      oprot.WriteStructEnd();
    }

    public override string ToString() {
      StringBuilder __sb = new StringBuilder("MobileContact(");
      bool __first = true;
      if (__isset.userMobileContactsId) {
        if(!__first) { __sb.Append(", "); }
        __first = false;
        __sb.Append("UserMobileContactsId: ");
        __sb.Append(UserMobileContactsId);
      }
      if (CountryCode != null && __isset.countryCode) {
        if(!__first) { __sb.Append(", "); }
        __first = false;
        __sb.Append("CountryCode: ");
        __sb.Append(CountryCode);
      }
      if (PrimaryContactNumber != null && __isset.primaryContactNumber) {
        if(!__first) { __sb.Append(", "); }
        __first = false;
        __sb.Append("PrimaryContactNumber: ");
        __sb.Append(PrimaryContactNumber);
      }
      __sb.Append(")");
      return __sb.ToString();
    }

  }

}
