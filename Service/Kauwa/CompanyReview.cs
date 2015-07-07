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
  public partial class CompanyReview : TBase
  {
    private int _userId;
    private int _companyId;
    private int _mode;
    private int _star;
    private string _title;
    private string _comment;
    private string _companyReviewGUID;
    private string _username;
    private string _name;
    private string _pictureUrl;
    private string _dateCommented;
    private bool _isApproved;
    private int _offSet;
    private int _pageSize;

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

    public int CompanyId
    {
      get
      {
        return _companyId;
      }
      set
      {
        __isset.companyId = true;
        this._companyId = value;
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

    public int Star
    {
      get
      {
        return _star;
      }
      set
      {
        __isset.star = true;
        this._star = value;
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

    public string Comment
    {
      get
      {
        return _comment;
      }
      set
      {
        __isset.comment = true;
        this._comment = value;
      }
    }

    public string CompanyReviewGUID
    {
      get
      {
        return _companyReviewGUID;
      }
      set
      {
        __isset.companyReviewGUID = true;
        this._companyReviewGUID = value;
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

    public string Name
    {
      get
      {
        return _name;
      }
      set
      {
        __isset.name = true;
        this._name = value;
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

    public string DateCommented
    {
      get
      {
        return _dateCommented;
      }
      set
      {
        __isset.dateCommented = true;
        this._dateCommented = value;
      }
    }

    public bool IsApproved
    {
      get
      {
        return _isApproved;
      }
      set
      {
        __isset.isApproved = true;
        this._isApproved = value;
      }
    }

    public int OffSet
    {
      get
      {
        return _offSet;
      }
      set
      {
        __isset.offSet = true;
        this._offSet = value;
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


    public Isset __isset;
    #if !SILVERLIGHT
    [Serializable]
    #endif
    public struct Isset {
      public bool userId;
      public bool companyId;
      public bool mode;
      public bool star;
      public bool title;
      public bool comment;
      public bool companyReviewGUID;
      public bool username;
      public bool name;
      public bool pictureUrl;
      public bool dateCommented;
      public bool isApproved;
      public bool offSet;
      public bool pageSize;
    }

    public CompanyReview() {
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
              CompanyId = iprot.ReadI32();
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 3:
            if (field.Type == TType.I32) {
              Mode = iprot.ReadI32();
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 4:
            if (field.Type == TType.I32) {
              Star = iprot.ReadI32();
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 5:
            if (field.Type == TType.String) {
              Title = iprot.ReadString();
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 6:
            if (field.Type == TType.String) {
              Comment = iprot.ReadString();
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 7:
            if (field.Type == TType.String) {
              CompanyReviewGUID = iprot.ReadString();
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
              Name = iprot.ReadString();
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 10:
            if (field.Type == TType.String) {
              PictureUrl = iprot.ReadString();
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 11:
            if (field.Type == TType.String) {
              DateCommented = iprot.ReadString();
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 12:
            if (field.Type == TType.Bool) {
              IsApproved = iprot.ReadBool();
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 13:
            if (field.Type == TType.I32) {
              OffSet = iprot.ReadI32();
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 14:
            if (field.Type == TType.I32) {
              PageSize = iprot.ReadI32();
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
      TStruct struc = new TStruct("CompanyReview");
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
      if (__isset.companyId) {
        field.Name = "companyId";
        field.Type = TType.I32;
        field.ID = 2;
        oprot.WriteFieldBegin(field);
        oprot.WriteI32(CompanyId);
        oprot.WriteFieldEnd();
      }
      if (__isset.mode) {
        field.Name = "mode";
        field.Type = TType.I32;
        field.ID = 3;
        oprot.WriteFieldBegin(field);
        oprot.WriteI32(Mode);
        oprot.WriteFieldEnd();
      }
      if (__isset.star) {
        field.Name = "star";
        field.Type = TType.I32;
        field.ID = 4;
        oprot.WriteFieldBegin(field);
        oprot.WriteI32(Star);
        oprot.WriteFieldEnd();
      }
      if (Title != null && __isset.title) {
        field.Name = "title";
        field.Type = TType.String;
        field.ID = 5;
        oprot.WriteFieldBegin(field);
        oprot.WriteString(Title);
        oprot.WriteFieldEnd();
      }
      if (Comment != null && __isset.comment) {
        field.Name = "comment";
        field.Type = TType.String;
        field.ID = 6;
        oprot.WriteFieldBegin(field);
        oprot.WriteString(Comment);
        oprot.WriteFieldEnd();
      }
      if (CompanyReviewGUID != null && __isset.companyReviewGUID) {
        field.Name = "companyReviewGUID";
        field.Type = TType.String;
        field.ID = 7;
        oprot.WriteFieldBegin(field);
        oprot.WriteString(CompanyReviewGUID);
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
      if (Name != null && __isset.name) {
        field.Name = "name";
        field.Type = TType.String;
        field.ID = 9;
        oprot.WriteFieldBegin(field);
        oprot.WriteString(Name);
        oprot.WriteFieldEnd();
      }
      if (PictureUrl != null && __isset.pictureUrl) {
        field.Name = "pictureUrl";
        field.Type = TType.String;
        field.ID = 10;
        oprot.WriteFieldBegin(field);
        oprot.WriteString(PictureUrl);
        oprot.WriteFieldEnd();
      }
      if (DateCommented != null && __isset.dateCommented) {
        field.Name = "dateCommented";
        field.Type = TType.String;
        field.ID = 11;
        oprot.WriteFieldBegin(field);
        oprot.WriteString(DateCommented);
        oprot.WriteFieldEnd();
      }
      if (__isset.isApproved) {
        field.Name = "isApproved";
        field.Type = TType.Bool;
        field.ID = 12;
        oprot.WriteFieldBegin(field);
        oprot.WriteBool(IsApproved);
        oprot.WriteFieldEnd();
      }
      if (__isset.offSet) {
        field.Name = "offSet";
        field.Type = TType.I32;
        field.ID = 13;
        oprot.WriteFieldBegin(field);
        oprot.WriteI32(OffSet);
        oprot.WriteFieldEnd();
      }
      if (__isset.pageSize) {
        field.Name = "pageSize";
        field.Type = TType.I32;
        field.ID = 14;
        oprot.WriteFieldBegin(field);
        oprot.WriteI32(PageSize);
        oprot.WriteFieldEnd();
      }
      oprot.WriteFieldStop();
      oprot.WriteStructEnd();
    }

    public override string ToString() {
      StringBuilder __sb = new StringBuilder("CompanyReview(");
      bool __first = true;
      if (__isset.userId) {
        if(!__first) { __sb.Append(", "); }
        __first = false;
        __sb.Append("UserId: ");
        __sb.Append(UserId);
      }
      if (__isset.companyId) {
        if(!__first) { __sb.Append(", "); }
        __first = false;
        __sb.Append("CompanyId: ");
        __sb.Append(CompanyId);
      }
      if (__isset.mode) {
        if(!__first) { __sb.Append(", "); }
        __first = false;
        __sb.Append("Mode: ");
        __sb.Append(Mode);
      }
      if (__isset.star) {
        if(!__first) { __sb.Append(", "); }
        __first = false;
        __sb.Append("Star: ");
        __sb.Append(Star);
      }
      if (Title != null && __isset.title) {
        if(!__first) { __sb.Append(", "); }
        __first = false;
        __sb.Append("Title: ");
        __sb.Append(Title);
      }
      if (Comment != null && __isset.comment) {
        if(!__first) { __sb.Append(", "); }
        __first = false;
        __sb.Append("Comment: ");
        __sb.Append(Comment);
      }
      if (CompanyReviewGUID != null && __isset.companyReviewGUID) {
        if(!__first) { __sb.Append(", "); }
        __first = false;
        __sb.Append("CompanyReviewGUID: ");
        __sb.Append(CompanyReviewGUID);
      }
      if (Username != null && __isset.username) {
        if(!__first) { __sb.Append(", "); }
        __first = false;
        __sb.Append("Username: ");
        __sb.Append(Username);
      }
      if (Name != null && __isset.name) {
        if(!__first) { __sb.Append(", "); }
        __first = false;
        __sb.Append("Name: ");
        __sb.Append(Name);
      }
      if (PictureUrl != null && __isset.pictureUrl) {
        if(!__first) { __sb.Append(", "); }
        __first = false;
        __sb.Append("PictureUrl: ");
        __sb.Append(PictureUrl);
      }
      if (DateCommented != null && __isset.dateCommented) {
        if(!__first) { __sb.Append(", "); }
        __first = false;
        __sb.Append("DateCommented: ");
        __sb.Append(DateCommented);
      }
      if (__isset.isApproved) {
        if(!__first) { __sb.Append(", "); }
        __first = false;
        __sb.Append("IsApproved: ");
        __sb.Append(IsApproved);
      }
      if (__isset.offSet) {
        if(!__first) { __sb.Append(", "); }
        __first = false;
        __sb.Append("OffSet: ");
        __sb.Append(OffSet);
      }
      if (__isset.pageSize) {
        if(!__first) { __sb.Append(", "); }
        __first = false;
        __sb.Append("PageSize: ");
        __sb.Append(PageSize);
      }
      __sb.Append(")");
      return __sb.ToString();
    }

  }

}
