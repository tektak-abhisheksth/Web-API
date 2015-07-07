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
  public partial class NotificationMobList : TBase
  {
    private bool _hasNextPage;
    private List<NotificationMob> _notifications;
    private int _pageIndex;
    private int _pageSize;
    private int _notificationCount;
    private int _requestCount;
    private string _userName;

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

    public List<NotificationMob> Notifications
    {
      get
      {
        return _notifications;
      }
      set
      {
        __isset.notifications = true;
        this._notifications = value;
      }
    }

    public int PageIndex
    {
      get
      {
        return _pageIndex;
      }
      set
      {
        __isset.pageIndex = true;
        this._pageIndex = value;
      }
    }

    public int PageSize
    {
      get
      {
        return _pageSize;
      }
      set
      {
        __isset.pageSize = true;
        this._pageSize = value;
      }
    }

    public int NotificationCount
    {
      get
      {
        return _notificationCount;
      }
      set
      {
        __isset.notificationCount = true;
        this._notificationCount = value;
      }
    }

    public int RequestCount
    {
      get
      {
        return _requestCount;
      }
      set
      {
        __isset.requestCount = true;
        this._requestCount = value;
      }
    }

    public string UserName
    {
      get
      {
        return _userName;
      }
      set
      {
        __isset.userName = true;
        this._userName = value;
      }
    }


    public Isset __isset;
    #if !SILVERLIGHT
    [Serializable]
    #endif
    public struct Isset {
      public bool hasNextPage;
      public bool notifications;
      public bool pageIndex;
      public bool pageSize;
      public bool notificationCount;
      public bool requestCount;
      public bool userName;
    }

    public NotificationMobList() {
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
                Notifications = new List<NotificationMob>();
                TList _list104 = iprot.ReadListBegin();
                for( int _i105 = 0; _i105 < _list104.Count; ++_i105)
                {
                  NotificationMob _elem106;
                  _elem106 = new NotificationMob();
                  _elem106.Read(iprot);
                  Notifications.Add(_elem106);
                }
                iprot.ReadListEnd();
              }
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 3:
            if (field.Type == TType.I32) {
              PageIndex = iprot.ReadI32();
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 4:
            if (field.Type == TType.I32) {
              PageSize = iprot.ReadI32();
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 5:
            if (field.Type == TType.I32) {
              NotificationCount = iprot.ReadI32();
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 6:
            if (field.Type == TType.I32) {
              RequestCount = iprot.ReadI32();
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 7:
            if (field.Type == TType.String) {
              UserName = iprot.ReadString();
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
      TStruct struc = new TStruct("NotificationMobList");
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
      if (Notifications != null && __isset.notifications) {
        field.Name = "notifications";
        field.Type = TType.List;
        field.ID = 2;
        oprot.WriteFieldBegin(field);
        {
          oprot.WriteListBegin(new TList(TType.Struct, Notifications.Count));
          foreach (NotificationMob _iter107 in Notifications)
          {
            _iter107.Write(oprot);
          }
          oprot.WriteListEnd();
        }
        oprot.WriteFieldEnd();
      }
      if (__isset.pageIndex) {
        field.Name = "pageIndex";
        field.Type = TType.I32;
        field.ID = 3;
        oprot.WriteFieldBegin(field);
        oprot.WriteI32(PageIndex);
        oprot.WriteFieldEnd();
      }
      if (__isset.pageSize) {
        field.Name = "pageSize";
        field.Type = TType.I32;
        field.ID = 4;
        oprot.WriteFieldBegin(field);
        oprot.WriteI32(PageSize);
        oprot.WriteFieldEnd();
      }
      if (__isset.notificationCount) {
        field.Name = "notificationCount";
        field.Type = TType.I32;
        field.ID = 5;
        oprot.WriteFieldBegin(field);
        oprot.WriteI32(NotificationCount);
        oprot.WriteFieldEnd();
      }
      if (__isset.requestCount) {
        field.Name = "requestCount";
        field.Type = TType.I32;
        field.ID = 6;
        oprot.WriteFieldBegin(field);
        oprot.WriteI32(RequestCount);
        oprot.WriteFieldEnd();
      }
      if (UserName != null && __isset.userName) {
        field.Name = "userName";
        field.Type = TType.String;
        field.ID = 7;
        oprot.WriteFieldBegin(field);
        oprot.WriteString(UserName);
        oprot.WriteFieldEnd();
      }
      oprot.WriteFieldStop();
      oprot.WriteStructEnd();
    }

    public override string ToString() {
      StringBuilder __sb = new StringBuilder("NotificationMobList(");
      bool __first = true;
      if (__isset.hasNextPage) {
        if(!__first) { __sb.Append(", "); }
        __first = false;
        __sb.Append("HasNextPage: ");
        __sb.Append(HasNextPage);
      }
      if (Notifications != null && __isset.notifications) {
        if(!__first) { __sb.Append(", "); }
        __first = false;
        __sb.Append("Notifications: ");
        __sb.Append(Notifications);
      }
      if (__isset.pageIndex) {
        if(!__first) { __sb.Append(", "); }
        __first = false;
        __sb.Append("PageIndex: ");
        __sb.Append(PageIndex);
      }
      if (__isset.pageSize) {
        if(!__first) { __sb.Append(", "); }
        __first = false;
        __sb.Append("PageSize: ");
        __sb.Append(PageSize);
      }
      if (__isset.notificationCount) {
        if(!__first) { __sb.Append(", "); }
        __first = false;
        __sb.Append("NotificationCount: ");
        __sb.Append(NotificationCount);
      }
      if (__isset.requestCount) {
        if(!__first) { __sb.Append(", "); }
        __first = false;
        __sb.Append("RequestCount: ");
        __sb.Append(RequestCount);
      }
      if (UserName != null && __isset.userName) {
        if(!__first) { __sb.Append(", "); }
        __first = false;
        __sb.Append("UserName: ");
        __sb.Append(UserName);
      }
      __sb.Append(")");
      return __sb.ToString();
    }

  }

}
