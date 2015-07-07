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
  public partial class MutualFriendResponse : TBase
  {
    private List<MutualFriend> _mutualFriends;
    private bool _hasNextPage;
    private int _count;

    public List<MutualFriend> MutualFriends
    {
      get
      {
        return _mutualFriends;
      }
      set
      {
        __isset.mutualFriends = true;
        this._mutualFriends = value;
      }
    }

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

    public int Count
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
      public bool mutualFriends;
      public bool hasNextPage;
      public bool count;
    }

    public MutualFriendResponse() {
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
                MutualFriends = new List<MutualFriend>();
                TList _list136 = iprot.ReadListBegin();
                for( int _i137 = 0; _i137 < _list136.Count; ++_i137)
                {
                  MutualFriend _elem138;
                  _elem138 = new MutualFriend();
                  _elem138.Read(iprot);
                  MutualFriends.Add(_elem138);
                }
                iprot.ReadListEnd();
              }
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 2:
            if (field.Type == TType.Bool) {
              HasNextPage = iprot.ReadBool();
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 3:
            if (field.Type == TType.I32) {
              Count = iprot.ReadI32();
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
      TStruct struc = new TStruct("MutualFriendResponse");
      oprot.WriteStructBegin(struc);
      TField field = new TField();
      if (MutualFriends != null && __isset.mutualFriends) {
        field.Name = "mutualFriends";
        field.Type = TType.List;
        field.ID = 1;
        oprot.WriteFieldBegin(field);
        {
          oprot.WriteListBegin(new TList(TType.Struct, MutualFriends.Count));
          foreach (MutualFriend _iter139 in MutualFriends)
          {
            _iter139.Write(oprot);
          }
          oprot.WriteListEnd();
        }
        oprot.WriteFieldEnd();
      }
      if (__isset.hasNextPage) {
        field.Name = "hasNextPage";
        field.Type = TType.Bool;
        field.ID = 2;
        oprot.WriteFieldBegin(field);
        oprot.WriteBool(HasNextPage);
        oprot.WriteFieldEnd();
      }
      if (__isset.count) {
        field.Name = "count";
        field.Type = TType.I32;
        field.ID = 3;
        oprot.WriteFieldBegin(field);
        oprot.WriteI32(Count);
        oprot.WriteFieldEnd();
      }
      oprot.WriteFieldStop();
      oprot.WriteStructEnd();
    }

    public override string ToString() {
      StringBuilder __sb = new StringBuilder("MutualFriendResponse(");
      bool __first = true;
      if (MutualFriends != null && __isset.mutualFriends) {
        if(!__first) { __sb.Append(", "); }
        __first = false;
        __sb.Append("MutualFriends: ");
        __sb.Append(MutualFriends);
      }
      if (__isset.hasNextPage) {
        if(!__first) { __sb.Append(", "); }
        __first = false;
        __sb.Append("HasNextPage: ");
        __sb.Append(HasNextPage);
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
