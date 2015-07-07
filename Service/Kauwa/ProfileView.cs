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
  public partial class ProfileView : TBase
  {
    private int _userId;
    private string _username;
    private string _firstName;
    private string _lastName;
    private string _picture;
    private string _title;
    private int _userTypeId;
    private int _isConnected;
    private int _pageIndex;
    private int _pageSize;
    private string _viewDate;
    private bool _observed;
    private int _viewType;
    private string _positionName;
    private int _positionId;
    private int _newViews;
    private int _viewersCount;
    private int _typeId;

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

    public string FirstName
    {
      get
      {
        return _firstName;
      }
      set
      {
        __isset.firstName = true;
        this._firstName = value;
      }
    }

    public string LastName
    {
      get
      {
        return _lastName;
      }
      set
      {
        __isset.lastName = true;
        this._lastName = value;
      }
    }

    public string Picture
    {
      get
      {
        return _picture;
      }
      set
      {
        __isset.picture = true;
        this._picture = value;
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

    public int UserTypeId
    {
      get
      {
        return _userTypeId;
      }
      set
      {
        __isset.userTypeId = true;
        this._userTypeId = value;
      }
    }

    public int IsConnected
    {
      get
      {
        return _isConnected;
      }
      set
      {
        __isset.isConnected = true;
        this._isConnected = value;
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

    public string ViewDate
    {
      get
      {
        return _viewDate;
      }
      set
      {
        __isset.viewDate = true;
        this._viewDate = value;
      }
    }

    public bool Observed
    {
      get
      {
        return _observed;
      }
      set
      {
        __isset.observed = true;
        this._observed = value;
      }
    }

    public int ViewType
    {
      get
      {
        return _viewType;
      }
      set
      {
        __isset.viewType = true;
        this._viewType = value;
      }
    }

    public string PositionName
    {
      get
      {
        return _positionName;
      }
      set
      {
        __isset.positionName = true;
        this._positionName = value;
      }
    }

    public int PositionId
    {
      get
      {
        return _positionId;
      }
      set
      {
        __isset.positionId = true;
        this._positionId = value;
      }
    }

    public int NewViews
    {
      get
      {
        return _newViews;
      }
      set
      {
        __isset.newViews = true;
        this._newViews = value;
      }
    }

    public int ViewersCount
    {
      get
      {
        return _viewersCount;
      }
      set
      {
        __isset.viewersCount = true;
        this._viewersCount = value;
      }
    }

    public int TypeId
    {
      get
      {
        return _typeId;
      }
      set
      {
        __isset.typeId = true;
        this._typeId = value;
      }
    }


    public Isset __isset;
    #if !SILVERLIGHT
    [Serializable]
    #endif
    public struct Isset {
      public bool userId;
      public bool username;
      public bool firstName;
      public bool lastName;
      public bool picture;
      public bool title;
      public bool userTypeId;
      public bool isConnected;
      public bool pageIndex;
      public bool pageSize;
      public bool viewDate;
      public bool observed;
      public bool viewType;
      public bool positionName;
      public bool positionId;
      public bool newViews;
      public bool viewersCount;
      public bool typeId;
    }

    public ProfileView() {
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
            if (field.Type == TType.String) {
              Username = iprot.ReadString();
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 3:
            if (field.Type == TType.String) {
              FirstName = iprot.ReadString();
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 4:
            if (field.Type == TType.String) {
              LastName = iprot.ReadString();
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 5:
            if (field.Type == TType.String) {
              Picture = iprot.ReadString();
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 6:
            if (field.Type == TType.String) {
              Title = iprot.ReadString();
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 7:
            if (field.Type == TType.I32) {
              UserTypeId = iprot.ReadI32();
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 8:
            if (field.Type == TType.I32) {
              IsConnected = iprot.ReadI32();
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 9:
            if (field.Type == TType.I32) {
              PageIndex = iprot.ReadI32();
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 10:
            if (field.Type == TType.I32) {
              PageSize = iprot.ReadI32();
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 11:
            if (field.Type == TType.String) {
              ViewDate = iprot.ReadString();
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 12:
            if (field.Type == TType.Bool) {
              Observed = iprot.ReadBool();
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 13:
            if (field.Type == TType.I32) {
              ViewType = iprot.ReadI32();
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 14:
            if (field.Type == TType.String) {
              PositionName = iprot.ReadString();
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 15:
            if (field.Type == TType.I32) {
              PositionId = iprot.ReadI32();
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 16:
            if (field.Type == TType.I32) {
              NewViews = iprot.ReadI32();
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 17:
            if (field.Type == TType.I32) {
              ViewersCount = iprot.ReadI32();
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 18:
            if (field.Type == TType.I32) {
              TypeId = iprot.ReadI32();
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
      TStruct struc = new TStruct("ProfileView");
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
      if (Username != null && __isset.username) {
        field.Name = "username";
        field.Type = TType.String;
        field.ID = 2;
        oprot.WriteFieldBegin(field);
        oprot.WriteString(Username);
        oprot.WriteFieldEnd();
      }
      if (FirstName != null && __isset.firstName) {
        field.Name = "firstName";
        field.Type = TType.String;
        field.ID = 3;
        oprot.WriteFieldBegin(field);
        oprot.WriteString(FirstName);
        oprot.WriteFieldEnd();
      }
      if (LastName != null && __isset.lastName) {
        field.Name = "lastName";
        field.Type = TType.String;
        field.ID = 4;
        oprot.WriteFieldBegin(field);
        oprot.WriteString(LastName);
        oprot.WriteFieldEnd();
      }
      if (Picture != null && __isset.picture) {
        field.Name = "picture";
        field.Type = TType.String;
        field.ID = 5;
        oprot.WriteFieldBegin(field);
        oprot.WriteString(Picture);
        oprot.WriteFieldEnd();
      }
      if (Title != null && __isset.title) {
        field.Name = "title";
        field.Type = TType.String;
        field.ID = 6;
        oprot.WriteFieldBegin(field);
        oprot.WriteString(Title);
        oprot.WriteFieldEnd();
      }
      if (__isset.userTypeId) {
        field.Name = "userTypeId";
        field.Type = TType.I32;
        field.ID = 7;
        oprot.WriteFieldBegin(field);
        oprot.WriteI32(UserTypeId);
        oprot.WriteFieldEnd();
      }
      if (__isset.isConnected) {
        field.Name = "isConnected";
        field.Type = TType.I32;
        field.ID = 8;
        oprot.WriteFieldBegin(field);
        oprot.WriteI32(IsConnected);
        oprot.WriteFieldEnd();
      }
      if (__isset.pageIndex) {
        field.Name = "pageIndex";
        field.Type = TType.I32;
        field.ID = 9;
        oprot.WriteFieldBegin(field);
        oprot.WriteI32(PageIndex);
        oprot.WriteFieldEnd();
      }
      if (__isset.pageSize) {
        field.Name = "pageSize";
        field.Type = TType.I32;
        field.ID = 10;
        oprot.WriteFieldBegin(field);
        oprot.WriteI32(PageSize);
        oprot.WriteFieldEnd();
      }
      if (ViewDate != null && __isset.viewDate) {
        field.Name = "viewDate";
        field.Type = TType.String;
        field.ID = 11;
        oprot.WriteFieldBegin(field);
        oprot.WriteString(ViewDate);
        oprot.WriteFieldEnd();
      }
      if (__isset.observed) {
        field.Name = "observed";
        field.Type = TType.Bool;
        field.ID = 12;
        oprot.WriteFieldBegin(field);
        oprot.WriteBool(Observed);
        oprot.WriteFieldEnd();
      }
      if (__isset.viewType) {
        field.Name = "viewType";
        field.Type = TType.I32;
        field.ID = 13;
        oprot.WriteFieldBegin(field);
        oprot.WriteI32(ViewType);
        oprot.WriteFieldEnd();
      }
      if (PositionName != null && __isset.positionName) {
        field.Name = "positionName";
        field.Type = TType.String;
        field.ID = 14;
        oprot.WriteFieldBegin(field);
        oprot.WriteString(PositionName);
        oprot.WriteFieldEnd();
      }
      if (__isset.positionId) {
        field.Name = "positionId";
        field.Type = TType.I32;
        field.ID = 15;
        oprot.WriteFieldBegin(field);
        oprot.WriteI32(PositionId);
        oprot.WriteFieldEnd();
      }
      if (__isset.newViews) {
        field.Name = "newViews";
        field.Type = TType.I32;
        field.ID = 16;
        oprot.WriteFieldBegin(field);
        oprot.WriteI32(NewViews);
        oprot.WriteFieldEnd();
      }
      if (__isset.viewersCount) {
        field.Name = "viewersCount";
        field.Type = TType.I32;
        field.ID = 17;
        oprot.WriteFieldBegin(field);
        oprot.WriteI32(ViewersCount);
        oprot.WriteFieldEnd();
      }
      if (__isset.typeId) {
        field.Name = "typeId";
        field.Type = TType.I32;
        field.ID = 18;
        oprot.WriteFieldBegin(field);
        oprot.WriteI32(TypeId);
        oprot.WriteFieldEnd();
      }
      oprot.WriteFieldStop();
      oprot.WriteStructEnd();
    }

    public override string ToString() {
      StringBuilder __sb = new StringBuilder("ProfileView(");
      bool __first = true;
      if (__isset.userId) {
        if(!__first) { __sb.Append(", "); }
        __first = false;
        __sb.Append("UserId: ");
        __sb.Append(UserId);
      }
      if (Username != null && __isset.username) {
        if(!__first) { __sb.Append(", "); }
        __first = false;
        __sb.Append("Username: ");
        __sb.Append(Username);
      }
      if (FirstName != null && __isset.firstName) {
        if(!__first) { __sb.Append(", "); }
        __first = false;
        __sb.Append("FirstName: ");
        __sb.Append(FirstName);
      }
      if (LastName != null && __isset.lastName) {
        if(!__first) { __sb.Append(", "); }
        __first = false;
        __sb.Append("LastName: ");
        __sb.Append(LastName);
      }
      if (Picture != null && __isset.picture) {
        if(!__first) { __sb.Append(", "); }
        __first = false;
        __sb.Append("Picture: ");
        __sb.Append(Picture);
      }
      if (Title != null && __isset.title) {
        if(!__first) { __sb.Append(", "); }
        __first = false;
        __sb.Append("Title: ");
        __sb.Append(Title);
      }
      if (__isset.userTypeId) {
        if(!__first) { __sb.Append(", "); }
        __first = false;
        __sb.Append("UserTypeId: ");
        __sb.Append(UserTypeId);
      }
      if (__isset.isConnected) {
        if(!__first) { __sb.Append(", "); }
        __first = false;
        __sb.Append("IsConnected: ");
        __sb.Append(IsConnected);
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
      if (ViewDate != null && __isset.viewDate) {
        if(!__first) { __sb.Append(", "); }
        __first = false;
        __sb.Append("ViewDate: ");
        __sb.Append(ViewDate);
      }
      if (__isset.observed) {
        if(!__first) { __sb.Append(", "); }
        __first = false;
        __sb.Append("Observed: ");
        __sb.Append(Observed);
      }
      if (__isset.viewType) {
        if(!__first) { __sb.Append(", "); }
        __first = false;
        __sb.Append("ViewType: ");
        __sb.Append(ViewType);
      }
      if (PositionName != null && __isset.positionName) {
        if(!__first) { __sb.Append(", "); }
        __first = false;
        __sb.Append("PositionName: ");
        __sb.Append(PositionName);
      }
      if (__isset.positionId) {
        if(!__first) { __sb.Append(", "); }
        __first = false;
        __sb.Append("PositionId: ");
        __sb.Append(PositionId);
      }
      if (__isset.newViews) {
        if(!__first) { __sb.Append(", "); }
        __first = false;
        __sb.Append("NewViews: ");
        __sb.Append(NewViews);
      }
      if (__isset.viewersCount) {
        if(!__first) { __sb.Append(", "); }
        __first = false;
        __sb.Append("ViewersCount: ");
        __sb.Append(ViewersCount);
      }
      if (__isset.typeId) {
        if(!__first) { __sb.Append(", "); }
        __first = false;
        __sb.Append("TypeId: ");
        __sb.Append(TypeId);
      }
      __sb.Append(")");
      return __sb.ToString();
    }

  }

}
