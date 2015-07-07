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
  public partial class UserSearchResult : TBase
  {
    private List<User> _users;
    private string _cursor;
    private int _limit;
    private long _count;

    public List<User> Users
    {
      get
      {
        return _users;
      }
      set
      {
        __isset.users = true;
        this._users = value;
      }
    }

    public string Cursor
    {
      get
      {
        return _cursor;
      }
      set
      {
        __isset.cursor = true;
        this._cursor = value;
      }
    }

    public int Limit
    {
      get
      {
        return _limit;
      }
      set
      {
        __isset.limit = true;
        this._limit = value;
      }
    }

    public long Count
    {
      get
      {
        return _count;
      }
      set
      {
        __isset.count = true;
        this._count = value;
      }
    }


    public Isset __isset;
    #if !SILVERLIGHT
    [Serializable]
    #endif
    public struct Isset {
      public bool users;
      public bool cursor;
      public bool limit;
      public bool count;
    }

    public UserSearchResult() {
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
            if (field.Type == TType.List) {
              {
                Users = new List<User>();
                TList _list132 = iprot.ReadListBegin();
                for( int _i133 = 0; _i133 < _list132.Count; ++_i133)
                {
                  User _elem134;
                  _elem134 = new User();
                  _elem134.Read(iprot);
                  Users.Add(_elem134);
                }
                iprot.ReadListEnd();
              }
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 2:
            if (field.Type == TType.String) {
              Cursor = iprot.ReadString();
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 3:
            if (field.Type == TType.I32) {
              Limit = iprot.ReadI32();
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 4:
            if (field.Type == TType.I64) {
              Count = iprot.ReadI64();
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
      TStruct struc = new TStruct("UserSearchResult");
      oprot.WriteStructBegin(struc);
      TField field = new TField();
      if (Users != null && __isset.users) {
        field.Name = "users";
        field.Type = TType.List;
        field.ID = 1;
        oprot.WriteFieldBegin(field);
        {
          oprot.WriteListBegin(new TList(TType.Struct, Users.Count));
          foreach (User _iter135 in Users)
          {
            _iter135.Write(oprot);
          }
          oprot.WriteListEnd();
        }
        oprot.WriteFieldEnd();
      }
      if (Cursor != null && __isset.cursor) {
        field.Name = "cursor";
        field.Type = TType.String;
        field.ID = 2;
        oprot.WriteFieldBegin(field);
        oprot.WriteString(Cursor);
        oprot.WriteFieldEnd();
      }
      if (__isset.limit) {
        field.Name = "limit";
        field.Type = TType.I32;
        field.ID = 3;
        oprot.WriteFieldBegin(field);
        oprot.WriteI32(Limit);
        oprot.WriteFieldEnd();
      }
      if (__isset.count) {
        field.Name = "count";
        field.Type = TType.I64;
        field.ID = 4;
        oprot.WriteFieldBegin(field);
        oprot.WriteI64(Count);
        oprot.WriteFieldEnd();
      }
      oprot.WriteFieldStop();
      oprot.WriteStructEnd();
    }

    public override string ToString() {
      StringBuilder __sb = new StringBuilder("UserSearchResult(");
      bool __first = true;
      if (Users != null && __isset.users) {
        if(!__first) { __sb.Append(", "); }
        __first = false;
        __sb.Append("Users: ");
        __sb.Append(Users);
      }
      if (Cursor != null && __isset.cursor) {
        if(!__first) { __sb.Append(", "); }
        __first = false;
        __sb.Append("Cursor: ");
        __sb.Append(Cursor);
      }
      if (__isset.limit) {
        if(!__first) { __sb.Append(", "); }
        __first = false;
        __sb.Append("Limit: ");
        __sb.Append(Limit);
      }
      if (__isset.count) {
        if(!__first) { __sb.Append(", "); }
        __first = false;
        __sb.Append("Count: ");
        __sb.Append(Count);
      }
      __sb.Append(")");
      return __sb.ToString();
    }

  }

}
